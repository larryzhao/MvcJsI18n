/*
 * MvcJsI18n is created by Larry Zhao.
 * Repository is at https://github.com/larryzhao/MvcJsI18n
 * Home page is at http://larryzhao.com/blog/ursacoder-mvcjsi18n
 * 
 * This is an opensource project, under license EPL.
 * You could use it in anywhere you want.
 * 
 * Thank you very much. :)
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UrsaCoder.MvcJsI18n.Config;
using System.Xml;

namespace UrsaCoder.MvcJsI18n
{
    public class JsI18nCore
    {
        private static string _defaultLocale;
        private static string _operation;
        private static string _defaultTargetPath;
        private static string _defaultSourcePath;
        private static Boolean _useShortLocale;
        private static Dictionary<string, string> _paths;

        /// <summary>
        /// The default locale name, eg. en, zh, it.
        /// </summary>
        public static string DefaultLocale
        {
            get { return _defaultLocale; }
            set { _defaultLocale = value; }
        }

        /// <summary>
        /// Current operation, one of keep and generate.
        /// keep:   if this operation type is set, MvcJsI18n will do nothing.
        /// generate: if this operation type is set, MvcJsI18n will delete all the files in the output dir and generate.
        /// </summary>
        public static string Operation
        {
            get { return _operation; }
            set { _operation = value; }
        }

        public static string DefaultTargetPath
        {
            get { return _defaultTargetPath; }
            set { _defaultTargetPath = value; }
        }

        public static string DefaultSourcePath
        {
            get { return _defaultSourcePath; }
            set { _defaultSourcePath = value; }
        }

        public static Boolean UseShortLocale
        {
            get { return _useShortLocale; }
            set { _useShortLocale = value; }
        }

        public static Dictionary<string, string> Paths
        {
            get { return _paths; }
            set { _paths = value; }
        }

        /// <summary>
        /// It reads the configuration section in the default configuration file. 
        /// Eg. Web.config,
        /// </summary>
        public static void Initialize()
        {
            JsI18nConfigHandler config = (JsI18nConfigHandler)System.Configuration.ConfigurationManager.GetSection("jsi18n");

            JsI18nCore._defaultLocale = config.DefaultLocale.Value;
            JsI18nCore._operation = config.Operation.Value;
            JsI18nCore._defaultSourcePath = config.DefaultSourcePath.Value.Replace("/", "\\");
            JsI18nCore._defaultTargetPath = config.DefaultTargetPath.Value.Replace("/", "\\");
            JsI18nCore._useShortLocale = config.UseShortLocale.Value;

            if (JsI18nCore._operation == "generate")
                JsI18nCore.GenerateJavascriptResources();
        }


        private static void GenerateJavascriptResources()
        {

            //foreach (string source in JsI18nCore.Paths.Keys) 
            //{
            //1. clear js locale folder.
            string jsLocaleDir = AppDomain.CurrentDomain.BaseDirectory + JsI18nCore._defaultTargetPath;
            foreach (string file in Directory.GetFiles(jsLocaleDir))
            {
                File.Delete(file);
            }

            //2. check all the resource files under the dir.
            string sourceDir = AppDomain.CurrentDomain.BaseDirectory + JsI18nCore._defaultSourcePath;
            string[] files = Directory.GetFiles(sourceDir, "*.resx", SearchOption.AllDirectories);

            //3. loop all the resource files and generate js resource files.
            foreach (string file in files)
                JsI18nCore.GenerateJsFileFromResxFile(file, jsLocaleDir);
            //}
        }


        private static void GenerateJsFileFromResxFile(string resxFile, string outputPath)
        {
            Dictionary<string, string> resxItems = JsI18nCore.GetResources(resxFile);

            string shortName = Path.GetFileName(resxFile);
            int shortNameLength = shortName.Length;
            string shortNameWithoutExt = shortName.Substring(0, shortNameLength - 5);
            string jsFileName = outputPath + shortNameWithoutExt + ".js";

            string resourceNameSpace = shortNameWithoutExt.Split(new char[] { '.' })[0];

            if (File.Exists(jsFileName))
                File.Delete(jsFileName);

            StreamWriter sw = new StreamWriter(File.Open(jsFileName, FileMode.Create), Encoding.UTF8);
            sw.WriteLine("var " + resourceNameSpace + " = { ");

            StringBuilder sb = new StringBuilder();
            foreach (string key in resxItems.Keys)
            {
                sb.Append(key + ": '" + resxItems[key] + "',");
            }

            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);

            sw.WriteLine(sb.ToString());
            sw.WriteLine("};");

            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// Get globalization entries from .resx file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static Dictionary<string, string> GetResources(string path)
        {
            XmlDocument dom = new XmlDocument();

            try
            {
                dom.Load(path);
            }
            catch (Exception ex)
            {
                return null;
            }

            Dictionary<string, string> resxItems = new Dictionary<string, string>();

            XmlNodeList nodes = dom.DocumentElement.SelectNodes("data");

            foreach (XmlNode node in nodes)
            {
                string value = null;

                XmlNodeList valueNodes = node.SelectNodes("value");
                if (valueNodes.Count == 1)
                    value = valueNodes[0].InnerText;
                else
                    value = node.InnerText;

                string name = node.Attributes["name"].Value;

                resxItems.Add(name, value);
            }

            return resxItems;
        }
    }
}
