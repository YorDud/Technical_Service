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

			WorkList.Clear();
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

			string schedule = $"Дни недели: {string.Join(", ", selectedDays)}; " +
							  $"Месяцы: {string.Join(", ", selectedMonths)}; " +
							  $"Повторять каждые: {repeatWeeks} недель; " +
							  $"День месяца: {specificDay}; ";

			// SQL-запрос для добавления записи в таблицу Types_TO
			string query = "INSERT INTO [Technical_Service].[dbo].[Types_TO] " +
						   "([Device_Type], [Name_TO], [Work_List], [Raspisanie]) " +
						   "VALUES (@DeviceType, @NameTO, @WorkList, @Schedule)";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Установка параметров для запроса
						command.Parameters.AddWithValue("@DeviceType", deviceType);
						command.Parameters.AddWithValue("@NameTO", nameTO);
						command.Parameters.AddWithValue("@WorkList", workList);
						command.Parameters.AddWithValue("@Schedule", schedule);

						connection.Open();
						command.ExecuteNonQuery();

						// Обновление данных в главном окне
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

		

	}

}
