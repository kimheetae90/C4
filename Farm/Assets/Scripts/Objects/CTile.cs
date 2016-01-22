using UnityEngine;
using System.Collections;

public class CTile : BaseObject
{

    SpriteRenderer sprite;
    CPlayer player;
    Color normal;
    BoxCollider coll;
	// Use this for initialization

    public int tileNum;
    public string tiletag;

    void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
        normal = sprite.color;
        coll = GetComponent<BoxCollider>();
        tiletag = "Play_Tile";
    }
	void Start () {
        player = FindObjectOfType<CPlayer>();
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
        if (gameObject.tag != "Play_Tile_Blue")
        {
            if (Input.GetMouseButton(0) && player.canHold==false)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.3f);

            }
            else if (Input.GetMouseButton(0) && player.readyToBomb==true)
            {
                sprite.color = new Color(normal.r, normal.g, normal.b, 0.3f);
            }
            if (Input.GetMouseButtonUp(0))
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
            }
        }
        else {
            if (Input.GetMouseButton(0))
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);

            }
        }
    }
    void OnMouseExit()
    {
        if (gameObject.tag != "Play_Tile_Blue")
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
        }
        else {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.3f);
        }
    }

    public void ChangeToRedtile() {
        sprite.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0f);
        tiletag = "Play_Tile_Red";
        gameObject.tag = "Play_Tile_Red";
    }

    public void ChangeToRedtileTemporarily()
    {
        sprite.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0f);
        gameObject.tag = "Play_Tile_Red";
    }

    public void ChangeToBlueTile() {

        gameObject.tag = "Play_Tile_Blue";
        sprite.color = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 0.3f);
    }

    public void ChangeToNormalTile() {
        if (tiletag == "Play_Tile") {

            sprite.color = normal;
        }
        else if (tiletag == "Play_Tile_Red")
        {
            sprite.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0f);
        }
        gameObject.tag = tiletag;
    }

    public void ChangeTileColliderScale(float _size) {
        coll.size = new Vector3(coll.size.x, coll.size.y, _size);
    }

    
}

