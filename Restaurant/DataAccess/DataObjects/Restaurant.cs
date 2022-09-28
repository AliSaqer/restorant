using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.DataObjects;

namespace Restaurant.DataAccess.DataObjects
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Restaurantmenus = new HashSet<Restaurantmenu>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public sbyte Archived { get; set; }

        public virtual ICollection<Restaurantmenu> Restaurantmenus { get; set; }
    }
}
