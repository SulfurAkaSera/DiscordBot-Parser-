using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient client;

        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient();
            string token = "OTExMTk4NjU1MTk1NzgzMTg4.YZd54A.YPeMKKbxty4HYLJ_D-i2fYtbM5k";

            client.MessageReceived += HandleCommandAsync;
            client.Log += clientLog;
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            Console.ReadLine();
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            if(!arg.Author.IsBot)
            {
                switch(arg.Content)
                {
                    case "!Погода":
                        List<string> weather = Parser.ParseWeather();
                        foreach (var res in weather)
                        {
                        await arg.Channel.SendMessageAsync(res);
                        }
                        break;
                    case "!Курс":
                        List<string> course = Parser.ParseCourse();
                        foreach (var cour in course)
                        {
                            await arg.Channel.SendMessageAsync(cour);
                        }
                        break;
                    case "!Снюс":
                        List<string> snews = Parser.ParseSnews();
                        foreach (var sn in snews)
                        {
                            await arg.Channel.SendMessageAsync(sn);
                        }
                        break;
                    case "!Виски":
                        await arg.Channel.SendMessageAsync("Колла, каралева танспола");
                        break;
                }
            }
        }

        private Task clientLog(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
    }
}