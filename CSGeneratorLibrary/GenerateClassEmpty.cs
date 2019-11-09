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
        public static void generateClass(string templateName, string package, string className, string outputPath, string[] files, string templatesFolder)
        {
            string[] text = readIn(templateName, templatesFolder);

            string replaced = "";

            for (int i = 0; i < text.Length; i++)
            {
                replaced = replaced + text[i] + "\n";
            }

            replaced = replaced.Replace("#namespaceName#", package);
            replaced = replaced.Replace("#className#", className);
            replaced = replaced.Replace("#parentClassName#", className + "Algebra");

            writeOut(replaced, className, outputPath);

            //EntityGenerate.entityGenerateMethods(files, package, outputPath, templatesFolder);
        }

        public static string[] readIn(string fileName, string templatesFolder)
        {

            string textFile = templatesFolder + fileName + ".csT";

            string[] text = File.ReadAllLines(textFile);

            return text;
        }

        public static void writeOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + "\\Final\\" + fileName + ".cs", text);

        }
    }
}