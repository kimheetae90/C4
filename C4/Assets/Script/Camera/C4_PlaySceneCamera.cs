using UnityEngine;
using System.Collections;

public class C4_PlaySceneCamera : C4_Camera
{
    public int[] cameraZoom;
    int toCameraZoom;
	
	bool isBackCameraMaxSizeCoroutin;
	bool isBackCameraMinSizeCoroutin;

    protected override void Awake()
    {
		base.Awake ();
		toCameraZoom = 1;
		isBackCameraMaxSizeCoroutin = false;
		isBackCameraMinSizeCoroutin = false;
    }

    public void cameraZoomInOneLevel()
    {
        toCameraZoom--;

        if (toCameraZoom < 0)
        {
            toCameraZoom = 0;
        }

        moveSpeed = cameraZoom[toCameraZoom] - (int)Camera.main.orthographicSize;
        moveSpeed *= 0.1f;

        StartCoroutine("adjustCameraZoom");
    }

    public void cameraZoomoutOneLevel()
    {
        toCameraZoom++;

        if (toCameraZoom > cameraZoom.Length-1)
        {
            toCameraZoom = cameraZoom.Length-1;
        }

        moveSpeed = cameraZoom[toCameraZoom] - (int)Camera.main.orthographicSize;
        moveSpeed *= 0.1f;

        StartCoroutine("adjustCameraZoom");
    }

    IEnumerator adjustCameraZoom()
    {
        yield return null;

        int distance = cameraZoom[toCameraZoom] - (int)Camera.main.orthographicSize;
        int distanceAbs = Mathf.Abs(distance);

        if (distanceAbs > 0.5f)
        {
            Camera.main.orthographicSize += moveSpeed;
            moveSpeed *= 0.9f; 
            StartCoroutine("adjustCameraZoom");
        }
        else
        {
            Camera.main.orthographicSize = cameraZoom[toCameraZoom];
            StopCoroutine("adjustCameraZoom");
        }
    }

	protected override void zooming(InputData data)
	{
		if (Camera.main.orthographicSize < cameraZoom [cameraZoom.Length - 1] + 5 && Camera.main.orthographicSize > cameraZoom [0] - 5) 
		{
			Camera.main.orthographicSize -= (data.multiTapDistance - data.preMultiTapDistance) / 30;
		}
	}

	protected override void returnToFixedZoomSize()
	{
		if (Camera.main.orthographicSize > cameraZoom [cameraZoom.Length - 1])
		{
			if(!isBackCameraMaxSizeCoroutin)
			{
				isBackCameraMaxSizeCoroutin = true;
				StartCoroutine("backCameraSizeToMax");
			}
		}
		else if(Camera.main.orthographicSize < cameraZoom [0]) 
		{
			if(!isBackCameraMinSizeCoroutin)
			{
				isBackCameraMinSizeCoroutin = true;
				StartCoroutine("backCameraSizeToMin");
			}
		}
	}

	IEnumerator backCameraSizeToMax()
	{
		yield return null;

		if (Camera.main.orthographicSize > cameraZoom [cameraZoom.Length - 1]) 
		{
			Camera.main.orthographicSize -= 1f;
			StartCoroutine("backCameraSizeToMax");
		}
		else
		{
			isBackCameraMaxSizeCoroutin = false;
			StopCoroutine("backCameraSizeToMax");
		}
	}

	IEnumerator backCameraSizeToMin()
	{
		yield return null;

		if (Camera.main.orthographicSize < cameraZoom [0]) 
		{
			Camera.main.orthographicSize += 1f;
			StartCoroutine("backCameraSizeToMin");
		}
		else 
		{
			isBackCameraMinSizeCoroutin = false;
			StopCoroutine("backCameraSizeToMin");
		}
	}

    public void moveToSomeObject()
    {
        toMove = C4_GameManager.Instance.sceneMode.getController(GameObjectType.Ally).GetComponent<C4_AllyController>().selectedAllyUnit.transform.position;
        moveSpeed = Vector3.Distance(toMove, transform.position);
        StartCoroutine("moveToSomeObjectCoroutine");
    }

    IEnumerator moveToSomeObjectCoroutine()
    {
        yield return null;

        double distance = Vector3.Distance(toMove, transform.position);

        if (distance > moveSpeed * 0.2f)
        {
            transform.Translate((toMove - transform.position).normalized * (float)moveSpeed * 10f * Time.deltaTime);
            StartCoroutine("moveToSomeObjectCoroutine");
        }
        else if (distance > moveSpeed * 0.02f)
        {
            transform.Translate((toMove - transform.position).normalized * (float)moveSpeed * Time.deltaTime);
            StartCoroutine("moveToSomeObjectCoroutine");
        }
        else
        {
            StopCoroutine("moveToSomeObjectCoroutine");
        }
    }
}
