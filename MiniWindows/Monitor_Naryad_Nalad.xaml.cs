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
	/// Логика взаимодействия для Monitor_Naryad_Nalad.xaml
	/// </summary>
	public partial class Monitor_Naryad_Nalad : Window
	{
		private MainWindow mainWindow;
		private int workNumber = 1;
		public Monitor_Naryad_Nalad(MainWindow mainWindow)
		{
			InitializeComponent();

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

		
	}
    
}
