using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Ex2_Maze
{
    /// <summary>
    /// Interaction logic for Multiplayer.xaml
    /// </summary>
    public partial class Multiplayer : Window
    {
        ViewModel viewModel;


        /// <summary>
        /// Constructor Method that receives the ViewModel</summary>
        /// <param name="vm">The ViewModel of the program</param>
        public Multiplayer(ViewModel vm)
        {
            this.viewModel = vm;
            this.DataContext = vm;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            //SoundPlayer MusicPlayer = new System.Media.SoundPlayer(@"C:\Users\Nava\Source\Repos\Ex2_Maze_GUI\Server\Ex2_Maze\sovtoda.wav");
            //MusicPlayer.Play();
        }
       

        /// <summary>
        /// This is the OnClick handler for the Start button</summary>
        /// <param name="sender">who clicked</param>
        /// <param name="e">Passed Params</param>
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.VM_Connected == false)
            {
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("No Connection with Server", "Connection Error",
                    MessageBoxButton.OK, icon);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Do you want to start a new game?",
                    "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    //close window 
                }
                //TODO to reload the window
                else if (result == MessageBoxResult.Yes)
                {
                    if (txtbMaze.Text == "")
                    {
                        MessageBox.Show("Please enter in a Game Name");
                    }
                    else
                    {
                        string mazeName = txtbMaze.Text;
                        string gen = "3 ";
                        gen += mazeName;
                        ProgIndicator.IsBusy = true;
                        Task.Factory.StartNew(() =>
                        {
                            viewModel.Command(gen);
                        }
                         ).ContinueWith((task) =>
                         {
                             ProgIndicator.IsBusy = false;

                             InitializeComponent();
                             myMaze.ItemsSource = viewModel.VM_MyMaze;
                             plr2.ItemsSource = viewModel.VM_Player2Maze;
                         }, TaskScheduler.FromCurrentSynchronizationContext()
            );
                    }
                }
            }
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();

            }

        }

        private void Suggestion_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.VM_Connected == false)
            {
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("No Connection with Server", "Connection Error", MessageBoxButton.OK, icon);
            }
            else
            {
            }
        }

        /// <summary>
        /// This will handle any keys pressed on the play window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                viewModel.Command("up");
            }
            else if (e.Key == Key.Right)
            {
                viewModel.Command("right");
            }
            else if (e.Key == Key.Down)
            {

            }
            else if (e.Key == Key.Left)
            {

            }
        }


    }
}
