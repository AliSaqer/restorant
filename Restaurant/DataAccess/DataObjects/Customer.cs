using System;
using System.Collections.Generic;

namespace Restaurant.DataAccess.DataObjects
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerRestaurantmenus = new HashSet<CustomerRestaurantmenu>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public sbyte Archived { get; set; }

        public virtual ICollection<CustomerRestaurantmenu> CustomerRestaurantmenus { get; set; }
    }
}
