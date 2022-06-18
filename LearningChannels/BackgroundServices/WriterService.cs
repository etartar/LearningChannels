using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace LearningChannels.BackgroundServices
{
    public class WriterService : BackgroundService
    {
        private readonly ChannelWriter<int> _channelWriter;

        public WriterService(ChannelWriter<int> channelWriter)
        {
            _channelWriter = channelWriter;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int count = 0;

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
                Console.WriteLine($"Write Data : {count}");
                await _channelWriter.WriteAsync(count++, stoppingToken);
            }
        }
    }
}
