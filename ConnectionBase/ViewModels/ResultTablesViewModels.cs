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
            People = GetEntity.GetList<Person>("api/Person/all");
            Operators = GetEntity.GetList<Operator>("api/Operator/all");
            Crosses = GetEntity.GetList<Cross>("api/Cross/all");
            Buildings = GetEntity.GetList<Building>("api/Building/all");
            Rooms = GetEntity.GetList<Room>("api/Room/all");
            Devices = GetEntity.GetList<Device>("api/Device/all");
            Models = GetEntity.GetList<DeviceModel>("api/DeviceModel/all");
            Departs = GetEntity.GetList<Depart>("api/Depart/all");

            DevicePeople = GetEntity.GetList<DevicePerson>("api/DevicePerson/all");
            foreach (DevicePerson d in DevicePeople){
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
        }

        public ObservableCollection<DeviceModel> Models { get; set; }
        public ObservableCollection<Depart> Departs { get; set; }
        public ObservableCollection<Cross> Crosses { get; set; }
        public ObservableCollection<Building> Buildings { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Device> Devices { get; set; }

        
        
        public ObservableCollection<Person> People { get; set; }
        public ObservableCollection<Operator> Operators { get; set; }
        public ObservableCollection<DevicePerson> DevicePeople { get; set; }

        public string GetBox(int? cross, int? device)
        {
            string result = string.Empty;
            if (device != null) result = Models.FirstOrDefault(x => x.ModelId == Devices.FirstOrDefault(x => x.DeviceId == device).Model).ModelName;
            if (cross != null) result += Crosses.FirstOrDefault(x => x.CrossId == cross).CrossName;
            return result;
        }

        private string searchNumberIn = string.Empty;
        public string SearchNumberIn
        {
            get { return searchNumberIn; }
            set { searchNumberIn = value; OnPropertyChanged("SearchNumberIn"); }
        }

        private string searchNumberOut = string.Empty;
        public string SearchNumberOut
        {
            get { return searchNumberOut; }
            set { searchNumberOut = value; OnPropertyChanged("SearchNumberOut"); }
        }

        private ObservableCollection<NumberIn> genNumberIn;
        public ObservableCollection<NumberIn> GenNumberIn
        {
            get { return genNumberIn; }
            set { genNumberIn = value; OnPropertyChanged("GenNumberIn"); }
        }
        private NumberIn selectedNumInItem;
        public NumberIn SelectedNumInItem
        {
            get { return selectedNumInItem; }
            set
            {
                selectedNumInItem = value;
                OnPropertyChanged("SelectedNumInItem");
                TablePositionIn();
            }
        }
        private ObservableCollection<NumberOut> genNumberOut;
        public ObservableCollection<NumberOut> GenNumberOut
        {
            get { return genNumberOut; }
            set { genNumberOut = value; OnPropertyChanged("GenNumberOut"); }
        }
        private NumberOut selectedNumOutItem;
        public NumberOut SelectedNumOutItem
        {
            get { return selectedNumOutItem; }
            set
            {
                selectedNumOutItem = value;
                OnPropertyChanged("SelectedNumOutItem");
                TablePositionOut();
            }
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
        private GenerationList selectedListItem;
        public GenerationList SelectedListItem
        {
            get { return selectedListItem; }
            set {
                selectedListItem = value;
                OnPropertyChanged("SelectedListItem");
                TableChanges();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public ICommand SearchPhoneIn
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        SearchNumberIn = SearchNumberIn.Replace(" ", "");
                        if (!string.IsNullOrEmpty(SearchNumberIn))
                        {
                            GenNumberIn = GetEntity.GetList<NumberIn>("api/NumberIn/all");
                            var numinView = CollectionViewSource.GetDefaultView(GenNumberIn);
                            numinView.Filter = p => (p as NumberIn).Number_In.Contains(SearchNumberIn);
                        }
                    });
        }
        public ICommand SearchPhoneOut
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        SearchNumberOut = SearchNumberOut.Replace(" ","");
                        if (!string.IsNullOrEmpty(SearchNumberOut))
                        {
                            GenNumberOut = GetEntity.GetList<NumberOut>("api/NumberOut/all");
                            foreach (NumberOut d in GenNumberOut) d.Operators = Operators;
                            var numoutView = CollectionViewSource.GetDefaultView(GenNumberOut);
                            numoutView.Filter = p => (p as NumberOut).Number_Out.Contains(SearchNumberOut);
                        }
                    });
        }
        public ICommand FilterCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        SelectedListItem = GenList[5];
                    });
        }

        public void TableChanges()
        {
            var collectionView = CollectionViewSource.GetDefaultView(DevicePeople);
            collectionView.Filter = p => (p as DevicePerson).Device == SelectedListItem.DeviceEnd;

            GenChain = GetEntity.GetList<GenerationChains>($"api/TableGenerator/{SelectedListItem.PairEnd}");
            foreach (GenerationChains d in GenChain)
            {
                d.Buildings = Buildings; d.Rooms = Rooms;
                d.DevCross = GetBox(d.Cross, d.Device);
            }
            GenNumberIn = GetEntity.GetList<NumberIn>("api/NumberIn/all");
            var numinView = CollectionViewSource.GetDefaultView(GenNumberIn);
            numinView.Filter = p => (p as NumberIn).PairAts == SelectedListItem.PairBegin;

            GenNumberOut = GetEntity.GetList<NumberOut>("api/NumberOut/all");
            foreach (NumberOut d in GenNumberOut) d.Operators = Operators;
            var numoutView = CollectionViewSource.GetDefaultView(GenNumberOut);
            numoutView.Filter = p => (p as NumberOut).PairAts == SelectedListItem.PairBegin;
        }

        public void TablePositionIn()
        {
            if (SelectedNumInItem != null)
                SelectedListItem = GenList.FirstOrDefault(x => x.PairBegin == SelectedNumInItem.PairAts);
        }
        public void TablePositionOut()
        {
            if (SelectedNumOutItem != null)
                SelectedListItem = GenList.FirstOrDefault(x => x.PairBegin == SelectedNumOutItem.PairAts);
        }
    }
}
