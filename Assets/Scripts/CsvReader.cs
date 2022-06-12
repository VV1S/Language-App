using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CsvReader : MonoBehaviour
{
    [SerializeField] TextAsset textAssetData;

    [System.Serializable]
    public class Text
    {
        public List<ReadedWord> loadedWords;
        public Text()
        {
            loadedWords = new List<ReadedWord>();
        }
    }

    Text myText = new Text();
    void Awake()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ";", "\r\n" }, StringSplitOptions.None);
        for (int idx = 0; idx < data.Length - 1; idx += 2)
        {
            var readedWords = new ReadedWord(data[idx], data[idx + 1]);
            myText.loadedWords.Add(readedWords);
        }
    }

    public string ReturnRandomEnglishWordFromList()
    {
        int idx = UnityEngine.Random.Range(0, myText.loadedWords.Count);
        var readedWords = myText.loadedWords[idx];
        return readedWords.englishWord;
    }

    public ReadedWord ReturnRandomReadedWords()
    {
        int idx = UnityEngine.Random.Range(0, myText.loadedWords.Count);
        var readedWords = myText.loadedWords[idx];
        return readedWords;
    }
}
