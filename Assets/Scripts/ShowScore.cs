using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowScore : MonoBehaviour {

    TextMeshPro scoreText;
    Game game;

	// Use this for initialization
	void Start () {
        scoreText = GameObject.FindWithTag("Score").GetComponent<TextMeshPro>();
        game = FindObjectOfType<Game>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = game.GetScore().ToString();
	}
}
