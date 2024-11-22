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
	/// Логика взаимодействия для Users_Nalad_Edit_Window.xaml
	/// </summary>
	public partial class Users_Nalad_Edit_Window : Window
	{

		private DataRowView _dataRow;
		private MainWindow mainWindow;

		public Users_Nalad_Edit_Window(MainWindow mainWindow, DataRowView dataRow)
		{
			InitializeComponent();

			this.mainWindow = mainWindow; // сохраняем ссылку на главное окно

			_dataRow = dataRow;
			
			FIO.Text = _dataRow["FIO"].ToString();
			Log.Text = _dataRow["Login"].ToString();
			Pass.Text = _dataRow["Password"].ToString();
			Role.Text = _dataRow["Role"].ToString();
			Smena.Text = _dataRow["Smena"].ToString();
			Phone.Text = _dataRow["Phone"].ToString();
		}

		private void User_Dobav_Click(object sender, RoutedEventArgs e)
		{
			

			var FIOv = FIO.Text;
			var Logv = Log.Text; 
			var Passv = Pass.Text; 
			var Rolev = Role.Text; 
			var Smenav = Smena.Text; 
			var Phonev = Phone.Text;

			var id = _dataRow["ID"];



			// SQL-запрос на добавление
			string query = "UPDATE [Technical_Service].[dbo].[Users] " +
				   "SET [FIO] = @FIO, [Login] = @Login, [Password] = @Password, " +
				   "[Role] = @Role, [Smena] = @Smena, [Phone] = @Phone " +
				   "WHERE [ID] = @ID";

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

						command.Parameters.AddWithValue("@ID", id);

						// Открытие соединения и выполнение команды
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();

						mainWindow.LoadData_Users();
						this.Close();

						//CustomMessageBoxService.Show($"Данные сотрудника, {FIOv} изменены!", "");



					}
				}

				
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}

		private void User_Delete_Click(object sender, RoutedEventArgs e)
		{
			// Получаем ID пользователя из _dataRow
			var id = _dataRow["ID"]; // Предполагается, что ID хранится в столбце "ID"

			// SQL-запрос на удаление
			string query = "DELETE FROM [Technical_Service].[dbo].[Users] WHERE [ID] = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавляем ID как параметр
						command.Parameters.AddWithValue("@ID", id);

						// Открытие соединения и выполнение команды
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();

						// Проверка, было ли что-то удалено
						if (rowsAffected > 0)
						{
							//CustomMessageBoxService.Show($"Пользователь удалён.", "");

							mainWindow.LoadData_Users();
							this.Close();
						}
						else
						{
							CustomMessageBoxService.Show($"Ошибка!", "");
						}
					}
				}

				
			}
			catch (Exception ex)
			{
				// Обработка исключений
				CustomMessageBoxService.Show($"Ошибка: {ex.Message}", "");
			}
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }



        //private void User_Dobav_Click(object sender, RoutedEventArgs e)
        //{
        //	var FIOv = FIO.Text;
        //	var Logv = Log.Text; // предполагаем, что id — это int
        //	var Passv = Pass.Text; // предполагаем, что id — это int
        //	var Rolev = Role.Text; // предполагаем, что id — это int
        //	var Smenav = Smena.Text; // предполагаем, что id — это int
        //	var Phonev = Phone.Text;  // предполагаем, что id — это int

        //	// Обновляем данные в базе данных
        //	// Укажите строку подключения к вашей базе данных

        //	using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
        //	{
        //		connection.Open();
        //		string query = "UPDATE [Technical_Service].[dbo].[Users] " +
        //		   "SET [FIO] = @FIO, [Login] = @Login, [Password] = @Password, " +
        //		   "[Role] = @Role, [Smena] = @Smena, [Phone] = @Phone " +
        //		   "WHERE [ID] = @ID";

        //		using (SqlCommand command = new SqlCommand(query, connection))
        //		{
        //			command.Parameters.AddWithValue("@FIO", FIOv);
        //			command.Parameters.AddWithValue("@Log", Logv);
        //			command.Parameters.AddWithValue("@Pass", Passv);
        //			command.Parameters.AddWithValue("@Role", Rolev);
        //			command.Parameters.AddWithValue("@Smena", Smenav);
        //			command.Parameters.AddWithValue("@Phone", Phonev);

        //			try
        //			{
        //				int rowsAffected = command.ExecuteNonQuery();
        //				if (rowsAffected > 0)
        //				{
        //					MessageBox.Show("Изменения успешно сохранены!");
        //				}
        //				else
        //				{
        //					MessageBox.Show("Не удалось сохранить изменения.");
        //				}
        //			}
        //			catch (Exception ex)
        //			{
        //				MessageBox.Show("Произошла ошибка: " + ex.Message);
        //			}
        //		}
        //	}





        //	this.Close();
        //}

    }

	
}

