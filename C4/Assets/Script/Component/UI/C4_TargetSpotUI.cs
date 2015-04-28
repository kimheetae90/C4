using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_TargetSpotUI : C4_TargetUI
{
   
    public override void showUI(Vector3 clickPosition)
    {
        targetUIImage.gameObject.SetActive(true);
        Vector3 targetPos = 4 * transform.position - 3 * clickPosition;
        targetUIImage.gameObject.transform.position = targetPos;
      }

  }
