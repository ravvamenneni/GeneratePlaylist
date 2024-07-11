using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.WebRequestMethods;


namespace GeneratePlaylist
{
    internal class Program
    {
        static void Main(string[] args)
        {


            //Read in full Playlist
            XmlDocument fullPlaylistDoc = new XmlDocument();
            String url = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            //correction in path to point it in Root directory
            String masterListFile = url.Replace("\\bin\\Debug", "") + "\\master.playlist";
            //load xml file
            fullPlaylistDoc.Load(masterListFile);

         
            StreamReader streamReader = new StreamReader("..\\..\\failed.csv");
            List<string> failedTests = new List<string>();
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                var values = line.Split(' ');
                foreach (var v in values)
                {
                    failedTests.Add(v);
                }
            }


            XmlNodeList nodes = fullPlaylistDoc.SelectNodes("/Playlist/Rule/Rule/Rule/Rule/Rule/Rule/Rule/Rule/Rule/Rule/Rule");
            foreach (XmlNode node in nodes) 
            {
                Console.WriteLine(node.ChildNodes[0]?.Attributes[1].Value);
                if (!failedTests.Contains(node.ChildNodes[0]?.Attributes[1].Value))
                {
                    //removed node
                    node.ParentNode.ParentNode.RemoveChild(node.ParentNode);
                }

            }

            fullPlaylistDoc.Save("..\\..\\Recording-failed-Staging-7-11.playlist");

            }
        }
}
