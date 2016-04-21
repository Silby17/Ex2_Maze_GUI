using System;
using System.Configuration;
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
        System.Configuration.Configuration config;

        public Settings(MainWindow mw)
        {
            this.config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            this.mainWindow = mw;
            
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ReadDefaultSettings();
            InitializeComponent();
            lblIP.Text = mw.dIP.ToString();
            lblPORT.Text = mw.dPORT;
        }


        /// <summary>
        /// Method that will deal with the click on the save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string ip = lblIP.Text.ToString();
            string port = lblPORT.Text.ToString();
            mainWindow.ChangeConnectionSettings(ip, port);
            config.AppSettings.Settings["PORT"].Value = port;
            config.AppSettings.Settings["IP"].Value = ip;
            config.Save(ConfigurationSaveMode.Modified);
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
            this.sPORT =(config.AppSettings.Settings["Port"].Value);
            this.sIP = (config.AppSettings.Settings["IP"].Value);
        }
    }
}
