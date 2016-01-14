﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models.Enum;

namespace Mango_Cards.Library.Models
{
    public class Field
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FieldValue { get; set; }
        public FieldType FieldType { get; set; }
    }
}