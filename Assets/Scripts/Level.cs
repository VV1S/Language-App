using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float secondsToReload = 1f;
    Coroutine waitAfterDeath;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<GameSession>()?.ResetGame();
    }

    public void LoadGameOver()
    {
        waitAfterDeath = StartCoroutine(WaitAndLoad());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(secondsToReload);
        SceneManager.LoadScene(2);
    }
}
