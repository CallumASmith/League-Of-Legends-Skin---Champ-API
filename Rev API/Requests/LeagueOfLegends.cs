using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RevAPI.Functions;
using RevAPI.Objects;
using RevAPI.Objects.JSON;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Version = RevAPI.Objects.Version;
namespace RevAPI.Requests
{
    public class LeagueOfLegends
    {
        private static readonly string API_URL = "https://ddragon.leagueoflegends.com";
        public static Version Version()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{API_URL}/realms/euw.json");
                request.Method = "GET";
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var dataStream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(dataStream))
                        {
                            return new Version(JsonConvert.DeserializeObject<RealmData>(reader.ReadToEnd()).v);
                        }
                    }
                }
            }
            catch (WebException e)
            {
                Debug.Write(e.Status);
            }
                return new Version("0");
        }
        public static GameContent GetData()
        {
            List<Champion> champions = new List<Champion>();
            List<Skin> skins = new List<Skin>();
            string responseContent = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{API_URL}/cdn/{Core.GetVersion().version}/data/en_US/championFull.json");
                request.Method = "GET";
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var dataStream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(dataStream))
                        {
                            responseContent = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException e)
            {
                Debug.WriteLine(e.Status);
                return null;
            }
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
            foreach (JProperty champion in jsonObject.keys)
            {
                champions.Add(new Champion(champion.Value.ToString(), champion.Name));
                if (!System.IO.Directory.Exists($"{Variables.environment.WebRootPath}\\champions\\"))
                {
                    System.IO.Directory.CreateDirectory($"{Variables.environment.WebRootPath}\\champions\\");
                }
                if (!System.IO.File.Exists($"{Variables.environment.WebRootPath}\\champions\\{champion.Value.ToString()}_0.jpg"))
                {
                    new System.Net.WebClient().DownloadFile($"{API_URL}/cdn/img/champion/loading/{champion.Value.ToString()}_0.jpg", $"{Variables.environment.WebRootPath}\\champions\\{champion.Value.ToString()}_0.jpg");
                }
            }
            foreach (JProperty champion in jsonObject.data)
            {
                string name = champion.Name;
                if (champion.Name == "Fiddlesticks")
                {
                    name = "FiddleSticks";
                }
                foreach (dynamic skin in JsonConvert.DeserializeObject<dynamic>(champion.Value.ToString()).skins)
                {
                    
                    skins.Add(new Skin { championName = champion.Name, count = skin.num, id = skin.id, skinName = skin.name, image = $"/champions/{champion.Name}_{skin.num}.jpg" });
                    if (!System.IO.Directory.Exists($"{Variables.environment.WebRootPath}\\champions\\"))
                    {
                        System.IO.Directory.CreateDirectory($"{Variables.environment.WebRootPath}\\champions\\");
                    }
                    if (!System.IO.File.Exists($"{Variables.environment.WebRootPath}\\champions\\{name}_{skin.num}.jpg"))
                    {
                        new System.Net.WebClient().DownloadFile($"{API_URL}/cdn/img/champion/loading/{name}_{skin.num}.jpg", $"{Variables.environment.WebRootPath}\\champions\\{name}_{skin.num}.jpg");
                    }
                }
            }
            return new GameContent(champions, skins);

        }
    }
}
