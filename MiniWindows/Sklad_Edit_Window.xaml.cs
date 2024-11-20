using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
	/// Логика взаимодействия для Sklad_Edit_Window.xaml
	/// </summary>
	/// 
	
	public partial class Sklad_Edit_Window : Window
	{
		private DataRowView _dataRow;
		private MainWindow mainWindow;

		public Sklad_Edit_Window(MainWindow mainWindow, DataRowView dataRow)
		{
			InitializeComponent();

			this.mainWindow = mainWindow; // сохраняем ссылку на главное окно

			_dataRow = dataRow;
			Deteil_Types.Text = _dataRow["Deteil_Types"].ToString();
			Model.Text = _dataRow["Model"].ToString();
			Proizvod.Text = _dataRow["Proizvod"].ToString();
			Postav.Text = _dataRow["Postav"].ToString();
			Image.Text = _dataRow["Image"].ToString();
			Location.Text = _dataRow["Location"].ToString();
			Kolich.Text = _dataRow["Kolich"].ToString();
		}

		private void Sklad_Update_Click(object sender, RoutedEventArgs e)
		{
			// Получение значений из текстовых полей с обработкой пустых значений
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
			var kolich = Kolich.Text; // Получаем количество

			// Здесь мы предполагаем, что у вас есть идентификатор записи, которую нужно обновить (например, ID)
			 // Замените на актуальный ID, который вы хотите обновить
			

			// SQL-запрос на обновление
			string query = "UPDATE [Sklad] SET " +
						   "[Deteil_Types] = @Deteil_Types, " +
						   "[Model] = @Model, " +
						   "[Proizvod] = @Proizvod, " +
						   "[Postav] = @Postav, " +
						   "[Devices_ID] = @Devices_ID, " +
						   "[Image] = @Image, " +
						   "[Location] = @Location, " +
						   "[Kolich] = @Kolich " +
						   "WHERE [ID] = @ID"; // Предполагаем, что поле ID - это имя вашего идентификатора

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
						command.Parameters.AddWithValue("@Kolich", (object)kolich ?? DBNull.Value);
						command.Parameters.AddWithValue("@ID", _dataRow["ID"]); // Добавляем ID для обновления

						// Открытие соединения и выполнение команды
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							//MessageBox.Show("Запись успешно обновлена в склад!");
							mainWindow.LoadData_Sklad(); // Обновляем данные на интерфейсе
							this.Close();
						}
						else
						{
							MessageBox.Show("Ошибка: не удалось обновить запись.");
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}

		private void Sklad_Delete_Click(object sender, RoutedEventArgs e)
		{
			
			string query = "DELETE FROM Sklad WHERE ID = @ID"; // Предполагаем, что поле ID - это имя вашего идентификатора

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавление значения для параметра ID
						command.Parameters.AddWithValue("@ID", _dataRow["ID"]); // Добавляем ID для удаления

						// Открытие соединения и выполнение команды**
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();
						if (rowsAffected > 0)
						{
							//MessageBox.Show("Запись успешно удалена из склада!");
							mainWindow.LoadData_Sklad(); // Обновляем данные на интерфейсе
							this.Close();
						}
						else
						{
							MessageBox.Show("Ошибка: не удалось удалить запись.");
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
