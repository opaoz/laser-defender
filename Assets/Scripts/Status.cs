using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    int score = 0;
    void Awake()
    {
        var players = FindObjectsOfType<Status>();

        if (players.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    public int GetScore()
    {
        return score;
    }

    public int AddScore(int diff)
    {
        score += diff;

        return score;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

}
