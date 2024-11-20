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
using System.Windows.Shapes;

namespace WpfApp4.MiniWindows
{
	/// <summary>
	/// Логика взаимодействия для Sklad_Create_Window.xaml
	/// </summary>
	/// 
	
	public partial class Sklad_Create_Window : Window
	{
		//private DataRowView _dataRow;
		private MainWindow mainWindow;

		public Sklad_Create_Window(MainWindow mainWindow/*DataRowView dataRow)*/)
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

		private void Sklad_Dobav_Click(object sender, RoutedEventArgs e)
		{
			var detailTypes = Deteil_Types.Text;
			var model = Model.Text;
			var proizvod = Proizvod.Text;
			var postav = Postav.Text;
			var devicesId = 0; // Задаем значение по умолчанию
			if (!string.IsNullOrWhiteSpace(Devices_ID.Text))
			{
				devicesId = Convert.ToInt32(Devices_ID.Text); // Преобразуем только если поле не пустое
			}
			var image = Image.Text; // Не требуется проверка, если не обязательно
			var location = Location.Text; // Также не требуется проверка
			var kolich = Kolich.Text;

			// SQL-запрос на добавление
			string query = "INSERT INTO [Technical_Service].[dbo].[Sklad] " +
						   "([Deteil_Types], [Model], [Proizvod], [Postav], [Devices_ID], [Image], [Location], [Kolich]) " +
						   "VALUES (@Deteil_Types, @Model, @Proizvod, @Postav, @Devices_ID, @Image, @Location, @Kolich)";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавление значений для параметров
						command.Parameters.AddWithValue("@Deteil_Types", (object)detailTypes ?? DBNull.Value);
						command.Parameters.AddWithValue("@Model", (object)model ?? DBNull.Value);
						command.Parameters.AddWithValue("@Proizvod", (object)proizvod ?? DBNull.Value);
						command.Parameters.AddWithValue("@Postav", (object)postav ?? DBNull.Value);
						command.Parameters.AddWithValue("@Devices_ID", devicesId == 0 ? (object)DBNull.Value : devicesId);
						command.Parameters.AddWithValue("@Image", (object)image ?? DBNull.Value);
						command.Parameters.AddWithValue("@Location", (object)location ?? DBNull.Value);
						command.Parameters.AddWithValue("@Kolich", (object)DBNull.Value);

						// Открытие соединения и выполнение команды
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							//MessageBox.Show("Запись успешно добавлена в склад!");
							mainWindow.LoadData_Sklad(); // Обновляем данные на интерфейсе
							this.Close();
						}
						else
						{
							MessageBox.Show("Ошибка: не удалось добавить запись.");
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}

	}
}
