using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Data;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Wpf.CartesianChart;
using Wpf.CartesianChart.BasicLine;
using Wpf.CartesianChart.Basic_Bars;
using Wpf.CartesianChart.Basic_Stacked_Bar;
using Wpf.CartesianChart.Bubbles;
using Wpf.CartesianChart.Chart_to_Image;
using Wpf.CartesianChart.ConstantChanges;
using Wpf.CartesianChart.Customized_Line_Series;
using Wpf.CartesianChart.CustomTooltipAndLegend;
using Wpf.CartesianChart.DataLabelTemplate;
using Wpf.CartesianChart.DateAxis;
using Wpf.CartesianChart.DynamicVisibility;
using Wpf.CartesianChart.Energy_Predictions;
using Wpf.CartesianChart.Events;
using Wpf.CartesianChart.Financial;
using Wpf.CartesianChart.FullyResponsive;
using Wpf.CartesianChart.Funnel_Chart;
using Wpf.CartesianChart.GanttChart;
using Wpf.CartesianChart.HeatChart;
using Wpf.CartesianChart.Inverted_Series;
using Wpf.CartesianChart.Irregular_Intervals;
using Wpf.CartesianChart.Linq;
using Wpf.CartesianChart.LogarithmScale;
using Wpf.CartesianChart.ManualZAndP;
using Wpf.CartesianChart.MaterialCards;
using Wpf.CartesianChart.Missing_Line_Points;
using Wpf.CartesianChart.NegativeStackedRow;
using Wpf.CartesianChart.PointState;
using Wpf.CartesianChart.ScatterPlot;
using Wpf.CartesianChart.Scatter_With_Pies;
using Wpf.CartesianChart.Sections;
using Wpf.CartesianChart.SectionsDragable;
using Wpf.CartesianChart.SectionsMouseMove;
using Wpf.CartesianChart.SolidColorChart;
using Wpf.CartesianChart.StackedArea;
using Wpf.CartesianChart.StepLine;
using Wpf.CartesianChart.ThreadSafe;
using Wpf.CartesianChart.UIElements;
using Wpf.CartesianChart.WindowAxis;
using Wpf.CartesianChart.ZoomingAndPanning;
using Wpf.Gauges;
using Wpf.Maps;
using Wpf.PieChart;
using Wpf.PieChart.DropDowns;

namespace Wpf.Home
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private UserControl _content;
        private bool _isMenuOpen;
        private string _criteria;
        private IEnumerable<SampleGroupVm> _samples;
        private readonly IEnumerable<SampleGroupVm> _dataSource;

        public HomeViewModel()
        {
            IsMenuOpen = true;
            _dataSource = new[]
            {
                new SampleGroupVm
                {
                    Name = "Master",
                    Items = new[]
                    {
                         new SampleVm("Company",typeof(Wpf.Master.Company.ucCompany))
                    }
                },
                new SampleGroupVm
                {
                    Name = "Transaction",
                    Items = new[]
                    {
                        new SampleVm("Lines", typeof(BasicLineExample)),
                        
                    }
                },
                new SampleGroupVm
                {
                    Name = "Graph",
                    Items = new[]
                    {
                       
                        new SampleVm("Chart to Image", typeof(ChartToImageSample)),
                        new SampleVm("DataLabelTemplate", typeof(DataLabelTemplateSample))
                    }
                },
                
            };

            _samples = _dataSource;
        }

        public IEnumerable<SampleGroupVm> Samples
        {
            get { return _samples; }
            set
            {
                _samples = value;
                OnPropertyChanged("Samples");
            }
        }
        public UserControl Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }
        }
        public bool IsMenuOpen
        {
            get { return _isMenuOpen; }
            set
            {
                _isMenuOpen = value;
                OnPropertyChanged("IsMenuOpen");
            }
        }
        public string Criteria
        {
            get { return _criteria; }
            set
            {
                _criteria = value;
                OnPropertyChanged("Criteria");
                OnCriteriaChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnCriteriaChanged()
        {
            if (string.IsNullOrWhiteSpace(Criteria))
            {
                Samples = _dataSource;
                return;
            }

            Samples = Samples.Select(x => new SampleGroupVm
            {
                Name = x.Name,
                Items = x.Items.Where(y => y.Title.ToLowerInvariant().Contains(Criteria.ToLowerInvariant()) ||
                                           y.Tags.ToLowerInvariant().Contains(Criteria.ToLowerInvariant()))
            });
        }
    }

    public class IsNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
