// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using MassTransit;
using Microsoft.Extensions.Hosting;
using PocAwsMasstransit;
using Host = Microsoft.Extensions.Hosting.Host;

var builder = Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddMassTransit(x =>
            {
                x.UsingAmazonSqs((context, cfg) =>
                {
                    // cfg.ConfigureJsonSerializerOptions(opt => {
                    //     opt.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;                        
                    //     return opt;
                    // });
                    
                    cfg.Host("us-east-1", h => {
                        
                        h.AccessKey("local");
                        h.SecretKey("local");
                        h.Config(new AmazonSimpleNotificationServiceConfig { ServiceURL = "https://localhost.localstack.cloud:4566" });
                        h.Config(new AmazonSQSConfig { ServiceURL = "https://localhost.localstack.cloud:4566" });
                    });
                    

                    cfg.ReceiveEndpoint("person-sqs", ec =>
                        {
                            ec.ConfigureConsumeTopology = false;    
                            ec.ClearSerialization();
                            ec.UseRawJsonSerializer();                       
                            ec.Subscribe("person-sns");
                            ec.ConfigureConsumer<PersonConsumer>(context);
                        });

                    cfg.ReceiveEndpoint("company-sqs", ec =>
                        {
                            ec.ConfigureConsumeTopology = false;    
                            ec.ClearSerialization();
                            ec.UseRawJsonSerializer();                       
                            ec.Subscribe("company-sns");
                            ec.ConfigureConsumer<CompanyConsumer>(context);
                        });
                });

                x.AddConsumer<PersonConsumer>();
                x.AddConsumer<CompanyConsumer>();
            });
            
        }).Build();

await builder.RunAsync();
