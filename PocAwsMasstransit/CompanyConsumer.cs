using MassTransit;

namespace PocAwsMasstransit;

public class CompanyConsumer : IConsumer<Company>
{
    public async Task Consume(ConsumeContext<Company> context)
    {
        var company = context.Message;
        Console.WriteLine($@"Company: {company.Name}, Address: {company.Address}, {company.BuildingNumber}, {company.City}, {company.State}
     Postal code:{company.PostalCode}, Acitive: {company.Active}" );
    }
}
