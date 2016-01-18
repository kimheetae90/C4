using UnityEngine;
using System.Collections;

public class CTile : BaseObject
{

    SpriteRenderer sprite;
    CPlayerController player;
	// Use this for initialization

    public int tileNum;

    void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
    }
	void Start () {
        player = FindObjectOfType<CPlayerController>();
	}

    protected override void UpdateState()
    {

    }

    protected override void ChangeState(ObjectState _objectState)
    {
        objectState = _objectState;
        switch (objectState)
        {

        }
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

    public void ChangeToRedtile() {
        sprite.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0f);
        gameObject.tag = "Play_Tile_Red";
    }

    
}

