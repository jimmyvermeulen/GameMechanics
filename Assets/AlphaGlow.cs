using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaGlow : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    Color spriteColor;
    public float alphaAmplitude;
    public float alphaFrequency;
    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteColor = spriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
        Color tempColor = spriteColor;
        tempColor.a = tempColor.a + (Mathf.Cos(Time.time * alphaFrequency) * alphaAmplitude);
        spriteRenderer.color = tempColor;
	}
}
