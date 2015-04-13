using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class C4_ButtonUI : MonoBehaviour
{

    public Button CharacterButton;
      

    List<GameObject> AllyList;
    List<Button> btlist;
    
    Canvas buttonuicanvas;


    // Use this for initialization
    void Start()
    {
        
        buttonuicanvas = this.GetComponentInChildren<Canvas>();
        AllyList = C4_GameManager.Instance.sceneMode.GetComponentInChildren<C4_PlayMode>().ListAllyGameObject;
        btlist = new List<Button>();

        for (int i = 0; i < AllyList.Count; i++)
        {
            btlist.Add(Instantiate(CharacterButton));
            btlist[i].transform.SetParent(buttonuicanvas.transform);
            btlist[i].GetComponent<C4_AllyButton>().myCharacter = C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectInList(i).gameObject;
        }

       
        allocate();


    }

    // Update is called once per frame
   
    void allocate()
    {
        int num = (AllyList.Count) / 2;

        if ((AllyList.Count) % 2 == 0)
        {
            if(num%2==0)
            num++;
            for (int i = 0; i < AllyList.Count; i++)
            {
                
                btlist[i].transform.localPosition = new Vector3(-490, 0, 0);
               
                btlist[i].transform.Translate(0, 35 * num, 0);
                
                num -=2;
                
            }
        }
        else
        {
            for (int i = 0; i < AllyList.Count; i++)
            {
                btlist[i].transform.localPosition = new Vector3(-490, 0, 0);
                btlist[i].transform.Translate(0, 70 * num, 0);
                num--;
            }
        }
    }
    
    public void done()
    {
        C4_GameManager.Instance.sceneMode.GetComponentInChildren<C4_PlayMode>().allyController.activeDone();
    }
   
}
