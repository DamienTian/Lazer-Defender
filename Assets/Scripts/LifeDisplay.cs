using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour {

    Image currentLife;
    Player player;
    int lifeIndex = 0;

    private void Awake()
    {
        currentLife = gameObject.transform.GetChild(lifeIndex).GetComponent<Image>();
        player = FindObjectOfType<Player>();
    }

    public void LoseLife()
    {
        if (player.health > 0)
        {
            currentLife.enabled = false;
            lifeIndex++;
            currentLife = gameObject.transform.GetChild(lifeIndex).GetComponent<Image>();
        }
    }
}
