using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace LearningChannels.BackgroundServices
{
    public class ReaderService : BackgroundService
    {
        private readonly ChannelReader<int> _channelReader;

        public ReaderService(ChannelReader<int> channelReader)
        {
            _channelReader = channelReader;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
                try
                {
                    var result = await _channelReader.ReadAsync(stoppingToken);
                    Console.WriteLine($"Read Data : {result}");
                }
                catch (ChannelClosedException)
                {
                    Console.WriteLine($"Channel was closed.");
                }
            }
        }
    }
}
