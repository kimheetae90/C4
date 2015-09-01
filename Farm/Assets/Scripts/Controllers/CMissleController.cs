using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CMissleController : Controller{

    Dictionary<int, Queue<GameObject>> missleDic;

    Dictionary<int, GameObject> firedMissleDic;

    public int missleAmount;

    void Awake()
	{
		Init ();
	}

	protected override void Start () {
		base.Start ();
        Generate();
	}

   
    public override void DispatchGameMessage(GameMessage _gameMessage)
    {
        switch (_gameMessage.messageName)
        {
            case MessageName.Play_MissleAttackMonster:
                MissleAttackMonster((int)_gameMessage.Get("monster_id"), (int)_gameMessage.Get("missle_power"));
                MissleDisappear((int)_gameMessage.Get("tool_id"), (int)_gameMessage.Get("missle_id"));
                break;
            case MessageName.Play_MissleOrderedByTool:
                MissleOrderedByTool((int)_gameMessage.Get("tool_id"), (Vector3)_gameMessage.Get("tool_position"));
                break;
            case MessageName.Play_MissleDisappear:
                MissleDisappear((int)_gameMessage.Get("tool_id"), (int)_gameMessage.Get("missle_id"));
                break;

        }
    }

    /*
    GameObject FindMissleOfID(int _index,int _id)
    {
        return missleList[_index].Find(missle => missle.GetComponent<CMissle>().id == _id) as GameObject;
    }
    */

    void Init()
    {
        missleDic = new Dictionary<int, Queue<GameObject>>();
        firedMissleDic = new Dictionary<int, GameObject>();

        missleAmount = 5;
    }
    

    void Generate() {

        CToolController toolController = FindObjectOfType<CToolController>();
        

        for (int i = 0; i < toolController.toolList.Count; i++)
        {
            int _id = toolController.toolList[i].GetComponent<CTool>().id;
            missleDic.Add(_id, new Queue<GameObject>());

            for (int j = 0; j < missleAmount; j++) {
                GameObject _missle = ObjectPooler.Instance.GetGameObject("Missle");
                _missle.GetComponent<CMissle>().SetController(this);
                _missle.GetComponent<CMissle>().SetOwner(toolController.toolList[i].GetComponent<CTool>());
                _missle.SetActive(false);
                missleDic[_id].Enqueue(_missle);
            }
        }
        
    }



    void MissleOrderedByTool(int _tool_id, Vector3 _tool_position) {
       
        GameObject _missle = (GameObject)missleDic[_tool_id].Dequeue();
        _missle.SetActive(true);
        _missle.transform.position = new Vector3(_tool_position.x, _tool_position.y, _tool_position.z);
        _missle.GetComponent<CMissle>().ReadyToMove();
        _missle.GetComponent<CMove>().SetTargetPos(new Vector3(transform.position.x + 100, transform.position.y, transform.position.z));
        _missle.GetComponent<CMove>().StartMove();

        firedMissleDic.Add(_missle.GetComponent<CMissle>().id, _missle);
    }

    void MissleAttackMonster(int _monster_id, int _missle_power) {

        GameMessage gameMsg = GameMessage.Create(MessageName.Play_MonsterDamagedByMissle);
        gameMsg.Insert("monster_id", _monster_id);
        gameMsg.Insert("missle_power", _missle_power);
        SendGameMessage(gameMsg);
    }

    void MissleDisappear(int _tool_id, int _id)
    {
        GameObject _missle = firedMissleDic[_id];
        firedMissleDic.Remove(_id);
        _missle.GetComponent<CMove>().isMove = false;
        _missle.SetActive(false);
        missleDic[_tool_id].Enqueue(_missle);
    }
}
