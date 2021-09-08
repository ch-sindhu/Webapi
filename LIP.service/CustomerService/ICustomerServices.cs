using LA.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIP.service.CustomerService
{
   public interface ICustomerServices
    {
        List<CustomerModel> GetCustomerList();
    }
}
