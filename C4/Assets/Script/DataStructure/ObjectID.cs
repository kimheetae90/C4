using UnityEngine;
using System.Collections;

public struct ObjectID
{
    public GameObjectType type;
    public int id;

    bool isValid()
    {
        return id != -1 ? true : false;
    }
}
