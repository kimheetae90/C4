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
    
    public Canvas buttonuicanvas;


    // Use this for initialization
    void Start()
    {

        buttonuicanvas = this.GetComponentInChildren<Canvas>();
        AllyList = C4_GameManager.Instance.sceneMode.GetComponentInChildren<C4_PlayMode>().ListAllyGameObject;


        for (int i = 0; i < AllyList.Count; i++)
        {
            btlist.Add(Instantiate(CharacterButton));
            btlist[i].transform.SetParent(buttonuicanvas.transform);
            btlist[i].GetComponent<C4_ButtonClick>().myCharacter = C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectInList(i).gameObject;
        }

       

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
    
    public void done()
    {
        
        
        C4_GameManager.Instance.sceneMode.GetComponentInChildren<C4_PlayMode>().allyController.activeDone();
    }
   
}
