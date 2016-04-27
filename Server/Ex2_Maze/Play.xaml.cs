using Ex1_Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Web.Script.Serialization;

namespace Ex2_Maze
{
    /// <summary>
    /// Interaction logic for Play.xaml
    /// </summary>
    public partial class Play : Window
    {
        private Random rand;
        ViewModel viewModel;
        private string MazeName;
        private int WIDTH;
        private int HEIGHT;
        private SoundPlayer MusicPlayer;

        public Play(ViewModel vm)
        {
            this.WIDTH  = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Width"]);
            this.HEIGHT = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Bredth"]);
            this.viewModel = vm;
            this.DataContext = vm;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.MazeName = "";
            /**
            string fileName = "Shuv Toda.wav";
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);
            this.MusicPlayer = new SoundPlayer(path);
            MusicPlayer.Play();
            **/
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.VM_Connected == false)
            {
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("No Connection with Server", "Connection Error", MessageBoxButton.OK, icon);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Do you want to start a new game?",
                    "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    //Start game from the begining
                }
                //TODO to reload the window
                else if (result == MessageBoxResult.Yes)
                {
                    String gen = "1 Maze";
                    this.rand = new Random();
                    string num = (rand.Next(0, 30)).ToString();
                    gen += num += " ";
                    int type = rand.Next(0, 2);
                    gen += type.ToString();
                    this.MazeName = gen;
                    viewModel.Command(gen);
                    InitializeComponent();
                    lst.ItemsSource = viewModel.VM_Maze;
                }
            }
        }


        /// <summary>
        /// OnClick handler for for the Exit button</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?",
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //this.MusicPlayer.Stop();
                this.Close();}
        }


        /// <summary>
        /// OnClick Handler for the getSugestion button</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Suggestion_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.VM_Connected == false)
            {
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("No Connection with Server", "Connection Error",
                    MessageBoxButton.OK, icon);
            }
            else if(this.MazeName == "")
            {
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("No Maze Loaded", "Maze Error",
                    MessageBoxButton.OK, icon);
            }
        }




        /// <summary>
        /// This will handle any keys pressed on the play window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Up)
            {
                viewModel.Move("up");
            }
            else if(e.Key == Key.Right)
            {
                viewModel.Move("right");
            }
            else if (e.Key == Key.Down)
            {
                viewModel.Move("down");
            }
            else if (e.Key == Key.Left)
            {
                viewModel.Move("left");
            }
            lst.Items.Refresh();
        }
    }  
}