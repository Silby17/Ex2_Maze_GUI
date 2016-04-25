using System;   
using System.Windows;


namespace Ex2_Maze
{
    /// <summary>
    /// Interaction logic for Multiplayer.xaml
    /// </summary>
    public partial class Multiplayer : Window
    {
        private Random rand;
        ViewModel viewModel;

        public Multiplayer(ViewModel vm)
        {
            this.viewModel = vm;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            //SoundPlayer MusicPlayer = new System.Media.SoundPlayer(@"C:\Users\Nava\Source\Repos\Ex2_Maze_GUI\Server\Ex2_Maze\sovtoda.wav");
            //MusicPlayer.Play();

        }
       
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
                    if(txtbMaze.Text == "")
                    {
                        MessageBox.Show("Please enter in a Game Name");
                    }
                    else
                    {
                        string mazeName = txtbMaze.Text;
                        string gen = "3 ";
                        gen += mazeName;
                        viewModel.Command(gen);
                        InitializeComponent();
                        myMaze.ItemsSource = viewModel.VM_MyMaze;
                        plr2.ItemsSource = viewModel.VM_Player2Maze;
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


    }
}
