using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fandom
{
    internal class FSWork
    {
        static public string Path(string location = "myDocs")
        {
            switch (location)
            {
                case "myDocs":
                    return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                case "Desktop":
                    return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                case "Current":
                    return Environment.CurrentDirectory;
                default:
                    return string.Empty;
                    break;
            }
             
        }
        static public List<string> ReadSQLFile(string filename, string start = "CREATE TABLE")
        {
            List<string> result = new List<string>();
            using (StreamReader sr = new StreamReader(filename))
            {
                string tmp =  sr.ReadToEnd();
                result =  tmp.Split(';').ToList<string>();
            }
            for (int i = 0; i < result.Count; i++)
            {
                result[i] += ";";
            }
            return result;
        }
        static public bool IsFileExist(string path)
        {
            bool result = false;
            if (File.Exists(path))
            {
                result = true;
            }
            return result;
        }
        static public byte[] GetImage()
        {
            byte[] result = null;
            string filename = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG files(*.JPG)|*.jpg|All files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.FileName;
            }
            else return result;
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                result = new byte[fs.Length];
                fs.Read(result, 0, result.Length);
            }
            return result;
        }
    }
}
