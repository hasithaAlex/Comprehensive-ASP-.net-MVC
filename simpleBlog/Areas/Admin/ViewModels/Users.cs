using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using simpleBlog.Models;

namespace simpleBlog.Areas.Admin.ViewModels
{
    public class UsersIndex
    {
        public IEnumerable<User> Users { get; set; } 
    }
}