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

    [System.NonSerialized]
    public GameObject selectedBoat;
    C4_Boat boatFeature;

    bool isAim;
    

    /* 조준하고 있는 방향으로 회전하고 UI를 출력할 함수 */
    void aiming(Vector3 clickPosition)
    {
        Vector3 aimDirection = (selectedBoat.transform.position - clickPosition).normalized;
        aimDirection.y = 0;
        boatFeature.startTurn(clickPosition);
    }


    /* 발포하고 상태를 초기화할 함수 */
    void orderShot(Vector3 shotDirection)
    {
        boatFeature.shot(shotDirection);
        activeDone();
    }


    /* 움직임을 명령할 함수 */
    void orderMove(Vector3 toMove)
    {
        boatFeature.startMove(toMove);
        boatFeature.startTurn(toMove);
        boatFeature.missile.SetActive(false);
        activeDone();
    }


    /* 배를 선택하는 함수 */
    void setBoatScript(GameObject clickBoat)
    {
        if (selectedBoat != null)   //이미 선택된 배가 있는 경우 그 배의 missile을 비활성화시킴
        {
            if(!boatFeature.missileFeature.moveScript.isMove)
            {
                boatFeature.missile.SetActive(false);
            }
        }
        selectedBoat = clickBoat;
        boatFeature = selectedBoat.GetComponent<C4_Boat>();
        boatFeature.missile.SetActive(true);
    }

    /* 선택 정보를 초기화 */
    void activeDone()
    {
        isAim = false;
        boatFeature = null;
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
                    if ((inputData.clickObjectType == InputData.ObjectType.BOAT) && !(inputData.dragObjectType == InputData.ObjectType.BOAT) && boatFeature.canShot)
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
