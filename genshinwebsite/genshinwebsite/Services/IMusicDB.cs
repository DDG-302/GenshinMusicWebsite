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
        /// 获取一个乐谱模型，如果查找失败会返回null
        /// </summary>
        /// <param name="id">乐谱id</param>
        /// <returns>乐谱模型，为null表示没找到</returns>
        public Task<M> get_by_id(int id);

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
        public Task<IEnumerable<T>> get_all();

        /// <summary>
        /// 根据乐谱模型添加到数据库
        /// </summary>
        /// <param name="musicModel"></param>
        /// <returns></returns>
        public Task<T> add_one(MusicModel musicModel);

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

        /// <summary>
        /// 获取相关查询下的数据表总条数
        /// </summary>
        /// <param name="music_title">音乐标题</param>
        /// <returns></returns>
        public int get_item_count(string music_title = "");

        /// <summary>
        /// 查询用户今天上传的乐谱数量
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public Task<int> get_user_today_upload_num(int uid);
        public Task<IEnumerable<M>> get_music_by_offset(int num_per_page = 10, int page_offset = 0, string music_title = "", MUSIC_SELECT_ORDER select_order = MUSIC_SELECT_ORDER.UPLOAD_DATE);

        /// <summary>
        /// 根据乐谱id增加下载量
        /// </summary>
        /// <param name="muid"></param>
        /// <returns></returns>
        public Task<DBOperationResult> add_or_set_download_num(int muid, int set_num = -1);

        /// <summary>
        /// 根据输入的model更新数据库
        /// </summary>
        /// <param name="music_model">全部赋值完成的模型类</param>
        /// <returns></returns>
        public Task<DBOperationResult> update_music(T music_model);
    }
}
