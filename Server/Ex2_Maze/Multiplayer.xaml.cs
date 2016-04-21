﻿using System;
using System.Collections.Generic;
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
    /// Interaction logic for Multiplayer.xaml
    /// </summary>
    public partial class Multiplayer : Window
    {
        private string gen = "1 Maze_ ";
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
                MessageBox.Show("No Connection with Server", "Connection Error", MessageBoxButton.OK, icon);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Do you want to start a new game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    //close window 
                }
                //TODO to reload the window
                else if (result == MessageBoxResult.Yes)
                {
                    this.rand = new Random();
                    string num = (rand.Next(0, 11)).ToString();
                    gen += num += " ";
                    int type = rand.Next(0, 2);
                    gen += type.ToString();
                    viewModel.Command(gen);
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
