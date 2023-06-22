using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwimApplication.Models
{
    public class Session
    {
        [Key]
        public int SessionID { get; set; }

        public string Title { get; set; }
        public DateTime Date { get; set; }

        //distance is in meters
        public int Distance { get; set; }

        public TimeSpan Duration { get; set; }

        //a swim session belongs to one swimmer
        //a swimmer can have many swim sessions
        [ForeignKey("Swimmer")]
        
        public int SwimmerId { get; set; }

        public virtual Swimmer Swimmer { get; set; }

        public Strokes StrokeType { get; set; }

        //references different types of strokes
        public enum Strokes
        {
            Freestyle,
            Backstroke,
            Breaststroke,
            Butterfly,
            SideStroke
        }
    }

    public class SessionDto
    {
        public int SessionID { get; set; }

        public string Title { get; set; }
        public DateTime Date { get; set; }

        public int Distance { get; set; }

        public TimeSpan Duration { get; set; }

        public Strokes StrokeType { get; set; }

        public string SwimmerName { get; set; }

        //references different types of strokes
        public enum Strokes
        {
            Freestyle,
            Backstroke,
            Breaststroke,
            Butterfly,
            SideStroke
        }

    }
}