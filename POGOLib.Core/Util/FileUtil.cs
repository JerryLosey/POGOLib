/*
 * Created by SharpDevelop.
 * User: Xelwon
 * Date: 01/01/2018
 * Time: 16:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace PokemonGoGUI.POGOLib.Core.Util
{
    /// <summary>
    /// Description of FileUtil.
    /// </summary>
    public static class FileUtil
    {
        public static string ReadAllText(string filename)
        {
            using (var sr = new StreamReader(filename)) {
                var allText = "";
                var line = "";
                while ((line = sr.ReadLine()) != null) {
                    allText += line;
                }
                sr.Close();
                return allText;
            }
        }
        
        public static void WriteAllText(string filename, string text)
        {
            using (var sr = new StreamWriter(filename)) {
                sr.WriteLine(text);
                sr.Close();
            }
        }
    }
}
