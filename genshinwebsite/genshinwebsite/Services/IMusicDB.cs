using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Models;

namespace genshinwebsite.Services
{
    public interface IMusicDB<T> where T: class // 泛型约束
    {
        /// <summary>
        /// 总乐谱数量
        /// </summary>
        /// <returns></returns>
        public int get_music_count();

        /// <summary>
        /// 获取一个乐谱模型，如果查找失败会返回null
        /// </summary>
        /// <param name="id">乐谱id</param>
        /// <returns>乐谱模型，为null表示没找到</returns>
        public T get_by_id(int id);

        /// <summary>
        /// 根据用户id寻找由该用户名下的所有乐谱
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public IEnumerable<T> get_by_uid(int uid, int num_per_page = 10, int page_offset = 0);

        /// <summary>
        /// 全表查询
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> get_all();

        /// <summary>
        /// 根据乐谱模型添加到数据库
        /// </summary>
        /// <param name="musicModel"></param>
        /// <returns></returns>
        public DBOperationResult add_one(MusicModel musicModel);

        /// <summary>
        /// 删除乐谱id对应的乐谱，数据库存储和实体文件都应当删除
        /// </summary>
        /// <param name="id">乐谱id</param>
        /// <returns></returns>
        public DBOperationResult delete_one_by_id(int id);


        /// <summary>
        /// 删除用户id名下的所有乐谱，数据库存储和实体文件都应当删除
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public DBOperationResult delete_ones_by_uid(int uid);

        public IEnumerable<T> get_music_by_offset(int num_per_page = 10, int page_offset = 0);

    }
}
