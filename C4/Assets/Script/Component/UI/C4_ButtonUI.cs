using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class C4_ButtonUI : MonoBehaviour
{

    public Button CharacterButton;
    //public Button char2;
    //public Button char3;
    //public Button char4;
      

    public List<GameObject> AllyList;
    public List<Button> btlist;
    public int count;

    public Canvas buttonuicanvas;


    // Use this for initialization
    void Start()
    {

        buttonuicanvas = this.GetComponentInChildren<Canvas>();
        //buttonuicanvas.transform.SetParent(gameobject);
        //playmode = GameObject.Find("PlayMode").GetComponent<C4_PlayMode>();
        AllyList = C4_GameManager.Instance.sceneMode.GetComponentInChildren<C4_PlayMode>().ListAllyGameObject;



        //btlist.Add(char1);
        //btlist.Add(char2);
        btlist.Add(Instantiate(CharacterButton));
        btlist.Add(Instantiate(CharacterButton));
        btlist[0].transform.SetParent(buttonuicanvas.transform);
        btlist[1].transform.SetParent(buttonuicanvas.transform);
        
        //btlist[0].GetComponent<C4_ButtonClick>().myCharacter = AllyList[0];
        //btlist[1].GetComponent<C4_ButtonClick>().myCharacter = AllyList[1];
        btlist[0].GetComponent<C4_ButtonClick>().myCharacter = C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectInList(0).gameObject;
        btlist[1].GetComponent<C4_ButtonClick>().myCharacter = C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectInList(1).gameObject;
        Debug.Log(btlist[0].GetComponent<C4_ButtonClick>().myCharacter.ToString());
        //btlist.Add(char3);
        //btlist.Add(char4);
        //char1.transform.Translate(0, 70, 0);
        allocate();


    }

    // Update is called once per frame
   
    void allocate()
    {
        int num = AllyList.Count / 2;
        if (AllyList.Count % 2 == 0)
        {
            for (int i = 0; i < AllyList.Count; i++)
            {
                btlist[i].transform.Translate(0, 35 * num, 0);
                num -= 2;
            }
        }
        else
        {
            for (int i = 0; i < AllyList.Count; i++)
            {
                btlist[i].transform.Translate(0, 70 * num, 0);
                num--;
            }
        }
        //char1.transform.Translate(0, 70, 0);
    }
    public void selectAlly1()
    {

        C4_GameManager.Instance.sceneMode.GetComponentInChildren<C4_PlayMode>().allyController.selectClickObject(C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectInList(0).gameObject);
    }
    public void selectAlly2()
    {
        C4_GameManager.Instance.sceneMode.GetComponentInChildren<C4_PlayMode>().allyController.selectClickObject(C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectInList(1).gameObject);
    }
    public void done()
    {
        
        
        C4_GameManager.Instance.sceneMode.GetComponentInChildren<C4_PlayMode>().allyController.activeDone();
    }
    public void movetoselect()
    {
        //C4_GameManager.Instance.GetComponentInChildren<C4_PlaySceneCamera>().moveToSomeObject();
        //Camera.main.GetComponent<C4_PlaySceneCamera>().moveToSomeObject();
        Camera.main.gameObject.GetComponent<C4_PlaySceneCamera>().moveToSomeObject();
    }
}
