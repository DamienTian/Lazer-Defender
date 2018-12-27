using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour {

    float score = 0f;
    [SerializeField] TextMeshPro scoreText;

    void Start()
    {
        int numberOfGameScore = FindObjectsOfType<Game>().Length;
        if (numberOfGameScore > 1)
        {
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
        Destroy(gameObject);
    }

}
