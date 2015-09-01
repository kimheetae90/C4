using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CPlayerController : Controller
{
    GameObject player;
    GameObject selectedGameObject;
    public Transform startPos;
    CMove move;
    bool isAdjacent;

    void Awake()
    {
        Init();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void DispatchGameMessage(GameMessage _gameMessage)
    {
        switch (_gameMessage.messageName)
        {
            case MessageName.Play_PlayerDamagedByMonster:
                PlayerAttackedByEnemy(player, (int)_gameMessage.Get("monster_power"));
                break;
            case MessageName.Play_PlayerMove:
                ConfirmSelectedGameObject(_gameMessage);
                
                if (selectedGameObject.tag == "Play_Tool")
                {
                    HoldOrPutDownTool(player, (GameObject)_gameMessage.Get("SelectedGameObject"), (Vector3)_gameMessage.Get("ClickPosition"));
                }
                else
                {
                    MovePlayerToTarget(player, (Vector3)_gameMessage.Get("ClickPosition"));
                }
                break;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////
    //////////////////////// 			구현               ////////////////////////
    /// //////////////////////////////////////////////////////////////////////////

    void Init()
    {
        player = ObjectPooler.Instance.GetGameObject("Play_Player");
        player.GetComponent<CPlayer>().SetController(this);
        player.transform.position = new Vector3(startPos.position.x, startPos.position.y + 0.5f, startPos.position.z);
        isAdjacent = false;
        move = player.GetComponent<CMove>();
    }

    /// <summary>
    /// 플레이어가 공격받았을때 파라미터로 넘겨준 데미지 값 만큼 플레이어의 hp를 감소시키는 함수
    /// </summary>
    /// <param name="_player"></param>
    /// <param name="_damage"></param>
    void PlayerAttackedByEnemy(GameObject _player, int _damage)
    {
        _player.GetComponent<CPlayer>().Damaged(_damage);
    }

    /// <summary>
    /// 파라미터로 넘겨준 목표지점(Vector3)으로 이동하는 함수.
    /// </summary>
    /// <param name="_player"></param>
    /// <param name="_targetPos"></param>
    void MovePlayerToTarget(GameObject _player, Vector3 _targetPos)
    {
        _player.GetComponent<CMove>().SetTargetPos(_targetPos);
        _player.GetComponent<CMove>().StartMove();
    }

    /// <summary>
    /// 툴을 클릭하였을 때, 상태에 따라 툴을 집어들거나 내려놓는 함수.
    /// </summary>
    /// <param name="_player"></param>
    /// <param name="_tool"></param>
    /// <param name="_click_position"></param>
    void HoldOrPutDownTool(GameObject _player, GameObject _tool, Vector3 _click_position)
    {
		ObjectState toolState = _tool.GetComponent<CTool>().GetToolState();

        switch (toolState)
        {
		case ObjectState.Play_Tool_CanHeld:
		case ObjectState.Play_Tool_CanAttack:
                HoldTool(_player, _tool, _click_position);
                break;
		case ObjectState.Play_Tool_CanNotHeld:
                PutDownTool(_player, _tool);
                break;
		case ObjectState.Play_Tool_UnAvailable:
                break;
        }
    }

    /// <summary>
    /// 플레이어가 툴을 클릭하였을 때, 툴을 집어드는 함수.
    /// </summary>
    /// <param name="_player"></param>
    /// <param name="_tool"></param>
    /// <param name="_click_position"></param>
    void HoldTool(GameObject _player, GameObject _tool, Vector3 _click_position)
    {
        MovePlayerToTarget(_player, _click_position);
        StartCoroutine("CheckIsAdjacentToTool");
    }

    /// <summary>
    /// 플레이어가 툴을 내려놓을 때 호출하는 함수.
    /// </summary>
    /// <param name="_player"></param>
    /// <param name="_tool"></param>
    void PutDownTool(GameObject _player, GameObject _tool)
    {
        _tool.GetComponent<CTool>().PutDownByPlayer(_player);
        _player.GetComponent<CPlayer>().PutDownTool(_tool);
        isAdjacent = false;
    }

    /// <summary>
    /// 툴을 집으러 갈 때, 툴과 거리가 2.0이하가 되었을 때만 툴을 집어드는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckIsAdjacentToTool()
    {
        while (!isAdjacent)
        {
            if (DistancePlayerToTool(player, selectedGameObject) < 2.0f)
            {
                isAdjacent = true;
                selectedGameObject.GetComponent<CTool>().HoldByPlayer(player);
                player.GetComponent<CPlayer>().HoldTool(selectedGameObject);
            }

            yield return null;          
        }
    }

    /// <summary>
    /// 두 오브젝트 사이의 거리를 float값으로 리턴하는 함수.
    /// </summary>
    /// <param name="_player"></param>
    /// <param name="_tool"></param>
    /// <returns></returns>
    float DistancePlayerToTool(GameObject _player, GameObject _tool)
    {
        return Vector3.Distance(_player.transform.position, _tool.transform.position);
    }

    /// <summary>
    /// 유저가 화면상에서 클릭한 오브젝트를 selectedGameObject라는 임시변수에 저장하는 함수.
    /// </summary>
    /// <param name="_gameMessage"></param>
    void ConfirmSelectedGameObject(GameMessage _gameMessage)
    {
        selectedGameObject = (GameObject)_gameMessage.Get("SelectedGameObject");
    }

}
