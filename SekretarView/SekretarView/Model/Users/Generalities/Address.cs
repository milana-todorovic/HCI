using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Users.Generalities
{
    public class Address
    {
        private City city;
        private String street;

        public City City { get => city; set => city = value; }
        public string Street { get => street; set => street = value; }

        public override string ToString()
        {
            String retVal = Street;
            if (City != null)
            {
                retVal += ", " + City.Name;
                if (City.Country != null)
                    retVal += ", " + City.Country.Name;
            }

            return retVal;
        }
    }
}
