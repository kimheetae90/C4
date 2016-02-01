using UnityEngine;
using System.Collections;

public class CTool_Train : CTool{

    public bool isFull;

    protected override void Start(){
    
        base.Start();
        isFull = false;
    }

	// Use this for initialization
    protected override void ToolShot()
    {

    }

    public override void Reset()
    {
        base.Reset();
        isFull = false;
    }

    /// <summary>
    /// 툴이 플레이어에게 잡힐 때 호출하는 함수
    /// </summary>
    /// <param name="_player"></param>
    public override void HoldByPlayer(GameObject _player)
    {
        if (canHeld)
        {
            player = _player;
            canHeld = false;
            shotable = false;

            if (attackRangeScript != null)
            {
                attackRangeScript.StopCoroutine("ToolAttack");
            }

            if (isAlive)
            {
                ChangeState(ObjectState.Play_Tool_Ready);
            }

            GameMessage gameMsg = GameMessage.Create(MessageName.Play_TrainHolded);
            SendGameMessageToSceneManage(gameMsg);
            GameMessage gameMsg2 = GameMessage.Create(MessageName.Play_TileScaleToLarge);
            SendGameMessageToSceneManage(gameMsg2);
        }
    }

    /// <summary>
    /// 툴이 플레이어에 의해 놓여질 때 호출되는 함수.
    /// </summary>
    /// <param name="_player"></param>
    public override void PutDownByPlayer(GameObject _player)
    {
        if (canHeld == false)
        {
            player = null;
            lineHelper.OrderingYPos(gameObject);
            canHeld = true;
            shotable = true;
            if (isAlive)
            {
                ChangeState(ObjectState.Play_Tool_Ready);
            }
            GameMessage gameMsg = GameMessage.Create(MessageName.Play_TrainPutdown);
            SendGameMessageToSceneManage(gameMsg);
            GameMessage gameMsg2 = GameMessage.Create(MessageName.Play_TileScaleToSmall);
            SendGameMessageToSceneManage(gameMsg2);
            //StartAttack();

            if (currentTileNum == 40&&isFull==true) {
                GameMessage gameMsg3 = GameMessage.Create(MessageName.Play_OneWaveOver);
                SendGameMessageToSceneManage(gameMsg3);
            }
        }
    }

    public override void Damaged(int damage)
    {
        
    }
}
