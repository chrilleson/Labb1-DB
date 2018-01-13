using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Threading;

namespace Labb1_DB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Connection connection = new Connection();
        public readonly string sqlconn = Connection.ConnectionString;

        private int SelectedCompany;
        private int CompanyID;
        private int GameID;

        public MainWindow()
        {
            InitializeComponent();
            InsertCompaniesToBox();
        }

        private void InsertCompaniesToBox()
        {
            Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    buttonUnavalibale();
                });
                Thread.Sleep(2000);

                using (SqlConnection conn = new SqlConnection(sqlconn))
                {
                    Dispatcher.Invoke(() =>
                    {
                        tblComapnies.Items.Clear();
                    });
                    try
                    {
                        conn.Open();
                        string query = "SELECT * FROM Companies";
                        SqlCommand command = new SqlCommand(query, conn);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int companyid = reader.GetInt32(0);
                            string companyname = reader.GetString(1);
                            int established = reader.GetInt32(2);

                            Dispatcher.Invoke(() =>
                            {
                                tblComapnies.Items.Add(new Companies(companyid, established, companyname));
                            });
                        }
                    }
                    catch (SqlException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    finally
                    {
                        conn.Close();
                        Dispatcher.Invoke(() =>
                        {
                            btnAddCompany.IsEnabled = true;
                            btnAddGame.IsEnabled = true;
                            btnClearAll.IsEnabled = true;
                        });
                    }
                }
            });
        }

        private void InsertGameToDB(int companyid)
        {
            this.CompanyID = companyid;
            Task.Run(() =>
            {
                int comid = companyid;
                using (SqlConnection conn = new SqlConnection(sqlconn))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT * FROM Games INNER JOIN ON Companies.CompanyID = Games.CompanyID WHERE CompanyID = @companyID";
                        SqlCommand command = new SqlCommand(query, conn);
                        SqlDataReader reader;
                        command.Parameters.AddWithValue("@companyid", companyid);
                        reader = command.ExecuteReader();

                        Dispatcher.Invoke(() =>
                        {
                            tblGames.Items.Clear();
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string gameName = reader.GetString(1);
                                int companyId = reader.GetInt32(2);
                                string genre = reader.GetString(3);
                                tblGames.Items.Add(new Games(id, companyid, gameName, genre));
                            }
                        });
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            });
        }

        private void InsertCompanyToTbl()
        {
            if (tblComapnies.SelectedIndex >= 0)
            {
                Companies c = (Companies)tblComapnies.SelectedItem;
                SelectedCompany = (c).CompanyId;
                Dispatcher.Invoke(() =>
                {
                    txtCompanyID.Text = (c).CompanyId.ToString();
                    txtCompanyName.Text = (c).CompanyName;
                    txtEstablished.Text = (c).Established.ToString();
                });
            }
        }

        private void InsertGameToTbl()
        {
            if (tblGames.SelectedIndex >= 0)
            {
                Dispatcher.Invoke(() =>
                {
                    txtGameID.Text = ((Games)tblGames.SelectedItem).Id.ToString();
                    txtGameName.Text = ((Games)tblGames.SelectedItem).GameName;
                    txtGenre.Text = ((Games)tblGames.SelectedItem).Genre;
                    txtFKCompanyID.Text = ((Games)tblGames.SelectedItem).CompanyID.ToString();
                });
            }
        }


        private void buttonUnavalibale()
        {
            btnAddCompany.IsEnabled = false;
            btnAddGame.IsEnabled = false;
            btnDeleteCompany.IsEnabled = false;
            btnDeleteGame.IsEnabled = false;
            btnUpdateCompany.IsEnabled = false;
            btnUpdateGame.IsEnabled = false;
            btnClearAll.IsEnabled = false;
        }

        private void btnAddCompany_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                using (SqlConnection conn = new SqlConnection(sqlconn))
                {
                    try
                    {
                        conn.Open();
                        string query = "INSERT INTO Companies (CompanyName, Established) VALUES (@Companyname, @established)";
                        SqlCommand command = new SqlCommand(query, conn);
                        Dispatcher.Invoke(() =>
                        {
                            command.Parameters.AddWithValue("@Companyname", this.txtCompanyName.Text);
                            command.Parameters.AddWithValue("@established", this.txtEstablished.Text.ToString());
                        });
                        command.ExecuteNonQuery();
                        MessageBox.Show("New Company has been saved!");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                        InsertCompaniesToBox();
                    }
                }
            });
        }

    }
}
