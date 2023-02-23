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

namespace ConnectionBase.View
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
        }


        private void ButtonBuilding_Click(object sender, RoutedEventArgs e)
        {
            EditBuilding editBuilding = new();
            editBuilding.Owner = this;
            this.Hide();
            editBuilding.ShowDialog();
            this.Close();
        }

        private void ButtonRoom_Click(object sender, RoutedEventArgs e)
        {
            EditRoom editRoom = new();
            editRoom.Owner = this;
            this.Hide();
            editRoom.ShowDialog();
            this.Close();
        }

        private void ButtonDevice_Click(object sender, RoutedEventArgs e)
        {
            EditDevice editDevice = new();
            editDevice.Owner = this;
            this.Hide();
            editDevice.ShowDialog();
            this.Close();
        }

        private void ButtonModel_Click(object sender, RoutedEventArgs e)
        {
            EditModel editModel = new();
            editModel.Owner = this;
            this.Hide();
            editModel.ShowDialog();
            this.Close();
        }

        private void ButtonDepart_Click(object sender, RoutedEventArgs e)
        {
            EditDepart editDepart = new();
            editDepart.Owner = this;
            this.Hide();
            editDepart.ShowDialog();
            this.Close();
        }

        private void ButtonPerson_Click(object sender, RoutedEventArgs e)
        {
            EditPerson editPerson = new();
            editPerson.Owner = this;
            this.Hide();
            editPerson.ShowDialog();
            this.Close();
        }

        private void ButtonCross_Click(object sender, RoutedEventArgs e)
        {
            EditCross editCross = new();
            editCross.Owner = this;
            this.Hide();
            editCross.ShowDialog();
            this.Close();
        }

        private void ButtonNumberIn_Click(object sender, RoutedEventArgs e)
        {
            EditNumberIn editNumberIn = new();
            editNumberIn.Owner = this;
            this.Hide();
            editNumberIn.ShowDialog();
            this.Close();
        }

        private void ButtonNumberOut_Click(object sender, RoutedEventArgs e)
        {
            EditNumberOut editNumberOut = new();
            editNumberOut.Owner = this;
            this.Hide();
            editNumberOut.ShowDialog();
            this.Close();
        }

        private void ButtonOperator_Click(object sender, RoutedEventArgs e)
        {
            EditOperator editOperator = new();
            editOperator.Owner = this;
            this.Hide();
            editOperator.ShowDialog();
            this.Close();
        }
    }
}
