using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfPersonne.Models;

namespace WpfPersonne.Services
{
    public class DataService
    {
        private static DataService instance = null;

        string connectionString = @"Data Source =.\sqlexpress;Initial Catalog=bdDemoPersonne;Integrated Security=True;Encrypt=False";

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

        private ObservableCollection<Personne> personnes = new ObservableCollection<Personne>();
        private ObservableCollection<Chaussure> chaussure = new ObservableCollection<Chaussure>();
        // a ajouter

        public ObservableCollection<Chaussure> Chaussures
        {
            get => chaussure;
            set
            {
                chaussure = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Personne> Personnes
        {
            get => personnes;
            set
            {
                personnes = value;
                OnPropertyChanged();

            }
        }

        private ObservableCollection<Profession> professions;

        public ObservableCollection<Profession> Professions
        {
            get { return professions; }
            set
            {
                professions = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Province> provinces;

        public ObservableCollection<Province> Provinces
        {
            get { return provinces; }
            set
            {
                provinces = value;
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
                LoadProfessions(connection);

                LoadPersonnes(connection);
                LoadChaussures(connection);
            }
        }

        private void LoadChaussures(SqlConnection connection)
        {
            Chaussures = new ObservableCollection<Chaussure>();
            string queryString = "SELECT * FROM dbo.Chaussure";
            SqlCommand command = new SqlCommand(queryString, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())// tant qu’il y a des enregistrements Personne de //table
                {
                    Chaussure chaussure = new Chaussure();


                    ReaderToOneChaussure(reader, chaussure); // remplir une personne

                    Chaussures.Add(chaussure);

                }
            }   // fin using reader

        }


        private void LoadProfessions(SqlConnection connection)
        {
            Professions = new ObservableCollection<Profession>();

            //  Provinces.Clear();// vide la liste si pas le new ici mais en haut

            string queryString = "SELECT * FROM dbo.Profession";

            SqlCommand command = new SqlCommand(queryString, connection);
            //connection.Open();  si pas déjà ouverte



            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())// tant qu’il y a des enregistrements Personne de //table
                {
                    Profession profession = new Profession();


                    ReaderToOneProfession(reader, profession); // remplir une personne

                    Professions.Add(profession);

                }
            }   // fin using reader
        }

        private void LoadProvinces(SqlConnection connection)
        {
            Provinces = new ObservableCollection<Province>();

            //  Provinces.Clear();// vide la liste si pas le new ici mais en haut

            string queryString = "SELECT * FROM dbo.Province";

            SqlCommand command = new SqlCommand(queryString, connection);
            //connection.Open();  si pas déjà ouverte



            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())// tant qu’il y a des enregistrements Personne de //table
                {
                    Province province = new Province();


                    ReaderToOneProvince(reader, province); // remplir une personne

                    Provinces.Add(province);

                }
            }   // fin using reader
        }

        private void LoadPersonnes(SqlConnection connection)
        {
            Personnes = new ObservableCollection<Personne>();

            //  Personnes.Clear();// vide la liste si pas le new ici mais en haut

            string queryString = "SELECT * FROM dbo.Personne";

            SqlCommand command = new SqlCommand(queryString, connection);
            //connection.Open();  si pas déjà ouverte



            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())// tant qu’il y a des enregistrements Personne de //table
                {
                    Personne personne = new Personne();


                    ReaderToOnePersonne(reader, personne); // remplir une personne

                    Personnes.Add(personne);

                }
            }   // fin using reader

            foreach (var p in Personnes)
            {
                LoadPersonneChaussure(connection, p);

            }
        }

        private void LoadPersonneChaussure(SqlConnection connection, Personne p)
        {
            // Vider la liste pour recharger les données
            p.PersonneChaussures.Clear();

            string queryString =
                "SELECT * " +
                "FROM dbo.PersonneChaussure " +
                "WHERE Id_Personne = @Id_Personne";

            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                command.Parameters.AddWithValue("@Id_Personne", p.Id_Personne);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PersonneChaussure ps = new PersonneChaussure();

                        ReaderToOnePersonneChaussure(reader, ps);

                        p.PersonneChaussures.Add(ps);
                    }
                }
            }

            // Nous ajouterons quelque chose ici plus tard pour les descriptions dans le DataGrid
        }

        private void ReaderToOnePersonneChaussure(SqlDataReader reader, PersonneChaussure ps)
        {
            ps.Id_personneChaussure = (Int32)reader["Id_personneChaussure"];
            ps.Id_personne = (Int32)reader["Id_personne"];
            ps.Id_chaussure = (Int32)reader["Id_chaussure"];
            ps.Prix_vente = (decimal)reader["Prix_vente"];
            ps.Qte_stock = (Int32)reader["Qte_stock"];
        }

        public void ReaderToOnePersonne(SqlDataReader reader, Personne personne)
        {
            personne.Id_Personne = (Int32)reader["Id_personne"];
            personne.Age = (Int32)reader["Age"];
            personne.NomFamille = (string)reader["NomFamille"];
            personne.Prenom = (string)reader["Prenom"];
            personne.Id_Profession = (int)reader["Id_Profession"];
            personne.Code_Province = (string)reader["Code_Province"];
            personne.Date_CreationBidon = (DateTime)reader["Date_CreationBidon"];
            personne.Actif = (byte)reader["Actif"];

            // tinyint
            personne.Total_Ventes = (decimal)reader["Total_Ventes"];
        }

        public void ReaderToOneProvince(SqlDataReader reader, Province province)
        {
            province.Code = (string)reader["Code_Province"];
            province.Description = (string)reader["Nom"];
        }

        public void ReaderToOneProfession(SqlDataReader reader, Profession profession)
        {
            profession.Id = (Int32)reader["Id_Profession"];
            profession.Description = (string)reader["Nom"];
        }


        public void ReaderToOneChaussure(SqlDataReader reader, Chaussure chaussure)
        {
            chaussure.Id_chaussure = (Int32)reader["Id_chaussure"];
            chaussure.Code_typeChaussure = (string)reader["Code_typeChaussure"];
            chaussure.Code_genreChaussure = (string)reader["Code_genreChaussure"];
            chaussure.Prix = (decimal)reader["Prix"];
            chaussure.Taille = (decimal)reader["Taille"];
        }


        public void Save(Personne personne)
        {            // insert ou update ? Je ne passe pas le Mode

            if (personne.Id_Personne == -1)
            {// donc assurez-vous que votre constructeur place -1
                Insert(personne);
            }
            else
            {
                Update(personne);
            }


        }



        public void Update(Personne p)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                string queryString = "UPDATE dbo.Personne" +
               " SET NomFamille = @NomFamille," +
                  "Prenom = @Prenom, Age = @Age," +
                  "Date_CreationBidon = @Date_CreationBidon," +
                  "Actif = @Actif," +
                  "Code_Province = @Code_Province," +
                  "Id_Profession = @Id_Profession," +
                  "Total_Ventes = @Total_Ventes " +
             "WHERE Id_Personne=@Id_Personne";

                SqlCommand command = new SqlCommand(queryString, connection);
                //  moins compliqué que le tableau mais je voulais vous montrer les tableaux aussi
                command.Parameters.AddWithValue("@NomFamille", p.NomFamille);
                command.Parameters.AddWithValue("@Prenom", p.Prenom);
                command.Parameters.AddWithValue("@Age", p.Age);
                command.Parameters.AddWithValue("@Date_CreationBidon", p.Date_CreationBidon);
                command.Parameters.AddWithValue("@Actif", p.Actif);
                command.Parameters.AddWithValue("@Code_Province", p.Code_Province);
                command.Parameters.AddWithValue("@Id_Profession", p.Id_Profession);
                command.Parameters.AddWithValue("@Total_Ventes", p.Total_Ventes);
                command.Parameters.AddWithValue("@Id_Personne", p.Id_Personne);
                command.ExecuteNonQuery();

                // TRAITEMENT DES ENFANTS : si id -1 on insert sinon update
                foreach (var perch in p.PersonneChaussures)
                {
                    if (perch.Id_personneChaussure == -1)
                    {
                        perch.Id_personne = p.Id_Personne;
                        InsertPersonneChaussurePers(connection, p, perch);
                    }
                    else
                    {
                        UpdatePersonneChaussurePers(connection, perch);
                    }
                }

                // Détruire tous ceux qu'on a supprimé de la liste
                foreach (var pdelete in p.ListpersonneChaussureToDelete)
                {
                    DeletePersonneChaussures(connection, pdelete);
                }
                p.ListpersonneChaussureToDelete.Clear();

            }

        }

        private void UpdatePersonneChaussurePers(SqlConnection connection, PersonneChaussure perch)
        {
            string queryString = "UPDATE dbo.PersonneChaussure " +
                                 "SET Qte_stock = @Qte_stock, Prix_vente = @Prix_vente " +
                                 "WHERE Id_personneChaussure = @Id_personneChaussure";

            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                command.Parameters.AddWithValue("@Qte_stock", perch.Qte_stock);
                command.Parameters.AddWithValue("@Prix_vente", perch.Prix_vente);
                command.Parameters.AddWithValue("@Id_personneChaussure", perch.Id_personneChaussure);
                command.ExecuteNonQuery();
            }
        }


        public void Reload(Personne personne)
        {
            string queryString = "SELECT * FROM dbo.Personne " +
                                "WHERE(Personne.Id_Personne = @Id_Personne)";

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Id_Personne", personne.Id_Personne);
                command.ExecuteNonQuery();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ReaderToOnePersonne(reader, personne);
                    }
                }

                // AJOUT : recharger les enfants aussi
                LoadPersonneChaussure(connection, personne);
            }
        }


        public void Delete(Personne p)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach(var pc in p.PersonneChaussures)
                {
                    DeletePersonneChaussures(connection, pc);
                }

                string queryString = "DELETE FROM dbo.Personne WHERE Id_Personne = @Id_Personne";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Id_Personne", p.Id_Personne);

                    int nb = command.ExecuteNonQuery();

                    if (nb > 0)
                    {
                        Personnes.Remove(p);
                    }
                }
            }
        }

        public void DeletePersonneChaussures(SqlConnection connection, PersonneChaussure pc)
        {
            string queryString = "DELETE dbo.PersonneChaussure " +
                                 "WHERE Id_PersonneChaussure=@Id_PersonneChaussure";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@Id_PersonneChaussure", pc.Id_personneChaussure);
            command.ExecuteNonQuery();
        }


        public void Insert(Personne p) {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                string queryString = "INSERT INTO dbo.Personne (Prenom, NomFamille, Age, Actif, Date_CreationBidon, Total_Ventes, Id_Profession, Code_Province) VALUES " +
                    "(@Prenom, @NomFamille, @Age, @Actif, @Date_CreationBidon, @Total_Ventes, @Id_Profession, @Code_Province)"
                 ;


                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    //  moins compliqué que le tableau mais je voulais vous montrer les tableaux aussi
                    command.Parameters.AddWithValue("@NomFamille", p.NomFamille);
                    command.Parameters.AddWithValue("@Prenom", p.Prenom);
                    command.Parameters.AddWithValue("@Age", p.Age);
                    command.Parameters.AddWithValue("@Date_CreationBidon", p.Date_CreationBidon);
                    command.Parameters.AddWithValue("@Actif", p.Actif);
                    command.Parameters.AddWithValue("@Code_Province", p.Code_Province);
                    command.Parameters.AddWithValue("@Id_Profession", p.Id_Profession);
                    command.Parameters.AddWithValue("@Total_Ventes", p.Total_Ventes);
                    command.Parameters.AddWithValue("@Id_Personne", p.Id_Personne);
                    command.ExecuteNonQuery();
                }
                
                queryString = "SELECT IDENT_CURRENT('Personne')";
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    p.Id_Personne = Convert.ToInt32(command.ExecuteScalar());
                }


                foreach(var perch in p.PersonneChaussures)
                {
                   perch.Id_personne = p.Id_Personne;
                    InsertPersonneChaussurePers(connection, p, perch);
                }

            }
        }

        private void InsertPersonneChaussurePers(SqlConnection connection, Personne p, PersonneChaussure perch)
        {
            string queryString = "INSERT INTO dbo.PersonneChaussure (Id_personne, Id_chaussure, Qte_stock, Prix_vente) " +
                                 "VALUES (@Id_personne, @Id_chaussure, @Qte_stock, @Prix_vente)";

            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                command.Parameters.AddWithValue("@Id_personne", perch.Id_personne);
                command.Parameters.AddWithValue("@Id_chaussure", perch.Id_chaussure);
                command.Parameters.AddWithValue("@Qte_stock", perch.Qte_stock);
                command.Parameters.AddWithValue("@Prix_vente", perch.Prix_vente);

                command.ExecuteNonQuery();
            }
            
            string identQuery = "SELECT IDENT_CURRENT('PersonneChaussure')";
            using (SqlCommand command = new SqlCommand(identQuery, connection))
            {
                perch.Id_personneChaussure = Convert.ToInt32(command.ExecuteScalar());
            }
        }
    }


}
