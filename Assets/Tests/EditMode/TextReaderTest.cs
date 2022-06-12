using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ReadedWordTest
{
    ReadedWord readedWorld;
    LoadedText usedText;
    [Test]
    public void ReadedWordGetsStrings()
    {
        // Arrange
        string polish = "polish";
        string english = "english";

        // Act
        readedWorld = new ReadedWord(polish, english);

        // Assert
        Assert.AreEqual(polish, readedWorld.polishWord);
        Assert.AreEqual(english, readedWorld.englishWord);
    }

    [Test]
    public void TextClassShuldContainReadWordClasses()
    {
        // Arrange
        string polish = "polish";
        string english = "english";
        string polish1 = "polish1";
        string english2 = "english2";
        readedWorld = new ReadedWord(polish, english);
        var readedWorld2 = new ReadedWord(polish1, english2);
        usedText = new LoadedText();

        // Act
        usedText.loadedWords.Add(readedWorld);
        usedText.loadedWords.Add(readedWorld2);

        // Assert
        Assert.AreEqual(2, usedText.loadedWords.Count);
        Assert.AreEqual("polish1", usedText.loadedWords[1].polishWord);
        Assert.AreEqual("english2", usedText.loadedWords[1].englishWord);
    }
}
