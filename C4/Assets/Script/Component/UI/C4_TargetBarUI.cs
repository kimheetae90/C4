using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_TargetBarUI : C4_TargetUI
{
    public override void showUI(Vector3 clickPosition)
    {
        targetUIImage.gameObject.SetActive(true);
        C4_Ally selectedBoat = C4_GameManager.Instance.sceneMode.getController(GameObjectType.Ally).GetComponent<C4_AllyController>().selectedAllyUnit;
        Vector3 targetPos = 4 * transform.position - 3 * clickPosition;
        float distance = Vector3.Distance(selectedBoat.transform.position, targetPos);
        Vector3 targetDirection = (targetPos - selectedBoat.transform.position).normalized;
        targetDirection.y = 0;

        targetUIImage.gameObject.transform.position = selectedBoat.transform.position;
        targetUIImage.gameObject.transform.rotation = Quaternion.LookRotation(targetDirection);
        targetUIImage.gameObject.transform.Rotate(Vector3.right, 90);
        targetUIImage.gameObject.transform.localScale = new Vector3(1, distance / targetUIImage.rectTransform.rect.height, 1);
    }

}
