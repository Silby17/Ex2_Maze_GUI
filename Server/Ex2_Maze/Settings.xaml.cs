using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Ex2_Maze
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private string IP;
        private string PORT;

        public Settings()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ReadFile();
            InitializeComponent();
        }


        public void ReadFile()
        {
            string direc = Directory.GetCurrentDirectory();
            direc += "\\connectionInfo.txt";
            
            string[] lines = System.IO.File.ReadAllLines(direc);
            this.IP = lines[0];
            this.PORT = lines[1];
        }
    }
}
