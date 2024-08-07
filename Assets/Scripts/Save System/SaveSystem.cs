using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public static class SaveSystem
{
    static string path = Application.persistentDataPath + "/data.save";

    public static GameData Load() {
        GameData saveData = null;
        if(File.Exists(path)) 
        {
            try
            {  
                string dataToLoad = "";
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                saveData = JsonUtility.FromJson<GameData>(dataToLoad);
                // saveData = JsonConvert.DeserializeObject<GameData>(dataToLoad);
                
            }
            catch (System.Exception)
            {
                Debug.Log("Error while trying to load data");
            }
        }
        return saveData;
    }

    public static void Save(SaveManager manager) {
        GameData data = new GameData(manager);
        try
        {
            string dataToStore = JsonUtility.ToJson(data, true);
            // string dataToStore = JsonConvert.SerializeObject(data);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Error while trying to save data");
        }
    }

    public static void Delete() {
        if ( !File.Exists( path ) )
		{
			Debug.Log("Error while trying to delete data");
		}
		else
		{	
			File.Delete( path );
			
			#if UNITY_EDITOR
		    UnityEditor.AssetDatabase.Refresh();
		    #endif
		}
    }
}
