using System;
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
using ControlzEx.Standard;

namespace WpfApp4.MiniWindows
{
	/// <summary>
	/// Логика взаимодействия для DeviceEditWindow.xaml
	/// </summary>
	public partial class DeviceEditWindow : Window
	{
		private DataRowView _dataRow;
		private MainWindow mainWindow;

		public DeviceEditWindow(MainWindow mainWindow, DataRowView dataRow)
		{
			InitializeComponent();

			_dataRow = dataRow;
			this.mainWindow = mainWindow; // сохраняем ссылку на главное окно

			Name_Device.Text = _dataRow["Name_Device"].ToString();
			Device_Type.Text = _dataRow["Device_Type"].ToString();
			Model.Text = _dataRow["Model"].ToString();
			Ser_Number.Text = _dataRow["Ser_Number"].ToString();
			Year_Create_Device.Text = _dataRow["Year_Create_Device"].ToString();
			Inventory_Number.Text = _dataRow["Inventory_Number"].ToString();
			Location.Text = _dataRow["Location"].ToString();
			Name_Buh_Uch.Text = _dataRow["Name_Buh_Uch"].ToString();
		}

		private void Device_Update_Click(object sender, RoutedEventArgs e)
		{
			var Name_Device1 = Name_Device.Text;
			var Device_Type1 = Device_Type.Text;
			var Model1 = Model.Text;
			var Ser_Number1 = Ser_Number.Text;
			var Year_Create_Device1 = Year_Create_Device.Text;
			var Inventory_Number1 = Inventory_Number.Text;
			var Location1 = Location.Text;
			var Name_Buh_Uch1 = Name_Buh_Uch.Text;

			var id = _dataRow["ID"]; // Предполагается, что у вас также есть ID устройства

			// SQL-запрос на обновление
			string query = "UPDATE [Technical_Service].[dbo].[Devices] " +
						   "SET [Name_Device] = @Name_Device, [Device_Type] = @Device_Type, " +
						   "[Model] = @Model, [Ser_Number] = @Ser_Number, " +
						   "[Year_Create_Device] = @Year_Create_Device, [Inventory_Number] = @Inventory_Number, " +
						   "[Location] = @Location, [Name_Buh_Uch] = @Name_Buh_Uch " +
						   "WHERE [ID] = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавление значений для параметров
						command.Parameters.AddWithValue("@Name_Device", Name_Device1);
						command.Parameters.AddWithValue("@Device_Type", Device_Type1);
						command.Parameters.AddWithValue("@Model", Model1);
						command.Parameters.AddWithValue("@Ser_Number", Ser_Number1);
						command.Parameters.AddWithValue("@Year_Create_Device", Year_Create_Device1);
						command.Parameters.AddWithValue("@Inventory_Number", Inventory_Number1);
						command.Parameters.AddWithValue("@Location", Location1);
						command.Parameters.AddWithValue("@Name_Buh_Uch", Name_Buh_Uch1);
						command.Parameters.AddWithValue("@ID", id);


						// Открытие соединения и выполнение команды
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();
						//CustomMessageBoxService.Show($"Пользователь, {FIOv} добавлен!", "");

						mainWindow.LoadData_Devices();
						this.Close();

					}
				}


			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}

		private void Device_Delete_Click(object sender, RoutedEventArgs e)
		{
			// Получаем ID устройства из _dataRow
			var id = _dataRow["ID"]; // Предполагается, что ID хранится в столбце "ID"

			// SQL-запрос на удаление
			string query = "DELETE FROM [Technical_Service].[dbo].[Devices] WHERE [ID] = @ID";

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
							//CustomMessageBoxService.Show($"Устройство удалено.", "");
							mainWindow.LoadData_Devices(); // Обновляем данные об устройствах
							this.Close(); // Закрываем текущее окно
						}
						else
						{
							CustomMessageBoxService.Show($"Ошибка! Устройство не найдено.", "");
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

	}
}
