using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGeneratorLibrary
{
    public class GenerateClassEmpty
    {
        public static void generateClass(string templateName, string languageExtension, string package, string className, string outputPath, string[] files)
        {
            string[] text = readIn(templateName, languageExtension);

            string replaced = "";

            for (int i = 0; i < text.Length; i++)
            {
                replaced = replaced + text[i] + "\n";
            }

            replaced = replaced.Replace("#namespaceName#", package);
            replaced = replaced.Replace("#className#", className);
            replaced = replaced.Replace("#parentClassName#", className + "Algebra");

            writeOut(replaced, className, languageExtension, outputPath);

            EntityGenerate.entityGenerateMethods(languageExtension, outputPath, files, package);
        }

        public static string[] readIn(string fileName, string languageExtension)
        {

            string textFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Templates\\", fileName + "." + languageExtension + "T");

            string[] text = File.ReadAllLines(textFile);

            return text;
        }

        public static void writeOut(string text, string fileName, string languageExtension, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + "." + languageExtension, text);

        }
    }
}
