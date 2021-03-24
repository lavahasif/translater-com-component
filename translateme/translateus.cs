using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

//you dont need strong name
//    use advanced isntaller for register your dll as .net componet 
//    com visible option chekc in assembly
//    com interop check
//    xml chekc

namespace translateme
{
    /// <summary>
    /// translate this
    /// </summary>
    public class Translateus
    {
        public String GetTranslateus(String word, String target)
        {
            transalte _transalte = new transalte();
            ;

            return _transalte.getTranslate(word, target);
        }

        public String Getcompany()
        {
            transalte _transalte = new transalte();
            ;

            return _transalte.getcompany()[0].name;
        }

        public String Getcompanys()
        {
            transalte _transalte = new transalte();
            ;

            List<Company> getcompany = _transalte.getcompany();
            String names = "";
            foreach (var VARIABLE in getcompany)
            {
                names += VARIABLE.name + "\t" + VARIABLE.code + "\n";
            }

            return names;
        }

        public int sum(int v)
        {
            Thread.Sleep(3000);
            //_transalte = new transalte();
            //_transalte.getTranslate(word, target);
            return 3 * v;
        }

        public int add(int v)
        {
            return 3 * v;
        }

        public void show(int v)
        {
            var form = new Form();
            form.ShowInTaskbar = false;
            
            form.LayoutMdi(MdiLayout.TileHorizontal);
            form.Width = 400;
            form.Height = 400;
            var label = new Label();
            label.Text = "hasif";
            label.Width = 100;
            label.Height = 100;
            label.Location = new Point(10, 50);
            var button = new Button();
            button.Location = new Point(150, 150);
            button.Width = 100;
            button.Height = 100;
            form.Controls.Add(button);
            form.Controls.Add(label);
            form.Show();
        }
    }


    public class transalte
    {
        public string getTranslate(String word, String target)
        {
            var urlEncode = HttpUtility.UrlEncode(word);
            var client = new RestClient("https://google-translate1.p.rapidapi.com/language/translate/v2");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("accept-encoding", "application/gzip");
            request.AddHeader("x-rapidapi-key", "38d4d43118msheba70f652783c8fp1f7388jsn78a8a1a47865");
            request.AddHeader("x-rapidapi-host", "google-translate1.p.rapidapi.com");
            request.AddParameter("application/x-www-form-urlencoded", $"q={word}&source=en&target={target}",
                ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var responseContent = response.Content;
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseContent);
            var translatedText = myDeserializedClass.data.translations[0].translatedText;
            // DialogResult result;
            // result = MessageBox.Show(translatedText, translatedText, MessageBoxButtons.YesNo);
            // if (result == System.Windows.Forms.DialogResult.Yes)
            // {
            //     // Closes the parent form.
            //     // this.Close();
            // }

            //Console.OutputEncoding = System.Text.Encoding.UTF8;
            // Console.WriteLine(translatedText);

            return translatedText;
        }

        public List<Company> getcompany()
        {
            var client = new RestClient("http://148.72.210.101:8084/company");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            List<Company> myDeserializedClass = JsonConvert.DeserializeObject<List<Company>>(response.Content);
            return myDeserializedClass;
        }
    }

    public class Company
    {
        public string code { get; set; }
        public string name { get; set; }
        public string customerCode { get; set; }
        public string date { get; set; }
    }


    public static class StringExtensions
    {
        public static string ToUTF8(this string text)
        {
            return Encoding.UTF8.GetString(Encoding.Default.GetBytes(text));
        }
    }

    public class Translation
    {
        public string translatedText { get; set; }
    }

    public class Data
    {
        public List<Translation> translations { get; set; }
    }

    public class Root
    {
        public Data data { get; set; }
    }
}