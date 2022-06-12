using System.Collections.Generic;

[System.Serializable]
public class LoadedText
{
    public List<ReadedWord> loadedWords;
    public LoadedText()
    {
        loadedWords = new List<ReadedWord>();
    }
}
