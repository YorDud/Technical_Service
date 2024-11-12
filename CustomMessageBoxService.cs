using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp4
{
	public static class CustomMessageBoxService
	{
		/// <summary>
		/// Показывает пользовательское MessageBox с заголовком, сообщением и кнопкой OK.
		/// </summary>
		public static bool Show(string message, string title)
		{
			// Создаём новый экземпляр окна при каждом вызове
			CustomMessageBox msgBox = new CustomMessageBox(title, message);
			msgBox.Owner = Application.Current.MainWindow;
			bool? result = msgBox.ShowDialog();
			return result == true;
		}

		/// <summary>
		/// Показывает пользовательское MessageBox с заголовком, сообщением и кнопками OK и Cancel.
		/// </summary>
		public static bool? Show(string message, string title, bool showCancel)
		{
			// Создаём новый экземпляр окна при каждом вызове
			CustomMessageBox msgBox = new CustomMessageBox(title, message, showCancel);
			msgBox.Owner = Application.Current.MainWindow;
			bool? result = msgBox.ShowDialog();
			return result;
		}
	}
}

