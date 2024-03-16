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

    public List<string> SearchWords(string input)
    {
        List<string> results = new List<string>();
        foreach (var word in words.Keys)
        {
            if (word.StartsWith(input))
            {
                results.Add(word);
            }
        }
        // print to console to see the words
        foreach (var word in results)
        {
            Debug.Log(word);
        }
        return results;
    }
}
