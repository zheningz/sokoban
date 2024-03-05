using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WordBook
{
    public Dictionary<string, string> words;

    public void Load()
    {
        words = new Dictionary<string, string>();

        TextAsset jsonTextAsset = Resources.Load<TextAsset>("WordBook");
        if (jsonTextAsset == null)
        {
            Debug.LogError("Failed to load JSON file from Resources folder.");
            return;
        }

        string jsonData = jsonTextAsset.text;

        JObject jsonObject = JObject.Parse(jsonData);
        foreach (var property in jsonObject.Properties())
        {
            words.Add(property.Name, property.Value.ToString());
        }
    }
}
