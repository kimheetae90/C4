﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CTerrainController : Controller
{


    public List<GameObject> terrainList;
    public List<Transform> tilePos;
    List<StageInfo> stageInfo;

    int MaxOre;
    int OreCount;

    void Awake() {
        stageInfo = (List<StageInfo>)GameMaster.Instance.tempData.Get("StageInfo");
    }

    protected override void Start()
    {
        base.Start();
        tilePos = FindObjectOfType<CTileController>().tilePos;
        Init();
        OreCount = 0;
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

        foreach (StageInfo node in stageInfo)
        {
            if (node.wave == 0)
            {
                if (node.id == 99998)
                { //나무
                    int tileNum = (node.line - 1) * 10 + (node.time - 1);
                    GameObject wood = ObjectPooler.Instance.GetGameObject("Play_Wood");
                    wood.GetComponent<CWood>().SetController(this);
                    wood.transform.position = tilePos[tileNum].position;
                    wood.GetComponent<CWood>().tileNum = tileNum;
                    terrainList.Add(wood);
                    GameMessage gameMsg = GameMessage.Create(MessageName.Play_TileChangeToRed);
                    gameMsg.Insert("tileNum", tileNum);
                    SendGameMessage(gameMsg);

                }
                else if (node.id == 99999)
                { //광물
                    /*
                    int tileNum = (node.line - 1) * 10 + (node.time - 1);
                    GameObject ore = ObjectPooler.Instance.GetGameObject("Play_Ore");
                    ore.GetComponent<COre>().SetController(this);
                    ore.transform.position = tilePos[tileNum].position;
                    ore.GetComponent<COre>().tileNum = tileNum;
                    terrainList.Add(ore);
                    GameMessage gameMsg = GameMessage.Create(MessageName.Play_TileChangeToRed);
                    gameMsg.Insert("tileNum", tileNum);
                    SendGameMessage(gameMsg);
                    MaxOre++;
                     */
                }
            }
        }

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
