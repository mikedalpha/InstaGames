﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.Database.EntitiesConfiguration
{
    internal class MessageConfig :EntityTypeConfiguration<Message>
    {
        public MessageConfig()
        {
            Property(m => m.Text)
                .IsRequired()
                .HasMaxLength(80000);

            Property(m => m.SubmitDate)
                .IsRequired()
                .HasColumnType("date");

            //One to many 
            HasRequired(a => a.Creator)
                .WithMany(m => m.Messages)
                .WillCascadeOnDelete();
        }
    }
}
