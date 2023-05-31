using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;
public static class CustomerModel
{
    public class Listing
    {
        public string FullName { get; set; }
        public int Id { get; set; }
    }
    public class DetailedView
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
    }
}
