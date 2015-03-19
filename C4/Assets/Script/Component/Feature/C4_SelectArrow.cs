using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_SelectArrow : MonoBehaviour {

    public Canvas playerArrow;
    public C4_Character selectObject;

    public void setSelect(C4_Character boat)
    {
        gameObject.SetActive(true);
        selectObject = boat;
        Vector3 pos = selectObject.transform.position;
        pos.y += 6.5f;
        pos.z += 11;
        playerArrow.transform.position = pos;
    }

    public void resetSelect()
    {
        transform.Translate(new Vector3(0, -20, 0));
        gameObject.SetActive(false);
    }

}
