using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb.Utility
{
    public class Utility
    {
        /// <summary>
        /// 获取指定文件夹下所有文件名（不包含后缀名）
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllFileName(string filePath,string searchPattern)
        {            
            try
            {
                List<string> fileNames = new List<string>();
                var files = Directory.GetFiles(filePath, searchPattern);
                foreach (var file in files)
                {
                    string _fileName = file.Substring(file.LastIndexOf("\\") + 1, (file.LastIndexOf(".") - file.LastIndexOf("\\") - 1));
                    fileNames.Add(_fileName);
                }
                return fileNames;
            }            
            catch
            {
                return null;
            }
        }
    }
}
