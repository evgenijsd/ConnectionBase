using ConnectionBase.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace ConnectionBase.ViewModels
{
    public class ResultTablesViewModels : INotifyPropertyChanged
    {
        private PersonDto selectedPerson;
        private DevicePersonDto selectedDevicePerson;
        private GenerationList selectedListItem;

        public ResultTablesViewModels()
        {
            People = GetEntity.GetList<PersonDto>("api/Person/all");
            DevicePeople = GetEntity.GetList<DevicePersonDto>("api/DevicePerson/all");
            foreach (DevicePersonDto d in DevicePeople) d.People = People;
            GenList = GetEntity.GetList<GenerationList>("api/TableGenerator/list");
            var gen = CollectionViewSource.GetDefaultView(GenList);
            gen.Filter = p => (p as GenerationList).DeviceEnd == 4;
            var collectionView = CollectionViewSource.GetDefaultView(DevicePeople);
            collectionView.Filter = p => (p as DevicePersonDto).Device == GenList[5].DeviceEnd;
        }



        public ObservableCollection<GenerationList> GenList { get; set; }
        public GenerationList SelectedListItem
        {
            get { return selectedListItem; }
            set
            {
                selectedListItem = value;
                OnPropertyChanged("SelectedListItem");
            }
        }
        public ObservableCollection<PersonDto> People { get; set; }
        public PersonDto SelectedPerson
        {
            get { return selectedPerson; }
            set { selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }
        public ObservableCollection<DevicePersonDto> DevicePeople { get; set; }
        public DevicePersonDto SelectedDevicePerson
        {
            get { return selectedDevicePerson; }
            set
            {
                selectedDevicePerson = value;
                OnPropertyChanged("SelectedDevicePerson");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
