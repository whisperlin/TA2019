using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
public class DirHelper  {

    /*[MenuItem("TA/FileUtil")]
    static void Init()
    {
        List<string> ls =  GetAllFiles("G:\\Script", "*.meta");
        foreach (var l in ls)
        {
            Debug.Log(l);
        }
    }*/
    public static  List<string> GetAllFiles(string sSourcePath,string mark = "*.*")
    {
        List<string> list = new List<string>();
        if (!System.IO.Directory.Exists(sSourcePath))
        {
            return list;
        }
    
        Stack<System.IO.DirectoryInfo> dirs = new Stack<System.IO.DirectoryInfo>();
 
        System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(sSourcePath);
        dirs.Push(d);

        while (dirs.Count>0)
        {
            var theFolder =   dirs.Pop();
            System.IO.FileInfo[] thefileInfo = theFolder.GetFiles(mark, System.IO.SearchOption.TopDirectoryOnly);
            foreach (System.IO.FileInfo NextFile in thefileInfo)  //遍历文件
                list.Add(NextFile.FullName);

            System.IO.DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            foreach (System.IO.DirectoryInfo NextFolder in dirInfo)
            {
                dirs.Push(NextFolder);
            }
        }


        return list;
    }

    public static bool Contains(string source, string value, System.StringComparison comparisonType)
    {
        return (source.IndexOf(value, comparisonType) >= 0);
    }

    
     
}
