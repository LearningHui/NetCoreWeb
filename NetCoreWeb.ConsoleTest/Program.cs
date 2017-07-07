using System;
using System.Security.Cryptography;
using System.Text;
using TlSLib;

namespace NetCoreWeb.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            Int32 ticks = System.Convert.ToInt32(ts.TotalSeconds);
            //当前UTC时间戳，从1970年1月1日0点0 分0 秒开始到现在的秒数(String)
            String curTime = ticks.ToString();
            String appSecret = "ec4573c39376";
            //随机数（最大长度128个字符）
            string nonce = "12345";
            string random = new Random().ToString();
            //SHA1(AppSecret + Nonce + CurTime),三个参数拼接的字符串，进行SHA1哈希计算，转化成16进制字符(String，小写)
            String checkSum = CheckSumBuilder.getCheckSum(appSecret, nonce, curTime);
            string url = "https://api.netease.im/sms/sendtemplate.action";
            url += "?templateid=3063716&mobiles=[\"18611693213\"]&params=[\"7点15分\"]";//306371
            var http = NetworkRequest.CreateHttp(url);            
            http.Request.Headers["AppKey"] = "78d74476f41e6d9bf1429118bf5316bd";
            http.Request.Headers["Nonce"] = nonce;
            http.Request.Headers["CurTime"] = curTime;
            http.Request.Headers["CheckSum"] = checkSum;
            http.Request.Headers["ContentType"] = "application/x-www-form-urlencoded;charset=utf-8";
            var bytes = http.Post();
            if(bytes!=null)
            {
                string str = Encoding.UTF8.GetString(bytes);
                Console.WriteLine(str);
            }
            Console.ReadLine();
        }
        class CheckSumBuilder
        {
            // 计算并获取CheckSum
            public static String getCheckSum(String appSecret, String nonce, String curTime)
            {
                byte[] data = Encoding.UTF8.GetBytes(appSecret + nonce + curTime);
                byte[] result;
                SHA1 sha = SHA1.Create();
                result = sha.ComputeHash(data);
                return getFormattedText(result);
            }

            // 计算并获取md5值
            public static String getMD5(String requestBody)
            {
                if (requestBody == null)
                    return null;

                // Create a new instance of the MD5CryptoServiceProvider object.
                MD5 md5Hasher = MD5.Create();

                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(requestBody));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return getFormattedText(Encoding.UTF8.GetBytes(sBuilder.ToString()));
            }

            private static String getFormattedText(byte[] bytes)
            {
                char[] HEX_DIGITS = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
                int len = bytes.Length;
                StringBuilder buf = new StringBuilder(len * 2);
                for (int j = 0; j < len; j++)
                {
                    buf.Append(HEX_DIGITS[(bytes[j] >> 4) & 0x0f]);
                    buf.Append(HEX_DIGITS[bytes[j] & 0x0f]);
                }
                return buf.ToString();
            }
        }
    }
}