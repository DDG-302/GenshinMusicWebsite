﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Models;
namespace genshinwebsite.Services
{
    public enum COMMENT_SELECT_ORDER { UPLOAD_DATE = 1, UPDATE_DATE = 2}
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">数据库模型</typeparam>
    /// <typeparam name="E">视图模型</typeparam>
    public interface ICommentDB<T, E> where T: class where E: class 
    {
        /// <summary>
        /// 插入一条评论
        /// </summary>
        /// <param name="comment">评论模型类对象</param>
        /// <returns></returns>
        public Task<DBOperationResult> add_comment(T comment);

        /// <summary>
        /// 修改评论
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>DBOperationResult</returns>
        public Task<DBOperationResult> update_comment(T comment);

        /// <summary>
        /// 删除特定评论
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>DBOperationResult</returns>
        public Task<DBOperationResult> delete_comment(T comment);

        /// <summary>
        /// 删除特定评论
        /// </summary>
        /// <param name="uid">uid</param>
        /// <param name="mudi">muid</param>
        /// <returns>DBOperationResult</returns>
        public DBOperationResult delete_comment(int uid, int muid);

        /// <summary>
        /// 删除某一用户的所有评论
        /// </summary>
        /// <param name="uid">uid</param>
        /// <returns></returns>
        public Task<DBOperationResult> delete_comments_by_uid(int uid);

        /// <summary>
        /// 搜索特定评论
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="muid"></param>
        /// <returns>评论模型类</returns>
        public Task<T> get_comment_by_uid_muid(int uid, int muid);

        /// <summary>
        /// 按页获取评论
        /// </summary>
        /// <param name="muid">muid</param>
        /// <param name="num_per_page"></param>
        /// <param name="page_offset"></param>
        /// <returns></returns>
        public Task<IEnumerable<E>> get_comment_list(int muid, int num_per_page = 10, int page_offset = 0, COMMENT_SELECT_ORDER select_order=COMMENT_SELECT_ORDER.UPLOAD_DATE);
    }
}
