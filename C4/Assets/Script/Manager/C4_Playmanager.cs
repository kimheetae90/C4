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

public class C4_Playmanager : MonoBehaviour {

    private static C4_Playmanager _instance;
    public static C4_Playmanager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(C4_Playmanager)) as C4_Playmanager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "C4_Playmanager";
                    _instance = container.AddComponent(typeof(C4_Playmanager)) as C4_Playmanager;
                }
            }

            return _instance;
        }
    }  

    [System.NonSerialized]
    public GameObject selectedBoat;
    C4_Player character;

    bool isAim;

    /* 조준하고 있는 방향으로 회전하고 UI를 출력할 함수 */
    void aiming(Vector3 clickPosition)
    {
        Vector3 aimDirection = (selectedBoat.transform.position - clickPosition).normalized;
        aimDirection.y = 0;
        character.startTurn(clickPosition);
    }


    /* 발포하고 상태를 초기화할 함수 */
    void orderShot(Vector3 shotDirection)
    {
        character.shot(shotDirection);
        activeDone();
    }


    /* 움직임을 명령할 함수 */
    void orderMove(Vector3 toMove)
    {
        character.startMove(toMove);
        character.startTurn(toMove);
        activeDone();
    }


    /* 배를 선택하는 함수 */
    public void setBoatScript(GameObject clickBoat)
    {
        selectedBoat = clickBoat;
        character = selectedBoat.GetComponent<C4_Player>();
    }

    /* 선택 정보를 초기화 */
    void activeDone()
    {
        isAim = false;
        character = null;
        selectedBoat = null;
    }

    /* InputManager로부터 전해받은 InputData를 분석하고 행동을 명령하는 함수 */
    public void dispatchData(InputData inputData)
    {

        if (selectedBoat != null)
        {
            if (inputData.keyState == InputData.KeyState.DRAG)
            {
                if (isAim)
                {
                    if ((inputData.clickObjectType == InputData.ObjectType.BOAT) && (inputData.dragObjectType == InputData.ObjectType.BOAT))
                    {
                        isAim = false;
                    }
                    aiming(inputData.dragPosition);
                }
                else
                {
                    if ((inputData.clickObjectType == InputData.ObjectType.BOAT) && !(inputData.dragObjectType == InputData.ObjectType.BOAT) && character.canShot)
                    {
                        isAim = true;
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
                    if (inputData.clickObjectType == InputData.ObjectType.WATER)
                    {
                        if(inputData.clickPosition == inputData.dragPosition)
                        {
                            orderMove(inputData.clickPosition);
                        }
                    }
                }
            }
        }
    }
}
