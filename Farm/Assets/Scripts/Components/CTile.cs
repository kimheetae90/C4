using UnityEngine;
using System.Collections;

public class CTile : MonoBehaviour {

    SpriteRenderer sprite;
    CPlayerController player;
	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
        player = FindObjectOfType<CPlayerController>();
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0)&&player.isAdjacent)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.3f);
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
        }
    }
    void OnMouseExit()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
    }
}

