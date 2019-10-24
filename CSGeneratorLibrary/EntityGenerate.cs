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
    public class EntityGenerate
    {

        public static void entityGenerateMethods(string languageExtension, string outputPath, string[] files, string _defaultNamespace)
        {

            List<Ac4yClass> list = new List<Ac4yClass>();
            string[] files2 = files;

            foreach (var _file in files2)
            {
                string _filename = Path.GetFileNameWithoutExtension(_file);
                list.Add(DeserialiseMethod.deser(_file));
            }

            for (var x = 0; x < files2.Length; x++)
            {
                string _filename = Path.GetFileNameWithoutExtension(files2[x]);

                Generator.contextGenerate(list[x], list[x].Name + "Db", list[x].Namespace, "Template", languageExtension, outputPath, _defaultNamespace);

                Generator.generateEntityMethods("TemplateEntityMethods", languageExtension, list[x].Namespace, list[x], outputPath, _defaultNamespace);

                Generator.programGenerator("TemplateSaveProgram", languageExtension, list[x].Namespace, list[x], outputPath, _defaultNamespace);
            }
        }
    }
}

