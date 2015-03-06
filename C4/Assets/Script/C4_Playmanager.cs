using UnityEngine;
using System.Collections;

public class C4_Playmanager : MonoBehaviour {

    [System.NonSerialized]
    public GameObject selectedBoat;
    C4_Boat boatFeature;

    bool isAim;

    void aiming(Vector3 clickPosition)
    {
        Vector3 aimDirection = (selectedBoat.transform.position - clickPosition).normalized;
        aimDirection.y = 0;
        boatFeature.startTurn(clickPosition);
    }

    void orderShot(Vector3 shotDirection)
    {
        boatFeature.shot(shotDirection);
        activeDone();
    }

    void orderMove(Vector3 toMove)
    {
        boatFeature.startMove(toMove);
        boatFeature.startTurn(toMove);
        activeDone();
    }

    void setBoatScript()
    {
        boatFeature = selectedBoat.GetComponent<C4_Boat>();
        boatFeature.missile.SetActive(true);
    }

    void activeDone()
    {
        isAim = false;
        boatFeature = null;
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
