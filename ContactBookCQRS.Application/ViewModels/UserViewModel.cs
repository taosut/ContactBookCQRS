using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContactBookCQRS.Application.ViewModels
{
    public class UserViewModel
    {
        public Guid ContactBookId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        [JsonIgnore]
        public bool LoginSucceeded { get; set; }
        [JsonIgnore]
        public string LoginErrorMessage { get; set; }
    }
}
