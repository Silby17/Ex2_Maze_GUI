using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System;
using System.Media;

namespace Ex2_Maze
{
    /// <summary>
    /// Interaction logic for Multiplayer.xaml
    /// </summary>
    public partial class Multiplayer : Window
    {
        ViewModel viewModel;
        private bool gameInMotion;
        public string MazeName;
        public string GameName;
        private SoundPlayer MusicPlayer;


        /// <summary>
        /// Constructor Method that receives the ViewModel</summary>
        /// <param name="vm">The ViewModel of the program</param>
        public Multiplayer(ViewModel vm)
        {
            this.viewModel = vm;
            this.DataContext = vm;
            //Subscribes to events 
            this.viewModel.PropertyChanged += ReceiveEvent;
            Closing += viewModel.OnWindowClosing;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            StartMusic();
        }

        
        /// <summary>
        /// This method will start the Background Music for the game</summary>
        public void StartMusic()
        {
            string fileName = "Shuv Toda.wav";
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);
            this.MusicPlayer = new SoundPlayer(path);
            MusicPlayer.Play();
        }


        /// <summary>
        /// Stops the music playing
        /// </summary>
        public void StopMusic()
        { this.MusicPlayer.Stop(); }


        /// <summary>
        /// This is the OnClick handler for the Start button</summary>
        /// <param name="sender">who clicked</param>
        /// <param name="e">Passed Params</param>
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //Checks if there is a connection to the server
            if (viewModel.VM_Connected == false)
            {
                //No connection - Show message
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("No Connection with Server", "Connection Error",
                    MessageBoxButton.OK, icon);
            }
            //This is Connection
            else
            {
                //Show Confirmation Message
                MessageBoxResult result = MessageBox.Show("Do you want to start a new game?",
                    "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    CloseWindow();
                }
                //User wants to start new Game
                else if (result == MessageBoxResult.Yes)
                {
                    if(gameInMotion == true)
                    {
                        CloseWindow();
                    }
                    //Checks if the user has entered a Game Name
                    else if (txtbMaze.Text == "")
                    { MessageBox.Show("Please enter in a Game Name");}
                    else
                    {
                        string name = txtbMaze.Text;
                        GameName = txtbMaze.Text;
                        string gen = "3 ";
                        gen += name;
                        //Shows the progress indicator
                        ProgIndicator.IsBusy = true;
                        //Disables the input box
                        txtbMaze.IsEnabled = false;
                        Task.Factory.StartNew(() =>
                        {
                            viewModel.Command(gen);
                        }
                         ).ContinueWith((task) =>
                         {
                             ProgIndicator.IsBusy = false;
                             viewModel.model.StartThread();
                             InitializeComponent();
                             //Sets the sources for the Data Binding
                             myMaze.ItemsSource = viewModel.VM_Maze;
                             plr2.ItemsSource = viewModel.VM_Player2Maze;
                             //Sets the focus on the Players Maze displayed
                             myMaze.Focus();
                             gameInMotion = true;
                             this.MazeName = viewModel.VW_MazeName;
                         }, TaskScheduler.FromCurrentSynchronizationContext()
            );
                    }
                }
            }
        }

        
        /// <summary>
        /// This is the handler for the Return button click</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?",
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {CloseWindow();}
        }


        /// <summary>
        /// This handles the suggestion button click</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Suggestion_Click(object sender, RoutedEventArgs e)
        {
            //Checks for Connection with the server
            if (viewModel.VM_Connected == false)
            {
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("No Connection with Server", "Connection Error",
                    MessageBoxButton.OK, icon);
            }
            else if(!gameInMotion)
            {
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("No Game in Progress", "Game error",
                    MessageBoxButton.OK, icon);
            }
            else
            {
                //Send Request to server to get solved maze
                Random rand = new Random();
                int solveType = rand.Next(0, 2);
                string solve = "2 " + MazeName + " " + solveType.ToString();
                viewModel.Command(solve);
            }
        }


        /// <summary>
        /// This will handle any keys pressed on the play window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (!MyGrid.IsFocused) { MyGrid.Focus(); }
            if (e.Key == Key.Up)
            {
                viewModel.MovePlayer("up", "myMove");
            }
            else if (e.Key == Key.Right)
            {
                viewModel.MovePlayer("right", "myMove");
            }
            else if(e.Key == Key.Down)
            {
                viewModel.MovePlayer("down", "myMove");
            }
            else if (e.Key == Key.Left)
            {
                viewModel.MovePlayer("left", "myMove");
            }
            Refresh();
        }



        /// <summary>
        /// This Method will refresh the data of the binding
        /// that is being displayed on the GUI</summary>
        public void Refresh()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                myMaze.Items.Refresh();
                plr2.Items.Refresh();
            }));
        }


        /// <summary>
        /// This method will be called when the 
        /// multiplayer window needs to be closed</summary>
        public void CloseWindow()
        {
            MusicPlayer.Stop();
            viewModel.model.KillThread();
            if(gameInMotion)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    this.Close();
                }));
            }
            else
            {this.Close();}
        }


        /// <summary>
        /// Event Handler that gets events from the viewModel</summary>
        /// <param name="eventData">Event Params</param>
        public void ReceiveEvent(object s, PropertyChangedEventArgs e)
        {
            //Event that the player has reached his goal point
            if (e.PropertyName.Equals("VM_Winner"))
            {
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show("You have reached the end!",
                    "You Won", MessageBoxButton.OK, icon);
                MusicPlayer.Stop();
                CloseWindow();
            }
            //The Second Player wins
            else if(e.PropertyName.Equals("VM_P2_Winner"))
            {
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show("Player 2 beat you to the end",
                    "You Lose", MessageBoxButton.OK, icon);
                MusicPlayer.Stop();
                CloseWindow();
            }
            //The second Player Moved
            else if (e.PropertyName.Equals("VM_Player_Moved") ||
                (e.PropertyName.Equals("VM_SuggestionReceived")))
            {Refresh();}
        }
    }
}