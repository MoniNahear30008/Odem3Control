using System.Text;

namespace OdemControl
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            string msg = (e.Exception as Exception).Message;
            string st = ((System.Exception)e.Exception).StackTrace;
            HandleExc(msg, st);
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            string msg = (e.ExceptionObject as Exception).Message;
            string st = ((System.Exception)e.ExceptionObject).StackTrace;
            HandleExc(msg, st);
        }
        static void HandleExc(string msg, string st)
        {
            MessageBox.Show("Unhandled excption occured: " + msg + "\n\nPlease send c:\\lidwave\\Exceptions.txt\nand decription of the action that caused it to support@lidwave.com");
            StreamWriter sw = new StreamWriter("c:\\lidwave\\Exceptions.txt", true, Encoding.ASCII);

            string ver = Application.ProductVersion;
            string dateTimeString = DateTime.Now.ToString("yyyy_MM_dd-HH_mm");

            sw.WriteLine("\n" + dateTimeString + " Version = " + ver + " ================================");
            sw.WriteLine(msg);
            sw.WriteLine(st);
            sw.Close();
        }
    }
}