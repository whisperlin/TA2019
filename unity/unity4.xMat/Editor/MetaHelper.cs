using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaHelper  {

    static string guid = "guid: ";

    public static string GetGuid(string path) {
        string[]  lines = System.IO.File.ReadAllLines(path);

        if (lines.Length > 2)
        {
            return lines[1].Substring(6);
        }
        return "";


    }
	
	 
}
