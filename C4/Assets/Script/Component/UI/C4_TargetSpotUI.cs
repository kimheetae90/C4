using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_TargetSpotUI : C4_TargetUI
{
   
    public override void showUI(Vector3 targetPos)
	{
		targetUIImage.SetActive(true);
        targetUIImage.transform.position = targetPos;
    }
  }
