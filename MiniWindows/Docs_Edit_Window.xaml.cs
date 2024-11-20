using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using Path = System.IO.Path;

namespace WpfApp4.MiniWindows
{
	/// <summary>
	/// Логика взаимодействия для Docs_Edit_Window.xaml
	/// </summary>
	public partial class Docs_Edit_Window : Window
	{

		private DataRowView _dataRow;
		private MainWindow mainWindow;

		private string filePath;
		public Docs_Edit_Window(MainWindow mainWindow, DataRowView dataRow)
		{
			InitializeComponent();

			FileDropBorder.AllowDrop = true;
			FileDropBorder.Drop += new DragEventHandler(FileDropBorder_Drop);
			FileDropBorder.DragOver += new DragEventHandler(FileDropBorder_DragOver);

			this.mainWindow = mainWindow; // сохраняем ссылку на главное окно

			_dataRow = dataRow;
			Opisaniye.Text = _dataRow["Opisaniye"].ToString();
			
		}

		private void Docs_Update_Click(object sender, RoutedEventArgs e)
		{
			string opisaniye1 = Opisaniye.Text;
			string query = "UPDATE [Technical_Service].[dbo].[Documentation] " +
						   "SET [Name_Doc] = @Name_Doc, [Doc] = @Doc, [Opisaniye] = @Opisaniye " +
						   "WHERE [ID] = @ID"; // Предположим, что у вас есть идентификатор документа

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
						{
							// Преобразуем файл в массив байтов
							byte[] fileData = File.ReadAllBytes(filePath);
							string fileName = FileNameTextBox.Text;

							// Добавляем параметры
							command.Parameters.AddWithValue("@Name_Doc", fileName);
							command.Parameters.AddWithValue("@Doc", fileData);
							command.Parameters.AddWithValue("@Opisaniye", opisaniye1);

							// Добавляем параметр ID для обновления конкретной записи
							command.Parameters.AddWithValue("@ID", _dataRow["ID"]); // Замените "ID" на ваше истинное имя столбца идентификатора
						}
						else
						{
							
						}

						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();
						if (rowsAffected > 0)
						{
							//MessageBox.Show("Документация обновлена!");
							mainWindow.LoadData_Docs(); // Обновляем данные, если нужно
							this.Close();
						}
						else
						{
							MessageBox.Show("Ошибка при обновлении документации.");
						}
					}
				}
			}
			catch
			{
				string query2 = "UPDATE [Technical_Service].[dbo].[Documentation] " +
						   "SET [Opisaniye] = @Opisaniye " +
						   "WHERE [ID] = @ID";

				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query2, connection))
					{

						

						// Преобразуем файл в массив байтов

						command.Parameters.AddWithValue("@Opisaniye", opisaniye1);

							// Добавляем параметр ID для обновления конкретной записи
							command.Parameters.AddWithValue("@ID", _dataRow["ID"]); // Замените "ID" на ваше истинное имя столбца идентификатора
						

						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();
						if (rowsAffected > 0)
						{
							//MessageBox.Show("Документация обновлена!");
							mainWindow.LoadData_Docs(); // Обновляем данные, если нужно
							this.Close();
						}
						else
						{
							MessageBox.Show("Ошибка при обновлении документации.");
						}
					}
				}
			}
		}

		private void Docs_Delete_Click(object sender, RoutedEventArgs e)
		{
			string query = "DELETE FROM [Technical_Service].[dbo].[Documentation] WHERE [ID] = @ID"; // Замените "ID" на ваше истинное имя столбца идентификатора

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавляем параметр ID для удаления конкретной записи
						command.Parameters.AddWithValue("@ID", _dataRow["ID"]); // Убедитесь, что _dataRow содержит идентификатор

						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();
						if (rowsAffected > 0)
						{
							//MessageBox.Show("Документация удалена!");
							mainWindow.LoadData_Docs(); // Обновляем данные, если нужно
							this.Close();
						}
						else
						{
							MessageBox.Show("Ошибка при удалении документации.");
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
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
					filePath = files[0]; // Сохраняем путь к первому перетаскиваемому файлу
										 // Проверка на то, что файл существует
					if (File.Exists(filePath))
					{
						FileNameTextBox.Text = Path.GetFileName(filePath); // Отображаем имя файла в TextBox
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

