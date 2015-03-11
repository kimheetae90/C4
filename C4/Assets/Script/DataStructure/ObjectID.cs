using UnityEngine;
using System.Collections;

public struct ObjectID
{
    public enum Type { Character, Enemy, Missile, Water, UI };
    public Type type;
    public int id;
}
