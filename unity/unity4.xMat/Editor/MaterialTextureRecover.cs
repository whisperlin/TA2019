using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
public class MaterialTextureRecover : EditorWindow
{
    string mMateiralDir = "Assets\\_ArtResources\\Character\\Monster\\human";
    string mResource = "Assets\\_ArtResources\\Character\\Monster\\human";
	bool saveAsMatDir = true;
    //string mMateiralDir = "Assets";
    //string mResource = "Assets";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    // Add menu named "My Window" to the Window menu
    [MenuItem("TA/材质恢复")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        MaterialTextureRecover window = (MaterialTextureRecover)EditorWindow.GetWindow(typeof(MaterialTextureRecover));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);


        GUILayout.BeginHorizontal();
        mMateiralDir = EditorGUILayout.TextField("材质目录", mMateiralDir);
        if (GUILayout.Button("选择"))
        {
            string path = EditorUtility.OpenFolderPanel("材质所在目录", mMateiralDir, "");
            if (path.Length > 0)
            {
                int len = System.Environment.CurrentDirectory.Length;


                mMateiralDir = path.Substring(len+1);
            }
        }
        GUILayout.EndHorizontal();
        saveAsMatDir = GUILayout.Toggle(saveAsMatDir, "资源查找目录与材质目录一致");
        if (!saveAsMatDir)
        {
            GUILayout.BeginHorizontal();
            mResource = EditorGUILayout.TextField("资源目录", mResource);
            if (GUILayout.Button("选择"))
            {
                string path = EditorUtility.OpenFolderPanel("资源所在目录", mResource, "");
                if (path.Length > 0)
                {

                    int len = System.Environment.CurrentDirectory.Length;
                    mResource = path.Substring(len+1);
                }
            }
            GUILayout.EndHorizontal();
        }
        
       
        
       
        if (GUILayout.Button("恢复"))
        {

			if (saveAsMatDir)
				mResource = mMateiralDir;
            int len = System.Environment.CurrentDirectory.Length+1;
            List<string> mats = DirHelper.GetAllFiles(mMateiralDir, "*.mat");
			int matCount = mats.Count;
			if (matCount > 0)
            {

                List<string> mates = DirHelper.GetAllFiles(mResource, "*.meta");
                Dictionary<string, string> guids = new Dictionary<string, string>();
                foreach (var l in mates)
                {
                    if (l.EndsWith(".png.meta") || l.EndsWith(".tga.meta"))
                    {
                        string guid = MetaHelper.GetGuid(l);
                        string path = l.Substring(len, l.Length - 5- len);
                        guids[guid] = path;
          
                    }
                    //DirHelper.Contains(l, ".png.meta",System.StringComparison.OrdinalIgnoreCase);

                }
				for(int k = 0 ; k < matCount ; k++ )
                //foreach (var l in mats)
                {
					string l = mats[k];
					EditorUtility.DisplayProgressBar("恢复材质", l,((float)k)/matCount);
					string path0 = l.Substring(len, l.Length - len);

					if (!System.IO.File.Exists (path0)) {
						Debug.LogError ("file not found "+path0);
						continue;
					}

					Material mat0 = (Material)AssetDatabase.LoadAssetAtPath<Material>(path0);
					Debug.LogError ("mat0 = "+mat0);
					Dictionary<string, string> dict = MaterialHelper.GetTextureInformations(l);


					if (dict.Count == 0) {
						continue;
						Debug.LogError ("非文本材质"+path0 + "跳过");
					}
					 
					foreach (string name in dict.Keys)
					{
						string guid = dict[name];
						string path;
						if (guids.TryGetValue (guid, out path)) {
							Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D> (path);
							mat0.SetTexture (name, tex);
							//mat1.SetTexture (name, tex);
							/*Debug.LogError ("set property[" + name + "]=" + tex + " "+name.Length);
							for (int i = 0; i < name.Length; i++) {
							
								Debug.LogWarning (name[i]);
							}*/
							 
	 					
						} else {
							//Debug.LogError ("texture not found["+name+"]"+guid);
						}
					}

					EditorUtility.SetDirty (mat0);
					AssetDatabase.SaveAssets ();
					 

					AssetDatabase.Refresh();

					 
                    

                }



            }
           
			EditorUtility.ClearProgressBar ();
        }

    }
}
 
