// Project | Programming 2
// Term 2, 2020
// Drones, Customers & Transactions Program
namespace AceDrones
{
    [System.Serializable]
    class Customer
    {
        private string custID;
        private string name;
        private string city;
        private string country;


        public string DisplayArray()
        {
            return (gsCustID + " - " + gsName + " - " + gsCity + " - " + gsCountry);
        }
        public string gsCustID
        {
            get { return custID; }
            set { custID = value; }
        }
        public string gsName
        {
            get { return name; }
            set { name = value; }
        }
        public string gsCity
        {
            get { return city; }
            set { city = value; }
        }
        public string gsCountry
        {
            get { return country; }
            set { country = value; }
        }
    }
}
