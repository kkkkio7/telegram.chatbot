using System.IO;
using Telegram.Bot.Types;

namespace Telegram.MasoniBot.Resources

{
    interface IBotResources
    {
        FileToSend Load(string filename);
    }
}

