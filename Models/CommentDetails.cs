using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class CommentDetails
    {
        public int CommentId { get; set; }
        public int TaskId { get; set; }
        public string Body { get; set; }
        public int EnteredById { get; set; }
        public System.DateTime DateAdded { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}