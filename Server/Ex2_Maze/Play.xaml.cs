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

namespace Ex2_Maze
{
    /// <summary>
    /// Interaction logic for Play.xaml
    /// </summary>
    public partial class Play : Window
    {
        private string gen = "1 Maze ";
        private Random rand;

        public Play(MainWindow mw)
        {
            this.rand = new Random();
            int type = rand.Next(0, 2);
            gen += type.ToString();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            mw.vm.Command(gen);
            SoundPlayer MusicPlayer = new System.Media.SoundPlayer(@"C:\Users\Nava\Source\Repos\Ex2_Maze_GUI\Server\Ex2_Maze\sovtoda.wav");
            MusicPlayer.Play();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void Suggestion_Click(object sender, RoutedEventArgs e)
        {

        }
    }
   
}
