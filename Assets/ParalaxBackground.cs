using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour {
    public MeshRenderer[] backgrounds;
    public float scrollingSpeed;
    public float[] scrollingSpeeds;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].material.SetTextureOffset("_MainTex", new Vector2(transform.position.x / (scrollingSpeed / scrollingSpeeds[i]), 0));
        }
	}
}
