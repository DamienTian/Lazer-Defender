using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundscroller : MonoBehaviour {

    [SerializeField] float backgroundScrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offset;

	// Use this for initialization
	void Start () {
        // this gives the my material the render material
        myMaterial = GetComponent<Renderer>().material;

        offset = new Vector2(0f, backgroundScrollSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        // this takes the mainTexure 
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
	}
}
