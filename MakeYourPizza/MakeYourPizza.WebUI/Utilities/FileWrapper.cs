using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeYourPizza.WebUI.Utilities
{
    /// <summary>
    ///	A wrapper class around System.IO.File class
    /// </summary>
    public class FileWrapper : IFileWrapper
    {
        public void Delete(string path)
        {
            System.IO.File.Delete(path);
        }

        public bool Exists(string path)
        {
            return System.IO.File.Exists(path);
        }
    }
}