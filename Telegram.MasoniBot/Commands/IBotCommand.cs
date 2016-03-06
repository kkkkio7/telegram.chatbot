using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram.MasoniBot.Commands
{
    interface IBotCommand
    {
       
        Api BotApi { get; set; }
        Task Process(Message message, Match messageMatches);
    }
}
