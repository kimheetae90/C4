using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CPlayerController : Controller
{
    GameObject player;
    
    GameObject selectedGameObject;
    GameObject selectedCorrectGameObject;
    public int currentTileNum;
    public Transform startPos;
    CMove move;
    bool isAdjacent;
    ObjectState playerState;
    GameObject holdedTool;

    CPlayer playerScript;

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
            case MessageName.Play_PlayersObjectDamagedByMonster:
                PlayerAttackedByEnemy((int)_gameMessage.Get("object_id"), (int)_gameMessage.Get("monster_power"));
                break;
            case MessageName.Play_PlayerMove:
                playerState = playerScript.GetPlayerState();
                ConfirmSelectedGameObject(_gameMessage);

                if (playerScript.readyToBomb)
                {   
                    if (selectedCorrectGameObject.tag == "Play_Tile_Blue" || selectedCorrectGameObject.tag == "Play_Tile" || selectedCorrectGameObject.tag == "Play_Tile_Red")
                    {
                        MoveToBomb(selectedCorrectGameObject);
                    }
                    
                }
                else if (playerState != ObjectState.Play_Player_Die)
                {
                    
                    if (selectedGameObject.transform.tag == "Play_Tool")
                    {
                        if (playerScript.canHold)
                        {
                            HoldOnTool(player, (GameObject)_gameMessage.Get("SelectedGameObject"), (Vector3)_gameMessage.Get("ClickPosition"));
                        }
                        else
                        {
                            MovePlayerToTarget(player, (Vector3)_gameMessage.Get("ClickPosition"));
                        }

                    }
                    else if (selectedGameObject.transform.tag == "Play_Terrain")
                    {
                        if (playerScript.canHold)
                        {
                            AccessToTerrain(player, (GameObject)_gameMessage.Get("SelectedGameObject"), (Vector3)_gameMessage.Get("ClickPosition"));
                        }
                        else
                        {
                            MovePlayerToTarget(player, (Vector3)_gameMessage.Get("ClickPosition"));
                        }

                    }
                    else
                    {
                        if (playerScript.canHold == false)
                        {
                            if (selectedCorrectGameObject.tag == "Play_Tile")
                            {
                                MovePlayerToTarget(player, new Vector3(selectedGameObject.transform.position.x, selectedGameObject.transform.position.y, ((Vector3)_gameMessage.Get("ClickPosition")).z));
                            }
                        }
                        else
                        {
                            MovePlayerToTarget(player, (Vector3)_gameMessage.Get("ClickPosition"));
                        }
                    }
                }
                break;
            case MessageName.Play_StageFailed:
                playerScript.ReadyToPause();
                break;
            case MessageName.Play_ShowPlayerSkillRange:
                ReadyToBomb();
                break;
            case MessageName.Play_ToolDiedWhileHelded:
                PutDownTool(player, (GameObject)_gameMessage.Get("tool"));
                break;
            case MessageName.Play_StageRestart:
                ResetStage();
                
                break;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////
    //////////////////////// 			구현               ////////////////////////
    /// //////////////////////////////////////////////////////////////////////////

    void Init()
    {
        player = ObjectPooler.Instance.GetGameObject("Play_Player");
        playerScript = player.GetComponent<CPlayer>();
        playerScript.SetController(this);
        player.transform.position = new Vector3(startPos.position.x, startPos.position.y, startPos.position.z);
        isAdjacent = false;
        move = player.GetComponent<CMove>();
        currentTileNum = 0;
    }

    /// <summary>
    /// 플레이어가 공격받았을때 파라미터로 넘겨준 데미지 값 만큼 플레이어의 hp를 감소시키는 함수
    /// </summary>
    /// <param name="_player"></param>
    /// <param name="_damage"></param>
    void PlayerAttackedByEnemy(int _id, int _damage)
    {
        if (_id == playerScript.id)
        {
            playerScript.Damaged(_damage);
        }
    }

    void MoveToBomb(GameObject _selectedTile) {

        if (_selectedTile.tag == "Play_Tile_Blue") {
            if (_selectedTile.transform.position.x >= player.transform.position.x)
            {
                playerScript.transform.localScale = new Vector3(1, 1, 1);
            }
            else 
            {
                playerScript.transform.localScale = new Vector3(-1, 1, 1);
            }
            GameMessage gameMsg2 = GameMessage.Create(MessageName.Play_PlayerSkill1Used);
            gameMsg2.Insert("tilePos", _selectedTile.transform.position);
            SendGameMessage(gameMsg2);
            playerScript.readyToBomb = false;
            
        }
        else if (_selectedTile.tag == "Play_Tile" || _selectedTile.tag == "Play_Tile_Red") {
            Vector3 targetPos;
            int tileNum;
            if (currentTileNum % 10 == _selectedTile.GetComponent<CTile>().tileNum % 10) { 
                //같은 타일 col에 있을때.
                targetPos = new Vector3(player.transform.position.x - 2f, _selectedTile.transform.position.y, 0);
                if (_selectedTile.GetComponent<CTile>().tileNum % 10 == 0)
                {
                    tileNum = _selectedTile.GetComponent<CTile>().tileNum;
                }
                else {
                    tileNum = _selectedTile.GetComponent<CTile>().tileNum - 1;
                }
                
                
            }
            else if (Mathf.Abs((currentTileNum % 10) - (_selectedTile.GetComponent<CTile>().tileNum % 10))<=3)
            {//x좌표상 거리가 타일 3개 내에 있을 때.
                targetPos = new Vector3(player.transform.position.x,_selectedTile.transform.position.y,0);
                tileNum = (_selectedTile.GetComponent<CTile>().tileNum / 10) * 10 + currentTileNum % 10;
            }
            else{//멀리있을때
                if (_selectedTile.transform.position.x >= player.transform.position.x)
                {
                    targetPos = new Vector3(_selectedTile.transform.position.x - 2f * 3f, _selectedTile.transform.position.y, 0);
                    tileNum = _selectedTile.GetComponent<CTile>().tileNum - 3;
                }
                else
                {
                    targetPos = new Vector3(_selectedTile.transform.position.x + 2f * 3f, _selectedTile.transform.position.y, 0);
                    tileNum = _selectedTile.GetComponent<CTile>().tileNum + 3;
                }
            
            }
            //move
            move.SetTargetPos(targetPos);
            move.StartMove();

            playerScript.ChangeStateToMove();

            StopCoroutine("CheckDistanceToTile");
            StartCoroutine(CheckDistanceToTile(targetPos,tileNum));
        }
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_HidePlayerSkillRange);
        SendGameMessage(gameMsg);
    }


    /// <summary>
    /// 파라미터로 넘겨준 목표지점(Vector3)으로 이동하는 함수.
    /// </summary>
    /// <param name="_player"></param>
    /// <param name="_targetPos"></param>
    void MovePlayerToTarget(GameObject _player, Vector3 _targetPos)
    {
        _targetPos.z = 0;

        GameMessage gameMsg = GameMessage.Create(MessageName.Play_GageStop);
        SendGameMessage(gameMsg);
        /* 플레이어가 울타리를 넘어갈때.
            if (_targetPos.x <= -15)
            {
                _targetPos = new Vector3(-15, _targetPos.y, _targetPos.z);
            }
             */
        
        
        if (selectedCorrectGameObject.tag == "Play_Tile" || selectedCorrectGameObject.tag == "Play_Tile_Red")
        {

           _targetPos = selectedCorrectGameObject.transform.position;
        }

        if (_targetPos.y >= -0.3f)
        {
            _targetPos = new Vector3(_targetPos.x, -0.3f, _targetPos.z);
        }
        move.SetTargetPos(_targetPos);
        move.StartMove();

        playerScript.ChangeStateToMove();

        StopCoroutine("CheckDistanceToTile");
        StartCoroutine(CheckDistanceToTile(_targetPos,0));

        if (playerScript.canHold == false)
        {//tool을 들고있을때
            if (selectedCorrectGameObject.tag == "Play_Tile")
            {
                _targetPos = new Vector3(selectedCorrectGameObject.transform.position.x - 2, selectedCorrectGameObject.transform.position.y, _targetPos.z);
                /* 플레이어가 울타리를 넘어갈때.
                if (_targetPos.x <= -15)
                {
                    _targetPos = new Vector3(-15, _targetPos.y, _targetPos.z);
                }
                 */
                move.SetTargetPos(_targetPos);
                if (holdedTool.GetComponent<CTool>().isAlive)
                {
                    holdedTool.GetComponent<CTool>().ChangeStateToMove();
                }
                if (_targetPos.x > player.transform.position.x)
                {
                    playerScript.ChangeStateToMoveFrontWithTool();
                }
                else
                {
                    playerScript.ChangeStateToMoveBackWithTool();
                }
                StopCoroutine("CheckPutDownTool");
                StartCoroutine(CheckPutDownTool(_targetPos));
            }
        }
        else {
            playerScript.ChangeStateToMove();
            if (_targetPos.x > player.transform.position.x)
            {
                playerScript.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                playerScript.transform.localScale = new Vector3(-1, 1, 1);
            }
        
        }
    }

    void ReadyToBomb() {
        player.GetComponent<CPlayer>().readyToBomb = true;
    }

    void AccessToTerrain(GameObject _player, GameObject _terrain, Vector3 _click_position)
    {
        _click_position.z = 0;
        if (_terrain.GetComponent<CTerrain>().canAccess)
        {
            Vector3 targetPos;
            if (_player.transform.position.x <= _terrain.transform.position.x)
            {
                targetPos = new Vector3(_click_position.x - 2, _click_position.y, _click_position.z);
            }
            else {
                targetPos = new Vector3(_click_position.x + 2, _click_position.y, _click_position.z);
            }
            /* 플레이어가 울타리를 넘어갈때.
                if (_targetPos.x <= -15)
                {
                    _targetPos = new Vector3(-15, _targetPos.y, _targetPos.z);
                }
                 */
            MovePlayerToTarget(_player, targetPos);
            StopCoroutine("CheckDistanceToTerrain");
            StartCoroutine(CheckDistanceToTerrain(targetPos));
        }
    }

    /// <summary>
    /// 툴을 클릭하였을 때, 상태에 따라 툴을 집어들거나 내려놓는 함수.
    /// </summary>
    /// <param name="_player"></param>
    /// <param name="_tool"></param>
    /// <param name="_click_position"></param>
    void HoldOnTool(GameObject _player, GameObject _tool, Vector3 _click_position)
    {
        _click_position.z = 0;
        if (_tool.GetComponent<CTool>().canHeld) {
            HoldTool(_player, _tool, _click_position);
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
        Vector3 targetPos = new Vector3(_click_position.x - 2, _click_position.y, _click_position.z);
        /* 플레이어가 울타리를 넘어갈때.
            if (_targetPos.x <= -15)
            {
                _targetPos = new Vector3(-15, _targetPos.y, _targetPos.z);
            }
             */
        MovePlayerToTarget(_player, targetPos);
        StopCoroutine("CheckIsAdjacentToTool");
        StartCoroutine(CheckIsAdjacentToTool(targetPos));
    }

    /// <summary>
    /// 플레이어가 툴을 내려놓을 때 호출하는 함수.
    /// </summary>
    /// <param name="_player"></param>
    /// <param name="_tool"></param>
    void PutDownTool(GameObject _player, GameObject _tool)
    {
        _tool.GetComponent<CTool>().PutDownByPlayer(_player);
        playerScript.PutDownTool(_tool);
        isAdjacent = false;
        holdedTool = null;
    }
    /// <summary>
    /// 툴을 놓을때 타겟포지션과 거리가 0.1차이 날때 내려놓게 하는 코루틴.
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    IEnumerator CheckPutDownTool(Vector3 targetPos) {
        
        float distance = Vector3.Distance(player.transform.position, targetPos);

        while (distance > 0.1f)
        {
            distance = Vector3.Distance(player.transform.position, targetPos);
            if (distance <= 0.1f && holdedTool != null )
            {
                if (selectedGameObject.transform.tag != "Play_Tool")
                {
                    holdedTool.GetComponent<CTool>().currentTileNum = selectedCorrectGameObject.GetComponent<CTile>().tileNum;
                    PutDownTool(player, holdedTool);
                    currentTileNum = selectedCorrectGameObject.GetComponent<CTile>().tileNum-1;
                    if (selectedCorrectGameObject.GetComponent<CTile>().tileNum % 10 == 0) {
                        currentTileNum = selectedCorrectGameObject.GetComponent<CTile>().tileNum;
                    }

                }
                else {
                    holdedTool.GetComponent<CTool>().ChangeStateToReady();
                }
                break;
            }
            yield return null;
        }

    }

    
    /// <summary>
    /// 툴을 집으러 갈 때, 툴과 거리가 0.1이하가 되었을 때만 툴을 집어드는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckIsAdjacentToTool(Vector3 targetPos)
    {
        while (!isAdjacent)
        {
            if (Vector3.Distance(player.transform.position, targetPos) < 0.1f)
            {
                if (selectedGameObject.GetComponent<CTool>() != null)
                {
                    isAdjacent = true;
                    selectedGameObject.GetComponent<CTool>().HoldByPlayer(player);
                    playerScript.HoldTool(selectedGameObject);
                    holdedTool = selectedGameObject;
                }
            }
            yield return null;          
        }
    }

    //도착햇는지 확인
    IEnumerator CheckDistanceToTile(Vector3 targetPos,int tileNum)
    {
        while (selectedCorrectGameObject.tag == "Play_Tile" || selectedCorrectGameObject.tag == "Play_Tile_Red")
        {
            if (Vector3.Distance(player.transform.position, targetPos) < 0.1f)
            {
                currentTileNum = selectedCorrectGameObject.GetComponent<CTile>().tileNum;
                if (playerScript.readyToBomb) {
                    currentTileNum = tileNum;
                    if (player.transform.position.x <= selectedCorrectGameObject.transform.position.x)
                    {
                        playerScript.transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        playerScript.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    GameMessage gameMsg = GameMessage.Create(MessageName.Play_PlayerSkill1Used);
                    gameMsg.Insert("tilePos", selectedCorrectGameObject.transform.position);
                    SendGameMessage(gameMsg);

                    playerScript.readyToBomb = false;
                }
                break;
            }
            yield return null;
        }
    }

    IEnumerator CheckDistanceToTerrain(Vector3 targetPos)
    {
        while (selectedGameObject.tag == "Play_Terrain")
        {
            if (Vector3.Distance(player.transform.position, targetPos) < 0.1f)
            {
                if (selectedGameObject.GetComponent<CTerrain>() != null)
                {
                    if (player.transform.position.x <= selectedGameObject.transform.position.x)
                    {
                        playerScript.transform.localScale = new Vector3(1, 1, 1);
                        if (selectedGameObject.GetComponent<CTerrain>().tileNum % 10 == 0)
                        {
                            currentTileNum = selectedGameObject.GetComponent<CTerrain>().tileNum;
                        }
                        else {
                            currentTileNum = selectedGameObject.GetComponent<CTerrain>().tileNum-1;
                        }
                    }
                    else
                    {
                        playerScript.transform.localScale = new Vector3(-1, 1, 1);

                        currentTileNum = selectedGameObject.GetComponent<CTerrain>().tileNum + 1;
                    }
                    selectedGameObject.GetComponent<CTerrain>().ChangeStateToGaging();

                }
                break;
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
        selectedCorrectGameObject = (GameObject)_gameMessage.Get("selectedCorrectGameObject");
        
    }

    /// <summary>
    /// 게임을 다시 시작하면 불러지는 함수.
    /// </summary>
    void ResetStage() {
        player.transform.position = new Vector3(startPos.position.x, startPos.position.y, startPos.position.z);
        isAdjacent = false;
        playerScript.Reset();
        
    }

}
