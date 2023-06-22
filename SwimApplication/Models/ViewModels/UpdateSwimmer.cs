using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwimApplication.Models.ViewModels
{
    public class UpdateSwimmer
    {
        //This viewmodel is a class which stores information that we need to present to /Swimmer/Update/{}

        //the existing swimmer information

        public SwimmerDto SelectedSwimmer { get; set; }
    }
}