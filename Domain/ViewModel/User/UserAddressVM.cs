using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.User
{
    public class UserAddressVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public string Mobile { get; set; }
        
        public string Phone { get; set; }

        public int ProvinceId { get; set; }

        public string ProvinceName { get; set; }

        public int CityId { get; set; }

        public string  CityName { get; set; }

        public string LeftAddress { get; set; }

        public string PostalCode { get; set; }

        public int? UserId { get; set; }
       
    }
}
