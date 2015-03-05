using UnityEngine;
using System.Collections;

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
