using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;


namespace WpfCsharpTextConnexionBdSqlServeur
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // *************************************************************
        // modifier ici votre connexion. pas de blanc dans le serveur ni de \\
        // devient inutile si automatique dans le constructeur
        string connectionString= @"Data Source = nomOrdi\sqlexpress; Initial Catalog = test; Integrated Security = True; Encrypt = False";
     
        // *************************************************************
        public MainWindow()
        {
                InitializeComponent();
// si chaine auto
            var builder = new SqlConnectionStringBuilder
            {
             //   DataSource = $@"{Environment.MachineName}\SQLEXPRESS",
                DataSource = $@".\SQLEXPRESS",
                InitialCatalog = "bdDemoPersonne",
                IntegratedSecurity = true,
                TrustServerCertificate = true // Souvent requis pour les connexions locales récentes
            };

          connectionString = builder.ConnectionString;
            TxtMachine.Text= Environment.MachineName.ToString();
 // fin chaine auto          
            txtConnexion.Text = connectionString;


        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            txtResultat.Text=string.Empty;
            txtResultat.Text = "Chaîne utilisée :\n" + connectionString + "\n\n";
            //chatgpt
            var match = Regex.Match(connectionString, @"Data\s*Source\s*=\s*([^;]+)", RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                Console.WriteLine("Data Source introuvable");
                return;
            }

            string serverName = match.Groups[1].Value.Trim();

            bool hasSpace = serverName.Contains(" ");
            bool hasDoubleSlash = serverName.Contains(@"\\");
            bool isValidFormat = Regex.IsMatch(serverName, @"^[A-Za-z0-9._-]+(\\[A-Za-z0-9._-]+)?$");
            if (!hasSpace && !hasDoubleSlash && isValidFormat)
            {
                Console.WriteLine("Nom de serveur valide : " + serverName);
                TxtServeur.Text = serverName + " valide";
            }
            else
            {
                Console.WriteLine("Nom de serveur invalide : " + serverName);
                TxtServeur.Text = serverName + " invalide ";
                if (hasSpace) TxtServeur.Text += "contient des blancs ";
                if (hasDoubleSlash) TxtServeur.Text += "ne pas mettre '\\', laiser un seul '\' ";
                return;
            }
           

            // On extrait juste la base pour faire un test supplémentaire
            var csb = new SqlConnectionStringBuilder(connectionString);
            string baseDemandee = csb.InitialCatalog;

            try
            {
                // 1) Test du serveur uniquement (on force master)
                var csServeur = new SqlConnectionStringBuilder(connectionString)
                {
                    InitialCatalog = "master"
                }.ConnectionString;

                using (var cnTest = new SqlConnection(csServeur))
                {
                    await cnTest.OpenAsync();
                    txtResultat.Text += $"✔ Serveur atteint : {cnTest.DataSource}\n";
                }

                // 2) Vérifier si la base existe
                bool baseExiste = await BaseExiste(csb.DataSource, baseDemandee, csServeur);

                if (!baseExiste)
                {
                    txtResultat.Text += $"❌ La base « {baseDemandee} » n'existe pas.\n";
                    txtResultat.Foreground = System.Windows.Media.Brushes.DarkRed;
                    return;
                }
                else
                {
                    txtResultat.Text += $"✔ Base trouvée : {baseDemandee}\n";
                }

                // 3) Connexion finale complète
                using (var cnFinal = new SqlConnection(connectionString))
                {
                    await cnFinal.OpenAsync();
                    txtResultat.Text += "🎉 Connexion réussie !\n";
                    txtResultat.Foreground = System.Windows.Media.Brushes.DarkGreen;
                }
            }
            catch (SqlException ex)
            {
                txtResultat.Text += "\nErreur SQL :\n" + ex.Message;
                txtResultat.Foreground = System.Windows.Media.Brushes.DarkRed;

            }
            catch (Exception ex)
            {
                txtResultat.Text += "\nErreur :\n" + ex.Message;
                txtResultat.Foreground = System.Windows.Media.Brushes.DarkRed;
            }
        }

        private async System.Threading.Tasks.Task<bool> BaseExiste(string serveur, string baseName, string csServeur)
        {
            using (var cn = new SqlConnection(csServeur))
            {
                await cn.OpenAsync();
                using (var cmd = new SqlCommand("SELECT DB_ID(@db)", cn))
                {
                    cmd.Parameters.AddWithValue("@db", baseName);
                    var result = await cmd.ExecuteScalarAsync();
                    return (result != null && result != DBNull.Value);
                }
            }
        }

       

    }

}
