using CSAc4yClass.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGeneratorLibrary
{
    public class Program
    {

        #region constant


        #endregion // constants

        #region base functions


        #endregion // base functions

        public static void MainMethod(string _inpath, string _outpath, string _defaultNamespace)
        {

            //Date: 2019. 11. 2. 12:58

            string[] files =
                Directory.GetFiles(_inpath, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var _file in files)
            {
                string _filename = Path.GetFileNameWithoutExtension(_file);
                Console.WriteLine(_filename);

                Ac4yClass ac4y = DeserialiseMethod.deser(_file);

                GenerateClass.generateClass(ac4y, _outpath, files, _defaultNamespace);
            }
        }
    }
}