using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_SelectUI : MonoBehaviour {

   
    public GameObject selectUIGameObject;

    void Start()
    {
        
        selectUIGameObject.SetActive(false);
    }

    public void showUI()
    {
        selectUIGameObject.SetActive(true);
    }

    public void hideUI()
    {
        selectUIGameObject.SetActive(false);
    }
}
