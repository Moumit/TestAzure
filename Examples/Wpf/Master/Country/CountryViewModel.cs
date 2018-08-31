using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Wpf.BO;
using Wpf.Service;

namespace Wpf.Master.Country
{
    public  class CountryViewModel : BaseViewModel
    {
        private Wpf.BO.Country editCountry=null;

        public CountryViewModel()
        {
            SaveCommand = new RelayCommand(Save);
            ClearCommand = new RelayCommand(Clear);
           
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(); }
        }

      

        public IEnumerable<BO.Country> Countries { get; set; }

       

        public ICommand SaveCommand { get; set; }

        public void Save()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                this.SetFocus(() => Name, "Name is required");
                return;
            }

            BO.Country country = editCountry ?? new BO.Country();
            if (editCountry==null)
            {
                country.CreatedBy = "System";
                country.CreatedDate = DateTime.Now;
            }

        }

        public ICommand ClearCommand { get; set; }

        public void Clear()
        {

        }

    }
}
