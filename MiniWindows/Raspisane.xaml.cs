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
	/// Логика взаимодействия для Raspisane.xaml
	/// </summary>
	public partial class Raspisane : Window
	{
		public Raspisane()
		{
			InitializeComponent();
		}


		private void SaveScheduleToDatabase(string schedule)
		{
			// Реализуйте метод для сохранения расписания в базу данных
			// Например, используя ADO.NET или Entity Framework
		}

		private void Frequency_Checked(object sender, RoutedEventArgs e)
		{
			bool isSpecificWeek = SpecificWeekRadioButton.IsChecked == true;
			SpecificWeekDatePicker.IsEnabled = isSpecificWeek;
			SpecificWeekDateLabel.Visibility = isSpecificWeek ? Visibility.Visible : Visibility.Collapsed;
			SpecificWeekDatePicker.Visibility = isSpecificWeek ? Visibility.Visible : Visibility.Collapsed;
		}

		private void CreateScheduleButton_Click(object sender, RoutedEventArgs e)
		{
			// Сбор данных из UI
			var selectedDays = new[] {
		MondayCheckBox.IsChecked == true ? "Понедельник" : null,
		TuesdayCheckBox.IsChecked == true ? "Вторник" : null,
		WednesdayCheckBox.IsChecked == true ? "Среда" : null,
		ThursdayCheckBox.IsChecked == true ? "Четверг" : null,
		FridayCheckBox.IsChecked == true ? "Пятница" : null,
		SaturdayCheckBox.IsChecked == true ? "Суббота" : null,
		SundayCheckBox.IsChecked == true ? "Воскресенье" : null
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

			var frequency = WeeklyRadioButton.IsChecked == true ? "Каждую неделю" :
							SpecificWeekRadioButton.IsChecked == true ? "Определенная неделя" : null;

			var specificDays = SpecificDaysTextBox.Text;

			var everySixMonths = EverySixMonthsCheckBox.IsChecked == true ? "Да" : "Нет";
			var everyQuarter = EveryQuarterCheckBox.IsChecked == true ? "Да" : "Нет";

			// Формирование строки расписания
			string schedule = $"Дни недели: {string.Join(", ", selectedDays)}; " +
							  $"Месяцы: {string.Join(", ", selectedMonths)}; " +
							  $"Частота: {frequency}; " +
							  $"Определенные дни: {specificDays}; " +
							  $"Каждые полгода: {everySixMonths}; " +
							  $"Каждый квартал: {everyQuarter}";

			// SQL-запрос для записи расписания
			string query = "INSERT INTO [Technical_Service].[dbo].[Types_TO] (Raspisanie) VALUES (@Schedule)";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();
					using (var command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Schedule", schedule);
						command.ExecuteNonQuery();
					}
				}

				OutputTextBox.Text = "Расписание успешно сохранено!";
			}
			catch (Exception ex)
			{
				OutputTextBox.Text = $"Ошибка: {ex.Message}";
			}
		}

	}
}
