using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CTileController : Controller
{
    public List<Transform> tilePos;
    public Transform trainEndPos;

    public GameObject tileParent;
    public List<GameObject> tileList;
    public List<GameObject> rangeTileList;
    public int playerCurrentTileNum;
    


    bool stageType;//false는 일반 true은 광물.

	// Use this for initialization


    void Awake()
    {
        Init();
    }

    protected override void Start()
    {
        base.Start();
    }
	
	// Update is called once per frame
    public override void DispatchGameMessage(GameMessage _gameMessage)
    {
        switch (_gameMessage.messageName)
        {
            
            case MessageName.Play_ShowPlayerSkillRange:
                TileScaleToLarge();
                ShowRange();
                break;
            case MessageName.Play_HidePlayerSkillRange:
                HideRange();
                break;
            case MessageName.Play_PlayerSkill1Used:
                TileScaleToSmall();
                break;
            case MessageName.Play_TileChangeToRed:
                TileToRed((int)_gameMessage.Get("tileNum"));
                break;
            case MessageName.Play_TileChangeToNormal:
                TileToNormal((int)_gameMessage.Get("tileNum"));
                break;
            case MessageName.Play_TileScaleToLarge:
                TileScaleToLarge();
                break;
            case MessageName.Play_TileScaleToSmall:
                TileScaleToSmall();
                break;
            case MessageName.Play_TrainHolded:
                TrainHolded();
                break;
            case MessageName.Play_TrainPutdown:
                TrainPutdown();
                break;
            case MessageName.Play_StageRestart: ResetStage();
                break;
        }
    }

    void Init()
    {
        stageType = (bool)GameMaster.Instance.tempData.Get("ClearInfo");

        tileList = new List<GameObject>();
        rangeTileList = new List<GameObject>();


        for (int i = 0; i < tilePos.Count; i++)
        {
            //toolList.Add(ObjectPooler.Instance.GetGameObject("Play_Tool_PitchingMachine"));
            //toolList.Add(ObjectPooler.Instance.GetGameObject("Play_Tool_Drum"));
            GameObject tile = ObjectPooler.Instance.GetGameObject("Play_Tile");
            tile.GetComponent<CTile>().SetController(this);
            tile.transform.position = tilePos[i].position;
            tile.GetComponent<CTile>().tileNum = i;
            tile.transform.SetParent(tileParent.transform);
            tileList.Add(tile);
        }

        //tool 3개 위치
        tileList[1].GetComponent<CTile>().ChangeToRedtileTemporarily();
        tileList[11].GetComponent<CTile>().ChangeToRedtileTemporarily();
        tileList[21].GetComponent<CTile>().ChangeToRedtileTemporarily();

        if (stageType) {

            GameObject tile = ObjectPooler.Instance.GetGameObject("Play_Tile");
            tile.GetComponent<CTile>().SetController(this);
            tile.transform.position = trainEndPos.position;
            tile.GetComponent<CTile>().tileNum = 40;
            tile.transform.SetParent(tileParent.transform);
            tileList.Add(tile);

            for (int i = 30; i < 41; i++)
            {
                tileList[i].GetComponent<CTile>().ChangeToRedtile();
            }
        }


    }
    void ResetStage() {

        foreach (GameObject tile in tileList)
        {
            tile.GetComponent<CTile>().ChangeToNormalTile();
        }

        tileList[1].GetComponent<CTile>().ChangeToRedtileTemporarily();
        tileList[11].GetComponent<CTile>().ChangeToRedtileTemporarily();
        tileList[21].GetComponent<CTile>().ChangeToRedtileTemporarily();
    
    }

    void ShowRange() {


        playerCurrentTileNum = FindObjectOfType<CPlayerController>().currentTileNum;
        if (playerCurrentTileNum % 10 == 0) {
            tileList[playerCurrentTileNum + 1].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum + 2].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum + 3].GetComponent<CTile>().ChangeToBlueTile();
        }
        else if (playerCurrentTileNum % 10 == 1)
        {
            tileList[playerCurrentTileNum - 1].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum + 1].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum + 2].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum + 3].GetComponent<CTile>().ChangeToBlueTile();
        }
        else if (playerCurrentTileNum % 10 == 2)
        {
            tileList[playerCurrentTileNum - 2].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum - 1].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum + 1].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum + 2].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum + 3].GetComponent<CTile>().ChangeToBlueTile();
        }
        else if (playerCurrentTileNum % 10 == 7)
        {
            tileList[playerCurrentTileNum - 3].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum - 2].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum - 1].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum + 1].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum + 2].GetComponent<CTile>().ChangeToBlueTile();
        }
        else if (playerCurrentTileNum % 10 == 8)
        {
            tileList[playerCurrentTileNum - 3].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum - 2].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum - 1].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum + 1].GetComponent<CTile>().ChangeToBlueTile();
        }
        else if (playerCurrentTileNum % 10 == 9)
        {
            tileList[playerCurrentTileNum - 3].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum - 2].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum - 1].GetComponent<CTile>().ChangeToBlueTile();
        }
        else
        {
            tileList[playerCurrentTileNum - 3].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum - 2].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum - 1].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum + 1].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum + 2].GetComponent<CTile>().ChangeToBlueTile();
            tileList[playerCurrentTileNum + 3].GetComponent<CTile>().ChangeToBlueTile();
        }
        
    }

    void HideRange() {
        foreach (GameObject tile in tileList)
        {
                tile.GetComponent<CTile>().ChangeToNormalTile();
        }
    }

    void TileScaleToLarge() {
        foreach (GameObject tile in tileList)
        {
            tile.GetComponent<CTile>().ChangeTileColliderScale(10f);
        }
    }
    void TileScaleToSmall() {
        foreach (GameObject tile in tileList)
        {
            tile.GetComponent<CTile>().ChangeTileColliderScale(0.2f);
        }
    }

    void TileToRed(int tileNum) {

        tileList[tileNum].GetComponent<CTile>().ChangeToRedtileTemporarily();
    }

    void TileToNormal(int tileNum)
    {
        tileList[tileNum].GetComponent<CTile>().ChangeToNormalTile();
    }

    void TrainHolded() {
        for (int i = 30; i < 41; i++) {
            tileList[i].GetComponent<CTile>().ChangeToBlueTile();
        }
    }

    void TrainPutdown()
    {
        for (int i = 30; i < 41; i++)
        {
            tileList[i].GetComponent<CTile>().ChangeToNormalTile();
        }
    }

}
