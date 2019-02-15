using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ExtensibleDiscordBot.CommandModules
{
    [Name("Example")]
    public class ExampleModule : ModuleBase<SocketCommandContext>
    {
        [Command("example")]
        [Summary("This is the summary for the example command.")]
        public async Task Example(IGuildUser user)
        {
  
        }

        [Command("ping")]
        [Summary("This is the summary for the ping command.")]
        public async Task Example(string input)
        {
            await ReplyAsync($"Hey {Context.User.Mention}, you typed {input}!");
        }

        [Command("example")]
        [Summary("This is the summary for the ping command.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AdminExample()
        {
            await ReplyAsync($"Hey {Context.User.Mention}, you are an admin!");
        }
    }
}