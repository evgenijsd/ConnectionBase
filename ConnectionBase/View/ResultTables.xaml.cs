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
    /// Логика взаимодействия для ResulTables.xaml
    /// </summary>
    public partial class ResulTables : Window
    {
        private DispatcherTimer timer;
        private object ItemSelected;

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

        private void listGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listGrid != null && listGrid.SelectedItem != null && ItemSelected != listGrid.SelectedItem)
            {
                listGrid.ScrollIntoView(listGrid.SelectedItem);
                ItemSelected = listGrid.SelectedItem;
            }
        }
    }
}
