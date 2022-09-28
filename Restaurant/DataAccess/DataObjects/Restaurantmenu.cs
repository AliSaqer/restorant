using System;
using System.Collections.Generic;

namespace Restaurant.DataAccess.DataObjects
{
    public partial class Restaurantmenu
    {
        public Restaurantmenu()
        {
            CustomerRestaurantmenus = new HashSet<CustomerRestaurantmenu>();
        }

        public int Id { get; set; }
        public string MealName { get; set; } = null!;
        public float PriceInNis { get; set; }
        public float PriceInUsd { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public sbyte Archived { get; set; }
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual ICollection<CustomerRestaurantmenu> CustomerRestaurantmenus { get; set; }
    }
}
