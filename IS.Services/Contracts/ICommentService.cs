using IS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Services.Contracts
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsAsync();
        Task<Comment> GetCommentAsync(long id);
        Task<Comment> CreateCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(long id);
    }
}
