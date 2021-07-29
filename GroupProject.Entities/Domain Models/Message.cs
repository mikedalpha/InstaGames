using System;

namespace GroupProject.Entities.Domain_Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public DateTime SubmitDate { get; set; }

        public ApplicationUser Creator { get; set; }

    }
}
