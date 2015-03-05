using UnityEngine;
using System.Collections;

public struct InputData
{
    public enum ObjectType { BOAT, WATER };
    public enum KeyState { DRAG, UP };

    public Vector3 clickPosition;
    public Vector3 dragPosition;
    public ObjectType clickObjectType;
    public ObjectType dragObjectType;
    public KeyState keyState;
}
