using UnityEngine;
using System.Collections;

/// <summary>
///  Input받은 정보에 대한 Data
///  Input Manager에서 Camera와 playManager로 전송된다.
/// </summary>

public struct InputData
{
    public enum ObjectType { BOAT, WATER };
    public enum KeyState { DRAG, UP };

    //좌표 정보
    public Vector3 clickPosition;
    public Vector3 dragPosition;

    //클릭한 대상에 대한 정보
    public ObjectType clickObjectType;
    public ObjectType dragObjectType;

    //클릭,드래그 상태
    public KeyState keyState;
}
