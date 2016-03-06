using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.MasoniBot.Commands;
using Telegram.MasoniBot.Resources;

namespace Telegram.MasoniBot.Bots
{
    public class FigaBot : IBotCommand
    {

      

        public Api BotApi { get; set; }

        public async Task Process(Message message, Match messageMatches)
        {
            try
            {
                string data;
                using (var client = new WebClient())
                {
                    data = client.DownloadString("http://www.sexyandfunny.com/10randomphotos.php");
                }
                var match = Regex.Match(data, "<img.*src=\"(.*)\".*class=\"random-image-img");
                string url = match.Groups[1].Value;
                byte[] photodata;
                using (var client = new WebClient())
                {
                    photodata = client.DownloadData(url);
                }

                Stream stream = new MemoryStream(photodata);

                var fl = new FileToSend(string.Format("figa_richiesta_da_{0}.jpg", message.Chat.FirstName), stream);
                await BotApi.SendPhoto(message.Chat.Id, fl);
                await BotApi.SendTextMessage(message.Chat.Id, string.Format("bravoh {0}.. adesso vatti a fare una sega con il presidente..... ", message.From.FirstName));
                stream.Dispose();
            }
            catch (Exception)
            {
                await BotApi.SendTextMessage(message.Chat.Id, "...idiota.. devi aver scritto qualche cazzata..");
            }
        }
    }
}
