using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using System.Threading.Tasks;

namespace evbot
{
    /// <summary> Detect whether a message is a command, then execute it. </summary>
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _cmds;

        public async Task InstallAsync(DiscordSocketClient c)
        {
            _client = c;
            _cmds = new CommandService();
            await _cmds.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null)
                return;

            var context = new SocketCommandContext(_client, msg);

            int argPos = 0;
            if (msg.HasStringPrefix(";:", ref argPos) ||
                msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _cmds.ExecuteAsync(context, argPos);

                if (!result.IsSuccess)
                    await context.Channel.SendMessageAsync(result.ToString());
            }
        }
    }
}