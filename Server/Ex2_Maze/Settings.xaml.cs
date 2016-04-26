using System.Configuration;
using System.Windows;
using System;


namespace Ex2_Maze
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private ViewModel viewModel;
        System.Configuration.Configuration config;
        private string IP;
        private string PORT;


        /// <summary>
        /// Constructor Method that receives the viewModel</summary>
        /// <param name="vm">the programs ViewModel</param>
        public Settings(ViewModel vm)
        {
            this.config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            this.viewModel = vm;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ReadDefaultSettings();
            InitializeComponent();
            lblIP.Text = this.IP;
            lblPORT.Text = this.PORT;
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
            viewModel.Connect(ip, port);
            config.AppSettings.Settings["PORT"].Value = port;
            config.AppSettings.Settings["IP"].Value = ip;
            config.Save(ConfigurationSaveMode.Modified);
            this.Close();
        }


        /// <summary>
        /// Closes the current window</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        { this.Close();}


        /// <summary>
        /// Gets the default network settings from the AppConfig file</summary>
        public void ReadDefaultSettings()
        {
            this.PORT =(config.AppSettings.Settings["Port"].Value);
            this.IP = (config.AppSettings.Settings["IP"].Value);
        }
    }
}