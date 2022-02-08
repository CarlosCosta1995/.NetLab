namespace CustomersTry
{
    public class Customer
    {
        public int CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

       /* public Customer(int _costumerNumber, string _firstName, string _lastName, string _city, string _region, long _phoneNumber, string _address)
        {
            this.CustomerNumber = _costumerNumber;
            this.FirstName = _firstName;
            this.LastName = _lastName;  
            this.City = _city;
            this.Region = _region;
            this.PhoneNumber = _phoneNumber;
            this.Address = _address;
        }*/
    }
}
