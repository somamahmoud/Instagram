using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Instagram.viewModels
{
    public class post_comment
    {
        public List<Post> post { get; set; }
        public List<Comment> comments { get; set; }
    }
}