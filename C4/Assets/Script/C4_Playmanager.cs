using UnityEngine;
using System.Collections;

public class C4_Playmanager : MonoBehaviour {

    [System.NonSerialized]
    public GameObject selectedBoat;
    
    Move moveScript;
    Turn turnScript;

    bool isAim;

    void aiming(Vector3 clickPosition)
    {
        Vector3 aimDirection = selectedBoat.transform.position - clickPosition;
        aimDirection.y = 0;
        turnScript.setToTurn(aimDirection);
    }

    void orderShot()
    {
        Debug.Log("orderShot");
        isAim = false;
    }

    void orderMove(Vector3 toMove)
    {
        moveScript.setToMove(toMove);
    }

    void setBoatScript()
    {
        moveScript = selectedBoat.GetComponent<Move>();
        turnScript = selectedBoat.GetComponent<Turn>();
    }

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
                    aiming(inputData.dragPosition); //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ 조준하고있을때의 행동넣기 (회전, 애니메이션, 조준bar)
                }
                else
                {
                    if ((inputData.clickObjectType == InputData.ObjectType.BOAT) && !(inputData.dragObjectType == InputData.ObjectType.BOAT))
                    {
                        isAim = true;
                    }

                }
            }
            else
            {
                if (isAim)
                {
                    orderShot();//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ 발포, 리셋
                }
                else
                {
                    if (inputData.clickObjectType == InputData.ObjectType.WATER)
                    {
                        if (Vector3.Distance(inputData.clickPosition, inputData.dragPosition) < 0.5)
                        {
                            orderMove(inputData.clickPosition);
                        }
                    }
                }
            }
        }
    }
}
