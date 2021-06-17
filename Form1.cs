using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
// Project | Programming 2
// Term 2, 2020
// Drones, Customers & Transactions Program
namespace AceDrones
{
    public partial class AceDrones : Form
    {
        public AceDrones()
        {
            InitializeComponent();
            LoadDrones();
            LoadCusts();
            LoadTrans();
        }
        private static int emptyPtr = 0;
        private static int emptyPtr2 = 0;
        private static int emptyPtr3 = 0;
        private static int columns = 3;
        private static int max = 20;
        private Drone[] drones = new Drone[max];
        private const string droneFile = "drones.dat";
        private Customer[] customers = new Customer[max];
        private const string custFile = "customers.dat";
        private string[,] Transactions = new string[max, columns];
        private const string transFile = "transactions.dat";

        // Add drone button - all input boxes must contain a string and the max of 20 must not be reach in order to add a new drone. to the array
        // Button also sorts array if larger than 1, clears the input boxes and displays after a successful addition.
        private void AddDroneBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(SerialNumBox.Text)) &&
                    !(string.IsNullOrEmpty(ModelBox.Text)) &&
                    !(string.IsNullOrEmpty(EngConfigBox.Text)) &&
                    !(string.IsNullOrEmpty(RangeBox.Text)) &&
                    !(string.IsNullOrEmpty(AccBox.Text)) &&
                    !(string.IsNullOrEmpty(PriceBox.Text)) &&
                    !(string.IsNullOrEmpty(PurchaseDateBox.Text)))
                {
                    if (emptyPtr < max)
                    {
                        Drone NewDrone = new Drone();
                        NewDrone.gsSerialNum = SerialNumBox.Text;
                        NewDrone.gsModel = ModelBox.Text;
                        NewDrone.gsEngConfig = EngConfigBox.Text;
                        NewDrone.gsRange = RangeBox.Text;
                        NewDrone.gsAccessories = AccBox.Text;
                        NewDrone.gsPrice = PriceBox.Text;
                        NewDrone.gsPurchaseDate = PurchaseDateBox.Text;
                        drones[emptyPtr] = NewDrone;
                        emptyPtr++;
                    }
                    else
                    {
                        MessageBox.Show("The list is now full.");
                    }
                }
                else
                {
                    MessageBox.Show("Enter details to each input box.");
                    return;
                }
                if (emptyPtr > 1)
                {
                    BubbleSortDrones();
                }
                ClearDroneInput();
                DisplayDroneArr();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Error occured.");
            }
        }
        // Add customer button - all input boxes must contain a string and the max of 20 must not be reach in order to add a new unique customer to the array.
        // If any of the input boxes are missing data then the user is asked to create a default customer with the option of no.
        // Button also clears the input boxes and displays after a successful addition.
        private void AddCustBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(CustIDBox.Text)) &&
                    !(string.IsNullOrEmpty(NameBox.Text)) &&
                    !(string.IsNullOrEmpty(CityBox.Text)) &&
                    !(string.IsNullOrEmpty(CountryBox.Text)))
                {
                    if (emptyPtr2 < max)
                    {
                        Customer AddNewCustomer = new Customer();
                        AddNewCustomer.gsCustID = CustIDBox.Text;
                        AddNewCustomer.gsName = NameBox.Text;
                        AddNewCustomer.gsCity = CityBox.Text;
                        AddNewCustomer.gsCountry = CountryBox.Text;
                        customers[emptyPtr2] = AddNewCustomer;
                        emptyPtr2++;
                    }
                    else
                    {
                        MessageBox.Show("The list is now full.");
                    }
                }
                else
                {
                    // Initializes the variables to pass to the MessageBox.Show method.
                    string message = "You did not enter all details. Would you like to create a default customer?";
                    string caption = "Error Detected in Input";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;

                    // Displays the MessageBox.
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (emptyPtr2 < max)
                        {
                            Customer AddNewCustomer = new Customer();
                            AddNewCustomer.gsCustID = "C999";
                            AddNewCustomer.gsName = "Unknown";
                            AddNewCustomer.gsCity = "Unknown";
                            AddNewCustomer.gsCountry = "Unknown";
                            customers[emptyPtr2] = AddNewCustomer;
                            emptyPtr2++;
                        }
                        else
                        {
                            MessageBox.Show("The list is now full.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Continue.");
                    }
                }
                ClearCustInput();
                DisplayCustArr();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Error occured.");
            }
        }
        // Add transaction button - all input boxes must contain a string and the max of 20 must not be reach in order to add a new transaction to the array.
        // The data for the customer ID and serial number are taken from each array and used in the creation of a new transaction.
        // If any of the input boxes are missing data then the user is displayed an error message.
        // Button also clears the input boxes and displays after a successful addition.
        private void AddTransBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(TransIDBox.Text)) &&
                  !(string.IsNullOrEmpty(CustIDBox2.Text)) &&
                  !(string.IsNullOrEmpty(SerialNumBox2.Text)))
                {
                    if (emptyPtr3 < max)
                    {
                        Transactions[emptyPtr3, 0] = (TransIDBox.Text);
                        Transactions[emptyPtr3, 1] = (CustIDBox2.Text);
                        Transactions[emptyPtr3, 2] = (SerialNumBox2.Text);
                        emptyPtr3++;
                    }
                    else
                    {
                        MessageBox.Show("The list is now full.");
                    }
                }
                else
                {
                    MessageBox.Show("Enter details to each input box.");
                }
                ClearTransInput();
                ClearCustInput();
                ClearDroneInput();
                DisplayTransArr();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Error occured.");
            }
        }
        // When a drone item is selected from the drone list box the matching details populate the input boxes.
        // The transaction serial number box is also populated for the creation of a transaction.
        private void DronesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DronesListBox.SelectedIndexChanged += new EventHandler(DronesListBox_SelectedIndexChanged);
            {
                var i = DronesListBox.SelectedIndex;
                SerialNumBox.Text = drones[i].gsSerialNum;
                SerialNumBox2.Text = drones[i].gsSerialNum;
                ModelBox.Text = drones[i].gsModel;
                EngConfigBox.Text = drones[i].gsEngConfig;
                RangeBox.Text = drones[i].gsRange;
                AccBox.Text = drones[i].gsAccessories;
                PriceBox.Text = drones[i].gsPrice;
                PurchaseDateBox.Text = drones[i].gsPurchaseDate;
            }
        }
        // When a customer item is selected from the customer list box the matching details populate the input boxes.
        // The transaction customer ID box is also populated for the creation of a transaction.
        private void CustListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustListBox.SelectedIndexChanged += new EventHandler(CustListBox_SelectedIndexChanged);
            {
                var i = CustListBox.SelectedIndex;
                CustIDBox.Text = customers[i].gsCustID;
                CustIDBox2.Text = customers[i].gsCustID;
                NameBox.Text = customers[i].gsName;
                CityBox.Text = customers[i].gsCity;
                CountryBox.Text = customers[i].gsCountry;
            }
        }
        // When a transaction item is selected from the customer list box the matching details populate the input boxes.
        // The matching drone and customer input boxes are also filled using custom search methods based on the transaction box inputs.
        private void TransListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TransListBox.SelectedIndexChanged += new EventHandler(TransListBox_SelectedIndexChanged);
            {
                var i = TransListBox.SelectedIndex;
                TransIDBox.Text = Transactions[i, 0];
                CustIDBox2.Text = Transactions[i, 1];
                SerialNumBox2.Text = Transactions[i, 2];
                SearchCust();
                SearchDrone();
            }
        }
        // Custom search method looking for the customer ID in customers array that matches the custome ID box data under transactions.
        private void SearchCust()
        {
            ClearCustInput();
            try
            {
                foreach (Customer c in customers)
                {
                    if (c.gsCustID == CustIDBox2.Text)
                    {
                        CustIDBox.Text = c.gsCustID;
                        NameBox.Text = c.gsName;
                        CityBox.Text = c.gsCity;
                        CountryBox.Text = c.gsCity;
                        int i = Array.IndexOf(customers, c);
                        CustListBox.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Null Reference.");
                return;
            }
        }
        // Custom search method looking for the serial number in drones array that matches the serial number box data under transactions.
        private void SearchDrone()
        {
            ClearDroneInput();
            try
            {
                foreach (Drone d in drones)
                {
                    if (d.gsSerialNum == SerialNumBox2.Text)
                    {
                        SerialNumBox.Text = d.gsSerialNum;
                        ModelBox.Text = d.gsModel;
                        EngConfigBox.Text = d.gsEngConfig;
                        RangeBox.Text = d.gsRange;
                        AccBox.Text = d.gsAccessories;
                        PriceBox.Text = d.gsPrice;
                        PurchaseDateBox.Text = d.gsPurchaseDate;
                        int i = Array.IndexOf(drones, d);
                        DronesListBox.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Null Reference.");
                return;
            }
        }
        // Display drones array using private variables and method in drone class.
        private void DisplayDroneArr()
        {
            DronesListBox.Items.Clear();
            for (int j = 0; j < emptyPtr; j++)
            {
                DronesListBox.Items.Add(drones[j].DisplayArray());
            }
        }
        // Display customers array using private variables and method in customer class.
        private void DisplayCustArr()
        {
            CustListBox.Items.Clear();
            for (int j = 0; j < emptyPtr2; j++)
            {
                CustListBox.Items.Add(customers[j].DisplayArray());
            }
        }
        // Display transactions array.
        private void DisplayTransArr()
        {
            TransListBox.Items.Clear();
            for (int j = 0; j < emptyPtr3; j++)
            {
                TransListBox.Items.Add(Transactions[j, 0] + "   " + Transactions[j, 1] + "  " + Transactions[j, 2]);
            }
        }
        // Bubble sort drones array using compare to and seperate swap method.
        private void BubbleSortDrones()
        {
            int comp;
            try
            {
                for (int i = 1; i < emptyPtr; i++)
                    for (int j = 1; j < emptyPtr; j++)
                    {
                        comp = drones[j - 1].gsSerialNum.CompareTo(drones[j].gsSerialNum);
                        if (comp > 0)
                        {
                            Swap(drones, j);
                        }
                    }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured.");
                return;
            }
        }
        // Swap method for use in drone bubble sort.
        private void Swap(Drone[] drones, int j)
        {
            Drone tempDrone;
            tempDrone = drones[j];
            drones[j] = drones[j - 1];
            drones[j - 1] = tempDrone;
        }
        // Clear drone inout boxes.
        private void ClearDroneInput()
        {
            SerialNumBox.Clear();
            ModelBox.Clear();
            EngConfigBox.Clear();
            RangeBox.Clear();
            AccBox.Clear();
            PriceBox.Clear();
            PurchaseDateBox.Clear();
        }
        // Clear customer inout boxes.
        private void ClearCustInput()
        {
            CustIDBox.Clear();
            NameBox.Clear();
            CityBox.Clear();
            CountryBox.Clear();
        }
        // Clear transaction inout boxes.
        private void ClearTransInput()
        {
            TransIDBox.Clear();
            CustIDBox2.Clear();
            SerialNumBox2.Clear();
        }
        // Double click serial number box method to clear drone input boxes.
        private void SerialNumBox_TextChanged(object sender, EventArgs e)
        {
            ClearDroneInput();
        }
        // Double click customer ID box method to clear customer input boxes.
        private void CustIDBox_TextChanged(object sender, EventArgs e)
        {
            ClearCustInput();
        }
        // Double click transaction ID box method to clear transaction input boxes.
        private void TransIDBox_TextChanged(object sender, EventArgs e)
        {
            ClearTransInput();
        }
        // Right click search method for customer array, data is entered to customer ID box then searched for the remaining data.
        // Once found the appropriate input boxes are filled else an error message is displayed to the user.
        private void searchCustomerIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Customer c in customers)
                {
                    if (c.gsCustID == CustIDBox.Text)
                    {
                        CustIDBox.Text = c.gsCustID;
                        NameBox.Text = c.gsName;
                        CityBox.Text = c.gsCity;
                        CountryBox.Text = c.gsCountry;
                        int i = Array.IndexOf(customers, c);
                        CustListBox.SelectedIndex = i;
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Customer ID not found.");
                        break;
                    }
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Null Reference."); 
                return;
            }
        }
        // Save all three array as binary files as the form is closed.
        private void AceDrones_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveDrones();
            SaveCusts();
            SaveTrans();
        }
        // Save the drones array to a binary file using file stream and binary formatter to serialize each object.
        private void SaveDrones()
        {
            using (FileStream fs = new FileStream(droneFile, FileMode.OpenOrCreate))
            {
                BinaryFormatter bf = new BinaryFormatter();
                for (int i = 0; i < emptyPtr; i++)
                {
                    bf.Serialize(fs, drones[i]);
                }
                fs.Flush();
                fs.Close();
                fs.Dispose();
            }
        }
        // Save the customers array to a binary file using file stream and binary formatter to serialize each object.
        private void SaveCusts()
        {
            using (FileStream fs = new FileStream(custFile, FileMode.OpenOrCreate))
            {
                BinaryFormatter bf = new BinaryFormatter();
                for (int i = 0; i < emptyPtr2; i++)
                {
                    bf.Serialize(fs, customers[i]);
                }
                fs.Flush();
                fs.Close();
                fs.Dispose();
            }
        }
        // Save the transactions array to a binary file using file stream and binary writer.
        private void SaveTrans()
        {
            BinaryWriter bw;
            using (bw = new BinaryWriter(File.Open(transFile, FileMode.OpenOrCreate))) 
            {
                for (int i = 0; i < emptyPtr3; i++)
                    for (int j = 0; j < columns; j++)
                    {
                        bw.Write(Transactions[i, j]);
                    }
            }
            bw.Close();
        }
        // Load the drones array from a binary file using file stream and binary formatter to deserialize each object.
        // Display drones array once loaded and bubble sort.
        private void LoadDrones()
        {
            emptyPtr = 0;
            if (File.Exists(droneFile))
            {
                try
                {
                    using (FileStream fs = new FileStream(droneFile, FileMode.Open))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        while (fs.Position < fs.Length)
                        {
                            Drone drone = (Drone)bf.Deserialize(fs);
                            drones[emptyPtr] = drone;
                            emptyPtr++;
                        }
                        fs.Close();
                        DisplayDroneArr();
                        BubbleSortDrones();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Could not load drones.");
                    return;
                }
            }
        }
        // Load the customers array from a binary file using file stream and binary formatter to deserialize each object.
        // Display customers array once loaded.
        private void LoadCusts()
        {
            emptyPtr2 = 0;
            if (File.Exists(custFile))
            {
                try
                {
                    using (FileStream fs = new FileStream(custFile, FileMode.Open))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        while (fs.Position < fs.Length)
                        {
                            Customer cust = (Customer)bf.Deserialize(fs);
                            customers[emptyPtr2] = cust;
                            emptyPtr2++;
                        }
                        fs.Close();
                        DisplayCustArr();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Could not load drones.");
                    return;
                }
            }
        }
        // Load the transactions array from a binary file using file stream and binary writer.
        // Display transactions array once loaded.
        private void LoadTrans()
        {
            FileStream fs;
            BinaryReader br;
            if (File.Exists(transFile))
            {
                using (fs = new FileStream(transFile, FileMode.Open))
                {
                    br = new BinaryReader(fs);
                    emptyPtr3 = 0;
                    int i = 0;
                    while (br.BaseStream.Position < br.BaseStream.Length) 
                    {                      
                        for (int j = 0; j < columns; j++)
                        {
                            Transactions[i, j] = br.ReadString();
                        }
                        i++;
                        emptyPtr3++;
                    }
                }
                br.Close();
                fs.Close();
                DisplayTransArr();
            }
        }
    }
}


 

