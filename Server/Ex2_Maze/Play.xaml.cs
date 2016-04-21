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
using System.ComponentModel;
using Ex1_Maze;

namespace Ex2_Maze
{
    /// <summary>
    /// Interaction logic for Play.xaml
    /// </summary>
    public partial class Play : Window, INotifyPropertyChanged
    {
        private string gen = "1 Maze_ ";
        private Random rand;
        MainWindow mainWin;
        public string _Image { get; set; }

        private void ShowImage(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                this.Title = "You pressed dowm!";
                //change the gui that the man is in one cube down
            }

            if (e.Key == Key.Right)
            {
                this.Title = "You pressed right!";
                //change the gui that the man is in one cube right
            }

            if (e.Key == Key.Up)
            {
                this.Title = "You pressed up!";
                //change the gui that the man is in one cube up
            }

            if (e.Key == Key.Left)
            {
                this.Title = "You pressed left!";
                //change the gui that the man is in one cube left
            }
        }

        public string Image
        {
            get
            {
                return _Image;
            }

            set
            {
                _Image = value;
                NotifyPropertyChanged("Image");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public Play(ViewModel vm)
        {
            this.DataContext = vm;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            
            //SoundPlayer MusicPlayer = new System.Media.SoundPlayer(@"C:\Users\Nava\Source\Repos\Ex2_Maze_GUI\Server\Ex2_Maze\sovtoda.wav");
            //MusicPlayer.Play();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to start a new game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                //close window 
            }
            //TODO to reload the window
            else if(result == MessageBoxResult.Yes)
            {
                this.rand = new Random();
                string num = (rand.Next(0, 11)).ToString();
                gen += num += " ";
                int type = rand.Next(0, 2);
                gen += type.ToString();
                this.mainWin.vm.Command(gen);
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

        }
    }
   
}
