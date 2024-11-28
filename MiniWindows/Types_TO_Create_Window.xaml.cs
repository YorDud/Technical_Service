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
using Avalonia.Controls;
using Window = System.Windows.Window;

namespace WpfApp4.MiniWindows
{
	/// <summary>
	/// Логика взаимодействия для Types_TO_Create_Window.xaml
	/// </summary>
	public partial class Types_TO_Create_Window : Window
	{
		private MainWindow mainWindow;
		private int workNumber = 1; // Номер текущей работы

		public Types_TO_Create_Window(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;

			LoadDevice_Type();
			LoadWork_List();
		}

		private void LoadWork_List()
		{
			if (DeviceType.SelectedItem == null)
			{
				//MessageBox.Show("Пожалуйста, выберите тип устройства.");
				return;
			}

			// Получаем выбранное значение из DeviceType ComboBox
			string selectedDevice = DeviceType.SelectedItem.ToString();

			// SQL-запрос с фильтром по Device_Type
			string query = @"
        SELECT [Work_List] 
        FROM [Technical_Service].[dbo].[Work_List] 
        WHERE [Device_Type] = @Device_Type";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Передаём значение выбранного устройства в качестве параметра
						command.Parameters.AddWithValue("@Device_Type", selectedDevice);

						using (SqlDataReader reader = command.ExecuteReader())
						{
							List<string> workListItems = new List<string>();
							while (reader.Read())
							{
								// Добавляем элементы Work_List в список
								workListItems.Add(reader["Work_List"].ToString());
							}

							// Устанавливаем источник данных для ComboBox WorkList
							WorkList.ItemsSource = workListItems;
						}
					}
				}
			}
			catch (Exception ex)
			{
				// Обработка ошибок
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

		// Привязка события на изменение DeviceType
		private void DeviceType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			// Загружаем Work_List в зависимости от выбранного Device_Type
			LoadWork_List();
		}


		private void AddWork_Click(object sender, RoutedEventArgs e)
		{
			// Создание новой строки с номером и текстом работы
			string workName = WorkList.Text; // Считываем название работы из текстового поля (можно сделать отдельное поле для наименования работы)
			string workLine = $"{workNumber}. {workName}";

			// Добавляем в ListBox
			WorkListBox.Items.Add(workLine);

			// Увеличиваем номер для следующей работы
			workNumber++;

			WorkList.Text = string.Empty;



		}


		private void LoadDevice_Type()
		{
			string query = "SELECT Device_Type FROM [Technical_Service].[dbo].[Devices_Types]";
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
								typesTO.Add(reader["Device_Type"].ToString());
							}
							DeviceType.ItemsSource = typesTO; // Устанавливаем источник данных для ComboBox
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



		private void FirstWeekMonthCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			SpecificDaysTextBox.IsEnabled = false;
			RepeatWeeksTextBox.IsEnabled = false;
		}

		private void FirstWeekMonthCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			SpecificDaysTextBox.IsEnabled = true;
			RepeatWeeksTextBox.IsEnabled = true;
		}


		private void AddTypeTO_Click(object sender, RoutedEventArgs e)
		{
			// Сбор данных из UI
			var deviceType = DeviceType.Text;
			var nameTO = NameTO.Text;

			// Создаем строку с перечнем работ
			var workList = string.Join("\n", WorkListBox.Items.Cast<string>());

			// Формирование расписания
			var selectedDays = new[] {
			MondayCheckBox.IsChecked == true ? "Пн" : null,
			TuesdayCheckBox.IsChecked == true ? "Вт" : null,
			WednesdayCheckBox.IsChecked == true ? "Ср" : null,
			ThursdayCheckBox.IsChecked == true ? "Чт" : null,
			FridayCheckBox.IsChecked == true ? "Пт" : null,
			SaturdayCheckBox.IsChecked == true ? "Сб" : null,
			SundayCheckBox.IsChecked == true ? "Вс" : null
		}.Where(day => day != null).ToList();

			var selectedMonths = new[] {
			JanuaryCheckBox.IsChecked == true ? "Январь" : null,
			FebruaryCheckBox.IsChecked == true ? "Февраль" : null,
			MarchCheckBox.IsChecked == true ? "Март" : null,
			AprilCheckBox.IsChecked == true ? "Апрель" : null,
			MayCheckBox.IsChecked == true ? "Май" : null,
			JuneCheckBox.IsChecked == true ? "Июнь" : null,
			JulyCheckBox.IsChecked == true ? "Июль" : null,
			AugustCheckBox.IsChecked == true ? "Август" : null,
			SeptemberCheckBox.IsChecked == true ? "Сентябрь" : null,
			OctoberCheckBox.IsChecked == true ? "Октябрь" : null,
			NovemberCheckBox.IsChecked == true ? "Ноябрь" : null,
			DecemberCheckBox.IsChecked == true ? "Декабрь" : null
		}.Where(month => month != null).ToList();

			int.TryParse(RepeatWeeksTextBox.Text, out int repeatWeeks);
			int.TryParse(SpecificDaysTextBox.Text, out int specificDay);

			string firstWeek = FirstWeekMonthCheckBox.IsChecked == true ? "Да" : "Нет";

			string schedule = $"Дни недели: {string.Join(", ", selectedDays)}; " +
							  $"Месяцы: {string.Join(", ", selectedMonths)}; " +
							  $"Повторять каждые: {repeatWeeks} недель; " +
							  $"День месяца: {specificDay}; " +
							  $"Первая неделя месяца: {firstWeek}";

			string query = "INSERT INTO [Technical_Service].[dbo].[Types_TO] " +
						   "([Device_Type], [Name_TO], [Work_List], [Raspisanie]) " +
						   "VALUES (@DeviceType, @NameTO, @WorkList, @Schedule)";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DeviceType", deviceType);
						command.Parameters.AddWithValue("@NameTO", nameTO);
						command.Parameters.AddWithValue("@WorkList", workList);
						command.Parameters.AddWithValue("@Schedule", schedule);

						connection.Open();
						command.ExecuteNonQuery();

						mainWindow.LoadData_TypesTO();
						this.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}

		private void DayOfWeekChecked(object sender, RoutedEventArgs e)
		{
			// Когда выбран день недели, нужно отключить дни месяца
			SpecificDaysTextBox.IsEnabled = false;
		}

		private void DayOfWeekUnchecked(object sender, RoutedEventArgs e)
		{
			// Когда день недели снят, включаем дни месяца
			if (!AreAnyDayOfWeekChecked())
			{
				SpecificDaysTextBox.IsEnabled = true;
			}
		}

		private bool AreAnyDayOfWeekChecked()
		{
			return MondayCheckBox.IsChecked == true ||
				   TuesdayCheckBox.IsChecked == true ||
				   WednesdayCheckBox.IsChecked == true ||
				   ThursdayCheckBox.IsChecked == true ||
				   FridayCheckBox.IsChecked == true ||
				   SaturdayCheckBox.IsChecked == true ||
				   SundayCheckBox.IsChecked == true;
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }
    }

}
