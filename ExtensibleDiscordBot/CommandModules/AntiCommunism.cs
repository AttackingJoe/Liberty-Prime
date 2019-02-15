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
            var u = Context.Message.Author;      // Or some other means of getting user_id
            string msg = "I'm ready to play Tropico you fuck.";
            await Discord.UserExtensions.SendMessageAsync(u, msg);
        }

    }
}
