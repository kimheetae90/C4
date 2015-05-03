using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_MoveUI : MonoBehaviour
{
    
    public GameObject moveUIGameObejct;
    bool isSelect;
    C4_UnitFeature unitFeature;
    C4_Ally selectedBoat;
    int moveRange;
    

    void Start()
    {
        
        isSelect = false;
        moveUIGameObejct.SetActive(false);

        //#if UNITY_EDITOR
        //thirdTexture = (Texture2D)Resources.LoadAssetAtPath("Assets/Resources/Texture/moveUI_third.png", typeof(Texture2D));
        //#else
        //thirdTexture = (Texture2D)Resources.Load("Texture/moveUI_third", typeof(Texture2D));
        //#endif

        //thirdSprite = Sprite.Create(thirdTexture, new Rect(0, 0, thirdTexture.width, thirdTexture.height), new Vector2(0.5f, 0.5f));
        //moveUIImage.sprite = thirdSprite;
    }

    void Update()
    {
        if (isSelect)
        {
            showUI();
        }
    }

    public void selectBoat()
    {
        isSelect = true;
        selectedBoat = C4_GameManager.Instance.sceneMode.getController(GameObjectType.Ally).GetComponent<C4_AllyController>().selectedAllyUnit;
        unitFeature = selectedBoat.GetComponent<C4_UnitFeature>();
    }

    public void showUI()
    {
        moveUIGameObejct.SetActive(true);
        moveRange = unitFeature.moveRange;
        moveUIGameObejct.transform.localScale = new Vector3((moveRange), 1, (moveRange));
    }

    public void hideUI()
    {
        isSelect = false;
        moveUIGameObejct.SetActive(false);
        moveUIGameObejct.transform.localScale = new Vector3(0, 0, 1);
    }
}
