using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

public class CsvReaderTest
{
    [UnityTest]
    public IEnumerator CsvReaderReadValuesFromFile()
    {
        //// Arrange
        var gameObject = new GameObject();
        var reader = gameObject.AddComponent<CsvReader>();
        reader.textAssetData = Resources.Load<TextAsset>("language");

        //// Act
        yield return new WaitForSeconds(1f);
        string i = reader.ReturnRandomEnglishWordFromList();

        //// Assert
        Assert.IsTrue(i.Length > 0);
    }

    [UnityTest]
    public IEnumerator CsvReaderReturnsAccurateEnglishWordFromList()
    {
        //// Arrange
        var gameObject = new GameObject();
        var reader = gameObject.AddComponent<CsvReader>();
        var asset = new TextAsset("1;2\r\n");
        reader.textAssetData = asset;


        //// Act
        yield return new WaitForSeconds(1f);
        string i = reader.ReturnRandomEnglishWordFromList();

        //// Assert
        Assert.AreEqual("2", i);
    }

    [UnityTest]
    public IEnumerator CsvReaderReturnsAccurateReturnRandomReadedWords()
    {
        //// Arrange
        var gameObject = new GameObject();
        var reader = gameObject.AddComponent<CsvReader>();
        var asset = new TextAsset("1;2\r\n");
        reader.textAssetData = asset;
        var testReadedWOrd = new ReadedWord("1", "2");


        //// Act
        yield return new WaitForSeconds(1f);
        ReadedWord i = reader.ReturnRandomReadedWords();

        //// Assert
        Assert.AreEqual(testReadedWOrd, i);
    }
}
