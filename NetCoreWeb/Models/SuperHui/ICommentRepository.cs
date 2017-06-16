using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Models.SuperHui
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> Comments { get; }
        void SaveProduct(Comment comment);
        Comment DeleteComment(int commentID);
    }
}
