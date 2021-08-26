using System;

namespace GroupProject.Entities.Domain_Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public string Reply { get; set; }
        public DateTime SubmitDate { get; set; }
        public bool Answered { get; set; }

        public virtual ApplicationUser Creator { get; set; }
    }
}
