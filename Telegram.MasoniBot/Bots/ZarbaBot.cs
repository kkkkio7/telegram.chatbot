﻿using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.MasoniBot.Commands;
using Telegram.MasoniBot.Resources;

namespace Telegram.MasoniBot.Bots
{
    public class ZarbaBot : IBotCommand
    {

        private LoadDictionary dictionary = new LoadDictionary();
        private LoadResource resource = new LoadResource();
        private string[] data;
        public Api BotApi { get; set; }

        public async Task Process(Message message, Match messageMatches)
        {
            try {
                switch (messageMatches.Groups["action"].Value.Trim())
                {

                    case "talk":
                        if (string.IsNullOrEmpty(messageMatches.Groups["pattern"].Value))
                        {
                            data = dictionary.Load("zarba");
                            await BotApi.SendTextMessage(message.Chat.Id, data[new Random().Next(0, data.Length)]);
                        }
                        else
                        {
                            var pattern = messageMatches.Groups["pattern"].Value.Trim();
                            data = dictionary.Load("zarba_" + pattern.Replace("about ", ""));
                            await BotApi.SendTextMessage(message.Chat.Id, data[new Random().Next(0, data.Length)]);
                            break;
                        }
                        break;

                    case "laugh":
                        await BotApi.SendVoice(message.Chat.Id, resource.Load("zarbalaugh.mp3"));
                        resource.Dispose();
                        break;

                    case "eat":
                        data = dictionary.Load("zarba_foto");
                        await BotApi.SendPhoto(message.Chat.Id, resource.Load(data[new Random().Next(0, data.Length)]));
                        resource.Dispose();
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