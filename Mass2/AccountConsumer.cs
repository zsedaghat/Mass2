using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mass2
{
    public class AccountConsumer : IConsumer<UpdateAccount>
    {
        public async Task Consume(ConsumeContext<UpdateAccount> context)
        {
            Console.WriteLine($"vaaaaaaaaaaay{context.Message.AccountNumber}");
        }
    }
}
