using System;
using System.Collections.Generic;

namespace Restaurant.DataAccess.DataObjects
{
    public partial class CustomerRestaurantmenu
    {
        public int CustomerRestaurantmenuId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantmenuId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Restaurantmenu Restaurantmenu { get; set; } = null!;
    }
}
