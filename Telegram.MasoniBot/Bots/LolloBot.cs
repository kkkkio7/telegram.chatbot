﻿using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.MasoniBot.Commands;
using Telegram.MasoniBot.Resources;

namespace Telegram.MasoniBot.Bots
{
    public class LolloBot : IBotCommand
    {
        private LoadDictionary dictionary = new LoadDictionary();
        private LoadResource resource = new LoadResource();

        public Api BotApi { get; set; }

        public async Task Process(Message message, Match messageMatches)
        {

            try
            {
                switch (messageMatches.Groups["action"].Value.Trim())
                {
                    case "retarded":
                        await BotApi.SendVideo(message.Chat.Id, resource.Load("lolloritardato.mp4"));
                        resource.Dispose();
                        break;

                    case "is":
                        if (string.IsNullOrEmpty(messageMatches.Groups["pattern"].Value))
                        {
                            var data = dictionary.Load("lollo");
                            var msg = data[new Random().Next(0, data.Length)];
                            await BotApi.SendTextMessage(message.Chat.Id, msg);
                        }
                        else
                        {
                            var pattern = messageMatches.Groups["pattern"].Value.Trim();
                            var data = dictionary.Load("lollo_" + pattern.Replace("about ", ""));
                            var msg = data[new Random().Next(0, data.Length)];
                            await BotApi.SendTextMessage(message.Chat.Id, msg);
                            break;
                        }
                        break;

                }
            }
            catch (Exception)
            {
                await BotApi.SendTextMessage(message.Chat.Id, "...idiota.. devi aver scritto qualche cazzata..");
            }
        }
    }
}