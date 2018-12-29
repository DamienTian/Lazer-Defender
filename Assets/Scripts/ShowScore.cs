using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowScore : MonoBehaviour {

    TextMeshProUGUI scoreText;
    Game game;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<TextMeshProUGUI>();
        game = FindObjectOfType<Game>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = game.GetScore().ToString();
	}
}
