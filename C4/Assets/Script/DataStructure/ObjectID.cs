using UnityEngine;
using System.Collections;

public struct ObjectID
{
    public enum Type { Player, Enemy, Missile, Water, UI };
    public Type type;
    public int id;
}
