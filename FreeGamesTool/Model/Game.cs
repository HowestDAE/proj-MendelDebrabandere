using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreeGamesTool.Model
{
    public class Game
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        //public string Status { get; set; }
        public string Short_Description { get; set; }
        public string Description { get; set; }
        //public string GameUrl { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        //public DateTime ReleaseDate { get; set; }
        //public string FreeToGameProfileUrl { get; set; }
        public SystemRequirements Minimum_System_Requirements { get; set; }
        public List<Screenshot> Screenshots { get; set; }

        public Game(int ID)
        {
            Id = ID;
            Title = "GAME NOT FOUND";
            Screenshots = new List<Screenshot>();
        }
    }

    public class SystemRequirements
    {
        public string OS { get; set; }
        public string Processor { get; set; }
        public string Memory { get; set; }
        public string Graphics { get; set; }
        public string Storage { get; set; }
    }

    public class Screenshot
    {
        public int Id { get; set; }
        public string Image { get; set; }
    }
}
