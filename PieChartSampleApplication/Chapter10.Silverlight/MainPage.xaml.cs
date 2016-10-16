using System;
using System.Windows;
using System.Windows.Controls;
using Chapter10.Silverlight.ViewModels;

namespace Chapter10.Silverlight
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            PieChartViewModel data = new PieChartViewModel();
            this.DataContext = data;
            data.Load();
        }
    }
}
