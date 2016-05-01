using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
             

        /// <summary>
        /// Constructor Method that receives the ViewModel</summary>
        /// <param name="vm">The ViewModel of the program</param>
        public Multiplayer(ViewModel vm)
        {
            this.viewModel = vm;
            this.DataContext = vm;
            this.viewModel.PropertyChanged += Event;
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
            //Checks if there is a connection to the server
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
                    CloseWindow();
                }
                //TODO to reload the window
                else if (result == MessageBoxResult.Yes)
                {
                    if(gameInMotion == true)
                    {
                        CloseWindow();
                    }
                    else if (txtbMaze.Text == "")
                    {
                        MessageBox.Show("Please enter in a Game Name");
                    }
                    else
                    {
                        string mazeName = txtbMaze.Text;
                        string gen = "3 ";
                        gen += mazeName;
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
                             myMaze.ItemsSource = viewModel.VM_MyMaze;
                             plr2.ItemsSource = viewModel.VM_Player2Maze;
                             this.MazeName = viewModel.model.myGenMaze
                             gameInMotion = true;
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
            {
                CloseWindow();
            }
        }


        /// <summary>
        /// This handles the suggestion button click</summary>
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


        public void Event(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("VM_Player_Moved"))
            {
                Refresh();
            }
        }

        public void Refresh()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                myMaze.Items.Refresh();
                plr2.Items.Refresh();
            }));
        }


        public void CloseWindow()
        {
            viewModel.model.KillThread();
            this.Close();
        }

        /// <summary>
        /// Event Handler that gets events from the viewModel</summary>
        /// <param name="eventData">Event Params</param>
        public void ReceiveEvent(string eventData)
        {
            //Event that the player has reached his goal point
            if (eventData == "VM_Winner")
            {
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show("You have reached the end!", "You Won", MessageBoxButton.OK, icon);
                this.Close();
            }
        }
    }
}