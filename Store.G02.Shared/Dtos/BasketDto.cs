﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Shared.Dtos
{
    public class BasketDto
    {
        public int Id { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; }
    }
}
