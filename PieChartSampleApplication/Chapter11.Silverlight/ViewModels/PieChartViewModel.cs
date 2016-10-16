using System;
using System.Collections.Generic;
using System.Windows;
using Chapter11.Silverlight.Models;
using Chapter11.Silverlight.Services;

namespace Chapter11.Silverlight.ViewModels
{
    public class PieChartViewModel : BaseViewModel
    {
        public List<PieChartFileType> FileTypes
        {
            get
            {
                return _fileTypes ?? new List<PieChartFileType>();
            }
            private set
            {
                _fileTypes = value;
                NotifyPropertyChanged("FileTypes");
            }
        }
        List<PieChartFileType> _fileTypes;

        public string LegendTitle
        {
            get
            {
                return "File Types";
            }
        }

        public string Title
        {
            get
            {
                string title = App.Current.Resources["ChartTitle"] as string;
                return (!String.IsNullOrEmpty(title)) ? title : "File Breakdown";
            }
        }

        public void Load()
        {
            PieChartDataService service = new PieChartDataService();

            service.GetData(
                (fileTypes) =>
                    {
                        this.FileTypes = fileTypes;
                    },
                (error) =>
                    {
                        MessageBox.Show("Error: " + error);
                    }
            );
        }
    }
}
