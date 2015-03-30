using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_MoveUI : MonoBehaviour
{
    public Image moveUIImage;
    GameObject moveUIGameObejct;
    bool isSelect;
    C4_BoatFeature boatFeature;
    C4_Player selectedBoat;
    int stackCount;
    int moveRange;

    Texture2D firstTexture;
    Texture2D secondTexture;
    Texture2D thirdTexture;

    Sprite firstSprite;
    Sprite secondSprite;
    Sprite thirdSprite;

    void Start()
    {
        moveUIGameObejct = moveUIImage.gameObject;
        isSelect = false;
        moveUIGameObejct.SetActive(false);


        #if UNITY_EDITOR
        firstTexture = (Texture2D)Resources.LoadAssetAtPath("Assets/Resources/Texture/moveUI_first.png", typeof(Texture2D));
        secondTexture = (Texture2D)Resources.LoadAssetAtPath("Assets/Resources/Texture/moveUI_second.png", typeof(Texture2D));
        thirdTexture = (Texture2D)Resources.LoadAssetAtPath("Assets/Resources/Texture/moveUI_third.png", typeof(Texture2D));
        #else
        firstTexture = (Texture2D)Resources.Load("Texture/moveUI_first", typeof(Texture2D));
        secondTexture = (Texture2D)Resources.Load("Texture/moveUI_second", typeof(Texture2D));
        thirdTexture = (Texture2D)Resources.Load("Texture/moveUI_third", typeof(Texture2D));
        #endif

        firstSprite = Sprite.Create(firstTexture, new Rect(0, 0, firstTexture.width, firstTexture.height), new Vector2(0.5f, 0.5f));
        secondSprite = Sprite.Create(secondTexture, new Rect(0, 0, secondTexture.width, secondTexture.height), new Vector2(0.5f, 0.5f));
        thirdSprite = Sprite.Create(thirdTexture, new Rect(0, 0, thirdTexture.width, thirdTexture.height), new Vector2(0.5f, 0.5f));
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
        selectedBoat = C4_ManagerMaster.Instance.sceneMode.getController(GameObjectType.Player).GetComponent<C4_PlayerController>().selectedBoat;
        boatFeature = selectedBoat.GetComponent<C4_BoatFeature>();
    }

    public void showUI()
    {
        moveUIGameObejct.SetActive(true);
        stackCount = boatFeature.stackCount;
        moveRange = boatFeature.moveRange;
        moveUIImage.transform.localScale = new Vector3((moveRange), (moveRange), 1);

        switch (stackCount)
        {
            case 0:
                moveUIImage.sprite = null;
                moveUIImage.transform.localScale = new Vector3(0, 0, 1);
                break;
            case 1:
                moveUIImage.sprite = firstSprite;
                break;
            case 2:
                moveUIImage.sprite = secondSprite;
                break;
            case 3:
                moveUIImage.sprite = thirdSprite;
                break;
        }
    }

    public void hideUI()
    {
        isSelect = false;
        moveUIGameObejct.SetActive(false);
        moveUIImage.transform.localScale = new Vector3(0, 0, 1);
    }
}
