using UnityEngine;
using System.Collections;

/// <summary>
///  Object 클래스
///  게임 내의 오브젝트들이 이 함수를 상속받아 제작된다.
///  고유의 ID와 속성에 대한 정보를 담고있다.
/// </summary>

public class C4_Object : MonoBehaviour {

    public ObjectID objectID;

    public bool isValid()
    {
        return true;
    }

    public bool isType(GameObjectType type)
    {
        return objectID.type == type;
    }
}
