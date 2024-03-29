﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConnectionBase.Model
{
    public partial class DevicePerson
    {
        private int devicePersonId;
        private int device;
        private int? person;
        private int? depart;

        public int DevicePersonId { get => devicePersonId; set { devicePersonId = value; OnPropertyChanged("DevicePersonId"); } }
        public int Device { get => device; set { device = value; OnPropertyChanged("Device"); } }
        public int? Person { get => person; set { person = value; OnPropertyChanged("Person"); } }
        public int? Depart { get => depart; set { depart = value; OnPropertyChanged("Depart"); } }
        public ObservableCollection<Person> People { get; set; }
        public ObservableCollection<Depart> Departs { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
