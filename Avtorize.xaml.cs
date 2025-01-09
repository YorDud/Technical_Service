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
using WpfApp4.MiniWindows;

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



			string connectionString = WC.ConnectionString; // Получаем FIO и Role по введенному логину и паролю
			string query = "SELECT id, FIO, Role, Login FROM Users WHERE Login = @login AND Password = @password";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@login", log_txt.Text);
				command.Parameters.AddWithValue("@password", pass_txt.Password);

				try
				{
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							WC.id_User = reader["id"].ToString();
							WC.login = reader["Login"].ToString();
							WC.fio = reader["FIO"].ToString();
							string role = reader["Role"].ToString();

							Window nextForm = null; // Объявляем переменную для следующей формы


							nextForm = new MainWindow();
							// Определяем, какую форму открыть в зависимости от роли
							if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
							{
								nextForm = new MainWindow();
							}
							else if (role.Equals("nalad", StringComparison.OrdinalIgnoreCase))
							{
								nextForm = new MainWindowNalad();
							}
							else
							{
								System.Windows.Forms.MessageBox.Show("Вашей роли не предусмотрен доступ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
								return;
							}



							// Закрываем DataReader перед выполнением новой команды
							reader.Close();

							

							//System.Windows.MessageBox.Show($"Добро пожаловать, {WC.fio}!", "Приветствие", (MessageBoxButton)MessageBoxButtons.OK);
							//bool result = CustomMessageBoxService.Show($"Добро пожаловать, {WC.fio}!", "Приветствие");
							//if (result)
							//{
							//	// Пользователь нажал OK

							//}

							//bool? confirm = CustomMessageBoxService.Show("Вы уверены, что хотите выйти?", "Подтверждение", true);
							//if (confirm == true)
							//{
							//	//System.Windows.Application.Current.Shutdown();
							//}


							this.Hide();
							nextForm.ShowDialog();
							this.Show();
						}
						else
						{
							bool result = CustomMessageBoxService.Show($"Вы ввели неверный логин или пароль", "Ошибка входа");
							//System.Windows.MessageBox.Show("Вы ввели неверный логин или пароль", "Ошибка входа", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
						}
					}
				}
				catch (Exception ex)
				{
					System.Windows.MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
				}
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Crash_Create_Wndow_Avtorize crash_Create_Wndow = new Crash_Create_Wndow_Avtorize();
			crash_Create_Wndow.ShowDialog();
		}

	}
}
