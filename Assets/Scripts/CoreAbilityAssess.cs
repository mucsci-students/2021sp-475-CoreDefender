using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAbilityAssess : MonoBehaviour
{

    public float drawSpeed;
    public float damage;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    //Charged every this many seconds
    public float payUp;
    public int cost;

    Camera cam;
    LineRenderer lr;
    bool fired = false;
    float counter;
    float distance;
    Vector2 target;
    bool laserActive = false;
    Core core;
    float timer;
    private Currency mon;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        core = FindObjectOfType<Core>();
        mon = GameObject.FindObjectOfType<Currency>();


        lr = core.gameObject.GetComponent<LineRenderer>();
        lr.SetPosition(0, core.gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        if(laserActive)
        {
            timer += Time.deltaTime;
            if(timer >= payUp)
            {
                if(mon.getMoney() > cost)
                {
                    mon.Spend(cost);
                    timer = 0;
                }
                else
                {
                    laserActive = false;
                }
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

    }

    private void Fire()
    {
        if (laserActive)
        {
            StartCoroutine("DrawLaser");
        }
    }

    public void SetLaserActive()
    {
        laserActive = !laserActive;

        if(laserActive)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }


    IEnumerator DrawLaser()
    {
        lr.enabled = true;
        fired = true;
        Vector3 point = new Vector3();
        Vector3 mousePos = Input.mousePosition;
        Vector3 place = cam.ScreenToWorldPoint(mousePos);
        lr.SetPosition(1, place);
        point = place;
        target = place;

        RaycastHit2D hit = Physics2D.Raycast(place, place.normalized);
        if(hit.collider)
        {
            if(hit.collider.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<BasicEnemy>().TakeDamage(damage);
            }
        }
        yield return new WaitForSeconds(1);

        fired = false;
        lr.enabled = false;
    }
}
