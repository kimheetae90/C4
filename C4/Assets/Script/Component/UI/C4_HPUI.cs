using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class C4_HPUI : MonoBehaviour
{


    public Image imgHPbar;
    [System.NonSerialized]
    public C4_BoatFeature boatFeature;

    // Use this for initialization
    void Start()
    {
        boatFeature = transform.GetComponentInParent<C4_BoatFeature>();
    }

    // Update is called once per frame

    public void HPChange(int dottime)
    {
        if (dottime > 1)
        {
            StartCoroutine(DotHPCheck(dottime));
        }
        else
        {
            imgHPbar.fillAmount = (float)boatFeature.hp / boatFeature.fullHP;
        }

    }

    IEnumerator DotHPCheck(int dottime)
    {


        imgHPbar.fillAmount = (float)boatFeature.hp / boatFeature.fullHP;
        dottime--;
        if (dottime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(DotHPCheck(dottime));

        }
        else
        {
            StopCoroutine(DotHPCheck(dottime));
        }
    }
}
