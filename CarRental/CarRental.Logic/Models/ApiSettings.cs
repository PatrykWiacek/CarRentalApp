using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Logic.Models;
public class ApiSettings
{
    public string CarApiEndpoint { get; set; }
    public string RentalApiEndpoint { get; set; }
    public string CustomerApiEndpoint { get; set; }
}
