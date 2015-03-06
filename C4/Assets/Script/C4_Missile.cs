using UnityEngine;
using System.Collections;

public class C4_Missile : C4_Object {

    public int power;
    public Move moveScript;
    public Turn turnSrcipt;
    
    public void startMove(Vector3 toMove)
    {
        moveScript.setToMove(toMove);
    }

	//충돌체 붙이기
}
