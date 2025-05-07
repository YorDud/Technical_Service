using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ControlzEx.Standard;

namespace WpfApp4.MiniWindows
{
	/// <summary>
	/// Логика взаимодействия для Users_Nalad_Create_Window.xaml
	/// </summary>
	public partial class Users_Nalad_Create_Window : Window
	{

		//private DataRowView _dataRow;
		private MainWindow mainWindow;

		public Users_Nalad_Create_Window(MainWindow mainWindow/*DataRowView dataRow)*/)
		{
			InitializeComponent();

			this.mainWindow = mainWindow; // сохраняем ссылку на главное окно

			//_dataRow = dataRow;
			//FIO.Text = _dataRow["ID"].ToString();
			//Log.Text = _dataRow["FIO"].ToString();
			//Pass.Text = _dataRow["Pass"].ToString();
			//Role.Text = _dataRow["Role"].ToString();
			//Smena.Text = _dataRow["Smena"].ToString();
			//Phone.Text = _dataRow["Phone"].ToString();
		}

		private void User_Dobav_Click(object sender, RoutedEventArgs e)
		{
			var FIOv = FIO.Text;
			var Logv = Log.Text; 
			var Passv = Pass.Text; 
			var Rolev = Role.Text; 
			var Smenav = Smena.Text; 
			var Phonev = Phone.Text;

			

			// SQL-запрос на добавление
			string query = "INSERT INTO [Technical_Service].[dbo].[Users] " +
						   "([FIO], [Login], [Password], [Role], [Smena], [Phone]) " +
						   "VALUES (@FIO, @Login, @Password, @Role, @Smena, @Phone)";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавление значений для параметров
						command.Parameters.AddWithValue("@FIO", FIOv);
						command.Parameters.AddWithValue("@Login", Logv);
						command.Parameters.AddWithValue("@Password", Passv);
						command.Parameters.AddWithValue("@Role", Rolev);
						command.Parameters.AddWithValue("@Smena", Smenav);
						command.Parameters.AddWithValue("@Phone", Phonev);

						// Открытие соединения и выполнение команды
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();
						//CustomMessageBoxService.Show($"Пользователь, {FIOv} добавлен!", "");

						mainWindow.LoadData_Users();
						this.Close();

					}
				}

				
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }
    }

	
}

