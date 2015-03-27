using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_MoveUI : MonoBehaviour
{

    Image moveRangeUI;
    GameObject moveImage;
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
        moveRangeUI = GetComponentInChildren<Image>();
        moveImage = moveRangeUI.gameObject;
        isSelect = false;

        firstTexture = (Texture2D)Resources.LoadAssetAtPath("Assets/Texture/moveUI_first.png", typeof(Texture2D));
        secondTexture = (Texture2D)Resources.LoadAssetAtPath("Assets/Texture/moveUI_second.png", typeof(Texture2D));
        thirdTexture = (Texture2D)Resources.LoadAssetAtPath("Assets/Texture/moveUI_third.png", typeof(Texture2D));

        firstSprite = Sprite.Create(firstTexture, new Rect(0, 0, firstTexture.width, firstTexture.height), new Vector2(0.5f, 0.5f));
        secondSprite = Sprite.Create(secondTexture, new Rect(0, 0, secondTexture.width, secondTexture.height), new Vector2(0.5f, 0.5f));
        thirdSprite = Sprite.Create(thirdTexture, new Rect(0, 0, thirdTexture.width, thirdTexture.height), new Vector2(0.5f, 0.5f));
    }

    void Update()
    {
        if (isSelect)
        {
            showMoveUI();
        }
    }

    public void selectBoat()
    {
        isSelect = true;
        selectedBoat = C4_ManagerMaster.Instance.sceneMode.getController(GameObjectType.Player).GetComponent<C4_PlayerController>().selectedBoat;
        boatFeature = selectedBoat.GetComponent<C4_BoatFeature>();
    }

    public void showMoveUI()
    {
        stackCount = boatFeature.stackCount;
        moveRange = boatFeature.moveRange;

        moveRangeUI.transform.localScale = new Vector3((moveRange), (moveRange), 1);


        switch (stackCount)
        {
            case 0:
                moveRangeUI.sprite = null;
                moveImage.SetActive(false);
                break;
            case 1:
                moveRangeUI.sprite = firstSprite;
                moveImage.SetActive(true);
                break;
            case 2:
                moveRangeUI.sprite = secondSprite;
                moveImage.SetActive(true);
                break;
            case 3:
                moveRangeUI.sprite = thirdSprite;
                moveImage.SetActive(true);
                break;
        }
    }

    public void hideMoveUI()
    {
        isSelect = false;
        moveImage.SetActive(false);
    }
}
