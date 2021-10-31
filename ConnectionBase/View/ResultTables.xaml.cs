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
        private const string APP_PATH = "http://localhost:16802/";
        public ObservableCollection<GenerationList> getList { get; set; }
        public ObservableCollection<GenerationChains> getChain { get; set; }
        public ObservableCollection<BuildingDto> getBuildings { get; set; }
        public ObservableCollection<NumberInDto> getNumIn { get; set; }
        public ObservableCollection<NumberOutDto> getNumOut { get; set; }
        public ObservableCollection<CrossDto> getCrosses { get; set; }
        public ObservableCollection<DevicePersonDto> getDevicePeople { get; set; }
        public ObservableCollection<PersonDto> getPeople { get; set; }
        public ObservableCollection<RoomDto> getRooms { get; set; }
        private DispatcherTimer timer;


        public static ObservableCollection<T> GetList<T>(string path)
        {
            ObservableCollection<T> list = new();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(APP_PATH);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponse = client.GetAsync(APP_PATH + path).Result;    
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = httpResponse.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
            }
            return list;
        }

        public ResulTables()
        {
            InitializeComponent();

            DataContext = new ResultTablesViewModels();

            var path = "api/Building/all";
            getBuildings = GetList<BuildingDto>(path);
            cbBuilding.ItemsSource = getBuildings;
            cbBuilding.DisplayMemberPath = "BuildingName";
            path = "api/Cross/all";
            getCrosses = GetList<CrossDto>(path);
            cbCrosses.ItemsSource = getCrosses;
            cbCrosses.DisplayMemberPath = "CrossName";
            path = "api/Room/all";
            getRooms = GetList<RoomDto>(path);
            cbRooms.ItemsSource = getRooms;
            cbRooms.DisplayMemberPath = "RoomName";
            /*path = "api/Person/all";
            getPeople = GetList<PersonDto>(path);*/

            //DataGridTextColumn TempColumn;
            //TempColumn = new DataGridTextColumn();
            //TempColumn.Header = "DisplayName0";
            //TempColumn.Binding = new Binding("Person");
            //listPeople.Columns.Add(TempColumn);
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

            /*var path = "api/TableGenerator/list";
            getList = GetList<GenerationList>(path);
            listGrid.ItemsSource = getList;
            if (getList.Count > 0)
            {
                path = $"api/TableGenerator/{getList[0].PairEnd}";
                getChain = GetList<GenerationChains>(path);
                listChain.ItemsSource = getChain;

                if (getList[0].DeviceEnd != null)
                {
                    path = $"api/DevicePerson/device/{getList[0].DeviceEnd}";
                    getDevicePeople = GetList<DevicePersonDto>(path);
                    listPeople.ItemsSource = getDevicePeople;
                }

                if (getList[0].CrossBegin != null)
                {
                    path = $"api/NumberIn/pair/{getList[0].PairBegin}";
                    getNumIn = GetList<NumberInDto>(path);
                    listNumIn.ItemsSource = getNumIn;

                    path = $"api/NumberOut/pair/{getList[0].PairBegin}";
                    getNumOut = GetList<NumberOutDto>(path);
                    listNumOut.ItemsSource = getNumOut;
                }*/

                /*path = "api/TableGenerator/list";
                getList = GetList<GenerationList>(path);
                listGrid.ItemsSource = getList;
                path = "api/TableGenerator/list";
                getList = GetList<GenerationList>(path);
                listGrid.ItemsSource = getList;*/
            //}
            // код здесь

        }


        /*private void listNumIn_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.StartsWith("Number_In")) e.Column.Header = "Номер вн.";
            if (e.PropertyName.StartsWith("NumberId")) e.Column.MaxWidth = 0;
            if (e.PropertyName.StartsWith("PairAts")) e.Column.MaxWidth = 0;
        }*/
    }
}
