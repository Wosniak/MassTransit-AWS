// See https://aka.ms/new-console-template for more information
using MassTransit;

namespace PocAwsMasstransit
{
    public class PersonConsumer : IConsumer<Person>
    {
        public async Task Consume(ConsumeContext<Person> context)
        {
            Console.WriteLine($"Name: {context.Message.Name}, Age: {context.Message.Age}, Birth Date: {context.Message.Birthdate}");
           
        }
    }
}