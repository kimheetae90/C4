using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class C4_ButtonUI : MonoBehaviour
{

    public Button char1;
    public Button char2;
    //public Button char3;
    //public Button char4;
      

    public List<GameObject> List;
    public List<Button> btlist;
    public int count;



    // Use this for initialization
    void Start()
    {

        //playmode = GameObject.Find("PlayMode").GetComponent<C4_PlayMode>();
        List = C4_GameManager.Instance.sceneMode.GetComponentInChildren<C4_PlayMode>().ListAllyGameObject;
        
        

        btlist.Add(char1);
        btlist.Add(char2);
        //btlist.Add(char3);
        //btlist.Add(char4);
        //char1.transform.Translate(0, 70, 0);
        allocate();


    }

    // Update is called once per frame
   
    void allocate()
    {
        int num = List.Count / 2;
        if (List.Count % 2 == 0)
        {
            for (int i = 0; i < List.Count; i++)
            {
                btlist[i].transform.Translate(0, 35 * num, 0);
                num -= 2;
            }
        }
        else
        {
            for (int i = 0; i < List.Count; i++)
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
}
