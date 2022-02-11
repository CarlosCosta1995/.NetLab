namespace CustomersNameSpace
{
    public class Customer
    {
        //Apenas listamos os atributos da class igual ao do JSON
        public int CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; }  
    }
}
