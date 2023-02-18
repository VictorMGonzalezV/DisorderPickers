using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//include this namespace so you can save data between sessions (serialize/deserialize)
using System.IO;

public class MainManager : MonoBehaviour
{
    //adding {get/set} turns the variable into a property and overrides the public modifier, it becomes read-only through the get accessor
    //but can still be set from within the class, useful in situations like this where instance must be public to prevent errors down the line
    public static MainManager instance { get; private set; }
    public Color teamColor;

    private void Awake()
    {
        //Use this code to prevent a new instance being stored every time the scene loads (enforcing a singleton)
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        //This method makes the object persist between scenes, NOT sessions.
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    //Add this attribute so the class can be converted to JSON by JsonUtility and stored elsewhere
    //It's good practice to create a small class that stores only the attributes that need to be saved
    [System.Serializable]
    class SaveData
    {
        public Color teamColor;
    }

    public void SaveColor()
    {
        SaveData saveData = new SaveData();
        saveData.teamColor = teamColor;
        //this will turn the saveData object into a JSON-compliant string
        string json = JsonUtility.ToJson(saveData);
        //Application.persistentDataPath is a static string variable needed to save the JSON string in file storage+string path, string JSON output
        //The different persistentDataPath values per platform can bee seen in the Unity Scripting API.
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        //This method reverses the Save method, it stores the path of the saved data in a string and then attempts to retrieve that file
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            //If the file exists, the File class can read the text and store it as a string
            string json = File.ReadAllText(path);
            //FromJson<Type>(string) turns the string into an object of the type in question
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            //Now the teamColor value can be extracted from the SaveData object and copied into the MainManager instance
            teamColor = data.teamColor;
        }
    }
}
