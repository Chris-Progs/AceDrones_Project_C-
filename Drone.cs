// Project | Programming 2
// Term 2, 2020
// Drones, Customers & Transactions Program
namespace AceDrones
{
    [System.Serializable]
    class Drone
    {
        private string serialNum;
        private string model;
        private string engConfig;
        private string range;
        private string accessories;
        private string price;
        private string purchaseDate;

        // Displpay arrays method that returns the string values of the Drone object.
        public string DisplayArray()
        {
            return (gsSerialNum + " - " + gsEngConfig + " - " + gsPrice);
        }



        // Use of setter and getter methods for the above private variables.
        public string gsSerialNum
        {
            get { return serialNum; }
            set { serialNum = value; }
        }
        public string gsModel
        {
            get { return model; }
            set { model = value; }
        }
        public string gsEngConfig
        {
            get { return engConfig; }
            set { engConfig = value; }
        }
        public string gsRange
        {
            get { return range; }
            set { range = value; }
        }
        public string gsAccessories
        {
            get { return accessories; }
            set { accessories = value; }
        }
        public string gsPrice
        {
            get { return price; }
            set { price = value; }
        }
        public string gsPurchaseDate
        {
            get { return purchaseDate; }
            set { purchaseDate = value; }
        }
    }
}
