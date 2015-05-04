using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C4_SceneMode : MonoBehaviour {

    protected List<C4_Controller> ListController;
    protected Dictionary<GameObjectType,C4_Controller> DicController;

    public virtual void Start()
    {
        ListController = new List<C4_Controller>();
        DicController = new Dictionary<GameObjectType, C4_Controller>();
    }

    public void sendInputDataToController(InputData inputData)
    {
        foreach (C4_Controller controller in ListController)
        {
            controller.dispatchData(inputData);
        }
    }

    public void sendSelectedGameObjectToController(GameObject clickGameObject)
    {
        foreach (C4_Controller controller in ListController)
        {
            controller.selectClickObject(clickGameObject);
        }
    }

    public C4_Controller getController(GameObjectType type)
    {
        return DicController[type];
    }

    protected void addController(GameObjectType type, C4_Controller controller)
    {
        ListController.Add(controller);
        DicController.Add(type, controller);
    }
}
