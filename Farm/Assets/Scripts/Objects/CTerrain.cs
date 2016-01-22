using UnityEngine;
using System.Collections;

public abstract class CTerrain : BaseObject
{
    public float gage;
    float _gage;
    float maxGage;
    public bool canAccess;
    public int tileNum;
	// Use this for initialization

    void Awake() {
        ChangeState(ObjectState.Play_Terrain_Uncomplete);
        _gage = 0;
        maxGage = gage;
        canAccess = true;
    }

    protected override void UpdateState()
    {

    }

    protected override void ChangeState(ObjectState _objectState)
    {
        objectState = _objectState;
        switch (objectState)
        {
            case ObjectState.Play_Terrain_Uncomplete:
                Uncomplete();
                break;
            case ObjectState.Play_Terrain_Gaging:
                Gaging();
                break;
            case ObjectState.Play_Terrain_Complete:
                Complete();
                break;
        }
    }

    public void ChangeStateToUncomplete() {
        ChangeState(ObjectState.Play_Terrain_Uncomplete);
    }

    public void ChangeStateToGaging()
    {
        ChangeState(ObjectState.Play_Terrain_Gaging);
    }
    public void ChangeStateToComplete()
    {
        ChangeState(ObjectState.Play_Terrain_Complete);
    }

    public void StopGaging() {
        StopCoroutine("ChargingGage");
    }

    void Uncomplete() { 
        _gage = 0;
        canAccess = true;
    }

    void Gaging() {
        StartCoroutine("ChargingGage");
    }

    protected abstract void Complete();

    IEnumerator ChargingGage() {

        while (canAccess) {
            if (_gage < maxGage) {
                _gage += Time.deltaTime;
                GameMessage gameMsg = GameMessage.Create(MessageName.Play_UIGageAmount);
                gameMsg.Insert("gage", _gage);
                gameMsg.Insert("maxGage", maxGage);
                SendGameMessageToSceneManage(gameMsg);
            }
            if (_gage >= maxGage) {
                canAccess = false;
                _gage = maxGage;
                ChangeState(ObjectState.Play_Terrain_Complete);
                GameMessage gameMsg = GameMessage.Create(MessageName.Play_GageStop);
                SendGameMessageToSceneManage(gameMsg);
            }
            yield return null;
        }
    }

    /// <summary>
    /// 남은 체력 비례 텍스쳐 변경.
    /// </summary>
    protected void ChangeTexture()
    {
        /*
        if ((float)_hp / hp <= 0.3f)
        {
            if (renderer.Count > 0 && renderer[0].material.mainTexture != texture[2])
            {
                foreach (Renderer rend in renderer)
                {
                    rend.material.mainTexture = texture[2];
                }
            }
        }
        else if ((float)_hp / hp <= 0.6f)
        {
            if (renderer.Count > 0 && renderer[0].material.mainTexture == texture[0])
            {
                foreach (Renderer rend in renderer)
                {
                    rend.material.mainTexture = texture[1];
                }
            }
        }
        */
    }
}
