using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CTerrainController : Controller
{


    public List<GameObject> terrainList;
    public List<Transform> tilePos;


    protected override void Start()
    {
        base.Start();
        tilePos = FindObjectOfType<CTileController>().tilePos;
        Init();
    }

    // Update is called once per frame
    public override void DispatchGameMessage(GameMessage _gameMessage)
    {
        switch (_gameMessage.messageName)
        {
            case MessageName.Play_GageStop:
                StopGaging();
                break;
            case MessageName.Play_WoodAttacked:
                WoodAttacked((int)_gameMessage.Get("wood_id"), (int)_gameMessage.Get("power"));
                break;
            case MessageName.Play_PlayersObjectDamagedByMonster:
                WoodAttacked((int)_gameMessage.Get("object_id"), (int)_gameMessage.Get("monster_power"));
                break;

        }
    }

    void Init()
    {
        terrainList = new List<GameObject>();

        //24번째 타일에 나무 설치
        GameObject wood = ObjectPooler.Instance.GetGameObject("Play_Wood");
        wood.GetComponent<CWood>().SetController(this);
        wood.transform.position = tilePos[24].position;
        terrainList.Add(wood);
    }

    void StopGaging() {
        for (int i = 0; i < terrainList.Count; i++) {
            terrainList[i].GetComponent<CTerrain>().StopGaging();
        }
    }

    void WoodAttacked(int _wood_id,int power) {
        for (int i = 0; i < terrainList.Count; i++)
        {
            if (terrainList[i].GetComponent<CTerrain>().id == _wood_id) {
                terrainList[i].GetComponent<CWood>().Damaged(power);

            }
        }

    }
}
