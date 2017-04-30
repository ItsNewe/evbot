using System.Threading.Tasks;
using Discord.Commands;
namespace evbot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("evs"), Summary("ev")]
        public async Task Evsp(int times)
        {
            for (int x = 0; x < times; x++)
            {
                await ReplyAsync($"{Context.Guild.EveryoneRole}");
            }
        }
    }
}
