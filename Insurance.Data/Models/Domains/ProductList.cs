using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Models.Domains
{
    public class ProductList :BaseObject
    {
        public string productCode { get; set; }
        public double price { get; set; }
        public decimal rate { get; set; }
        public string description { get; set; }
        public string duration { get; set; }

    }

    public class ProductBenefit : BaseObject
    {
        public int malaria { get; set; }
        public int life { get; set; }
        public int property { get; set; }

        public int propertyCrop { get; set; }
        public int hospitalization { get; set; }
        public int permanentDisability { get; set; }
        public int airTravel { get; set; }
        public int roadTravel { get; set; }
        public string productCode { get; set; }
    }

    public class ProviderList: BaseObject
    {
        public string name { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string Lga { get; set; }
        public string productCode { get; set; }

    }
}
