using UnityEngine;
using System.Collections;

public struct ObjectID
{
    public enum Type { Player, Enemy, Missile, Water, UI , Camera};
    public Type type;
    public int id;
}
