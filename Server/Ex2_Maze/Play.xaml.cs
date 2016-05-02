using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Media;
using System;

namespace Ex2_Maze
{
    /// <summary>
    /// Interaction logic for Play.xaml
    /// </summary>
    public partial class Play : Window
    {
        private SoundPlayer MusicPlayer;
        private string MazeName;
        private Random rand;
        ViewModel viewModel;
        private bool GameStarted;
        
        /// <summary>
        /// Constructor Method</summary>
        /// <param name="vm">ViewModel</param>
        public Play(ViewModel vm)
        {
            this.viewModel = vm;
            this.DataContext = vm;
            //Subscribe the viewModel to Closing events of this window
            Closing += viewModel.OnWindowClosing;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.MazeName = "";
            this.viewModel.PropertyChanged += delegate (object seder, PropertyChangedEventArgs e)
            {
                ReceiveEvent(e.PropertyName);
            };
            StartMusic();
        }


        /// <summary>
        /// Starts background music</summary>
        public void StartMusic()
        {
            string fileName = "Shuv Toda.wav";
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);
            this.MusicPlayer = new SoundPlayer(path);
            MusicPlayer.Play();
        }

        /// <summary>
        /// This method will stop the music Player</summary>
        public void StopMusic()
        { this.MusicPlayer.Stop(); }


        /// <summary>
        /// Event Handler that gets events from the viewModel</summary>
        /// <param name="eventData">Event Params</param>
        public void ReceiveEvent(string eventData)
        {
            //Event that the player has reached his goal point
            if(eventData == "VM_Winner")
            {
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show("You have reached the end!", "You Won",
                    MessageBoxButton.OK, icon);
                this.Close();
            }
        }


        /// <summary>
        /// Start Button Click handler</summary>
        /// <param name="sender">Sender of the button click</param>
        /// <param name="e">params of the click</param>
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //Checks if there is a connection to the server
            if (viewModel.VM_Connected == false)
            {
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("No Connection with Server", "Connection Error",
                    MessageBoxButton.OK, icon);
            }
            else
            {
                //Show confirmation Message
                MessageBoxResult result = MessageBox.Show("Do you want to start a new game?",
                    "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    string gen = "1 " + MazeName + " 1";
                    viewModel.Command(gen);
                    InitializeComponent();
                    lst.ItemsSource = viewModel.VM_Maze;
                }
                else if (result == MessageBoxResult.Yes)
                {
                    String gen = "1 Maze";
                    this.rand = new Random();
                    string num = (rand.Next(0, 30)).ToString();
                    gen += num += " ";
                    //Chose a random Algo to Generate Maze
                    int type = rand.Next(0, 2);
                    gen += type.ToString();
                    //Send Generate Command to the server
                    viewModel.Command(gen);
                    InitializeComponent();
                    this.MazeName = viewModel.VW_MazeName;
                    //Set Source for data binding
                    lst.ItemsSource = viewModel.VM_Maze;
                    GameStarted = true;
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
                this.Close();}
        }


        /// <summary>
        /// OnClick Handler for the getSugestion button</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Suggestion_Click(object sender, RoutedEventArgs e)
        {
            //Checks if there is a connection to the server
            if (viewModel.VM_Connected == false)
            {
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("No Connection with Server", "Connection Error",
                    MessageBoxButton.OK, icon);
            }
            //Check if there is a game in progress
            else if(!GameStarted)
            {
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("No Game started", "Game error",
                    MessageBoxButton.OK, icon);
            }
            else
            {
                //Send Solve Request to the Server
                int solveType = rand.Next(0, 2);
                string solve = "2 " + this.MazeName + " " + solveType.ToString();
                viewModel.Command(solve);
                lst.Items.Refresh();
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
                viewModel.MovePlayer("up", "play");
            }
            else if(e.Key == Key.Right)
            {
                viewModel.MovePlayer("right", "play");
            }
            else if (e.Key == Key.Down)
            {
                viewModel.MovePlayer("down", "play");
            }
            else if (e.Key == Key.Left)
            {
                viewModel.MovePlayer("left", "play");
            }
            lst.Items.Refresh();
        }
    }  
}