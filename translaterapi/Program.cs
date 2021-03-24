using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GoogleTranslateFreeApi;
using translateme;

namespace translaterapi
{
    internal class Program
    {
        private static readonly GoogleTranslator Translator = new GoogleTranslator();

        static void Main(string[] args)
        {
            new Translateus().show();
        }
        
        private struct TranslateItem : ITranslatable
        {
            public string OriginalText { get; }
            public Language FromLanguage { get; }
            public Language ToLanguage { get; }

            // Some other user defined properties...

            public TranslateItem(string text)
            {
                OriginalText = text;
                FromLanguage = new Language("English", "en");
                ToLanguage = GoogleTranslator.GetLanguageByISO("fr");
            }
        }
        
    }

}