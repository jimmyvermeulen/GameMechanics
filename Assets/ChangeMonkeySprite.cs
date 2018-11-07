using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMonkeySprite : MonoBehaviour {
    SpriteRenderer monkeySpriteRenderer;
    public Sprite angryMonkeySprite;
	// Use this for initialization
	void Start () {
		if(GameManager.instance.level == 4)
        {
            monkeySpriteRenderer = GetComponent<SpriteRenderer>();
            monkeySpriteRenderer.color = new Color(255, 160, 160);
            monkeySpriteRenderer.sprite = angryMonkeySprite;
        }
	}
}
