using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Telegram.MasoniBot.Resources
{
    public class LoadDictionary : IBotDictionaries
    {
        public string[] Load(string filename)
        {
            
            return File.ReadAllLines(ConfigurationManager.AppSettings["DictionariesPath"] + filename + ".txt");
        }
    }
}
