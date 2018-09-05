using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Wpf.BO;
using Wpf.Service;

namespace Wpf.Master.Company
{
    public  class CompanyViewModel:BaseViewModel
    {
        private Wpf.BO.Company editCompany=null;

        public CompanyViewModel()
        {
            SaveCommand = new RelayCommand(Save);
            ClearCommand = new RelayCommand(Clear);
            Countries = new CountryService().GetCountries().ToList();
            CompanyStatuses = Enum.GetValues(typeof(CompanyStatus)).Cast<CompanyStatus>().ToList();
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(); }
        }

        private IEnumerable<BO.Country> _Countries;

        public IEnumerable<BO.Country> Countries
        {
            get { return _Countries; }
            set { _Countries = value; NotifyPropertyChanged(); }
        }

        private BO.Country _Country;

        public BO.Country Country
        {
            get { return _Country; }
            set { _Country = value; NotifyPropertyChanged(); }
        }

        public IEnumerable<BO.Company> Companies { get; set; }

        private IEnumerable<CompanyStatus> _CompanyStatuses;

        public IEnumerable<CompanyStatus> CompanyStatuses
        {
            get { return _CompanyStatuses; }
            set { _CompanyStatuses = value; NotifyPropertyChanged(); }
        }

        private CompanyStatus? _CompanyStatus;

        public CompanyStatus? CompanyStatus
        {
            get { return _CompanyStatus; }
            set { _CompanyStatus = value; NotifyPropertyChanged(); }
        }

        public ICommand SaveCommand { get; set; }

        public void Save()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                this.SetFocus(() => Name, "Name is required");
                return;
            }

            if (Country==null)
            {
                this.SetFocus(() => Countries, "Country is required");
                return;
            }

            if ( CompanyStatus== null)
            {
                this.SetFocus(() => CompanyStatuses, "Status is required");
                return;
            }


        }

        public ICommand ClearCommand { get; set; }

        public void Clear()
        {

        }

    }
}
