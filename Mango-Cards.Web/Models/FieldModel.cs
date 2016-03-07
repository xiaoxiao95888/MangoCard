using Mango_Cards.Web.Models.Enum;
using System;

namespace Mango_Cards.Web.Models
{
    public class FieldModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FieldValue { get; set; }
        public FieldType FieldType { get; set; }
    }
}