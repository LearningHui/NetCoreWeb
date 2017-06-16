using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Models.SuperHui.ViewModels
{
    public class CommentsListViewModel
    {
        public IEnumerable<Comment> Comments { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
