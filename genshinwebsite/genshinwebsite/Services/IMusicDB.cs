using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Models;

namespace genshinwebsite.Services
{
    public enum MUSIC_SELECT_ORDER { UPLOAD_DATE = 1, DOWNLOAD_NUM = 2, VIEW_NUM = 3 , TITLE=4 }
    public interface IMusicDB<T, M> where T:class where M : class // 泛型约束
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
        /// /// <param name="max_item_num">所有uid=查询用户id的乐谱数量</param>
        /// <returns></returns>
        public IEnumerable<T> get_by_uid( int uid, out int max_item_num, int num_per_page = 10, int page_offset = 0, MUSIC_SELECT_ORDER select_order = MUSIC_SELECT_ORDER.UPLOAD_DATE);

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
        /// 记录一次浏览
        /// </summary>
        /// <param name="musicModel"></param>
        /// <returns></returns>
        public DBOperationResult view_one(MusicModel musicModel);


        /// <summary>
        /// 删除乐谱muid+用户uid对应的乐谱，数据库存储和实体文件都应当删除；uid是为了校验用户身份
        /// </summary>
        /// <param name="muid">乐谱id</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public DBOperationResult delete_one_by_id(int muid, int uid);


        /// <summary>
        /// 删除用户id名下的所有乐谱，数据库存储和实体文件都应当删除
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public DBOperationResult delete_ones_by_uid(int uid);

        public IEnumerable<M> get_music_by_offset(out int max_item_num, int num_per_page = 10, int page_offset = 0, string music_title = "", MUSIC_SELECT_ORDER select_order = MUSIC_SELECT_ORDER.UPLOAD_DATE);

        /// <summary>
        /// 根据乐谱id增加下载量
        /// </summary>
        /// <param name="muid"></param>
        /// <returns></returns>
        public DBOperationResult add_or_set_download_num(int muid, int set_num = -1);

    }
}
