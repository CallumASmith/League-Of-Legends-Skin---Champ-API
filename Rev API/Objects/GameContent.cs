using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevAPI.Objects
{
    public class GameContent
    {
        public GameContent(List<Champion> champs, List<Skin> skins)
        {
            this.version = Requests.LeagueOfLegends.Version().version;
            this.champions = champs;
            this.skins = skins;
        }
        public string version { get; set; }
        public List<Champion> champions { get; set; }
        public List<Skin> skins { get; set; }
    }
}
