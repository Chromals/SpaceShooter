using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //5 PLAYER BULLETS DETROYS BOSS => 10pts.
    [SerializeField]
    int bossHitsToKill;

    [SerializeField]
    string deathSoundSFX;

    [SerializeField]
    float points;

    int currentHits = 0;



    public void TakeHit()
    {

        currentHits++;
        if (currentHits >= bossHitsToKill)
        {
            DestroyBoss();
        }
    }

    public void DestroyBoss()
    {
        BossController controller = GetComponent<BossController>();

        //implement getPoints on centinelController -> 10pts  check
        float points = controller.GetPoints();

        //increase score -> 10pts -> check
        UIController.Instance.IncreaseScore(points);
        Destroy(gameObject);
        SoundManager.Instance.PlaySFX(deathSoundSFX);
    }

    public float GetPoints()
    {
        return points;
    }
}
