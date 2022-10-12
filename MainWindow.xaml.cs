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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Work_with_JS.Classes;
using Newtonsoft.Json;

namespace Work_with_JS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DGOrder.ItemsSource = ConnectToDB.TradeEntities.Order.ToList();
        }

        private void SaveasJS(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("input.json", string.Empty);
            foreach (Order s in ConnectToDB.TradeEntities.Order)
            {
                Order d = new Order()
                {
                    OrderDate = Convert.ToDateTime(s.OrderDate),
                    OrderDeliveryDate = Convert.ToDateTime(s.OrderDeliveryDate),
                    Code = Convert.ToInt32(s.Code),
                    OrderStatus = s.OrderStatus
                };
                File.AppendAllText("input.json", JsonConvert.SerializeObject(d));
            }
        }

        private void Search_JS(object sender, RoutedEventArgs e)
        {
            List<Order> notes = new List<Order>();
            JsonTextReader reader = new JsonTextReader(new StreamReader("input.json"));
            reader.SupportMultipleContent = true;
            while (reader.Read())
            {
                JsonSerializer serializer = new JsonSerializer();
                Order temp_point = serializer.Deserialize<Order>(reader);
                if (temp_point.OrderStatus.Contains(txtSearch.Text))
                    notes.Add(temp_point);
            }
            DGOrder.ItemsSource = notes;
            if (txtSearch.Text == String.Empty)
                DGOrder.SelectedItem = ConnectToDB.TradeEntities.Order.ToList();
        }
    }
}
