using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Models;
using genshinwebsite.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using genshinwebsite.ViewModels;
using System.Data;



namespace genshinwebsite.Services
{
    public class MusicDBService : IMusicDB<MusicModel, MusicViewModel>
    {
        private readonly MusicDataContext _musicDataContext;
        public MusicDBService(MusicDataContext musicDataContext)
        {
            _musicDataContext = musicDataContext;
        }



        public async Task<MusicModel> add_one(MusicModel musicModel)
        {
            MusicModel rtn_model;
            try
            {
                var model = _musicDataContext.Add(musicModel);
                rtn_model = model.Entity;
                await _musicDataContext.SaveChangesAsync();
            }
            catch (DbUpdateException exp)
            {
                Console.WriteLine(exp.Message);
                return null;
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp.Message);
                return null;
            }
            return rtn_model;
        }

        public DBOperationResult view_one(MusicModel musicModel)
        {
            try
            {
                _musicDataContext.Update(musicModel);
                _musicDataContext.SaveChanges();
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



        public DBOperationResult delete_ones_by_uid(int uid)
        {
            int t;
            var musics = get_by_uid(uid, out t);
            try
            {
                _musicDataContext.RemoveRange(musics);
                _musicDataContext.SaveChangesAsync();
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

        public DBOperationResult delete_one_by_id(int muid, int uid)
        {
            var music = _musicDataContext.Music.Where(m =>  m.Id == muid &&  m.User_id == uid).ToList(); 
            if(music.Count == 0)
            {
                return DBOperationResult.ERROR;
            }
            try
            {
                _musicDataContext.Music.Remove(music[0]);
                _musicDataContext.SaveChanges();
            }
            catch (DbUpdateException exp)
            {
                Console.WriteLine(exp.Message);
                return DBOperationResult.DbUpdateException;
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp.Message);
                return DBOperationResult.ERROR;
            }

            return DBOperationResult.OK;
            
        }

        public async Task<IEnumerable<MusicModel>> get_all()
        {
            return await _musicDataContext.Music.AsNoTracking().ToListAsync();
        }

        public async Task<MusicViewModel> get_by_id(int id)
        {
            var music = await _musicDataContext.Music.Join(_musicDataContext.AspNetUsers, music => music.User_id, user => user.Id, (music, user) => new MusicViewModel
            {
                Id = music.Id,
                MusicTitle = music.MusicTitle,
                Abstract_content = music.Abstract_content,
                Datetime = music.Datetime,
                View_num = music.View_num,
                Download_num = music.Download_num,
                Uid = music.User_id,
                Uploader = user.UserName

            }).Where(music=>music.Id == id).FirstOrDefaultAsync();

            return music;
        }

        public IEnumerable<MusicModel> get_by_uid(int uid, out int max_item_num, int num_per_page = 10, int page_offset = 0, MUSIC_SELECT_ORDER select_order = MUSIC_SELECT_ORDER.UPLOAD_DATE)
        {
            page_offset = Math.Max(page_offset, 0);
            num_per_page = Math.Max(num_per_page, 1);
            IEnumerable<MusicModel> result = null;
            var content = _musicDataContext.Music.AsNoTracking().Where(m => m.User_id == uid);

            max_item_num = content.Count();

            switch (select_order)
            {
                case MUSIC_SELECT_ORDER.UPLOAD_DATE:
                    result = content.OrderByDescending(m => m.Datetime).Skip(num_per_page * page_offset).Take(num_per_page);
                    break;
                case MUSIC_SELECT_ORDER.TITLE:
                    result = content.OrderBy(m => m.MusicTitle).Skip(num_per_page * page_offset).Take(num_per_page);
                    break;
                case MUSIC_SELECT_ORDER.VIEW_NUM:
                    result = content.OrderByDescending(m => m.View_num).Skip(num_per_page * page_offset).Take(num_per_page);
                    break;
                case MUSIC_SELECT_ORDER.DOWNLOAD_NUM:
                    result = content.OrderByDescending(m => m.Download_num).Skip(num_per_page * page_offset).Take(num_per_page);
                    break;
                default:
                    result = content.OrderByDescending(m => m.Datetime).Skip(num_per_page * page_offset).Take(num_per_page);
                    break;

            }

            return result;
        }

        public int get_item_count(string music_title = "")
        {
            var content = _musicDataContext.Music.AsNoTracking();
            if (music_title != null && music_title != string.Empty && music_title != "")
            {
                content = content.Where(m => m.MusicTitle.Contains(music_title));
            }
            return content.Count();
        }

        public async Task<int> get_user_today_upload_num(int uid)
        {
            var num = await _musicDataContext.Music.AsNoTracking().Where(m => m.User_id == uid).ToListAsync();
            return num.Count;
        }

        public async Task<IEnumerable<MusicViewModel>> get_music_by_offset(int num_per_page = 10, int page_offset = 0, string music_title = "", MUSIC_SELECT_ORDER select_order = MUSIC_SELECT_ORDER.UPLOAD_DATE)
        {
            page_offset = Math.Max(page_offset, 0);
            num_per_page = Math.Max(num_per_page, 1);
            IEnumerable<MusicViewModel> result = null;

            var content = _musicDataContext.Music.AsNoTracking();
            if (music_title != null && music_title != string.Empty && music_title != "")
            {
                content = content.Where(m => m.MusicTitle.Contains(music_title));
            }
            

            switch (select_order)
            {
                case MUSIC_SELECT_ORDER.UPLOAD_DATE:
                    result = await content.OrderByDescending(m => m.Datetime).Skip(num_per_page * page_offset).Take(num_per_page).Join(_musicDataContext.AspNetUsers, music => music.User_id, user => user.Id, (music, user) => new MusicViewModel
                    {
                        Id = music.Id,
                        MusicTitle = music.MusicTitle,
                        Abstract_content = music.Abstract_content,
                        Datetime = music.Datetime,
                        View_num = music.View_num,
                        Download_num = music.Download_num,
                        Uid = music.User_id,
                        Uploader = user.UserName

                    }).ToListAsync();
                    break;
                case MUSIC_SELECT_ORDER.TITLE:
                    result = await content.OrderBy(m => m.MusicTitle).Skip(num_per_page * page_offset).Take(num_per_page).Join(_musicDataContext.AspNetUsers, music => music.User_id, user => user.Id, (music, user) => new MusicViewModel
                    {
                        Id = music.Id,
                        MusicTitle = music.MusicTitle,
                        Abstract_content = music.Abstract_content,
                        Datetime = music.Datetime,
                        View_num = music.View_num,
                        Download_num = music.Download_num,
                        Uid = music.User_id,
                        Uploader = user.UserName

                    }).ToListAsync();
                    break;
                case MUSIC_SELECT_ORDER.VIEW_NUM:
                    result = await content.OrderByDescending(m => m.View_num).Skip(num_per_page * page_offset).Take(num_per_page).Join(_musicDataContext.AspNetUsers, music => music.User_id, user => user.Id, (music, user) => new MusicViewModel
                    {
                        Id = music.Id,
                        MusicTitle = music.MusicTitle,
                        Abstract_content = music.Abstract_content,
                        Datetime = music.Datetime,
                        View_num = music.View_num,
                        Download_num = music.Download_num,
                        Uid = music.User_id,
                        Uploader = user.UserName

                    }).ToListAsync();
                    break;
                case MUSIC_SELECT_ORDER.DOWNLOAD_NUM:
                    result = await content.OrderByDescending(m => m.Download_num).Skip(num_per_page * page_offset).Take(num_per_page).Join(_musicDataContext.AspNetUsers, music => music.User_id, user => user.Id, (music, user) => new MusicViewModel
                    {
                        Id = music.Id,
                        MusicTitle = music.MusicTitle,
                        Abstract_content = music.Abstract_content,
                        Datetime = music.Datetime,
                        View_num = music.View_num,
                        Download_num = music.Download_num,
                        Uid = music.User_id,
                        Uploader = user.UserName

                    }).ToListAsync();
                    break;
                default:
                    result = await content.OrderByDescending(m => m.Datetime).Skip(num_per_page * page_offset).Take(num_per_page).Join(_musicDataContext.AspNetUsers, music => music.User_id, user => user.Id, (music, user) => new MusicViewModel
                    {
                        Id = music.Id,
                        MusicTitle = music.MusicTitle,
                        Abstract_content = music.Abstract_content,
                        Datetime = music.Datetime,
                        View_num = music.View_num,
                        Download_num = music.Download_num,
                        Uid = music.User_id,
                        Uploader = user.UserName

                    }).ToListAsync();
                    break;

            }
            return result;
        }
        public async Task<DBOperationResult> add_or_set_download_num(int muid, int set_num = -1)
        {
            try { 
           
                var mu = await _musicDataContext.Music.FindAsync(muid);
                if(mu == null)
                {
                    return DBOperationResult.ERROR;
                }
                if (set_num >= 0)
                {
                    mu.Download_num = set_num;
                }
                else
                {
                    mu.Download_num += 1;
                
                }
                _musicDataContext.Music.Update(mu);
                await _musicDataContext.SaveChangesAsync();
                return DBOperationResult.OK;
            }
            catch
            {
                return DBOperationResult.DbUpdateException;
            }


        }

        public async Task<DBOperationResult> update_music(MusicModel music_model)
        {
            try
            {
                _musicDataContext.Update(music_model);
                await _musicDataContext.SaveChangesAsync();
                return DBOperationResult.OK;
            }
            catch
            {
                return DBOperationResult.DbUpdateException;
            }
            
        }
    }
}
