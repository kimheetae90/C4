using UnityEngine;
using System.Collections;

public struct ObjectID
{
    public GameObjectType type;
    public int id;
    public GameObjectInputType inputType;

    bool isValid()
    {
        return id != -1 ? true : false;
    }

	public bool isInputTypeTrue(GameObjectInputType iBits) { return iBits == (inputType & iBits); }

    public bool isInputTypeFalse(GameObjectInputType iBits) { return 0 == (inputType & iBits); }

    public bool hasTrueInputType(GameObjectInputType iBits) { return 0 != (inputType & iBits); }

    public void setBits(GameObjectInputType iBits, bool b)
	{
		if (b)
            inputType |= iBits;
		else
            inputType &= ~iBits;
	}
    public void setBits(GameObjectInputType iBits)
	{
        inputType = iBits;
	}


}
