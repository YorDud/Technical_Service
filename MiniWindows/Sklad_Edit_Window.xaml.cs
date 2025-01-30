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
using System.IO;
using Path = System.IO.Path;

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

		private string imageFilePath;
		private string filePath;

		public Sklad_Edit_Window(MainWindow mainWindow, DataRowView dataRow)
		{
			InitializeComponent();

			this.mainWindow = mainWindow; // сохраняем ссылку на главное окно

			_dataRow = dataRow;
			Deteil_Types.Text = _dataRow["Deteil_Types"].ToString();
			Model.Text = _dataRow["Model"].ToString();
			Proizvod.Text = _dataRow["Proizvod"].ToString();
			Postav.Text = _dataRow["Postav"].ToString();
			DeviceName.SelectedValue = _dataRow["Devices_ID"].ToString();
			//FileNameTextBox.Text = _dataRow["Name_Image"].ToString();
			Location.Text = _dataRow["Location"].ToString();
			Kolich.Text = _dataRow["Kolich"].ToString();

			LoadDeteil_Types();
			LoadLocation();
			LoadDeviceNames();
		}


		private void LoadDeteil_Types()
		{
			string query = "SELECT Deteil_Types FROM [Technical_Service].[dbo].[Deteil_Types]";
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
								typesTO.Add(reader["Deteil_Types"].ToString());
							}
							Deteil_Types.ItemsSource = typesTO; // Устанавливаем источник данных для ComboBox
						}
					}
				}

				// Подписка на событие выбора элемента
				//Device_Type.SelectionChanged += Device_Type_SelectionChanged;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

		private void LoadLocation()
		{
			string query = "SELECT Location FROM [Technical_Service].[dbo].[Location]";
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
								typesTO.Add(reader["Location"].ToString());
							}
							Location.ItemsSource = typesTO; // Устанавливаем источник данных для ComboBox
						}
					}
				}

				// Подписка на событие выбора элемента
				//Device_Type.SelectionChanged += Device_Type_SelectionChanged;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

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


		private void Sklad_Update_Click(object sender, RoutedEventArgs e)
		{
			// Считываем данные из UI
			string detailTypes = Deteil_Types.Text;
			string model = Model.Text;
			string proizvod = Proizvod.Text;
			string postav = Postav.Text;
			string devicesId = DeviceName.Text;
			string location = Location.Text;
			string kolich = Kolich.Text;
			byte[] imageBytes = null; // Массив байтов для изображения
			string imageName = null;  // Имя файла изображения

			// Если файл выбран, преобразуем его в массив байтов и извлекаем имя
			if (!string.IsNullOrEmpty(imageFilePath) && File.Exists(imageFilePath))
			{
				imageBytes = File.ReadAllBytes(imageFilePath);
				imageName = Path.GetFileName(imageFilePath); // Имя файла
			}

			// Базовый SQL-запрос
			string query = "UPDATE [Sklad] SET " +
						   "[Deteil_Types] = @Deteil_Types, " +
						   "[Model] = @Model, " +
						   "[Proizvod] = @Proizvod, " +
						   "[Postav] = @Postav, " +
						   "[Devices_ID] = @Devices_ID, " +
						   "[Location] = @Location, " +
						   "[Kolich] = @Kolich";

			// Добавляем поля для изображения только если оно загружено
			if (imageBytes != null && !string.IsNullOrEmpty(imageName))
			{
				query += ", [Name_Image] = @Name_Image, [Image] = @Image";
			}

			query += " WHERE [ID] = @ID"; // Условие обновления по идентификатору

			try
			{
				// Подключение к базе данных
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Параметры запроса
						command.Parameters.AddWithValue("@Deteil_Types", string.IsNullOrEmpty(detailTypes) ? (object)DBNull.Value : detailTypes);
						command.Parameters.AddWithValue("@Model", string.IsNullOrEmpty(model) ? (object)DBNull.Value : model);
						command.Parameters.AddWithValue("@Proizvod", string.IsNullOrEmpty(proizvod) ? (object)DBNull.Value : proizvod);
						command.Parameters.AddWithValue("@Postav", string.IsNullOrEmpty(postav) ? (object)DBNull.Value : postav);
						command.Parameters.AddWithValue("@Devices_ID", devicesId);
						command.Parameters.AddWithValue("@Location", string.IsNullOrEmpty(location) ? (object)DBNull.Value : location);
						command.Parameters.AddWithValue("@Kolich", string.IsNullOrEmpty(kolich) ? (object)DBNull.Value : kolich);
						command.Parameters.AddWithValue("@ID", _dataRow["ID"]);

						// Добавляем параметры для изображений только если они существуют
						if (imageBytes != null && !string.IsNullOrEmpty(imageName))
						{
							command.Parameters.AddWithValue("@Name_Image", imageName);
							command.Parameters.Add("@Image", SqlDbType.VarBinary).Value = imageBytes;
						}

						// Открываем соединение и выполняем запрос
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();

						// Проверяем результат
						if (rowsAffected > 0)
						{
							mainWindow.LoadData_Sklad(); // Обновляем данные
							this.Close(); // Закрываем окно
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
				// Выводим сообщение об ошибке
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}


		private void ChooseFileHyperlink_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
			{
				Filter = "Изображения (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|Все файлы (*.*)|*.*",
				Title = "Выберите изображение"
			};

			if (openFileDialog.ShowDialog() == true)
			{
				imageFilePath = openFileDialog.FileName; // Сохраняем путь к изображению
				FileNameTextBox.Text = Path.GetFileName(imageFilePath); // Отображаем имя файла

				// Отображаем изображение
				SelectedImage.Source = new BitmapImage(new Uri(imageFilePath));
				SelectedImage.Visibility = Visibility.Visible;
				FileDropText.Visibility = Visibility.Collapsed;
			}
		}



		private void FileDropBorder_DragOver(object sender, DragEventArgs e)
		{
			// Проверка на наличие файлов
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effects = DragDropEffects.Copy; // Разрешаем копирование
			}
			else
			{
				e.Effects = DragDropEffects.None; // Отклоняем другие операции
			}
		}

		private void FileDropBorder_Drop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

				if (files.Length > 0)
				{
					imageFilePath = files[0]; // Сохраняем путь к файлу

					if (File.Exists(imageFilePath))
					{
						FileNameTextBox.Text = Path.GetFileName(imageFilePath);

						// Проверяем, является ли файл изображением
						try
						{
							SelectedImage.Source = new BitmapImage(new Uri(imageFilePath));
							SelectedImage.Visibility = Visibility.Visible;
							FileDropText.Visibility = Visibility.Collapsed;
						}
						catch
						{
							MessageBox.Show("Выбранный файл не является изображением.");
						}
					}
					else
					{
						MessageBox.Show("Файл не найден.");
					}
				}
			}
			else
			{
				MessageBox.Show("Перетаскиваемый объект не является файлом.");
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

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }
    }
}
