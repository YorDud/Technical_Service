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
	/// Логика взаимодействия для Monitor_Naryad_Nalad.xaml
	/// </summary>
	public partial class Monitor_Naryad_Nalad : Window
	{
		private MainWindowNalad mainWindow;
		private DataRowView _dataRow;
		private int workNumber = 1;
		public Monitor_Naryad_Nalad(MainWindowNalad mainWindow, DataRowView dataRow)
		{
			InitializeComponent();
			_dataRow = dataRow;
			this.mainWindow = mainWindow;
			LoadFIO();

		}

		

		// Привязка события на изменение DeviceType
		private void DeviceType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			// Загружаем Work_List в зависимости от выбранного Device_Type
			LoadFIO();
		}


		private void AddWork_Click(object sender, RoutedEventArgs e)
		{
			// Создание новой строки с номером и текстом работы
			string workName = FIONalad.Text; // Считываем название работы из текстового поля (можно сделать отдельное поле для наименования работы)
			string workLine = $"{workNumber}. {workName}";

			// Добавляем в ListBox
			FIONaladBox.Items.Add(workLine);

			// Увеличиваем номер для следующей работы
			workNumber++;

			FIONalad.Text = string.Empty;



		}


		private void LoadFIO()
		{
			string query = "SELECT [FIO] FROM [Technical_Service].[dbo].[Users]";
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
								typesTO.Add(reader["FIO"].ToString());
							}
							FIONalad.ItemsSource = typesTO; // Устанавливаем источник данных для ComboBox
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

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void AddNalad_Click(object sender, RoutedEventArgs e)
		{
			// Создание новой строки с номером и текстом работы
			string workName = FIONalad.Text; // Считываем название работы из текстового поля (можно сделать отдельное поле для наименования работы)
			string workLine = $"{workNumber}. {workName}";

			// Добавляем в ListBox
			FIONaladBox.Items.Add(workLine);

			// Увеличиваем номер для следующей работы
			workNumber++;

			FIONalad.Text = string.Empty;
		}

		private void StartWork_Click(object sender, RoutedEventArgs e)
		{
			var workList = string.Join("\n", FIONaladBox.Items.Cast<string>());

			var id = _dataRow["ID"];

			// SQL-запрос для обновления данных в базе
			string query = "UPDATE [Technical_Service].[dbo].[Naryad] " +
						   "SET [Users_FIO] = @Users_FIO, [Date_Start] = @Date_Start, " +
						   "[Status] = @Status " +
						   "WHERE [ID] = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Users_FIO", workList); // Обновляем перечень работ
						command.Parameters.AddWithValue("@Date_Start", DateTime.Now);
						command.Parameters.AddWithValue("@Status", "В работе");
						command.Parameters.AddWithValue("@ID", id);

						connection.Open();
						command.ExecuteNonQuery();

						// После сохранения изменений обновляем данные в главном окне
						mainWindow.LoadData_Monitor_Naryad();
						this.Close();
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
