using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class C4_ButtonUI : MonoBehaviour
{

    public Button CharacterButton;

    List<Button> btlist;

    Canvas buttonuicanvas;
    int Allynum;
    // Use this for initialization

    public GameObject pausebox;
    public GameObject PlayerUI;
    public void initButtonUI()
    {
        buttonuicanvas = this.GetComponentInChildren<Canvas>();
        btlist = new List<Button>();
        Allynum = C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectCount();

        for (int i = 0; i < Allynum; i++)
        {
            btlist.Add(Instantiate(CharacterButton));
            btlist[i].transform.SetParent(buttonuicanvas.transform,false);
            btlist[i].GetComponent<C4_AllyButton>().myCharacter = C4_GameManager.Instance.objectManager.getSubObjectManager(GameObjectType.Ally).getObjectInList(i).gameObject;
        }

        allocate();
        pausebox.SetActive(false);
        
    }
    
    // Update is called once per frame
   
    void allocate()
    {
        
        int num = (Allynum) / 2;

        if ((Allynum) % 2 == 0)
        {
            if (num % 2 == 0)
                num++;
            for (int i = 0; i < Allynum; i++)
            {
                btlist[i].transform.Translate(0,Screen.height*num*0.08f, 0);
                //btlist[i].transform.Translate(0, ButtonHeight*0.6f * num, 0);
                num -= 2;

            }
        }
        else
        {
            for (int i = 0; i < Allynum; i++)
            {
                btlist[i].transform.Translate(0, Screen.height * num * 0.16f, 0);
                num--;
            }
        }
    }

    public void done()
    {
        C4_GameManager.Instance.sceneMode.GetComponentInChildren<C4_PlayMode>().allyController.activeDone();
    }

    public void unactive(GameObject input)
    {
        for (int i = 0; i < Allynum; i++)
        {
            if (btlist[i].GetComponent<C4_AllyButton>().myCharacter == input)
            {
                btlist[i].interactable = false;
            }
        }
    }

    public void pause()
    {
        if (Time.timeScale > 0){
            Time.timeScale = 0;
        }
        pausebox.SetActive(true);
        for (int i = 0; i < Allynum; i++)
        {
                btlist[i].interactable = false;
        }
        PlayerUI.SetActive(false);
    }
    public void restart()
    {
        Time.timeScale = 1;
        Application.LoadLevel(0);
    }
    public void unpause()
    {
        Time.timeScale = 1;
        pausebox.SetActive(false);
        for (int i = 0; i < Allynum; i++)
        {
            btlist[i].interactable = true;
        }
        PlayerUI.SetActive(true);
    }

}
