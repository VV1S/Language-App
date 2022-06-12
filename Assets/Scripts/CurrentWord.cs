using UnityEngine.UI;
using UnityEngine;

public class CurrentWord : MonoBehaviour
{
    public Text displayedText;
    public ReadedWord currentPair;
    void Start()
    {
        UpdateTextbox();
    }
    public void UpdateTextbox()
    {
        var reader = FindObjectOfType<CsvReader>();
        currentPair = reader.ReturnRandomReadedWords();
        displayedText.text = currentPair.polishWord;
    }


}
