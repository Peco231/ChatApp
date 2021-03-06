﻿using System;

namespace ChatApp.Models
{
    public class Message
    {
        public Message()
        {
            Time = DateTime.Now;
        }

        public string MessageId { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        public virtual ApplicationUser Receiver { get; set; }
    }
}
