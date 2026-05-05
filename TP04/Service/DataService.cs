using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TP02.Models;

namespace TP02.Service
{
    internal class DataService
    {
        private static DataService instance = null;

        string connectionString = @"Data Source =.\sqlexpress;Initial Catalog=bdH2026;Integrated Security=True;Encrypt=False";


        public static DataService GetInstance()
        {
            if (instance == null)
            {
                instance = new DataService();
            }
            return instance;
        }
        private DataService()
        {
            LoadAll();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Client> clients = new ObservableCollection<Client>();


        public ObservableCollection<Client> Clients
        {
            get => clients;
            set
            {
                clients = value;
                OnPropertyChanged();

            }
        }

        private ObservableCollection<Provinces> provinces;

        public ObservableCollection<Provinces> Provinces
        {
            get { return provinces; }
            set
            {
                provinces = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Pays> pays;

        public ObservableCollection<Pays> Pays
        {
            get { return pays; }
            set
            {
                pays = value;
                OnPropertyChanged();
            }
        }

        public void LoadAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // ou connection.ConnectionString = connectionString;
                connection.Open();

                LoadProvinces(connection);
                LoadPays(connection);

                LoadClient(connection);
            }
        }

        private void LoadClient(SqlConnection connection)
        {
            Clients = new ObservableCollection<Client>();

            //  Provinces.Clear();// vide la liste si pas le new ici mais en haut

            string queryString = "SELECT * FROM dbo.Client";

            SqlCommand command = new SqlCommand(queryString, connection);
            //connection.Open();  si pas déjà ouverte



            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())// tant qu’il y a des enregistrements Personne de //table
                {
                    Client client = new Client();


                    ReaderToOneClient(reader, client); // remplir une personne

                    Clients.Add(client);

                }
            }   // fin using reader
        }

        private void ReaderToOneClient(SqlDataReader reader, Client client)
        {
            client.Id_client = (Int32)reader["Id_client"];
            client.Solde = (decimal)reader["Solde"];
            client.Nom = (string)reader["Nom"];
            client.Ville = (string)reader["Ville"];
            client.Telephone = (string)reader["Telephone"];
            client.Code_postal = (string)reader["CodePostal"];
            client.Code_pays = (string)reader["Code_pays"];
            client.Code_province = (string)reader["Code_province"];
            client.Adresse = (string)reader["Adresse_ligne1"];
        }

        private void LoadPays(SqlConnection connection)
        {
            Pays = new ObservableCollection<Pays>();

            //  Provinces.Clear();// vide la liste si pas le new ici mais en haut

            string queryString = "SELECT * FROM dbo.Pays";

            SqlCommand command = new SqlCommand(queryString, connection);
            //connection.Open();  si pas déjà ouverte



            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())// tant qu’il y a des enregistrements Personne de //table
                {
                    Pays pays = new Pays();


                    ReaderToOnePays(reader, pays); // remplir une personne

                    Pays.Add(pays);

                }
            }   // fin using reader
        }

        private void ReaderToOnePays(SqlDataReader reader, Pays pays)
        {
            pays.Code_Pays = (string)reader["Code_Pays"];
            pays.Nom = (string)reader["Nom"];
        }

        private void LoadProvinces(SqlConnection connection)
        {
            Provinces = new ObservableCollection<Provinces>();

            //  Provinces.Clear();// vide la liste si pas le new ici mais en haut

            string queryString = "SELECT * FROM dbo.Province";

            SqlCommand command = new SqlCommand(queryString, connection);
            //connection.Open();  si pas déjà ouverte



            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())// tant qu’il y a des enregistrements Personne de //table
                {
                    Provinces province = new Provinces();


                    ReaderToOneProvince(reader, province); // remplir une personne

                    Provinces.Add(province);

                }
            }   // fin using reader
        }

        private void ReaderToOneProvince(SqlDataReader reader, Provinces province)
        {
            province.Code_Province = (string)reader["Code_Province"];
            province.Nom = (string)reader["Nom"];
        }

        public void Save(Client client)
        {            // insert ou update ? Je ne passe pas le Mode

            if (client.Id_client == -1)
            {// donc assurez-vous que votre constructeur place -1
                Insert(client);
            }
            else
            {
                Update(client);
            }


        }

        public void Update(Client client)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                string queryString = "UPDATE dbo.Client" +
                      " SET Nom = @Nom," +
                      "Telephone = @Telephone, Adresse_ligne1 = @Adresse," +
                      "Ville = @Ville," +
                      "CodePostal = @Code_postal," +
                      "Code_Province = @Code_province," +
                      "Code_Pays = @Code_pays," +
                      "Solde = @Solde " +
                      "WHERE Id_client = @Id_client";

                SqlCommand command = new SqlCommand(queryString, connection);
                //  moins compliqué que le tableau mais je voulais vous montrer les tableaux aussi
                command.Parameters.AddWithValue("@Nom", client.Nom);
                command.Parameters.AddWithValue("@Telephone", client.Telephone);
                command.Parameters.AddWithValue("@Adresse", client.Adresse);
                command.Parameters.AddWithValue("@Ville", client.Ville);
                command.Parameters.AddWithValue("@Code_postal", client.Code_postal);
                command.Parameters.AddWithValue("@Code_province", client.Code_province);
                command.Parameters.AddWithValue("@Code_pays", client.Code_pays);
                command.Parameters.AddWithValue("@Solde", client.Solde);
                command.Parameters.AddWithValue("@Id_client", client.Id_client);
                command.ExecuteNonQuery();

            }

        }

        public void Reload(Client client)
        {
            // vider les enfants
            string queryString = "SELECT * FROM dbo.Client " +
                                "WHERE(Client.Id_client = @Id_client)";

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;



                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Id_client", client.Id_client);
                command.ExecuteNonQuery();


                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {

                        ReaderToOneClient(reader, client);

                    }

                }
            }
        }

        public void Insert(Client c)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                string queryString = "INSERT INTO dbo.Client (Nom, Telephone, Adresse_ligne1, Ville, CodePostal, Code_Province, Code_Pays, Solde) VALUES " +
                    "(@Nom, @Telephone, @Adresse, @Ville, @Code_postal, @Code_province, @Code_pays, @Solde)";

                SqlCommand command = new SqlCommand(queryString, connection);
                //  moins compliqué que le tableau mais je voulais vous montrer les tableaux aussi
                command.Parameters.AddWithValue("@Nom", c.Nom);
                command.Parameters.AddWithValue("@Telephone", c.Telephone);
                command.Parameters.AddWithValue("@Adresse", c.Adresse);
                command.Parameters.AddWithValue("@Ville", c.Ville);
                command.Parameters.AddWithValue("@Code_postal", c.Code_postal);
                command.Parameters.AddWithValue("@Code_province", c.Code_province);
                command.Parameters.AddWithValue("@Code_pays", c.Code_pays);
                command.Parameters.AddWithValue("@Solde", c.Solde);
                command.ExecuteNonQuery();

                //var result = 0;
                queryString = "SELECT IDENT_CURRENT('Client')";
                using (SqlCommand IdCommand = new SqlCommand(queryString, connection))
                {
                    c.Id_client = Convert.ToInt32(IdCommand.ExecuteScalar());
                }
            }
        }


        public void Delete(Client c)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                string queryString = "DELETE dbo.Client " +
                                     "WHERE Id_client = @Id_client";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Id_client", c.Id_client);
                command.ExecuteNonQuery();
            }
        }



    }
}
