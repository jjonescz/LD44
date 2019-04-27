using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float power;
    public bool moving;
    public Animator anim;
    GameObject gameFlow;

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
            power -= 0.1f;
            if (power < 0)
            {
                moving = false;
                anim.SetBool("end", true);
            }
        }
    }

    public void StartMoveWithPower(float actualPower)
    {
        power = actualPower;
        moving = true;
        gameFlow.GetComponent<GameFlow>().ControlArena();
    }

    Vector3 GetDirection()
    {
        return gameObject.transform.parent.transform.forward;
    }
}
