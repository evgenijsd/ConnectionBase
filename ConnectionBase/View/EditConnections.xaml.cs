﻿using ConnectionBase.ViewModels;
using System;
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
using System.Windows.Threading;

namespace ConnectionBase.View
{
    /// <summary>
    /// Логика взаимодействия для EditConnections.xaml
    /// </summary>
    public partial class EditConnections : Window
    {
        private DispatcherTimer timer;

        public EditConnections()
        {
            InitializeComponent();

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();

            timer.Tick += new EventHandler(timer_Tick);

            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)

        {
            timer.Stop();

            DataContext = new EditConnectiosViewModel();
        }
    }
}
