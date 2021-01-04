using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] string gameScene = "Game";
    [SerializeField] string startScene = "Start";
    [SerializeField] string overScene = "Over";

    [Header("Config")]
    [SerializeField] int delay = 2;

    public void LoadGameOver()
    {
        StartCoroutine(GameOver());
    }

    public void LoadStartOver()
    {
        var status = FindObjectOfType<Status>();
        status.ResetGame();
        SceneManager.LoadScene(startScene);
    }

    public void LoadGame()
    {
        var status = FindObjectOfType<Status>();
        status.ResetGame();
        SceneManager.LoadScene(gameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(overScene);
    }
}
