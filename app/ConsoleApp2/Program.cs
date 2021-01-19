namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        //public static IQueryable<Customer> SearchCustomers(
        //    SearchCustomerOptions options)
        //{
        //    using var dbContext = new CrmDbContext();

        //    var q = dbContext.Set<Customer>()
        //        .AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(options?.Name)) {
        //        q = q.Where(c => c.Name == options.Name);
        //    }

        //    if (!string.IsNullOrWhiteSpace(options?.Vatnumber)) {
        //        q = q.Where(c => c.VatNumber == options.Vatnumber);
        //    }

        //    if (options.GrossFrom != null) {
        //        q = q.Where(c => c.TotalGross >= options.GrossFrom.Value);
        //    }

        //    if (options.GrossTo != null) {
        //        q = q.Where(c => c.TotalGross < options.GrossTo.Value);
        //    }

        //    if (options.Skip != null) {
        //        q = q.Skip(options.Skip.Value);
        //    }

        //    if (options.MaxResults != null) {
        //        q = q.Take(options.MaxResults.Value);
        //    } else {
        //        q = q.Take(500);
        //    }

        //    return q;
        //}
    }
}
