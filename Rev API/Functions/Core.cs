using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using RevAPI.Requests;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace RevAPI.Functions
{
    public class Core
    {
        public static string PackedFile()
        {
            string version = GetVersion().version;
            if (System.IO.File.Exists($"{Variables.environment.WebRootPath}\\{version}_Packed.zip"))
            {
                return $"{version}_Packed.zip";
            }
            var json = LeagueOfLegends.GetData();
            System.IO.File.WriteAllText($"{Variables.environment.WebRootPath}\\gameContent.json", JsonConvert.SerializeObject(json));
            using (var zip = new Ionic.Zip.ZipFile())
            {
                zip.AddDirectory($"{Variables.environment.WebRootPath}\\champions\\", "champions");
                zip.AddFile($"{Variables.environment.WebRootPath}\\gameContent.json", "\\");
                zip.Save($"{Variables.environment.WebRootPath}\\{version}_Packed.zip");
            }
            return $"{version}_Packed.zip";
        }
        public static RevAPI.Objects.Version GetVersion()
        {
            RevAPI.Objects.Version version = LeagueOfLegends.Version();
            if (System.IO.File.Exists($"{Variables.environment.WebRootPath}\\version.json"))
            {
                RevAPI.Objects.Version localVersion = JsonConvert.DeserializeObject<RevAPI.Objects.Version>(System.IO.File.ReadAllText($"{Variables.environment.WebRootPath}\\version.json"));
                if (version.version == "0")
                {
                    return localVersion;
                }
                if (version.version != localVersion.version)
                {
                    System.IO.File.WriteAllText($"{Variables.environment.WebRootPath}\\version.json", JsonConvert.SerializeObject(version));
                    return version;
                }
            }
            else
            {
                System.IO.File.WriteAllText($"{Variables.environment.WebRootPath}\\version.json", JsonConvert.SerializeObject(version));
            }
            return version;
        }
    }
}
