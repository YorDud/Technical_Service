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
using System.IO;
using Path = System.IO.Path;
using System.Data;


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

		private string imageFilePath;
		private string filePath;
		public Sklad_Create_Window(MainWindow mainWindow/*DataRowView dataRow)*/)
		{
			InitializeComponent();

			this.mainWindow = mainWindow; // сохраняем ссылку на главное окно

		}

		private void Sklad_Dobav_Click(object sender, RoutedEventArgs e)
		{
			// Считываем данные из UI
			string detailTypes = Deteil_Types.Text;
			string model = Model.Text;
			string proizvod = Proizvod.Text;
			string postav = Postav.Text;
			int devicesId = string.IsNullOrWhiteSpace(Devices_ID.Text) ? 0 : Convert.ToInt32(Devices_ID.Text);
			string location = Location.Text;
			string kolich = Kolich.Text;
			byte[] imageBytes = null; // Массив байтов для изображения
			string imageName = null;  // Имя файла изображения

			// Если файл выбран, преобразуем его в массив байтов и извлекаем имя
			if (!string.IsNullOrEmpty(imageFilePath) && File.Exists(imageFilePath))
			{
				imageBytes = File.ReadAllBytes(imageFilePath);
				imageName = Path.GetFileName(imageFilePath); // Извлекаем имя файла
			}

			// SQL-запрос
			string query = "INSERT INTO [Technical_Service].[dbo].[Sklad] " +
						   "([Deteil_Types], [Model], [Proizvod], [Postav], [Devices_ID], [Name_Image], [Image], [Location], [Kolich]) " +
						   "VALUES (@Deteil_Types, @Model, @Proizvod, @Postav, @Devices_ID, @Name_Image, @Image, @Location, @Kolich)";

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
						command.Parameters.AddWithValue("@Devices_ID", devicesId == 0 ? (object)DBNull.Value : devicesId);
						command.Parameters.AddWithValue("@Name_Image", string.IsNullOrEmpty(imageName) ? (object)DBNull.Value : imageName);

						// Проверяем, является ли imageBytes массивом байтов
						if (imageBytes != null && imageBytes is byte[])
						{
							command.Parameters.Add("@Image", SqlDbType.VarBinary).Value = imageBytes;
						}
						else
						{
							command.Parameters.Add("@Image", SqlDbType.VarBinary).Value = DBNull.Value;
						}

						command.Parameters.AddWithValue("@Location", string.IsNullOrEmpty(location) ? (object)DBNull.Value : location);
						command.Parameters.AddWithValue("@Kolich", string.IsNullOrEmpty(kolich) ? (object)DBNull.Value : kolich);

						// Открываем соединение и выполняем запрос
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();

						// Проверка результата
						if (rowsAffected > 0)
						{
							mainWindow.LoadData_Sklad(); // Обновляем данные
							this.Close(); // Закрываем текущее окно
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


	}
}
