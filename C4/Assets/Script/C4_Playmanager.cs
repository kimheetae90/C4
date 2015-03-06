using UnityEngine;
using System.Collections;

public class C4_Playmanager : MonoBehaviour {

    [System.NonSerialized]
    public GameObject selectedBoat;
    
    Move moveScript;
    Turn turnScript;
    C4_Boat boatFeature;

    bool isAim;

    void aiming(Vector3 clickPosition)
    {
        Vector3 aimDirection = (selectedBoat.transform.position - clickPosition).normalized;
        aimDirection.y = 0;
        turnScript.setToTurn(aimDirection);
    }

    void orderShot()
    {
        activeDone();
    }

    void orderMove(Vector3 toMove)
    {
        moveScript.setToMove(toMove);
        turnScript.setToTurn(toMove);
        activeDone();
    }

    void setBoatScript()
    {
        moveScript = selectedBoat.GetComponent<Move>();
        turnScript = selectedBoat.GetComponentInChildren<Turn>();
        boatFeature = selectedBoat.GetComponent<C4_Boat>();
    }

    void activeDone()
    {
        isAim = false;
        moveScript = null;
        turnScript = null;
        boatFeature.resetActive();
        selectedBoat = null;        
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
                    aiming(inputData.dragPosition);
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
