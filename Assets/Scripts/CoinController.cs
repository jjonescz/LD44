using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float power;
    public bool moving;
    public Animator anim;
    GameObject gameFlow;
    public Vector3 smer;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        gameFlow = GameObject.Find("GameFlow");
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            power -= 0.3f;
            if (power < 0)
            {
                Stop();
            }
        }
    }

    public void StartMoveWithPower(float actualPower)
    {
        power = actualPower;
        moving = true;
        gameFlow.GetComponent<GameFlow>().ControlArena();
        smer = GetDirection();
        gameObject.GetComponentInParent<Rigidbody>().AddForce(GetDirection() * power * 10);
    }

    Vector3 GetDirection()
    {
        return gameObject.transform.parent.transform.forward;
    }

    void Stop()
    {
        moving = false;
        anim.SetBool("end", true);
        GameObject.Find("Arena").GetComponent<ArenaMove>().StopMoving();
        StartCoroutine(Drag());
        
    }

    IEnumerator Drag()
    {
        float actualDrag = gameObject.GetComponentInParent<Rigidbody>().drag;
        gameObject.GetComponentInParent<Rigidbody>().drag = 2;
        yield return new WaitForSeconds(4);
        gameObject.GetComponentInParent<Rigidbody>().drag = actualDrag;
    }
}
