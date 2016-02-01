using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CFenceController : Controller
{

    public List<GameObject> fenceList;
    public List<Transform> fencePosition;

    public GameObject fenceFrame;
    public GameObject fenceFrameShort;

    bool stageType;//false는 일반 true은 광물.

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
                FenceAttackedByEnemy((int)_gameMessage.Get("object_id"), (int)_gameMessage.Get("monster_power"));
                break;
            case MessageName.Play_FenceDie: FenceDie((int)_gameMessage.Get("fence_id"));
                break;
            case MessageName.Play_StageRestart: ResetStage();
                break;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////
    //////////////////////// 			구현               ////////////////////////
    /// //////////////////////////////////////////////////////////////////////////
    
    /// <summary>
    /// 파라미터로 받은 id값에 해당하는 게임오브젝트를 찾아서 리턴해주는 함수.
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    GameObject FindFenceOfID(int _id)
    {
        return fenceList.Find(fence => fence.GetComponent<CFence>().id == _id) as GameObject;
    }
    
    void Init()
    {

        stageType = (bool)GameMaster.Instance.tempData.Get("ClearInfo");
        fenceList = new List<GameObject>();

        for (int i = 0; i < 3; i++)
        {
            fenceList.Add(ObjectPooler.Instance.GetGameObject("Play_Fence"));
            fenceList[i].GetComponent<CFence>().SetController(this);
            fenceList[i].SetActive(true);
            fenceList[i].transform.position = fencePosition[i].position;
        }

        if (stageType == false)
        {
            fenceList.Add(ObjectPooler.Instance.GetGameObject("Play_Fence"));
            fenceList[3].GetComponent<CFence>().SetController(this);
            fenceList[3].SetActive(true);
            fenceList[3].transform.position = fencePosition[3].position;
            fenceFrame.SetActive(true);
            fenceFrameShort.SetActive(false);
            
        }
        else {
            fenceFrame.SetActive(false);
            fenceFrameShort.SetActive(true);
        }

        
    }

    /// <summary>
    /// 파라미터로 넘겨준 id값에 해당하는 울타리가 공격 당했을때 몬스터의 공격력만큼
    /// 울타리의 hp를 감소시키는 함수.
    /// </summary>
    /// <param name="_id"></param>
    /// <param name="_monster_power"></param>
    void FenceAttackedByEnemy(int _id, int _monster_power)
    {
        if (FindFenceOfID(_id) != null)
        {
            FindFenceOfID(_id).GetComponent<CFence>().Damaged(_monster_power);
        }
    }

    /// <summary>
    /// 파라미터로 받은 id값에 해당하는 울타리가 파괴될 때 호출하는 함수.
    /// </summary>
    /// <param name="_id"></param>
    void FenceDie(int _id) {
        GameMessage gameMsg = GameMessage.Create(MessageName.Play_FenceDisappear_MonsterMove);
        gameMsg.Insert("fence_id", _id);
        SendGameMessage(gameMsg);
        FindFenceOfID(_id).SetActive(false);
    }
    /// <summary>
    /// 게임이 다시시작하면 불러지는 함수.
    /// </summary>
    void ResetStage() {
        for (int i = 0; i < fenceList.Count; i++)
        {
            fenceList[i].SetActive(true);
            fenceList[i].transform.position = fencePosition[i].position;
            fenceList[i].GetComponent<CFence>().Reset();
        }
    }
}
