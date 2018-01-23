using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Redis;


namespace OA.Common
{
    /// <summary>
    /// 非集群模式下的RedisClient客户端
    /// </summary>
    public class RedisClientBase
    {
        /// <summary>
        /// RedisClient对象
        /// </summary>
        private RedisClient _Client = null;

        /// <summary>
        /// 初始化RedisClient对象
        /// </summary>
        /// <returns></returns>
        public bool Init_Server()
        {
            try
            {
                if (_Client == null)
                {
                    _Client = new RedisClient("127.0.0.1", 6379);
                    //_Client = new RedisClient(GlobalConfiguration.Configuration["RedisServer"],
                    //    int.Parse(GlobalConfiguration.Configuration["RedisPort"]),
                    //    GlobalConfiguration.Configuration["RedisPassWord"]);
                }
                return true;
            }
            catch
            {
                _Client.Dispose();
                _Client = null;
                return false;
            }
        }

        /// <summary>
        /// 缓存Key
        /// </summary>
        /// <typeparam name="T">可序列化类型</typeparam>
        /// <param name="_Key">键</param>
        /// <param name="_Value">值</param>
        /// <param name="expriedDay">缓存有效天数</param>
        /// <returns>是否缓存成功</returns>
        public bool SetKey<T>(string _Key, T _Value, int expriedDay = -1)
        {
            try
            {
                if (_Client == null)
                {
                    if (!Init_Server())
                    {
                        return false;
                    }
                }
                DateTime Exprieds = DateTime.Now;
                if (expriedDay == -1)
                {
                    Exprieds = DateTime.MaxValue;
                }
                else
                {
                    Exprieds = Exprieds.AddDays(expriedDay);
                }
                return _Client.Add<T>(_Key, _Value, Exprieds);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据键值获取缓存的内容
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="_Key">键</param>
        /// <returns>T</returns>
        public T FetchKey<T>(string _Key)
        {
            try
            {
                if (_Client == null)
                {
                    if (!Init_Server())
                    {
                        return default(T);
                    }
                }
                return _Client.Get<T>(_Key);
            }
            catch
            {
                return default(T);
            }
        }


        /// <summary>
        /// 向有序集合中添加元素
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        public bool AddItemToSortedSet(string set, string value, long score)
        {
            try
            {
                if (_Client == null)
                {
                    if (!Init_Server())
                    {
                        return false;
                    }
                }
                return _Client.AddItemToSortedSet(set, value, score);
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 获得有序集合中，某个分数范围内的所有值，降序
        /// </summary>
        /// <param name="set"></param>
        /// <param name="beginScore"></param>
        /// <param name="endScore"></param>
        /// <returns></returns>
        public IDictionary<string, double> GetRangeFromSortedSetDesc(string set, double beginScore, double endScore)
        {
            try
            {
                if (_Client == null)
                {
                    if (!Init_Server())
                    {
                        return null;
                    }
                }
                return _Client.GetRangeWithScoresFromSortedSetByLowestScore(set, beginScore, endScore);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 删除key为value的数据
        /// </summary>
        public bool RemoveItemFromSortedSet(string key, string value)
        {
            try
            {
                if (_Client == null)
                {
                    if (!Init_Server())
                    {
                        return false;
                    }
                }
                return _Client.RemoveItemFromSortedSet(key, value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 释放连接
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                _Client.Dispose();
                _Client = null;
                return true;
            }
            catch
            { }
            return false;
        }
    }
}