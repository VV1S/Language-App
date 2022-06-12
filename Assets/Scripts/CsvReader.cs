using System.Collections;
using UnityEngine;
using System;

public class CsvReader : MonoBehaviour
{
    public TextAsset textAssetData;

    LoadedText myText = new LoadedText();
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
