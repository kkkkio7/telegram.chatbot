using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.MasoniBot.Commands;
using File = System.IO.File;

namespace Telegram.MasoniBot
{
    class Program
    {
        static void Main(string[] args)
        {

            Run().Wait();


        }

        static async Task Run()
        { 
            var Bot = new Api("171139565:AAGZDEsv0gw_6LDsAHZR9wDCwMDTgfWzAIE");

            var me = await Bot.GetMe();

            Console.WriteLine("Hello my name is {0}", me.Username);
         
            var offset = 0;

            while (true)
            {

                var updates = await Bot.GetUpdates(offset);
                foreach (var update in updates)
                {

                    if (update.Message.Type == MessageType.TextMessage) // && previous.Where(j=> j.Equals(update.Message.From.Username)).Count() < 3)
                    {

                        await Bot.SendChatAction(update.Message.Chat.Id, ChatAction.Typing);
                        await Task.Delay(2000);

                        var match = Regex.Match(update.Message.Text + " ", @"(?'command'^\/.*?\s)?(?'action'.*?\s)?(?'pattern'.*)");

                        if (match.Groups.Count > 1)
                        {

                            BotCommandFactory factory = new BotCommandFactory();
                            var command = factory.CreateCommand(match.Groups["command"].Value.Trim());
                            if (command == null)
                                await Bot.SendTextMessage(update.Message.Chat.Id, "...idiota.. devi aver scritto qualche cazzata..");
                            else
                            {
                                command.BotApi = Bot;
                                await command.Process(update.Message, match);
                            }
                        }
                    }
                    offset = update.Id + 1;
                }


                await Task.Delay(3000);
            }
        }
    }
}