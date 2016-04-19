using System;
using System.Windows;


namespace Ex2_Maze
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private string sIP;
        private string sPORT;
        private MainWindow mainWindow;

        public Settings(MainWindow mw)
        {
            this.mainWindow = mw;
            
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ReadDefaultSettings();
            InitializeComponent();
            lblIP.Text = mw.dIP.ToString();
            lblPORT.Text = mw.dPORT;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Save clicked");
            string ip = lblIP.Text.ToString();
            string port = lblPORT.Text.ToString();
            mainWindow.ChangeConnectionSettings(ip, port);
            this.Close();
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Cancle CLicked");
            string g = lblIP.Text.ToString();
            this.Close();
        }

        public void ReadDefaultSettings()
        {
            this.sPORT =(System.Configuration.ConfigurationManager.AppSettings["Port"]);
            this.sIP = (System.Configuration.ConfigurationManager.AppSettings["IP"]);
        }
    }
}
