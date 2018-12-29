using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour {

    float score = 0f;

    private void Awake()
    {
        int numberOfGameScore = FindObjectsOfType<Game>().Length;
        if (numberOfGameScore > 1)
        {
            // Destroy itself if a new Game appears
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Destroy enemies and score goes up
    public void ScoreIncrement(Enemy enemy)
    {
        score += enemy.enemyValue;
    }

    public float GetScore ()
    {
        return score;
    }

    public void ResetScore ()
    {
        // In my version:
        //      I put Level.cs and Game.cs in a same game object (GameManager)
        //      So when I destroy the gameobject, it means the GameManager 
        //      is destroied.
        Destroy(gameObject);
    }

}
