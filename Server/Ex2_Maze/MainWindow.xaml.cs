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


namespace Ex2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
           // SoundPlayer player = new SoundPlayer(Krewella - Enjoy The Ride(Vicetone Remix));
            //player.Load();
            //player.Play();
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
