using System.Threading.Tasks;
using Discord;
using Discord.Commands;


namespace ExtensibleDiscordBot.CommandModules
{
    [Name("AntiCommunism")]
    public class CommunismModule : ModuleBase<SocketCommandContext>
    {
        [Command("communismbad")]
        [Summary("This is the summary for the example command.")]
        public async Task communismbad()
        {
await Context.Channel.SendMessageAsync("Communism BAD!")

    }
}
