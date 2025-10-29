﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Domain.Entities.Baskets
{
    public class CustomerBasket
    {
        public int Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
    }
}
