using UnityEngine;
using System.Collections;

public class C4_Playmanager : MonoBehaviour {

    [System.NonSerialized]
    public GameObject selectedBoat;

    bool isAim;

    void aiming()
    { 
    }

    void orderShot()
    { 
    }

    void orderMove()
    { 
    }

    public void dispatchData(InputData inputData)
    {
        if (selectedBoat != null)
        {
            if (inputData.keyState == InputData.KeyState.DRAG)
            {
                if (isAim)
                {
                    aiming(); //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ 조준하고있을때의 행동넣기 (회전, 애니메이션, 조준bar)
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
                    isAim = false;
                }
                else
                {
                    if (inputData.clickObjectType == InputData.ObjectType.WATER)
                    {
                        if (Vector3.Distance(inputData.clickPosition, inputData.dragPosition) < 0.5)
                        {
                            orderMove();//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ 클릭지점으로 selectedBoat 이동하기, 리셋
                        }
                    }
                }
            }
        }
    }
}
