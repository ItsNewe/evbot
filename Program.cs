using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace evbot
{
    public class Program
    {
        public static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandHandler _commands;


        public async Task StartAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig()
            //for w7
            /*_client = new DiscordSocketClient(new DiscordSocketConfig { 
                WebSocketProvider = Discord.Net.Providers.WS4Net.WS4NetProvider.Instance
            });;*/
            {
                LogLevel = LogSeverity.Info,
            });

            _client.Log += (l)
                => Console.Out.WriteLineAsync(l.ToString());

            await _client.LoginAsync(TokenType.Bot, "TOKEN");
            await _client.StartAsync();

            _commands = new CommandHandler();
            await _commands.InstallAsync(_client);

            await Task.Delay(-1);
        }
    }


}