using UnityEngine;
using System.Collections;

public class C4_StartAIBehave : MonoBehaviour {

    public int checkBound;
    public int attackOrMove;
    public int attackSuccessOrFail;
    int attackPercent;
    int successAttackPercent;
    C4_Player shortestDistancePlayer;
    double distanceWithPlayer;
    double checkDistanceEachPlayer;
    C4_BoatFeature boatFeature;

    void Start()
    {
        attackPercent = 0;
        successAttackPercent = 0;
        distanceWithPlayer = 0;
        boatFeature = GetComponent<C4_BoatFeature>();
    }

    public void startBehave()
    {
        checkDistanceWithPlayer();
        if (distanceWithPlayer < checkBound)
        {
        }
        else
        {
 
        }
    }

    void checkDistanceWithPlayer()
    {
        for (int i = 0; i < C4_PlayManager.Instance.objectList.Count; i++)
        {
            checkDistanceEachPlayer = Vector3.Distance(C4_PlayManager.Instance.objectList[i].transform.position, transform.position);
            if (distanceWithPlayer > checkDistanceEachPlayer)
            {
                distanceWithPlayer = checkDistanceEachPlayer;
                shortestDistancePlayer = C4_PlayManager.Instance.objectList[i].GetComponent<C4_Player>();
            }
        }
    }
}
