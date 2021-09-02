﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupProject.Entities.Domain_Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GroupProject.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public Plan? SubscribePlan { get; set; }
        public bool IsSubscribed { get; set; }
        public DateTime? SubscriptionDay { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? ExpireDate { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Game> UserList { get; set; }
        public virtual ICollection<UserGameRatings> UserGameRatings { get; set; }

        public ApplicationUser()
        {
            Messages = new HashSet<Message>();
            UserList = new HashSet<Game>();
            UserGameRatings = new HashSet<UserGameRatings>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
           
            return userIdentity;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
         
            return userIdentity;
        }
    }
}