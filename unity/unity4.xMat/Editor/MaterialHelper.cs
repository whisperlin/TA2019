using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHelper  {

    public static Dictionary<string, string> GetTextureInformations(string path)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        string[] lines = System.IO.File.ReadAllLines(path);

        for (int i = 0 , c = lines.Length; i < c; i++)
        {
            string s = lines[i];
            if (!s.Contains("m_Texture"))
                continue;
            int index = s.IndexOf("guid:");
            if (index <= 0)
                continue;
            int indedx2 = s.IndexOf(',', index);
            if (index < 0)
                continue;
           
           string ls = lines[i - 1];

           
           int index3 = ls.IndexOf("-");
            if (index3 == -1)
                continue;
           int index4 = ls.IndexOf(":");
            if (index4 == -1)
                continue;
            int begin0 = index3 + 1;
			string name = ls.Substring (begin0, index4 - begin0).Trim ();

            int begin1 = index + 5;
            string guid = s.Substring(begin1, indedx2- begin1).Trim();

            //Debug.Log(name + ""+ guid + " "+ guid.Length);
            dict[name] = guid;
        }

        return dict;
    }
}
