using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using tpFinal.Models;

namespace tpFinal.Services
{
    public class DataService
    {
        private static DataService instance = null;

        string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=bdaventurier26;Integrated Security=True;Encrypt=False";

        public static DataService GetInstance()
        {
            if (instance == null)
                instance = new DataService();
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

        private ObservableCollection<Aventurier> aventuriers = new ObservableCollection<Aventurier>();
        private ObservableCollection<Equipement> equipements = new ObservableCollection<Equipement>();
        private ObservableCollection<Metier> metiers = new ObservableCollection<Metier>();

        public ObservableCollection<Aventurier> Aventuriers
        {
            get => aventuriers;
            set { aventuriers = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Equipement> Equipements
        {
            get => equipements;
            set { equipements = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Metier> Metiers
        {
            get => metiers;
            set { metiers = value; OnPropertyChanged(); }
        }

        private void LoadAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                LoadAventurier(connection);
                LoadEquipement(connection);
                LoadMetier(connection);
            }
        }

        private void LoadAventurier(SqlConnection connection)
        {
            Aventuriers = new ObservableCollection<Aventurier>();
            string queryString = "SELECT * FROM dbo.Aventurier";
            SqlCommand command = new SqlCommand(queryString, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Aventurier aventurier = new Aventurier();
                    ReaderToOneAventurier(reader, aventurier);
                    Aventuriers.Add(aventurier);
                }
            }

            foreach (Aventurier aventurier in Aventuriers)
                LoadAventurierEquipement(connection, aventurier);
        }

        private void LoadAventurierEquipement(SqlConnection connection, Aventurier aventurier)
        {
            string queryString = @"SELECT ae.Id as AeId, ae.IdAventurier, ae.IdEquipement,
                                          ae.QuantiteTotale, ae.QuantiteActive, ae.PrixAchat,
                                          e.Description, e.CodeTypeObjet, e.Quantite, e.PrixUnitaire,
                                          t.Description as TypeDescription
                                   FROM dbo.AventurierEquipement ae
                                   INNER JOIN dbo.Equipement e ON ae.IdEquipement = e.Id
                                   INNER JOIN dbo.TypeObjet t ON e.CodeTypeObjet = t.CodeType
                                   WHERE ae.IdAventurier = @IdAventurier";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@IdAventurier", aventurier.id);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    AventurierEquipement ae = new AventurierEquipement();
                    ae.id = Convert.ToInt32(reader["AeId"]);
                    ae.id_aventurier = Convert.ToInt32(reader["IdAventurier"]);
                    ae.id_equipement = Convert.ToInt32(reader["IdEquipement"]);
                    ae.quantite_totale = Convert.ToInt32(reader["QuantiteTotale"]);
                    ae.quantite_active = Convert.ToInt32(reader["QuantiteActive"]);
                    ae.prix_achat = (decimal)reader["PrixAchat"];

                    ae.Equipement = new Equipement();
                    ae.Equipement.id = ae.id_equipement;
                    ae.Equipement.description = (string)reader["Description"];
                    ae.Equipement.code_type_objet = Convert.ToInt32(reader["CodeTypeObjet"]);
                    ae.Equipement.quantite = Convert.ToInt32(reader["Quantite"]);
                    ae.Equipement.prix_unitaire = (decimal)reader["PrixUnitaire"];
                    ae.Equipement.TypeObjet = new TypeObjet();
                    ae.Equipement.TypeObjet.code_type = ae.Equipement.code_type_objet;
                    ae.Equipement.TypeObjet.description = (string)reader["TypeDescription"];

                    aventurier.AventurierEquipements.Add(ae);
                }
            }
        }

        private void LoadMetier(SqlConnection connection)
        {
            Metiers = new ObservableCollection<Metier>();
            string queryString = "SELECT * FROM dbo.Metier";
            SqlCommand command = new SqlCommand(queryString, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Metier metier = new Metier();
                    ReaderToOneMetier(reader, metier);
                    Metiers.Add(metier);
                }
            }
        }

        private void LoadEquipement(SqlConnection connection)
        {
            Equipements = new ObservableCollection<Equipement>();
            string queryString = @"SELECT e.*, t.Description as TypeDescription
                                   FROM dbo.Equipement e
                                   INNER JOIN dbo.TypeObjet t ON e.CodeTypeObjet = t.CodeType";
            SqlCommand command = new SqlCommand(queryString, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Equipement equipement = new Equipement();
                    ReaderToOneEquipement(reader, equipement);
                    Equipements.Add(equipement);
                }
            }
        }

        private void ReaderToOneAventurier(SqlDataReader reader, Aventurier aventurier)
        {
            aventurier.id = Convert.ToInt32(reader["id"]);
            aventurier.nom = (string)reader["Nom"];
            aventurier.niveau = Convert.ToInt32(reader["Niveau"]);
            aventurier.id_metier = Convert.ToInt32(reader["IdMetier"]);
            aventurier.solde_or = (decimal)reader["SoldeOr"];
            aventurier.DateInscription = (DateTime)reader["DateInscription"];
        }

        private void ReaderToOneMetier(SqlDataReader reader, Metier metier)
        {
            metier.id = Convert.ToInt32(reader["Id"]);
            metier.name = (string)reader["Nom"];
            metier.bonus_combat = Convert.ToInt32(reader["BonusCombat"]);
        }

        private void ReaderToOneEquipement(SqlDataReader reader, Equipement equipement)
        {
            equipement.id = Convert.ToInt32(reader["id"]);
            equipement.description = (string)reader["description"];
            equipement.code_type_objet = Convert.ToInt32(reader["CodeTypeObjet"]);
            equipement.quantite = Convert.ToInt32(reader["Quantite"]);
            equipement.prix_unitaire = (decimal)reader["PrixUnitaire"];
            equipement.TypeObjet = new TypeObjet();
            equipement.TypeObjet.code_type = equipement.code_type_objet;
            equipement.TypeObjet.description = (string)reader["TypeDescription"];
        }

        public void Save(Aventurier aventurier)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (aventurier.id == -1)
                {
                    string query = @"INSERT INTO dbo.Aventurier (Nom, Niveau, IdMetier, SoldeOr, DateInscription)
                                     VALUES (@Nom, @Niveau, @IdMetier, @SoldeOr, @DateInscription);
                                     SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Nom", aventurier.nom);
                    cmd.Parameters.AddWithValue("@Niveau", aventurier.niveau);
                    cmd.Parameters.AddWithValue("@IdMetier", aventurier.id_metier);
                    cmd.Parameters.AddWithValue("@SoldeOr", aventurier.solde_or);
                    cmd.Parameters.AddWithValue("@DateInscription", aventurier.DateInscription);
                    aventurier.id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                {
                    string query = @"UPDATE dbo.Aventurier
                                     SET Nom=@Nom, Niveau=@Niveau, IdMetier=@IdMetier,
                                         SoldeOr=@SoldeOr, DateInscription=@DateInscription
                                     WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Nom", aventurier.nom);
                    cmd.Parameters.AddWithValue("@Niveau", aventurier.niveau);
                    cmd.Parameters.AddWithValue("@IdMetier", aventurier.id_metier);
                    cmd.Parameters.AddWithValue("@SoldeOr", aventurier.solde_or);
                    cmd.Parameters.AddWithValue("@DateInscription", aventurier.DateInscription);
                    cmd.Parameters.AddWithValue("@Id", aventurier.id);
                    cmd.ExecuteNonQuery();
                }

                foreach (AventurierEquipement ae in aventurier.ListEquipementsToDelete)
                    DeleteAventurierEquipement(connection, ae);

                aventurier.ListEquipementsToDelete.Clear();

                foreach (AventurierEquipement ae in aventurier.AventurierEquipements)
                    SaveAventurierEquipement(connection, ae, aventurier.id);
            }
        }

        private void SaveAventurierEquipement(SqlConnection connection, AventurierEquipement ae, int idAventurier)
        {
            if (ae.id == -1)
            {
                string query = @"INSERT INTO dbo.AventurierEquipement
                                 (IdAventurier, IdEquipement, QuantiteTotale, QuantiteActive, PrixAchat)
                                 VALUES (@IdAventurier, @IdEquipement, @QuantiteTotale, @QuantiteActive, @PrixAchat);
                                 SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdAventurier", idAventurier);
                cmd.Parameters.AddWithValue("@IdEquipement", ae.id_equipement);
                cmd.Parameters.AddWithValue("@QuantiteTotale", ae.quantite_totale);
                cmd.Parameters.AddWithValue("@QuantiteActive", ae.quantite_active);
                cmd.Parameters.AddWithValue("@PrixAchat", ae.prix_achat);
                ae.id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            else
            {
                string query = @"UPDATE dbo.AventurierEquipement
                                 SET QuantiteTotale=@QuantiteTotale, QuantiteActive=@QuantiteActive, PrixAchat=@PrixAchat
                                 WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@QuantiteTotale", ae.quantite_totale);
                cmd.Parameters.AddWithValue("@QuantiteActive", ae.quantite_active);
                cmd.Parameters.AddWithValue("@PrixAchat", ae.prix_achat);
                cmd.Parameters.AddWithValue("@Id", ae.id);
                cmd.ExecuteNonQuery();
            }
        }

        private void DeleteAventurierEquipement(SqlConnection connection, AventurierEquipement ae)
        {
            string query = "DELETE FROM dbo.AventurierEquipement WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", ae.id);
            cmd.ExecuteNonQuery();
        }

        public void Delete(Aventurier aventurier)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteEnfants = "DELETE FROM dbo.AventurierEquipement WHERE IdAventurier=@Id";
                SqlCommand cmdEnfants = new SqlCommand(deleteEnfants, connection);
                cmdEnfants.Parameters.AddWithValue("@Id", aventurier.id);
                cmdEnfants.ExecuteNonQuery();

                string query = "DELETE FROM dbo.Aventurier WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", aventurier.id);
                cmd.ExecuteNonQuery();

                Aventuriers.Remove(aventurier);
            }
        }

        public void Reload(Aventurier aventurier)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM dbo.Aventurier WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", aventurier.id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        ReaderToOneAventurier(reader, aventurier);
                }

                aventurier.AventurierEquipements.Clear();
                aventurier.ListEquipementsToDelete.Clear();
                LoadAventurierEquipement(connection, aventurier);
            }
        }
    }
}