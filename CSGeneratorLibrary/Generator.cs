﻿using CSAc4yClass.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGeneratorLibrary
{
    public class Generator
    {
        public static void contextGenerate(string fileName, string outputPath, List<Ac4yClass> list, string package)
        {
            string[] text = readIn(fileName + "Context");
            string replaced = "";
            string newLine = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Equals("#classes#"))
                {
                    foreach (var ac4y in list)
                    {
                        newLine = newLine + text[i + 1].Replace("#classesName#", ac4y.Name).Replace("#tableName#", ac4y.Name + "s") + "\n";
                        newLine = newLine + "\n";
                    }

                    replaced = replaced + newLine + "\n";


                    i = i + 2;
                }

                replaced = replaced + text[i] + "\n";
            }
            replaced = replaced.Replace("#className#", "All").Replace("#namespaceName#", package);

            writeOut(replaced, "AllContext", outputPath);
        }

        public static void programGenerator(string fileName, string namespaceName, Ac4yClass ac4y, string outputPath)
        {
            List<Ac4yProperty> values = ac4y.PropertyList;
            string[] text = readIn(fileName);
            string replaced = "";
            string newLine = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Equals("#values#"))
                {
                    newLine = newLine + text[i + 1].Replace("#valueName#", ac4y.Name.Substring(0, 1).ToLower()).Replace("#className#", ac4y.Name) + "\n";

                    replaced = replaced + newLine;
                    newLine = "";

                    i = i + 2;
                }
                else if (text[i].Equals("#adds#"))
                {
                    newLine = newLine + text[i + 1].Replace("#valueName#", ac4y.Name.Substring(0, 1).ToLower()).Replace("#className#", ac4y.Name) + "\n";

                    replaced = replaced + newLine;
                    newLine = "";

                    i = i + 2;
                }
                replaced = replaced + text[i] + "\n";
            }
            replaced = replaced.Replace("#namespaceName#", namespaceName).Replace("#classContextName#", ac4y.Name + "Context");

            writeOut(replaced, ac4y.Name + "SaveTest", outputPath);
        }

        public static void generateEntityMethods(string fileName, string namespaceName, Ac4yClass ac4y, string outputPath)
        {
            List<Ac4yProperty> props = ac4y.PropertyList;
            string[] text = readIn(fileName);
            string replaced = "";
            string newLine = "";

            if (namespaceName == null || namespaceName.Equals(""))
            {
                namespaceName = ConfigurationManager.AppSettings["namespace"];
            }

            int y = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Equals("#findFirstBy#"))
                {
                    foreach (var prop in props)
                    {
                        for (int x = 1; x < 15; x++)
                        {
                            newLine = newLine + text[i + x] + "\n";
                        }
                        newLine = newLine.Replace("#className#", ac4y.Name).Replace("#propName#", prop.Name)
                                         .Replace("#PropName#", prop.Name.Substring(0, 1).ToUpper() + prop.Name.Substring(1))
                                         .Replace("#type#", prop.Type).Replace("#valueName#", ac4y.Name.Substring(0, 1).ToLower())
                                         .Replace("#classContextName#", ac4y.Name + "Context").Replace("#contextPropName#", ac4y.Name + "s");
                    }
                    y = y + 1;

                    replaced = replaced + newLine;
                    newLine = "";

                    i = i + 14;
                    y = 0;
                }
                else if (text[i].Equals("#exists#"))
                {
                    foreach (var prop in props)
                    {
                        for (int x = 1; x < 22; x++)
                        {
                            newLine = newLine + text[i + x] + "\n";
                        }
                        newLine = newLine.Replace("#className#", ac4y.Name).Replace("#propName#", prop.Name)
                                         .Replace("#PropName#", prop.Name.Substring(0, 1).ToUpper() + prop.Name.Substring(1))
                                         .Replace("#type#", prop.Type).Replace("#valueName#", ac4y.Name.Substring(0, 1).ToLower())
                                         .Replace("#classContextName#", ac4y.Name + "Context").Replace("#contextPropName#", ac4y.Name + "s");
                    }
                    y = y + 1;

                    replaced = replaced + newLine;
                    newLine = "";

                    i = i + 21;
                    y = 0;
                }
                else if (text[i].Equals("#findListBy#"))
                {
                    foreach (var prop in props)
                    {
                        if (!prop.Name.Equals("id") || !prop.Name.Equals("Id") || !prop.Name.Equals("ID"))
                        {
                            for (int x = 1; x < 14; x++)
                            {
                                newLine = newLine + text[i + x] + "\n";
                            }
                            newLine = newLine.Replace("#className#", ac4y.Name).Replace("#propName#", prop.Name)
                                             .Replace("#PropName#", prop.Name.Substring(0, 1).ToUpper() + prop.Name.Substring(1))
                                             .Replace("#type#", prop.Type).Replace("#valueName#", ac4y.Name.Substring(0, 1).ToLower())
                                         .Replace("#classContextName#", ac4y.Name + "Context").Replace("#contextPropName#", ac4y.Name + "s");
                        }
                    }
                    y = y + 1;

                    replaced = replaced + newLine;
                    newLine = "";

                    i = i + 13;
                    y = 0;
                }
                else if (text[i].Contains("#update#"))
                {
                    newLine = text[i + 1].Replace("#className#", ac4y.Name) + "\n" + text[i + 2] + "\n";
                    newLine = newLine + text[i + 3].Replace("#classContextName#", ac4y.Name + "Context") + "\n" + text[i + 4] + "\n";
                    newLine = newLine + text[i + 5].Replace("#contextPropName#", ac4y.Name + "s").Replace("#className#", ac4y.Name)
                                      + "\n" + text[i + 6].Replace("#className#", ac4y.Name) + "\n\n";

                    foreach (var prop in props)
                    {
                        newLine = newLine + text[i + 8].Replace("#className#", ac4y.Name).Replace("#prop#", prop.Name) + "\n";
                    }

                    newLine = newLine + text[i + 9] + "\n" + text[i + 10] + "\n" + text[i + 11] + "\n";

                    replaced = replaced + newLine + "\n";

                    i = i + 11;
                }
                else if (text[i].Equals("#deleteById#"))
                {
                    foreach (var prop in props)
                    {
                        if (prop.Name.Equals("id") || prop.Name.Equals("Id") || prop.Name.Equals("ID"))
                        {
                            for (int x = 1; x < 11; x++)
                            {
                                newLine = newLine + text[i + x] + "\n";
                            }
                            newLine = newLine.Replace("#className#", ac4y.Name).Replace("#propName#", prop.Name)
                                             .Replace("#PropName#", prop.Name.Substring(0, 1).ToUpper() + prop.Name.Substring(1))
                                             .Replace("#type#", prop.Type).Replace("#valueName#", ac4y.Name.Substring(0, 1).ToLower())
                                         .Replace("#classContextName#", ac4y.Name + "Context").Replace("#contextPropName#", ac4y.Name + "s");
                        }
                        y = y + 1;

                    }
                    replaced = replaced + newLine;
                    newLine = "";

                    i = i + 10;
                    y = 0;
                }
                else if (text[i].Equals("#adds#"))
                {
                    for (int x = 1; x < 11; x++)
                    {
                        newLine = newLine + text[i + x] + "\n";
                    }
                    newLine = newLine.Replace("#className#", ac4y.Name).Replace("#classContextName#", ac4y.Name + "Context")
                                     .Replace("#contextPropName#", ac4y.Name + "s");

                    y = y + 1;

                    replaced = replaced + newLine;
                    newLine = "";

                    i = i + 10;
                    y = 0;
                }
                else
                {
                    replaced = replaced + text[i] + "\n";
                }
                newLine = "";
            }

            replaced = replaced.Replace("#className#", ac4y.Name).Replace("#namespaceName#", namespaceName);
            replaced = replaced.Replace("#mainClassName#", ac4y.Name);

            writeOut(replaced, ac4y.Name + "EntityMethods", outputPath);
        }

        public static string[] readIn(string fileName)
        {

            string textFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Templates\\", fileName + ".csT");

            string[] text = File.ReadAllLines(textFile);

            return text;


        }

        public static void writeOut(string text, string fileName, string outputPath)
        {
            System.IO.File.WriteAllText(outputPath + fileName + ".cs", text);

        }
    }
}
