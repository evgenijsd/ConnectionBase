using ConnectionBase.Model;
using ConnectionBase.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace ConnectionBase.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class ResulTables : Window
    {
        private DispatcherTimer timer;

        public ResulTables()
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

            DataContext = new ResultTablesViewModels();
        }
    }
}
