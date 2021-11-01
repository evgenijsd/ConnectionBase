using ConnectionBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ConnectionBase.ViewModels
{
    public class SelectedCommand : ICommand
    {
        protected GenerationList _gen;
        public SelectedCommand(GenerationList gen)
        {
            _gen = gen;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MessageBox.Show(_gen.DeviceEnd.ToString());
        }
    }
}
