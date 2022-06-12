using System.Collections.Generic;

public class ReadedWord
{
    public string polishWord;
    public string englishWord;

    public ReadedWord(string polishWord, string englishWord)
    {
        this.polishWord = polishWord;
        this.englishWord = englishWord;
    }

    public override bool Equals(object obj)
    {
        return obj is ReadedWord word &&
               polishWord == word.polishWord &&
               englishWord == word.englishWord;
    }

    public override int GetHashCode()
    {
        int hashCode = 1214968491;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(polishWord);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(englishWord);
        return hashCode;
    }
}
