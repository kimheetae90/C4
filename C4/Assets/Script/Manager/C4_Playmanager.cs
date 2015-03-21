using UnityEngine;
using System.Collections;

/// <summary>
///  선택한 배를 플레이 하는 것을 관리하는 Manager
///  Input Manager로부터 받은 Data를 dispatchData에서 분석하여 선택된 배에게 행동을 명령한다.
///  aiming : 조준(드래그상태)
///  orderShot : 발포
///  orderMove : 이동
///  setBoatScript : 배 선택(Input Manager가 호출)
///  activeDone : 행동을 완료하여 상태를 reset
///  dispatchData : 전달받은 Data 분석
/// </summary>

public class C4_PlayManager : MonoBehaviour, C4_IntInitInstance{
    
    private static C4_PlayManager _instance;
    public static C4_PlayManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(C4_PlayManager)) as C4_PlayManager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "C4_PlayManager";
                    _instance = container.AddComponent(typeof(C4_PlayManager)) as C4_PlayManager;
                }
            }

            return _instance;
        } 
    }

    public void initInstance()
    {
        if (!_instance)
        {
            _instance = GameObject.FindObjectOfType(typeof(C4_PlayManager)) as C4_PlayManager;
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "C4_PlayManager";
                _instance = container.AddComponent(typeof(C4_PlayManager)) as C4_PlayManager;
            }
        }
    }

    public C4_Player ourBoat; //시작 시 배 불러오는 부분(나중에 지울것)
    [System.NonSerialized]
    public C4_Player selectedBoat;
    public GameObject selectUIGameObject;
    public C4_SelectUI selectUI;
    public GameObject moveUIGameObject;
    public GameObject aimUIGameObject;
    public C4_MoveUI moveUI;
    public C4_AimUI aimUI;
    public C4_BoatFeature selectedBoatFeature;
    public bool isAim;

    void Start()
    {
        ourBoat = FindObjectOfType(typeof(C4_Player)) as C4_Player;
        ourBoat.objectID.id = C4_ObjectManager.Instance.currentObjectCode++;
        ourBoat.objectID.type = GameObjectType.Player;
        C4_ObjectManager.Instance.addObjectToAll(ourBoat);
        selectUIGameObject = GameObject.Find("PlayerSelectArrow");
        moveUIGameObject = GameObject.Find("MoveRangeUI");
        moveUI = moveUIGameObject.GetComponent<C4_MoveUI>();
        selectUI = selectUIGameObject.GetComponent<C4_SelectUI>();
        aimUIGameObject = GameObject.Find("AimUI");
        aimUI = aimUIGameObject.GetComponent<C4_AimUI>();
    }


    /* 조준하고 있는 방향으로 회전하고 UI를 출력할 함수 */
    void aiming(Vector3 clickPosition)
    {
        Vector3 aimDirection = (selectedBoat.transform.position - clickPosition).normalized;
        aimDirection.y = 0;
        selectedBoat.turn(clickPosition);
        aimUI.showAimUI(clickPosition);
    }


    /* 발포하고 상태를 초기화할 함수 */
    void orderShot(Vector3 shotDirection)
    {
        selectedBoat.shot(shotDirection);
        activeDone();
    }


    /* 움직임을 명령할 함수 */
    void orderMove(Vector3 toMove)
    {
        selectedBoat.move(toMove);
        selectedBoat.turn(toMove);
        activeDone();
    }


    /* 배를 선택하는 함수 */
    public void setBoatScript(GameObject clickBoat)
    {
        selectedBoat = clickBoat.GetComponent<C4_Player>();
        selectedBoatFeature = clickBoat.GetComponent<C4_BoatFeature>();
        selectUIGameObject.SetActive(true);
        selectUI.setSelect(selectedBoat);
        
    }

    /* 선택 정보를 초기화 */
    void activeDone()
    {
        isAim = false;
        selectedBoat = null;
        selectUIGameObject.SetActive(false);
        moveUI.hideMoveUI();
        aimUI.hideAimUI();
    }

    /* InputManager로부터 전해받은 InputData를 분석하고 행동을 명령하는 함수 */
    public void dispatchData(InputData inputData)
    {

        if (selectedBoat != null)
        {
            if (inputData.keyState == InputData.KeyState.Down)
            {
                if (isAim)
                {
                    if (inputData.clickObjectID.id == inputData.dragObjectID.id)
                    {
                        isAim = false;
                    }
                    aiming(inputData.dragPosition);
                }
                else
                {
                    if ((inputData.clickObjectID.type == GameObjectType.Player) && (inputData.clickObjectID.id != inputData.dragObjectID.id))
                    {
                        isAim = true;
                        aimUI.selectBoat(selectedBoat);
                    }
                }
            }
            else
            {
                if (isAim)
                {
                    orderShot(inputData.dragPosition);
                }
                else
                {
                    if (inputData.clickObjectID.type == GameObjectType.Water)
                    {
                        if (inputData.clickPosition == inputData.dragPosition)
                        {
                            orderMove(inputData.clickPosition);
                        }
                    }
                    else
                    {
                        moveUI.selectBoat(selectedBoat);
                    }
                }
            }
        }
    }
}
