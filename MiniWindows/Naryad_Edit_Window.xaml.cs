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

namespace WpfApp4.MiniWindows
{
	/// <summary>
	/// Логика взаимодействия для Naryad_Edit_Window.xaml
	/// </summary>
	public partial class Naryad_Edit_Window: Window
	{
		private MainWindow mainWindow;
		private DataRowView _dataRow;
		public Naryad_Edit_Window(MainWindow mainWindow, DataRowView dataRow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			_dataRow = dataRow;

			LoadData_ComboBox();


			DeviceName.Text = _dataRow["Device_Name"].ToString();
			TypesTOName.Text = _dataRow["Types_TO_Name"].ToString();
			TypesTOWorkList.Text = _dataRow["Types_TO_Work_List"].ToString();
			UsersFIO.Text = _dataRow["Users_FIO"].ToString();
			DateStart.Text = _dataRow["Date_Start"].ToString();
			DateEnd.Text = _dataRow["Date_End"].ToString();
			Status.Text = _dataRow["Status"].ToString();
			Comment.Text = _dataRow["Comment"].ToString();
			SkladDeteilID.Text = _dataRow["Sklad_Deteil_ID"].ToString();
			SkladKolich.Text = _dataRow["Sklad_Kolich"].ToString();
			DocumentationNameID.Text = _dataRow["Documentation_Name_ID"].ToString();
		}

		private void Naryad_Update_Click(object sender, RoutedEventArgs e)
		{
			// Предположим, идентификатор записи, которую нужно обновить
			var id = _dataRow["ID"]; // Получите этот идентификатор из контекста, например, из выбранного элемента списка
			var deviceName = DeviceName.Text;
			var typesTOName = TypesTOName.Text;
			var typesTOWorkList = TypesTOWorkList.Text;
			var usersFIO = UsersFIO.Text;
			var dateStart = DateStart.SelectedDate;
			var dateEnd = DateEnd.SelectedDate;
			var status = Status.Text;
			var comment = Comment.Text;
			var skladDeteilID = SkladDeteilID.Text; // Получение ID запчасти
			var skladKolich = SkladKolich.Text; // Получение количества запчастей
			var documentationNameID = DocumentationNameID.Text; // Получение ID документации

			string query = "UPDATE [Technical_Service].[dbo].[Naryad] SET " +
						   "[Device_Name] = @DeviceName, " +
						   "[Types_TO_Name] = @TypesTOName, " +
						   "[Types_TO_Work_List] = @TypesTOWorkList, " +
						   "[Users_FIO] = @UsersFIO, " +
						   "[Date_Start] = @DateStart, " +
						   "[Date_End] = @DateEnd, " +
						   "[Status] = @Status, " +
						   "[Comment] = @Comment, " +
						   "[Sklad_Deteil_ID] = @SkladDeteilID, " +
						   "[Sklad_Kolich] = @SkladKolich, " +
						   "[Documentation_Name_ID] = @DocumentationNameID " +
						   "WHERE [ID] = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@ID", id); // Параметр для ID наряда
						command.Parameters.AddWithValue("@DeviceName", deviceName);
						command.Parameters.AddWithValue("@TypesTOName", typesTOName);
						command.Parameters.AddWithValue("@TypesTOWorkList", typesTOWorkList);
						command.Parameters.AddWithValue("@UsersFIO", usersFIO);
						command.Parameters.AddWithValue("@DateStart", (object)dateStart ?? DBNull.Value);
						command.Parameters.AddWithValue("@DateEnd", (object)dateEnd ?? DBNull.Value);
						command.Parameters.AddWithValue("@Status", status);
						command.Parameters.AddWithValue("@Comment", comment);
						command.Parameters.AddWithValue("@SkladDeteilID", skladDeteilID);
						command.Parameters.AddWithValue("@SkladKolich", (object)skladKolich ?? DBNull.Value);
						command.Parameters.AddWithValue("@DocumentationNameID", documentationNameID);

						connection.Open();
						command.ExecuteNonQuery();
						mainWindow.LoadData_Naryad();
						this.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}
		private void Naryad_Delete_Click(object sender, RoutedEventArgs e)
		{
			// Предположим, что у вас есть идентификатор записи, которую нужно удалить
			var id = _dataRow["ID"]; // Получите этот идентификатор из контекста, например, из выбранного элемента списка

			string query = "DELETE FROM [Technical_Service].[dbo].[Naryad] WHERE [ID] = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@ID", id); // Параметр для ID наряда

						connection.Open();
						command.ExecuteNonQuery();
						mainWindow.LoadData_Naryad();
						this.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}





























		private void LoadDevices()
		{
			string query = "SELECT Name_Device FROM [Technical_Service].[dbo].[Devices]";
			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							List<string> deviceNames = new List<string>();
							while (reader.Read())
							{
								deviceNames.Add(reader["Name_Device"].ToString());
							}

							DeviceName.ItemsSource = deviceNames; // Устанавливаем источник данных для ComboBox
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}




		private void LoadData_ComboBox()
		{
			LoadDeviceNames();   // Загрузка устройств
			LoadTypesTOName();   // Загрузка ТО
			LoadUsersFIO();      // Загрузка ФИО сотрудников
			LoadStatus();        // Загрузка статусов
			LoadSkladDeteilID(); // Загрузка деталей
			LoadDocumentationNameID(); // Загрузка документации
		}

		// Загрузка названий устройств
		private void LoadDeviceNames()
		{
			string query = "SELECT Name_Device FROM [Technical_Service].[dbo].[Devices]";
			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							List<string> deviceNames = new List<string>();
							while (reader.Read())
							{
								deviceNames.Add(reader["Name_Device"].ToString());
							}
							DeviceName.ItemsSource = deviceNames; // Устанавливаем источник данных для ComboBox
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

		// Загрузка типов ТО в ComboBox
		private void LoadTypesTOName()
		{
			string query = "SELECT Name_TO FROM [Technical_Service].[dbo].[Types_TO]";
			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							List<string> typesTO = new List<string>();
							while (reader.Read())
							{
								typesTO.Add(reader["Name_TO"].ToString());
							}
							TypesTOName.ItemsSource = typesTO; // Устанавливаем источник данных для ComboBox
						}
					}
				}

				// Подписка на событие выбора элемента
				TypesTOName.SelectionChanged += TypesTOName_SelectionChanged;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

		// Загрузка списка работ в TextBox по выбранному виду ТО
		private void TypesTOName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (TypesTOName.SelectedItem != null)
			{
				string selectedNameTO = TypesTOName.SelectedItem.ToString();
				LoadWorkList(selectedNameTO);
			}
		}

		// Получение списка работ для выбранного типа ТО
		private void LoadWorkList(string nameTO)
		{
			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(
					"SELECT Work_List FROM [Technical_Service].[dbo].[Types_TO] WHERE Name_TO = @NameTO", connection);
				command.Parameters.AddWithValue("@NameTO", nameTO);

				var result = command.ExecuteScalar();
				TypesTOWorkList.Text = result?.ToString() ?? string.Empty;
			}
		}

		// Загрузка ФИО сотрудников в ComboBox
		private void LoadUsersFIO()
		{
			string query = "SELECT FIO FROM [Technical_Service].[dbo].[Users]";
			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							List<string> usersFIO = new List<string>();
							while (reader.Read())
							{
								usersFIO.Add(reader["FIO"].ToString());
							}
							UsersFIO.ItemsSource = usersFIO; // Устанавливаем источник данных для ComboBox
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

		// Загрузка статусов в ComboBox
		private void LoadStatus()
		{
			Status.Items.Add("В работе");
			Status.Items.Add("Выполнен");
			Status.Items.Add("Закрыт");
		}

		// Загрузка деталей в ComboBox SkladDeteilID
		private void LoadSkladDeteilID()
		{
			string query = "SELECT [ID], [Deteil_Types] FROM [Technical_Service].[dbo].[Sklad]";
			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							List<ComboBoxItem> detailTypes = new List<ComboBoxItem>();
							while (reader.Read())
							{
								detailTypes.Add(new ComboBoxItem
								{
									Content = reader["Deteil_Types"].ToString(),
									Tag = reader["ID"] // Сохраняем ID в Tag 
								});
							}
							SkladDeteilID.ItemsSource = detailTypes; // Устанавливаем источник данных для ComboBox
						}
					}
				}

				// Подписка на событие выбора элемента
				SkladDeteilID.SelectionChanged += SkladDeteilID_SelectionChanged;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

		// Обработка выбора детали в ComboBox
		private void SkladDeteilID_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (SkladDeteilID.SelectedItem is ComboBoxItem selectedItem)
			{
				string selectedID = selectedItem.Tag.ToString();
				LoadSkladKolich(selectedID); // Загрузка количества по выбранной детали
			}
		}

		// Получение количества по ID детали
		private void LoadSkladKolich(string skladDeteilID)
		{
			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand("SELECT [Kolich] FROM [Technical_Service].[dbo].[Sklad] WHERE [ID] = @ID", connection);
				command.Parameters.AddWithValue("@ID", skladDeteilID);

				var result = command.ExecuteScalar();
				SkladKolich.Text = result?.ToString() ?? string.Empty;
			}
		}

		// Загрузка документации в ComboBox DocumentationNameID
		private void LoadDocumentationNameID()
		{
			string query = "SELECT Name_Doc FROM [Technical_Service].[dbo].[Documentation]";
			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							List<string> documentationNames = new List<string>();
							while (reader.Read())
							{
								documentationNames.Add(reader["Name_Doc"].ToString());
							}
							DocumentationNameID.ItemsSource = documentationNames; // Устанавливаем источник данных для ComboBox
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

		

	}
}
