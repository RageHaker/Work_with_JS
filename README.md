# Work_with_JS

## Creat
> 1. First, to create a database, you need to create a new model ADO.NET ADM. Secondly, select the server and choose which database you will use. And finally, we need to create a Connect Helper class to use the data in the program.
> 2. Second we add code to MainWindow.xaml and MainWindow.xaml
> * Xaml code:
   ```XAML
   <Window x:Class="Work_with_JS.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:Work_with_JS"
        mc:Ignorable="d"
        Title="Work with JSON" Height="600" Width="800">
    <Grid>
        <StackPanel Orientation="Vertical">
            <StackPanel Height="100" VerticalAlignment="Center" HorizontalAlignment="Center" Orientation="Horizontal">
                <StackPanel VerticalAlignment="Center" HorizontalAlignment="Center" Orientation="Horizontal">
                    <Button Width="120" Height="40" Click="SaveasJS">Save as .json</Button>
                    <TextBox Width="200" x:Name="txtSearch" Height="50" FontSize="30" Margin="15"></TextBox>
                    <Button Width="120" Height="40" Click="Search_JS">Search</Button>
                </StackPanel>
            </StackPanel>
            <StackPanel>
                <Grid>
                    <Grid.RowDefinitions>
                        <RowDefinition Height="1*"/>
                        <RowDefinition Height="4*"/>
                        <RowDefinition Height="1*"/>
                    </Grid.RowDefinitions>
                    <DataGrid Grid.Row="1" x:Name="DGOrder" AutoGenerateColumns="False">
                        <DataGrid.Columns>
                            <DataGridTextColumn Width="200" Header="Дата заказа" Binding="{Binding OrderDate}"/>
                            <DataGridTextColumn Width="200" Header="Дата доставки заказа" Binding="{Binding OrderDeliveryDate}"/>
                            <DataGridTextColumn Width="200" Header="Код" Binding="{Binding Code}"/>
                            <DataGridTextColumn Width="200" Header="Статус" Binding="{Binding OrderStatus}"/>
                        </DataGrid.Columns>
                    </DataGrid>
                </Grid>
            </StackPanel>
        </StackPanel>
    </Grid>
</Window>
   ```
> C# code
  ```c#
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
  ```
> Create ne catalog and create ConnecToDB
> Code ConnectToDB
  ```C#
  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Work_with_JS.Classes
{
    internal class ConnectToDB
    {
        public static TradeEntities TradeEntities = new TradeEntities();
    }
}

  ```
