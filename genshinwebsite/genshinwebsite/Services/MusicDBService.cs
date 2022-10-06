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
        public int get_music_count()
        {
            return _musicDataContext.Music.AsNoTracking().Count();
        }
        public DBOperationResult add_one(MusicModel musicModel)
        {
            try
            {
                _musicDataContext.Add(musicModel);
                _musicDataContext.SaveChanges();
            }
            catch (DbUpdateException exp)
            {
                return DBOperationResult.DbUpdateException;
            }
            catch(Exception exp)
            {
                return DBOperationResult.ERROR;
            }
            return DBOperationResult.OK;
        }

        public DBOperationResult delete_ones_by_uid(int uid)
        {
            var musics = get_by_uid(uid);
            try
            {
                _musicDataContext.RemoveRange(musics);
                _musicDataContext.SaveChanges();
            }
            catch (DbUpdateException exp)
            {
                return DBOperationResult.DbUpdateException;
            }
            catch (Exception exp)
            {
                return DBOperationResult.ERROR;
            }
            return DBOperationResult.OK;
        }

        public DBOperationResult delete_one_by_id(int id)
        {
            var music = new MusicModel()
            {
                Id = id
            };
            try
            {
                _musicDataContext.Music.Remove(music);
                _musicDataContext.SaveChanges();
            }
            catch (DbUpdateException exp)
            {
                return DBOperationResult.DbUpdateException;
            }
            catch(Exception exp)
            {
                return DBOperationResult.ERROR;
            }

            return DBOperationResult.OK;
            
        }

        public IEnumerable<MusicModel> get_all()
        {
            return _musicDataContext.Music.AsNoTracking().ToList();
        }

        public MusicModel get_by_id(int id)
        {
            return _musicDataContext.Music.Find(id);
        }

        public IEnumerable<MusicModel> get_by_uid(int uid, int num_per_page = 10, int page_offset = 0, MUSIC_SELECT_ORDER select_order = MUSIC_SELECT_ORDER.UPLOAD_DATE)
        {
            page_offset = Math.Max(page_offset, 0);
            num_per_page = Math.Max(num_per_page, 1);
            IEnumerable<MusicModel> result = null;
            
            switch (select_order)
            {
                case MUSIC_SELECT_ORDER.UPLOAD_DATE:
                    result = _musicDataContext.Music.AsNoTracking().OrderByDescending(m => m.Datetime).Skip(num_per_page * page_offset).Take(num_per_page).Where(m => m.User_id == uid).ToList();
                    break;
                case MUSIC_SELECT_ORDER.TITLE:
                    result = _musicDataContext.Music.AsNoTracking().OrderBy(m => m.MusicTitle).Skip(num_per_page * page_offset).Take(num_per_page).Where(m => m.User_id == uid).ToList();
                    break;
                case MUSIC_SELECT_ORDER.VIEW_NUM:
                    result = _musicDataContext.Music.AsNoTracking().OrderByDescending(m => m.View_num).Skip(num_per_page * page_offset).Take(num_per_page).Where(m => m.User_id == uid).ToList();
                    break;
                default:
                    result = _musicDataContext.Music.AsNoTracking().OrderByDescending(m => m.Datetime).Skip(num_per_page * page_offset).Take(num_per_page).Where(m => m.User_id == uid).ToList();
                    break;

            }

            return result;
        }


        public IEnumerable<MusicViewModel> get_music_by_offset(out int max_item_num, int num_per_page = 10, int page_offset = 0, string music_title = "", MUSIC_SELECT_ORDER select_order = MUSIC_SELECT_ORDER.UPLOAD_DATE)
        {
            page_offset = Math.Max(page_offset, 0);
            num_per_page = Math.Max(num_per_page, 1);
            IEnumerable<MusicViewModel> result = null;

            var content = _musicDataContext.Music.AsNoTracking();
            if(music_title != null && music_title != string.Empty && music_title != "")
            {
                content = content.Where(m => m.MusicTitle.Contains(music_title));
            }
            max_item_num = content.Count();
            
            //IQueryable<MusicModel> content = null;
            //if(music_title_list != null && music_title_list.Count > 0)
            //{
            //    string sql = "SELECT * FROM Music WHERE Music_Title=={0}";
            //    for(int i = 1; i < music_title_list.Count; i++)
            //    {
            //        sql += " || Music_Title=={" + i.ToString() + "}";
            //    }
            //    var a = new SqlParameter();
            //    //content = _musicDataContext.Music.FromSqlRaw(sql, new sqlpara);
            //}
            //else
            //{
            //    content = _musicDataContext.Music.AsNoTracking();
            //}
            switch (select_order)
            {
                case MUSIC_SELECT_ORDER.UPLOAD_DATE:
                    result = content.OrderByDescending(m => m.Datetime).Skip(num_per_page * page_offset).Take(num_per_page).Join(_musicDataContext.AspNetUsers, music => music.User_id, user => user.Id, (music, user) => new MusicViewModel
                    {
                        Id = music.Id,
                        MusicTitle = music.MusicTitle,
                        Abstract_content = music.Abstract_content,
                        Datetime = music.Datetime,
                        View_num = music.View_num,
                        Download_num = music.Download_num,
                        Uid = music.User_id,
                        Uploader = user.UserName

                    }).ToList();
                    break;
                case MUSIC_SELECT_ORDER.TITLE:
                    result = content.OrderBy(m => m.MusicTitle).Skip(num_per_page * page_offset).Take(num_per_page).Join(_musicDataContext.AspNetUsers, music => music.User_id, user => user.Id, (music, user) => new MusicViewModel
                    {
                        Id = music.Id,
                        Abstract_content = music.Abstract_content,
                        Datetime = music.Datetime,
                        View_num = music.View_num,
                        Download_num = music.Download_num,
                        Uid = music.User_id,
                        Uploader = user.UserName

                    }).ToList();
                    break;
                case MUSIC_SELECT_ORDER.VIEW_NUM:
                    result = content.OrderByDescending(m => m.View_num).Skip(num_per_page * page_offset).Take(num_per_page).Join(_musicDataContext.AspNetUsers, music => music.User_id, user => user.Id, (music, user) => new MusicViewModel
                    {
                        Id = music.Id,
                        Abstract_content = music.Abstract_content,
                        Datetime = music.Datetime,
                        View_num = music.View_num,
                        Download_num = music.Download_num,
                        Uid = music.User_id,
                        Uploader = user.UserName

                    }).ToList();
                    break;
                default:
                    result = content.OrderByDescending(m => m.Datetime).Skip(num_per_page * page_offset).Take(num_per_page).Join(_musicDataContext.AspNetUsers, music => music.User_id, user => user.Id, (music, user) => new MusicViewModel
                    {
                        Id = music.Id,
                        Abstract_content = music.Abstract_content,
                        Datetime = music.Datetime,
                        View_num = music.View_num,
                        Download_num = music.Download_num,
                        Uid = music.User_id,
                        Uploader = user.UserName

                    }).ToList();
                    break;

            }
            return result;
        }
        public DBOperationResult add_or_set_download_num(int muid, int set_num = -1)
        {
            try { 
           
                var mu = _musicDataContext.Music.Find(muid);
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
                _musicDataContext.SaveChanges();
                return DBOperationResult.OK;
            }
            catch
            {
                return DBOperationResult.DbUpdateException;
            }


        }
    }
}
