using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.Admin
{
    public class SalesAgencyViewModel
    {
        public SalesAgencyViewModel()
        {
            Provinces = new List<Province>();
        }
        public int? Id { get; set; }
        public string Address { get; set; }
        public string ManagerName { get; set; }
        public string Telephone { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string ProvinceName { get; set; }
        public int LanguageId { get; set; }
        public int? ProvinceId { get; set; }
        public ICollection<Province> Provinces { get; set; }
    }
}
