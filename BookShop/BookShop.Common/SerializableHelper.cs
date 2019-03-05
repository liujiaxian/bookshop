using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Common
{
    public class SerializableHelper
    {
        /// <summary>
        /// json.net序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializableToString(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
        /// <summary>
        /// json.net反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T DeserializeToObject<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
