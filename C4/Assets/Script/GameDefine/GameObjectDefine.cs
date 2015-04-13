using UnityEngine;
using System.Collections;

public enum GameObjectType
{
    Invalid,
    Ally,
    Enemy,
    Missile,
    Ground,
    UI,
    Camera
};

public enum GameObjectInputType
{
    Invalid = 0x0000,
    ClickAbleObject = 0x0001,
    SelectAbleObject = 0x0001 << 1,
    CameraMoveAbleObject = 0x0001 << 2,
    ToMoveAbleObject = 0x0001 << 3
};

public enum KeyState
{
	Sleep,
	Down,
    Up,
	Drag,
	MultiTaping,
	MultiTapStart
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