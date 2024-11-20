using System;
using System.Collections.Generic;
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
			StringBuilder scheduleBuilder = new StringBuilder();
			scheduleBuilder.AppendLine("Выбранные дни недели:");

			if (MondayCheckBox.IsChecked == true) scheduleBuilder.Append("Понедельник, ");
			if (TuesdayCheckBox.IsChecked == true) scheduleBuilder.Append("Вторник, ");
			if (WednesdayCheckBox.IsChecked == true) scheduleBuilder.Append("Среда, ");
			if (ThursdayCheckBox.IsChecked == true) scheduleBuilder.Append("Четверг, ");
			if (FridayCheckBox.IsChecked == true) scheduleBuilder.Append("Пятница, ");
			if (SaturdayCheckBox.IsChecked == true) scheduleBuilder.Append("Суббота, ");
			if (SundayCheckBox.IsChecked == true) scheduleBuilder.Append("Воскресенье, ");

			scheduleBuilder.AppendLine();
			scheduleBuilder.AppendLine("Выбранные месяцы:");

			if (JanuaryCheckBox.IsChecked == true) scheduleBuilder.Append("Январь, ");
			if (FebruaryCheckBox.IsChecked == true) scheduleBuilder.Append("Февраль, ");
			if (MarchCheckBox.IsChecked == true) scheduleBuilder.Append("Март, ");
			if (AprilCheckBox.IsChecked == true) scheduleBuilder.Append("Апрель, ");
			if (MayCheckBox.IsChecked == true) scheduleBuilder.Append("Май, ");
			if (JuneCheckBox.IsChecked == true) scheduleBuilder.Append("Июнь, ");
			if (JulyCheckBox.IsChecked == true) scheduleBuilder.Append("Июль, ");
			if (AugustCheckBox.IsChecked == true) scheduleBuilder.Append("Август, ");
			if (SeptemberCheckBox.IsChecked == true) scheduleBuilder.Append("Сентябрь, ");
			if (OctoberCheckBox.IsChecked == true) scheduleBuilder.Append("Октябрь, ");
			if (NovemberCheckBox.IsChecked == true) scheduleBuilder.Append("Ноябрь, ");
			if (DecemberCheckBox.IsChecked == true) scheduleBuilder.Append("Декабрь, ");

			scheduleBuilder.AppendLine();
			scheduleBuilder.AppendLine("Частота выполнения:");
			if (WeeklyRadioButton.IsChecked == true) scheduleBuilder.AppendLine("Каждую неделю");
			if (SpecificWeekRadioButton.IsChecked == true)
			{
				scheduleBuilder.AppendLine($"Определенная неделя: {SpecificWeekDatePicker.SelectedDate}");
			}

			scheduleBuilder.AppendLine($"Определенные дни месяца: {SpecificDaysTextBox.Text}");
			scheduleBuilder.AppendLine($"Каждые полгода: {EverySixMonthsCheckBox.IsChecked}");
			scheduleBuilder.AppendLine($"Каждый квартал: {EveryQuarterCheckBox.IsChecked}");

			OutputTextBox.Text = scheduleBuilder.ToString(); 
			Obrabotka_Raspisanie scheduleProcessor = new Obrabotka_Raspisanie(scheduleBuilder.ToString());
			scheduleProcessor.Show();
		}
	}
}
