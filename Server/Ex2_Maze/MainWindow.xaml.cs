using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public ViewModel viewModel;   

        /// <summary>
        /// Constructor Method for this MainWindow</summary>
        public MainWindow()
        {
            this.viewModel = new ViewModel(new Model(new TelnetClient()));
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            DataContext = viewModel;
            string fileName = "Krewella - Enjoy_The_Ride.wav";
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);
            SoundPlayer MusicPlayer = new SoundPlayer(path);
            MusicPlayer.Play();
        }


        /// <summary>
        /// Method that will handle and the click of the settings button
        /// </summary>
        /// <param name="sender">Who sent the click</param>
        /// <param name="e">and params to be passed</param>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings(viewModel);
            settingsWindow.ShowDialog();
        }


        /// <summary>
        /// Method that will handle the OnClick of the Play button</summary>
        /// <param name="sender">Sender that clicked the button</param>
        /// <param name="e">Event Params</param>
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Play playerWindow = new Play(viewModel);
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            playerWindow.ShowDialog();
        }


        /// <summary>
        /// Method that will handle the the OnClick of the multiplayer Button</summary>
        /// <param name="sender">Sender of the button click</param>
        /// <param name="e">Params that are send to the handler</param>
        private void Multiplayer_Click(object sender, RoutedEventArgs e)
        {
            Multiplayer multiplayerWindow  = new Multiplayer(viewModel);
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            multiplayerWindow.ShowDialog();
        }
    }
}