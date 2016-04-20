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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;
using System.IO;

namespace Ex2_Maze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string dIP{get; set;}
        public string dPORT { get; set; }
        public ViewModel vm;   


        public MainWindow()
        {
            GetConnectionInfo();
            this.vm = new ViewModel(new Model(new TelnetClient()));
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            DataContext = vm;
            string musicPath = Directory.GetCurrentDirectory();
            musicPath += "Krewella_-_Enjoy_The_Ride_Vicetone_Remix_.wav";
            //SoundPlayer MusicPlayer = new System.Media.SoundPlayer(@musicPath);
            //vm.Connect(dIP, dPORT);
            //SoundPlayer MusicPlayer = new System.Media.SoundPlayer(@"C:\Users\Nava\Source\Repos\Ex2_Maze\Ex2_Maze\Ex2_Maze\Krewella_-_Enjoy_The_Ride_Vicetone_Remix_.wav");
            //MusicPlayer.Play();
        }


        /// <summary>
        /// Method that will handle and the click of the settings button
        /// </summary>
        /// <param name="sender">Who sent the click</param>
        /// <param name="e">and params to be passed</param>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings(this);
            settingsWindow.ShowDialog();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            Play playerWindow = new Play(this);
            playerWindow.ShowDialog();
        }

        private void Multiplayer_Click(object sender, RoutedEventArgs e)
        {
           Multiplayer multiplayerWindow  = new Multiplayer();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //multiplayerWindow.Show();
        }

        private void GetConnectionInfo()
        {
            this.dPORT = (System.Configuration.ConfigurationManager.AppSettings["Port"]);
            this.dIP = (System.Configuration.ConfigurationManager.AppSettings["IP"]);
        }

        public void ChangeConnectionSettings(string ip, string port)
        {
            this.dIP = ip;
            this.dPORT = port;
            vm.Connect(ip, port);
            
        }
    }
}
