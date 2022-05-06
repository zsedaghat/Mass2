using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mass2
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var busController = Bus.Factory.CreateUsingRabbitMq(cfq =>
           {
               cfq.Host("localhost", h =>
               {
                   h.Username("guest");
                   h.Password("guest");
               });
               cfq.ReceiveEndpoint("AccountService", e =>
               {
                   e.Lazy = true;
                   e.PrefetchCount = 20;
                   e.Consumer<AccountConsumer>();
               }
              );


           });

            using var cancelation = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            await busController.StartAsync(cancelation.Token);
            try
            {
                await busController.Publish<UpdateAccount>(new
                {
                    AccountNumber = "123iiii"
                });
            }
            finally
            {
                await busController.StopAsync(cancelation.Token);
            }

        }
    }
}
