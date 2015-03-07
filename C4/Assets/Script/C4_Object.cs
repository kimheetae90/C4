using UnityEngine;
using System.Collections;

/// <summary>
///  Object 클래스
///  게임 내의 오브젝트들이 이 함수를 상속받아 제작된다.
///  고유의 ID와 속성에 대한 정보를 담고있다.
/// </summary>

public class C4_Object : MonoBehaviour {

    private byte objectID;
    private byte attributeCode;

    public byte getObjectID()
    {
        return objectID;
    }

    public void setObjectID(byte value)
    {
        objectID = value;
    }

    public byte getAttributeCode()
    {
        return attributeCode;
    }

    public void setAttributeCode(byte value)
    {
        attributeCode = value;
    }
}
