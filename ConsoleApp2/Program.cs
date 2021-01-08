namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            using var dbContext = new CrmDbContext();

            var customer = new Customer()
            {
                Name = "Dimitris",
                Surname = "Pnevmatikos",
                VatNumber = "123456789",
                TotalGross = 1500M
            };

            dbContext.Add(customer);
            dbContext.SaveChanges();
        }
    }
}
