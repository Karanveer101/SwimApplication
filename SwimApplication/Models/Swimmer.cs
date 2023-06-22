using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SwimApplication.Models
{
    public class Swimmer
    {

        [Key]
        
        public int SwimmerID { get; set; }
        public string SwimmerName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
    }

        public enum Gender
    {
        Male,
        Female,
        Other
    }

    public class SwimmerDto
    {
        public int SwimmerID { get; set; }
        public string SwimmerName { get; set; }
        public int Age { get; set; }
        public Genders Gender { get; set; }
    }

    public enum Genders
    {
        Male,
        Female,
        Other
    }
}


