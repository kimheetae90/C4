using UnityEngine;
using System.Collections;

public class COre : CTerrain {

    public bool canHeld;
    GameObject player;
    void Start() {

        canHeld = false;
    }
	// Use this for initialization
    protected override void Complete()
    {
        canHeld = true;
    }

    void Update()
    {
        UpdateState();
    }

    protected override void UpdateState()
    {
        if (canHeld == false&&objectState==ObjectState.Play_Terrain_Complete)
        {
            transform.position = player.transform.position + new Vector3(2.0f, 0, 0);
        }

    }

    /// <summary>
    /// 툴이 플레이어에게 잡힐 때 호출하는 함수
    /// </summary>
    /// <param name="_player"></param>
    public void HoldByPlayer(GameObject _player)
    {
        if (canHeld)
        {
            player = _player;
            canHeld = false;

            GameMessage gameMsg = GameMessage.Create(MessageName.Play_TileChangeToNormal);
            gameMsg.Insert("tileNum", tileNum);
            SendGameMessageToSceneManage(gameMsg);
        }
    }

    /// <summary>
    /// 툴이 플레이어에 의해 놓여질 때 호출되는 함수.
    /// </summary>
    /// <param name="_player"></param>
    public void PutDownByPlayer(GameObject _player)
    {
        
        if (canHeld == false)
        {
            player = null;
            canHeld = true;
            GameMessage gameMsg = GameMessage.Create(MessageName.Play_TileChangeToRed);
            gameMsg.Insert("tileNum", tileNum);
            SendGameMessageToSceneManage(gameMsg);
        }
    }

    public void PutIntoTrain() {
        if (canHeld == false)
        {
            player = null;
            canHeld = true;
            GameMessage gameMsg = GameMessage.Create(MessageName.Play_PutOreIntoTrain);
            SendGameMessageToSceneManage(gameMsg);
            gameObject.SetActive(false);
            
        }
    }

    public override void Reset()
    {
        base.Reset();
        canHeld = false;
        gameObject.SetActive(true);
    }

}
