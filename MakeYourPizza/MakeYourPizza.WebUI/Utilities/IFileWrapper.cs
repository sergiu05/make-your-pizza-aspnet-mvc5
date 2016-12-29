using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourPizza.WebUI.Utilities
{
    /// <summary>
    ///	Since the System.IO.File class contains static methods that cannot be mocked with Moq,
    ///	a thin wrapper around the File class will be used and this is the interface that will
    ///	be injected into the client classes
    ///	Learning source: http://stackoverflow.com/questions/6892978/mock-file-exists-method-in-unit-test-c
    /// </summary>
    public interface IFileWrapper
    {
        bool Exists(string path);
        void Delete(string path);
    }
}
