using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ExtensibleDiscordBot.Services;

namespace ExtensibleDiscordBot
{
    public class Program
    {

        public static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private IConfigurationRoot _config;

        public DiscordSocketClient Client { get; set; }

        public async Task StartAsync()
        {
            var builder = new ConfigurationBuilder()    // Begin building the configuration file
                .SetBasePath(AppContext.BaseDirectory)  // Specify the location of the config
                .AddJsonFile("_configuration.json");    // Add the configuration file
            _config = builder.Build();                  // Build the configuration file
            Client = new DiscordSocketClient(new DiscordSocketConfig // Add the discord client to the service provider
            {
                LogLevel = LogSeverity.Verbose,
                AlwaysDownloadUsers = true,
                MessageCacheSize = 1000 // Tell Discord.Net to cache 1000 messages per channel
            });
            var services = new ServiceCollection()      // Begin building the service provider
                .AddSingleton(Client)
                .AddSingleton(new CommandService(new CommandServiceConfig     // Add the command service to the service provider
                {
                    DefaultRunMode = RunMode.Async,     // Force all commands to run async
                    LogLevel = LogSeverity.Verbose
                }))
                .AddSingleton<CommandHandler>()     // Add remaining services to the provider
                .AddSingleton<LoggingService>()
                .AddSingleton<StartupService>()
                .AddSingleton<Random>()             // You get better random with a single instance than by creating a new one every time you need it
                .AddSingleton(_config);

            var provider = services.BuildServiceProvider();     // Create the service provider

            provider.GetRequiredService<LoggingService>();      // Initialize the logging service, startup service, and command handler
            await provider.GetRequiredService<StartupService>().StartAsync();
            provider.GetRequiredService<CommandHandler>();

            Client.UserJoined += UserJoinedEventHandler;

            await Task.Delay(-1);     // Prevent the application from closing
        }



        /// <summary>
        /// When a new <see cref="SocketGuildUser"/> joins a <see cref="SocketGuild"/>, send out a message welcoming <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The <see cref="SocketGuildUser"/> that joined.</param>
        /// <returns>The <see cref="Task"/> upon completion.</returns>
        private async Task UserJoinedEventHandler(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            await channel.SendMessageAsync($"{user.Mention}, welcome to {guild.Name}!");
        }
    }
}
