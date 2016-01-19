using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CTileController : Controller
{
    public List<Transform> tilePos;

    public GameObject tileParent;
    public List<GameObject> tileList;
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
            

        }
    }

    void Init()
    {
        tileList = new List<GameObject>();



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
        //24번쨰 타일에 나무더미 설치.
        tileList[24].GetComponent<CTile>().ChangeToRedtile();


    }
}
