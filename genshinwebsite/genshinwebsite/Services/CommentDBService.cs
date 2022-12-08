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

        public int get_item_count(string content)
        {
            var content_db = _commentDataContext.Comment.AsNoTracking();
            if (content != null && content != string.Empty && content != "")
            {
                content_db = content_db.Where(m => m.CommentContent.Contains(content));
            }
            return content_db.Count();
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
                if(await _commentDataContext.Comment.AsNoTracking().FirstOrDefaultAsync(c=>c.Uid == comment.Uid && c.Muid == comment.Muid) != null)
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

        public async Task<CommentViewModel> get_comment_by_uid_muid(int uid, int muid)
        {
            var comment = await _commentDataContext.Comment.AsNoTracking().Where(c => c.Uid == uid && c.Muid == muid).Join(_commentDataContext.AspNetUsers, c => c.Uid, u => u.Id, (comment, user) => new CommentViewModel
            {
                Uid = comment.Uid,
                UserName = user.UserName,
                Muid = comment.Muid,
                CommentContent = comment.CommentContent,
                UpdateDate = comment.UpdateDate,
                UploadDate = comment.UploadDate,
                UpdateDateStr = comment.UpdateDate.ToString("f"),
                UploadDateStr = comment.UploadDate.ToString("f")
            }).FirstOrDefaultAsync();
            return comment;

        }

        public async Task<IEnumerable<CommentViewModel>> get_comment_list(int muid, int num_per_page = 10, int page_offset = 0, COMMENT_SELECT_ORDER select_order  = COMMENT_SELECT_ORDER.COMMENT_UPDATE_DATE_ORDER)
        {
            page_offset = Math.Max(page_offset, 0);
            num_per_page = Math.Max(num_per_page, 1);
            IEnumerable<CommentViewModel> result = null;
            switch (select_order)
            {
                case COMMENT_SELECT_ORDER.COMMENT_UPLOAD_DATE_ORDER:
                    result = await _commentDataContext.Comment.Where(c => c.Muid == muid).Skip(page_offset * num_per_page).Take(num_per_page).OrderBy(c => c.UploadDate).Join(_commentDataContext.AspNetUsers, comment=>comment.Uid, user=>user.Id, (comment, user)=> new CommentViewModel
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
                case COMMENT_SELECT_ORDER.COMMENT_UPDATE_DATE_ORDER:
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
                case COMMENT_SELECT_ORDER.MUSICSHEET_UPDATE_ORDER:
                    result = await _commentDataContext.Comment.AsNoTracking().Where(c => c.Muid == muid).Skip(page_offset * num_per_page).Take(num_per_page)
                        .Join(_commentDataContext.Music, comment=>comment.Muid, music => music.Id, (comment, music) => new { comment, music.Datetime })
                        .OrderByDescending(r => r.Datetime)
                        .Join(_commentDataContext.AspNetUsers, r => r.comment.Uid, user => user.Id, (r, user) => new CommentViewModel
                        {
                            Uid = r.comment.Uid,
                            UserName = user.UserName,
                            Muid = r.comment.Muid,
                            CommentContent = r.comment.CommentContent,
                            UpdateDate = r.comment.UpdateDate,
                            UploadDate = r.comment.UploadDate,
                            UpdateDateStr = r.comment.UpdateDate.ToString("f"),
                            UploadDateStr = r.comment.UploadDate.ToString("f")
                            //UploadIP = comment.UploadIP
                        })
                        .ToListAsync();
                    break;
                case COMMENT_SELECT_ORDER.MUSICSHEET_TITLE_ORDER:
                    result = await _commentDataContext.Comment.AsNoTracking().Where(c => c.Muid == muid).Skip(page_offset * num_per_page).Take(num_per_page)
                        .Join(_commentDataContext.Music, comment => comment.Muid, music => music.Id, (comment, music) => new { comment, music.MusicTitle })
                        .OrderBy(r => r.MusicTitle)
                        .Join(_commentDataContext.AspNetUsers, r => r.comment.Uid, user => user.Id, (r, user) => new CommentViewModel
                        {
                            Uid = r.comment.Uid,
                            UserName = user.UserName,
                            Muid = r.comment.Muid,
                            CommentContent = r.comment.CommentContent,
                            UpdateDate = r.comment.UpdateDate,
                            UploadDate = r.comment.UploadDate,
                            UpdateDateStr = r.comment.UpdateDate.ToString("f"),
                            UploadDateStr = r.comment.UploadDate.ToString("f")
                            //UploadIP = comment.UploadIP
                        })
                        .ToListAsync();
                    break;
                case COMMENT_SELECT_ORDER.COMMENT_UID_ORDER:
                    result = await _commentDataContext.Comment.AsNoTracking().Where(c => c.Muid == muid).Skip(page_offset * num_per_page).Take(num_per_page)
                        .Join(_commentDataContext.AspNetUsers, c => c.Uid, user => user.Id, (c, user) => new CommentViewModel
                        {
                            Uid = c.Uid,
                            UserName = user.UserName,
                            Muid = c.Muid,
                            CommentContent = c.CommentContent,
                            UpdateDate = c.UpdateDate,
                            UploadDate = c.UploadDate,
                            UpdateDateStr = c.UpdateDate.ToString("f"),
                            UploadDateStr = c.UploadDate.ToString("f")
                            //UploadIP = comment.UploadIP
                        })
                        .OrderBy(c => c.Uid)
                        .ToListAsync();
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

        public async Task<IEnumerable<CommentViewModel>> get_comment_list_all(int num_per_page = 10, int page_offset = 0, COMMENT_SELECT_ORDER select_order = COMMENT_SELECT_ORDER.COMMENT_UPDATE_DATE_ORDER)
        {
            page_offset = Math.Max(page_offset, 0);
            num_per_page = Math.Max(num_per_page, 1);
            IEnumerable<CommentViewModel> result = null;
            switch (select_order)
            {
                case COMMENT_SELECT_ORDER.COMMENT_UPLOAD_DATE_ORDER:
                    result = await _commentDataContext.Comment.Skip(page_offset * num_per_page).Take(num_per_page).OrderBy(c => c.UploadDate).Join(_commentDataContext.AspNetUsers, comment => comment.Uid, user => user.Id, (comment, user) => new CommentViewModel
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
                case COMMENT_SELECT_ORDER.COMMENT_UPDATE_DATE_ORDER:
                    result = await _commentDataContext.Comment.AsNoTracking().Skip(page_offset * num_per_page).Take(num_per_page).OrderBy(c => c.UpdateDate).Join(_commentDataContext.AspNetUsers, comment => comment.Uid, user => user.Id, (comment, user) => new CommentViewModel
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
                case COMMENT_SELECT_ORDER.MUSICSHEET_UPDATE_ORDER:
                    result = await _commentDataContext.Comment.AsNoTracking().Skip(page_offset * num_per_page).Take(num_per_page)
                        .Join(_commentDataContext.Music, comment => comment.Muid, music => music.Id, (comment, music) => new { comment, music.Datetime })
                        .OrderByDescending(r => r.Datetime)
                        .Join(_commentDataContext.AspNetUsers, r => r.comment.Uid, user => user.Id, (r, user) => new CommentViewModel
                        {
                            Uid = r.comment.Uid,
                            UserName = user.UserName,
                            Muid = r.comment.Muid,
                            CommentContent = r.comment.CommentContent,
                            UpdateDate = r.comment.UpdateDate,
                            UploadDate = r.comment.UploadDate,
                            UpdateDateStr = r.comment.UpdateDate.ToString("f"),
                            UploadDateStr = r.comment.UploadDate.ToString("f")
                            //UploadIP = comment.UploadIP
                        })
                        .ToListAsync();
                    break;
                case COMMENT_SELECT_ORDER.MUSICSHEET_TITLE_ORDER:
                    result = await _commentDataContext.Comment.AsNoTracking().Skip(page_offset * num_per_page).Take(num_per_page)
                        .Join(_commentDataContext.Music, comment => comment.Muid, music => music.Id, (comment, music) => new { comment, music.MusicTitle })
                        .OrderBy(r => r.MusicTitle)
                        .Join(_commentDataContext.AspNetUsers, r => r.comment.Uid, user => user.Id, (r, user) => new CommentViewModel
                        {
                            Uid = r.comment.Uid,
                            UserName = user.UserName,
                            Muid = r.comment.Muid,
                            CommentContent = r.comment.CommentContent,
                            UpdateDate = r.comment.UpdateDate,
                            UploadDate = r.comment.UploadDate,
                            UpdateDateStr = r.comment.UpdateDate.ToString("f"),
                            UploadDateStr = r.comment.UploadDate.ToString("f")
                            //UploadIP = comment.UploadIP
                        })
                        .ToListAsync();
                    break;
                case COMMENT_SELECT_ORDER.COMMENT_UID_ORDER:
                    result = await _commentDataContext.Comment.AsNoTracking().Skip(page_offset * num_per_page).Take(num_per_page)
                        .Join(_commentDataContext.AspNetUsers, c => c.Uid, user => user.Id, (c, user) => new CommentViewModel
                        {
                            Uid = c.Uid,
                            UserName = user.UserName,
                            Muid = c.Muid,
                            CommentContent = c.CommentContent,
                            UpdateDate = c.UpdateDate,
                            UploadDate = c.UploadDate,
                            UpdateDateStr = c.UpdateDate.ToString("f"),
                            UploadDateStr = c.UploadDate.ToString("f")
                            //UploadIP = comment.UploadIP
                        })
                        .OrderBy(c => c.Uid)
                        .ToListAsync();
                    break;
                default:
                    result = await _commentDataContext.Comment.AsNoTracking().Skip(page_offset * num_per_page).Take(num_per_page).OrderBy(c => c.UploadDate).Join(_commentDataContext.AspNetUsers, comment => comment.Uid, user => user.Id, (comment, user) => new CommentViewModel
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

        public async Task<IEnumerable<CommentViewModel>> search_comment_list(string search_content, int num_per_page = 10, int page_offset = 0, COMMENT_SELECT_ORDER select_order = COMMENT_SELECT_ORDER.COMMENT_UPDATE_DATE_ORDER)
        {
            page_offset = Math.Max(page_offset, 0);
            num_per_page = Math.Max(num_per_page, 1);
            IEnumerable<CommentViewModel> result = null;
            switch (select_order)
            {
                case COMMENT_SELECT_ORDER.COMMENT_UPLOAD_DATE_ORDER:
                    result = await _commentDataContext.Comment.Where(c=>c.CommentContent.Contains(search_content)).Skip(page_offset * num_per_page).Take(num_per_page).OrderBy(c => c.UploadDate).Join(_commentDataContext.AspNetUsers, comment => comment.Uid, user => user.Id, (comment, user) => new CommentViewModel
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
                case COMMENT_SELECT_ORDER.COMMENT_UPDATE_DATE_ORDER:
                    result = await _commentDataContext.Comment.Where(c => c.CommentContent.Contains(search_content)).AsNoTracking().Skip(page_offset * num_per_page).Take(num_per_page).OrderBy(c => c.UpdateDate).Join(_commentDataContext.AspNetUsers, comment => comment.Uid, user => user.Id, (comment, user) => new CommentViewModel
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
                case COMMENT_SELECT_ORDER.MUSICSHEET_UPDATE_ORDER:
                    result = await _commentDataContext.Comment.Where(c => c.CommentContent.Contains(search_content)).AsNoTracking().Skip(page_offset * num_per_page).Take(num_per_page)
                        .Join(_commentDataContext.Music, comment => comment.Muid, music => music.Id, (comment, music) => new { comment, music.Datetime })
                        .OrderByDescending(r => r.Datetime)
                        .Join(_commentDataContext.AspNetUsers, r => r.comment.Uid, user => user.Id, (r, user) => new CommentViewModel
                        {
                            Uid = r.comment.Uid,
                            UserName = user.UserName,
                            Muid = r.comment.Muid,
                            CommentContent = r.comment.CommentContent,
                            UpdateDate = r.comment.UpdateDate,
                            UploadDate = r.comment.UploadDate,
                            UpdateDateStr = r.comment.UpdateDate.ToString("f"),
                            UploadDateStr = r.comment.UploadDate.ToString("f")
                            //UploadIP = comment.UploadIP
                        })
                        .ToListAsync();
                    break;
                case COMMENT_SELECT_ORDER.MUSICSHEET_TITLE_ORDER:
                    result = await _commentDataContext.Comment.Where(c => c.CommentContent.Contains(search_content)).AsNoTracking().Skip(page_offset * num_per_page).Take(num_per_page)
                        .Join(_commentDataContext.Music, comment => comment.Muid, music => music.Id, (comment, music) => new { comment, music.MusicTitle })
                        .OrderBy(r => r.MusicTitle)
                        .Join(_commentDataContext.AspNetUsers, r => r.comment.Uid, user => user.Id, (r, user) => new CommentViewModel
                        {
                            Uid = r.comment.Uid,
                            UserName = user.UserName,
                            Muid = r.comment.Muid,
                            CommentContent = r.comment.CommentContent,
                            UpdateDate = r.comment.UpdateDate,
                            UploadDate = r.comment.UploadDate,
                            UpdateDateStr = r.comment.UpdateDate.ToString("f"),
                            UploadDateStr = r.comment.UploadDate.ToString("f")
                            //UploadIP = comment.UploadIP
                        })
                        .ToListAsync();
                    break;
                case COMMENT_SELECT_ORDER.COMMENT_UID_ORDER:
                    result = await _commentDataContext.Comment.Where(c => c.CommentContent.Contains(search_content)).AsNoTracking().Skip(page_offset * num_per_page).Take(num_per_page)
                        .Join(_commentDataContext.AspNetUsers, c => c.Uid, user => user.Id, (c, user) => new CommentViewModel
                        {
                            Uid = c.Uid,
                            UserName = user.UserName,
                            Muid = c.Muid,
                            CommentContent = c.CommentContent,
                            UpdateDate = c.UpdateDate,
                            UploadDate = c.UploadDate,
                            UpdateDateStr = c.UpdateDate.ToString("f"),
                            UploadDateStr = c.UploadDate.ToString("f")
                            //UploadIP = comment.UploadIP
                        })
                        .OrderBy(c => c.Uid)
                        .ToListAsync();
                    break;
                default:
                    result = await _commentDataContext.Comment.Where(c => c.CommentContent.Contains(search_content)).AsNoTracking().Skip(page_offset * num_per_page).Take(num_per_page).OrderBy(c => c.UploadDate).Join(_commentDataContext.AspNetUsers, comment => comment.Uid, user => user.Id, (comment, user) => new CommentViewModel
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
    }
}
