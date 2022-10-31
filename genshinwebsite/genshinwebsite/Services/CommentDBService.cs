using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Models;
using genshinwebsite.ViewModels;
using genshinwebsite.Data;
using Microsoft.EntityFrameworkCore;

namespace genshinwebsite.Services
{
    public class CommentDBService : ICommentDB<CommentModel, CommentViewModel>
    {
        private readonly CommentDataContext _commentDataContext;

        public CommentDBService(CommentDataContext commentDataContext)
        {
            _commentDataContext = commentDataContext;
        }

        public async Task<DBOperationResult> add_comment(CommentModel comment)
        {
            try
            {
                _commentDataContext.Add(comment);
                await _commentDataContext.SaveChangesAsync();
            }
            catch (DbUpdateException exp)
            {
                Console.WriteLine(exp.Message);
                return DBOperationResult.DbUpdateException;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return DBOperationResult.ERROR;
            }
            return DBOperationResult.OK;
           
        }

        public async Task<DBOperationResult> delete_comment(CommentModel comment)
        {
            try
            {
                if(await _commentDataContext.Music.FindAsync(comment) != null)
                {
                    _commentDataContext.Remove(comment);
                    await _commentDataContext.SaveChangesAsync();
                }
                
            }
           
            catch (DbUpdateException exp)
            {
                Console.WriteLine(exp.Message);
                return DBOperationResult.DbUpdateException;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return DBOperationResult.ERROR;
            }

            return DBOperationResult.OK;
        }

        public DBOperationResult delete_comment(int uid, int muid)
        {
            try
            {
                var comment = new CommentModel
                {
                    Uid = uid,
                    Muid = muid
                };
                _commentDataContext.Remove(comment);
                _commentDataContext.SaveChanges();
            }

            catch (DbUpdateException exp)
            {
                Console.WriteLine(exp.Message);
                return DBOperationResult.DbUpdateException;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return DBOperationResult.ERROR;
            }

            return DBOperationResult.OK;
        }

        public async Task<DBOperationResult> delete_comments_by_uid(int uid)
        {
            try
            {
                var comment_list = await _commentDataContext.Comment.Where(c => c.Uid == uid).ToListAsync();
                if(comment_list.Count > 0)
                {
                    _commentDataContext.RemoveRange(comment_list);
                    await _commentDataContext.SaveChangesAsync();
                }
               
            }
            catch (DbUpdateException exp)
            {
                Console.WriteLine(exp.Message);
                return DBOperationResult.DbUpdateException;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return DBOperationResult.ERROR;
            }

            return DBOperationResult.OK;

        }

        public async Task<CommentModel> get_comment_by_uid_muid(int uid, int muid)
        {
            var comment = await _commentDataContext.Comment.AsNoTracking().Where(c => c.Uid == uid && c.Muid == muid).FirstOrDefaultAsync();
            return comment;

        }

        public async Task<IEnumerable<CommentViewModel>> get_comment_list(int muid, int num_per_page = 10, int page_offset = 0, COMMENT_SELECT_ORDER select_order  = COMMENT_SELECT_ORDER.UPLOAD_DATE)
        {
            page_offset = Math.Max(page_offset, 0);
            num_per_page = Math.Max(num_per_page, 1);
            IEnumerable<CommentViewModel> result = null;
            switch (select_order)
            {
                case COMMENT_SELECT_ORDER.UPLOAD_DATE:
                    result = await _commentDataContext.Comment.AsNoTracking().Where(c => c.Muid == muid).Skip(page_offset * num_per_page).Take(num_per_page).OrderBy(c => c.UploadDate).Join(_commentDataContext.AspNetUsers, comment=>comment.Uid, user=>user.Id, (comment, user)=> new CommentViewModel
                    { 
                        Uid = comment.Uid,
                        UserName = user.UserName,
                        Muid = comment.Muid,
                        CommentContent = comment.CommentContent,
                        UpdateDate = comment.UpdateDate,
                        UploadDate = comment.UploadDate,
                        UpdateDateStr = comment.UpdateDate.ToString("f"),
                        UploadDateStr = comment.UploadDate.ToString("f")
                        //UploadIP = comment.UploadIP

                    }).ToListAsync();
                    break;
                case COMMENT_SELECT_ORDER.UPDATE_DATE:
                    result = await _commentDataContext.Comment.AsNoTracking().Where(c => c.Muid == muid).Skip(page_offset * num_per_page).Take(num_per_page).OrderBy(c => c.UpdateDate).Join(_commentDataContext.AspNetUsers, comment => comment.Uid, user => user.Id, (comment, user) => new CommentViewModel
                    {
                        Uid = comment.Uid,
                        UserName = user.UserName,
                        Muid = comment.Muid,
                        CommentContent = comment.CommentContent,
                        UpdateDate = comment.UpdateDate,
                        UploadDate = comment.UploadDate,
                        UpdateDateStr = comment.UpdateDate.ToString("f"),
                        UploadDateStr = comment.UploadDate.ToString("f")
                        //UploadIP = comment.UploadIP

                    }).ToListAsync(); 
                    break;
                default:
                    result = await _commentDataContext.Comment.AsNoTracking().Where(c => c.Muid == muid).Skip(page_offset * num_per_page).Take(num_per_page).OrderBy(c => c.UploadDate).Join(_commentDataContext.AspNetUsers, comment => comment.Uid, user => user.Id, (comment, user) => new CommentViewModel
                    {
                        Uid = comment.Uid,
                        UserName = user.UserName,
                        Muid = comment.Muid,
                        CommentContent = comment.CommentContent,
                        UpdateDate = comment.UpdateDate,
                        UploadDate = comment.UploadDate,
                        UpdateDateStr = comment.UpdateDate.ToString("f"),
                        UploadDateStr = comment.UploadDate.ToString("f")
                        //UploadIP = comment.UploadIP

                    }).ToListAsync();
                    break;
            }
            return result;
        }

        public async Task<DBOperationResult> update_comment(CommentModel comment)
        {
            try
            {
                _commentDataContext.Comment.Update(comment);
                await _commentDataContext.SaveChangesAsync();
            }
             catch (DbUpdateException exp)
            {
                Console.WriteLine(exp.Message);
                return DBOperationResult.DbUpdateException;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return DBOperationResult.ERROR;
            }
            return DBOperationResult.OK;
        }
    }
}
