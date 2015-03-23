using UnityEngine;
using System.Collections;

public enum GameObjectType 
{ 
    Invalid,
    Player, 
    Enemy, 
    Missile, 
    Water,
    UI, 
    Camera
};


public enum EnemyAction
{
    NULL, 
    Attack, 
    Move 
};

class GameObjectDefine
{
    static GameObjectDefine()
    {
        INVALID_OBJECT_ID.id = -1;
        INVALID_OBJECT_ID.type = GameObjectType.Invalid;
    }

    static public readonly ObjectID INVALID_OBJECT_ID;
}