using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Models;
using genshinwebsite.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace genshinwebsite.Services
{
    public class MusicDBService : IMusicDB<MusicModel>
    {
        private readonly MusicDataContext _musicDataContext;
        public MusicDBService(MusicDataContext musicDataContext)
        {
            _musicDataContext = musicDataContext;
        }
        public int get_music_count()
        {
            return _musicDataContext.Music.Count();
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
            return _musicDataContext.Music.ToList();
        }

        public MusicModel get_by_id(int id)
        {
            return _musicDataContext.Music.Find(id);
        }

        public IEnumerable<MusicModel> get_by_uid(int uid, int num_per_page = 10, int page_offset = 0)
        {
            page_offset = Math.Max(page_offset, 0);
            num_per_page = Math.Max(num_per_page, 1);
            return _musicDataContext.Music.Skip(num_per_page*page_offset).Take(num_per_page).Where(m => m.User_id == uid).ToList();
        }


        public IEnumerable<MusicModel> get_music_by_offset(int num_per_page = 10, int page_offset = 0)
        {
            page_offset = Math.Max(page_offset, 0);
            num_per_page = Math.Max(num_per_page, 1);
            return _musicDataContext.Music.Skip(num_per_page * page_offset).Take(num_per_page).ToList();
        }
    }
}
