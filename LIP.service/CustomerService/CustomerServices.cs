using LA.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LIP.service.CustomerService
{
    public class CustomerServices : ICustomerServices
    {
        public CustomerServices()
        {
        }

        public List<CustomerModel> GetCustomerList()
        {
            var customerList = new List<CustomerModel>();
            using(var client= new HttpClient())
            {
                client.BaseAddress = new Uri("");
            }
            return customerList;
        }

        
       
    }
}
