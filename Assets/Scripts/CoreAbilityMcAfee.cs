using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAbilityMcAfee : MonoBehaviour
{
    public Core core;
    //private List<GameObject> listOfEnemies = new List<GameObject>();
    //private bool used = false;
    private GameManager manager;
    private Currency mon;

    private bool IsActive = false;
    private float Timer = 0;
    private float StartTime;
    private ParticleSystem part;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        mon = GameObject.FindObjectOfType<Currency>();
        part = core.gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if (mon.getMoney() < 25)
        {
            IsActive = false;
            core.YourSubscriptionHasExpired();
        }

        if (IsActive)
        {
            if ((Timer - StartTime) > 1)
            {
                mon.Spend (25);
                StartTime += 1;
                if(!part.isPlaying)
                {
                    part.Play();
                }
            }
        }
        else
        {
            if(part.isPlaying)
            {
                part.Stop();
            }
        }
    }

    public void TogetherIsPower()
    {
        IsActive = !IsActive;

        if (mon.getMoney() < 25)
        {
            IsActive = false;
            core.YourSubscriptionHasExpired();
        }

        else if (IsActive)
        {
            core.YouAreNowProtected();

            StartTime = Timer;
        }

        else
        {
            core.YourSubscriptionHasExpired();
        }
    }
}