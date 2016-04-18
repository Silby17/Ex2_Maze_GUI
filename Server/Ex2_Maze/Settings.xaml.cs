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
        private string sIP;
        private string sPORT;

        public Settings()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ReadDefaultSettings();
            InitializeComponent();
        }


        public void ReadDefaultSettings()
        {
            this.sPORT =(System.Configuration.ConfigurationManager.AppSettings["Port"]);
            this.sIP = (System.Configuration.ConfigurationManager.AppSettings["IP"]);
        }
    }
}
