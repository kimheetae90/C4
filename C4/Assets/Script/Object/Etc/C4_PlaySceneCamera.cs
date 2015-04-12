using UnityEngine;
using System.Collections;

public class C4_PlaySceneCamera : C4_Camera
{
    public int[] cameraZoom;
    int toCameraZoom;

    protected override void Start()
    {
        toCameraZoom = 1;
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
