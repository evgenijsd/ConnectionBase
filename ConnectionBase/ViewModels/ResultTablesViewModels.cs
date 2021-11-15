using ConnectionBase.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ConnectionBase.ViewModels
{
    public class ResultTablesViewModels : INotifyPropertyChanged
    {
        

        public ResultTablesViewModels()
        {
            People = GetEntity.GetList<PersonDto>("api/Person/all");
            Operators = GetEntity.GetList<OperatorDto>("api/Operator/all");
            Crosses = GetEntity.GetList<CrossDto>("api/Cross/all");
            Buildings = GetEntity.GetList<BuildingDto>("api/Building/all");
            Rooms = GetEntity.GetList<RoomDto>("api/Room/all");
            Devices = GetEntity.GetList<DeviceDto>("api/Device/all");
            Models = GetEntity.GetList<DeviceModelDto>("api/DeviceModel/all");
            Departs = GetEntity.GetList<DepartDto>("api/Depart/all");

            DevicePeople = GetEntity.GetList<DevicePersonDto>("api/DevicePerson/all");
            foreach (DevicePersonDto d in DevicePeople){
                d.People = People; d.Departs = Departs;
                if (d.Person != null) d.Depart = People.FirstOrDefault(x => x.PersonId == d.Person).Depart;
            }

            GenList = GetEntity.GetList<GenerationList>("api/TableGenerator/list");
            //var gen = CollectionViewSource.GetDefaultView(GenList);
            //gen.Filter = p => (p as GenerationList).DeviceEnd == 4;
            foreach (GenerationList d in GenList){
                d.Buildings = Buildings; d.Rooms = Rooms;
                d.DevCrossBegin = GetBox(d.CrossBegin, d.DeviceBegin);
                d.DevCrossEnd = GetBox(d.CrossEnd, d.DeviceEnd);
            }
            SelectedListItem = GenList[0];
            var collectionView = CollectionViewSource.GetDefaultView(DevicePeople);
            collectionView.Filter = p => (p as DevicePersonDto).Device == GenList[0].DeviceEnd;

            GenChain = GetEntity.GetList<GenerationChains>($"api/TableGenerator/{GenList[0].PairEnd}");
            foreach (GenerationChains d in GenChain) {
                d.Buildings = Buildings; d.Rooms = Rooms;
                d.DevCross = GetBox(d.Cross, d.Device);
             }
            GenNumberIn = GetEntity.GetList<NumberInDto>("api/NumberIn/all");
            var numinView = CollectionViewSource.GetDefaultView(GenNumberIn);
            numinView.Filter = p => (p as NumberInDto).PairAts == GenList[0].PairBegin;

            GenNumberOut = GetEntity.GetList<NumberOutDto>("api/NumberOut/all");
            foreach (NumberOutDto d in GenNumberOut) d.Operators = Operators;
            var numoutView = CollectionViewSource.GetDefaultView(GenNumberOut);
            numoutView.Filter = p => (p as NumberOutDto).PairAts == GenList[0].PairBegin;

        }

        public ObservableCollection<DeviceModelDto> Models { get; set; }
        public ObservableCollection<DepartDto> Departs { get; set; }
        public ObservableCollection<CrossDto> Crosses { get; set; }
        public ObservableCollection<BuildingDto> Buildings { get; set; }
        public ObservableCollection<RoomDto> Rooms { get; set; }
        public ObservableCollection<DeviceDto> Devices { get; set; }
        public ObservableCollection<NumberInDto> GenNumberIn { get; set; }
        public ObservableCollection<NumberOutDto> GenNumberOut { get; set; }
        
        
        public ObservableCollection<PersonDto> People { get; set; }
        public ObservableCollection<OperatorDto> Operators { get; set; }
        public ObservableCollection<DevicePersonDto> DevicePeople { get; set; }

        public string GetBox(int? cross, int? device)
        {
            string result = string.Empty;
            if (device != null) result = Models.FirstOrDefault(x => x.ModelId == Devices.FirstOrDefault(x => x.DeviceId == device).Model).ModelName;
            if (cross != null) result += Crosses.FirstOrDefault(x => x.CrossId == cross).CrossName;
            return result;
        }

        private ObservableCollection<GenerationChains> genChain;
        public ObservableCollection<GenerationChains> GenChain
        {
            get { return genChain; }
            set { genChain = value; OnPropertyChanged("GenChain"); }
        }
        private ObservableCollection<GenerationList> genList;
        public ObservableCollection<GenerationList> GenList
        {
            get { return genList; }
            set { genList = value; OnPropertyChanged("GenList"); }
        }
        private GenerationList selectedListItem = new GenerationList();
        public GenerationList SelectedListItem
        {
            get { return selectedListItem; }
            set {
                selectedListItem = value;
                OnPropertyChanged("SelectedListItem");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public ICommand SearchPhone
        {
            get
            {
                return new RelayCommand(
                    parameter =>
                    {
                        MessageBox.Show($"11");
                        /*SelectedNames.Clear();
                        foreach (var item in AvailableNames)
                        {
                            SelectedNames.Add(item);
                        }*/
                    });
            }
        }

        void ItemSelectedExecute()
        {
            //++count;
            //UpdateLabel = string.Format("({0})+({1})", count, updateTextBox);
            MessageBox.Show($"11");
            MessageBox.Show($"{SelectedListItem.PairBegin}!");
            
        }

        bool CanItemSelectedExecute()
        {
            return true;
        }

        public ICommand ItemSelected //ICommand name must be the same as the Button binding name in XAML 
        {                               //<Button Content="Button" Command="{Binding UpdateLabelName}"/>
            get { return new RelayCommand_2(ItemSelectedExecute, CanItemSelectedExecute); }
        }                               
    }
}
