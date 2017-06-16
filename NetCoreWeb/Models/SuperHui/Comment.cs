using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Models.SuperHui
{
    //留言、评论
    public class Comment
    {
        public int CommentID { get; set; }
        [Required(ErrorMessage = "请输入留言内容")]
        public string Content { get; set; }
        public DateTime Time { get; set; }
    }
}
