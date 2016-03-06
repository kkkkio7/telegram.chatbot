using System;
using System.Configuration;
using System.IO;
using Telegram.Bot.Types;

namespace Telegram.MasoniBot.Resources
{
    public class LoadResource : IBotResources, IDisposable
    {
        private Stream stream { get; set; }

        public void Dispose()
        {
            stream.Dispose();
        }

        public FileToSend Load(string filename)
        {
            stream = new MemoryStream(System.IO.File.ReadAllBytes(ConfigurationManager.AppSettings["ResourcesPath"] + filename));
            return new FileToSend(filename, stream);

        }
    }
}
