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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp4
{
	/// <summary>
	/// Логика взаимодействия для Avtorize.xaml
	/// </summary>
	public partial class Avtorize : Window
	{
		public Avtorize()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (log_txt.Text == "1" && pass_txt.Password == "1")
			{
				this.Hide();
				MainWindow mainWindow = new MainWindow();
				mainWindow.ShowDialog();
				
			}


			//string connectionString = WC.ConnectionString; // Получаем FIO и Role по введенному логину и паролю
			//string query = "SELECT id, FIO, Role, Login FROM Users WHERE Login = @login AND Password = @password";

			//using (SqlConnection connection = new SqlConnection(connectionString))
			//{
			//	SqlCommand command = new SqlCommand(query, connection);
			//	command.Parameters.AddWithValue("@login", log_txt.Text);
			//	command.Parameters.AddWithValue("@password", pass_txt.Password);

			//	try
			//	{
			//		connection.Open();
			//		using (SqlDataReader reader = command.ExecuteReader())
			//		{
			//			if (reader.Read())
			//			{
			//				WC.id_User = reader["id"].ToString();
			//				WC.login = reader["Login"].ToString();
			//				WC.fio = reader["FIO"].ToString();
			//				string role = reader["Role"].ToString();

			//				Window nextForm = null; // Объявляем переменную для следующей формы


			//				nextForm = new MainWindow();
			//				// Определяем, какую форму открыть в зависимости от роли
			//				//if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
			//				//{
			//				//	nextForm = new StartWindow_admin();
			//				//}
			//				//else if (role.Equals("laborant", StringComparison.OrdinalIgnoreCase))
			//				//{
			//				//	nextForm = new StartWindow_laborant();
			//				//}
			//				//else if (role.Equals("technolog", StringComparison.OrdinalIgnoreCase))
			//				//{
			//				//	nextForm = new StartWindow_technolog();
			//				//}
			//				//else if (role.Equals("corrector", StringComparison.OrdinalIgnoreCase))
			//				//{
			//				//	nextForm = new StartWindow_correcter();
			//				//}
			//				//else if (role.Equals("info_corrector", StringComparison.OrdinalIgnoreCase))
			//				//{
			//				//	nextForm = new Start_Info_correcter();
			//				//}
			//				//else if (role.Equals("info_technolog", StringComparison.OrdinalIgnoreCase))
			//				//{
			//				//	nextForm = new Start_Info_technolog();
			//				//}
			//				//else
			//				//{
			//				//	MessageBox.Show("Вашей роли не предусмотрен доступ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//				//	return;
			//				//}



			//				// Закрываем DataReader перед выполнением новой команды
			//				reader.Close();

			//				// SQL-запрос для вставки данных в таблицу Logs_Avtorize
			//				string insertQuery = @"INSERT INTO Logs_Avtorize (User_ID, User_Login, User_Role, User_FIO, Date_Avtorize)
   //                                        VALUES (@User_ID, @User_Login, @User_Role, @User_FIO, @Date_Avtorize)";

			//				using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
			//				{
			//					// Установка значений параметров
			//					insertCommand.Parameters.AddWithValue("@User_ID", WC.id_User);
			//					insertCommand.Parameters.AddWithValue("@User_Login", WC.login);
			//					insertCommand.Parameters.AddWithValue("@User_Role", role);
			//					insertCommand.Parameters.AddWithValue("@User_FIO", WC.fio);
			//					insertCommand.Parameters.AddWithValue("@Date_Avtorize", DateTime.Now); // Текущее время

			//					// Выполняем команду вставки
			//					insertCommand.ExecuteNonQuery();
			//				}

			//				//System.Windows.MessageBox.Show($"Добро пожаловать, {WC.fio}!", "Приветствие", (MessageBoxButton)MessageBoxButtons.OK);
			//				bool result = CustomMessageBoxService.Show($"Добро пожаловать, {WC.fio}!", "Приветствие");
			//				//if (result)
			//				//{
			//				//	// Пользователь нажал OK
								
			//				//}

			//				//bool? confirm = CustomMessageBoxService.Show("Вы уверены, что хотите выйти?", "Подтверждение", true);
			//				//if (confirm == true)
			//				//{
			//				//	//System.Windows.Application.Current.Shutdown();
			//				//}
						

			//				this.Hide();
			//				nextForm.ShowDialog();
			//				this.Show();
			//			}
			//			else
			//			{
			//				bool result = CustomMessageBoxService.Show($"Вы ввели неверный логин или пароль", "Ошибка входа");
			//				//System.Windows.MessageBox.Show("Вы ввели неверный логин или пароль", "Ошибка входа", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
			//			}
			//		}
			//	}
			//	catch (Exception ex)
			//	{
			//		System.Windows.MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
			//	}
			//}
		}
	}
}
