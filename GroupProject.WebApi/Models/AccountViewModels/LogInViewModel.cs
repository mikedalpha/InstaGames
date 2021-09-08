using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.WebApi.Models.AccountViewModels
{
    public class LogInViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public string Role { get; set; }
    }
}