using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
	public class WC
	{
		//public static string ConnectionString = "Server=alb-pcb-sql;Database=Lab_Rez;User Id=sa;Password=b^Ax*m>7;";
		public static string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";
		public static string fio;
		public static string login;
		public static string id_User;
		public static int id_click;
		public static string selectedLogin;

		public Bitmap Screenshot { get; private set; }

		public void SetScreenshot(Bitmap screenshot)
		{
			this.Screenshot = screenshot;
		}







		public static string Value_Cond;


	}
}
