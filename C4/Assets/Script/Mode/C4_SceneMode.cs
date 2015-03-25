using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_SceneMode : MonoBehaviour {

    protected List<C4_Controller> controllerList;
    protected Dictionary<GameObjectType,C4_Controller> controllerDictionary;

    public virtual void Start()
    {
        controllerList = new List<C4_Controller>();
        controllerDictionary = new Dictionary<GameObjectType, C4_Controller>();
    }

    public void sendInputData(InputData inputData)
    {
        foreach (C4_Controller controller in controllerList)
        {
            controller.dispatchData(inputData);
        }
    }

    public void sendSelectGameObject(GameObject clickGameObject)
    {
        foreach (C4_Controller controller in controllerList)
        {
            controller.selectClickObject(clickGameObject);
        }
    }

    public C4_Controller getController(GameObjectType type)
    {
        return controllerDictionary[type];
    }

    protected void addController(GameObjectType type, C4_Controller controller)
    {
        controllerList.Add(controller);
        controllerDictionary.Add(type, controller);
    }
}
