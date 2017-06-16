using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Models.SuperHui
{
    public class EFCommentRepository : ICommentRepository
    {
        private SuperHuiDbContext context;
        public EFCommentRepository(SuperHuiDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Comment> Comments => context.Comments;

        public Comment DeleteComment(int commentID)
        {
            Comment dbEntry = context.Comments.FirstOrDefault(c => c.CommentID == commentID);
            if (dbEntry != null)
            {
                context.Comments.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Comment comment)
        {
            if (comment.CommentID == 0)
            {
                context.Comments.Add(comment);
            }
            else
            {
                Comment dbEntry = context.Comments.FirstOrDefault(c => c.CommentID == comment.CommentID);
                if (dbEntry != null)
                {
                    dbEntry.Content = comment.Content;
                }
            }
            context.SaveChanges();
        }
    }
}
