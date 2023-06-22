using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwimApplication.Models.ViewModels
{
    public class UpdateSession
    {
        //This viewmodel is a class which stores information that we need to present to /Session/Update/{}

        //the existing session information

        public SessionDto SelectedSession { get; set; }


    }
}