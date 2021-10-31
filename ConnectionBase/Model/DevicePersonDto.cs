using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConnectionBase.Model
{
    public partial class DevicePersonDto
    {
        private int devicePersonId;
        private int device;
        private int? person;

        public int DevicePersonId { get => devicePersonId; set { devicePersonId = value; OnPropertyChanged("DevicePersonId"); } }
        public int Device { get => device; set { device = value; OnPropertyChanged("Device"); } }
        public int? Person { get => person; set { person = value; OnPropertyChanged("Person"); } }
        public ObservableCollection<PersonDto> People { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
