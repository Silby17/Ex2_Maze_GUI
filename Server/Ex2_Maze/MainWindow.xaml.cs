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

namespace Ex2_Maze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel vm;   
        public MainWindow()
        {
            this.vm = new ViewModel(new Model(new TelnetClient()));
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            DataContext = vm;
            //SoundPlayer MusicPlayer = new System.Media.SoundPlayer(@"C:\Users\Nava\Source\Repos\Ex2_Maze\Ex2_Maze\Ex2_Maze\Krewella_-_Enjoy_The_Ride_Vicetone_Remix_.wav");
            //MusicPlayer.Play();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings();
            settingsWindow.ShowDialog();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Play playerWindow = new Play();
           // playerWindow.Show();
        }

        private void Multiplayer_Click(object sender, RoutedEventArgs e)
        {
           Multiplayer multiplayerWindow  = new Multiplayer();
            //multiplayerWindow.Show();
        }
    }
}
