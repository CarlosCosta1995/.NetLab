namespace CustomersNameSpace
{
    public class Customers
    {
        // criar uma lista de Customer
        // criar um construtor da class para adicionar os Customer
        public List<Customer> CustomersList { get; set; }

        public Customers ()
        {
            this.CustomersList = new List<Customer> ();
        }
    }
}
