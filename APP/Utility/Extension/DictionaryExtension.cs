/************************************************************************
 * 文件标识：  e8aa73a5-2e38-42ec-910d-7494e06ae610
 * 项目名称：  APP.Utility.Extension  
 * 项目描述：  
 * 类 名 称：  DictionaryExtension
 * 版 本 号：  v1.0.0.0 
 * 说    明：  
 * 作    者：  尹自强
 * 创建时间：  2018/2/7 11:34:06
 * 更新时间：  2018/2/7 11:34:06
************************************************************************
 * Copyright @ 尹自强 2018. All rights reserved.
************************************************************************/

using System.Collections;

namespace APP.Utility.Extension
{
    public static class DictionaryExtension
    {
        public static Hashtable ToHashtable(this IDictionary dic)
        {
            Hashtable ht = new Hashtable();

            if (dic != null && dic.Count > 0)
            {
                foreach (string key in dic.Keys)
                {
                    if (key != null)
                        ht.Add(key, dic[key]);
                }
            }

            return ht;
        }
    }
}
