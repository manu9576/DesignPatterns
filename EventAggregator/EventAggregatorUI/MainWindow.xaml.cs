using System.Windows;
using System.Windows.Controls;
using EventAggregator;

namespace EventAggregatorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IEventAggregator ea;

        public MainWindow()
        {           

            InitializeComponent();

            ea = EventAggregator.EventAggregator.Instance;

            ItemListView.EventAggregator = this.ea;

            var tabs = this.ItemView.Items;

            tabs.Add(new TabItem() { Header = "Item Header", Content = new ItemView(this.ea) });
            tabs.Add(new TabItem() { Header = "Item Details", Content = new ItemDetailsView(this.ea) });            

        }   

    }
}
