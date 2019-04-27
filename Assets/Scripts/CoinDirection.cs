using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDirection : MonoBehaviour
{    
    void FixedUpdate()
    {
        float rotMultiplier = 3.0f;
        float rot = Input.GetAxis("X");

        gameObject.transform.Rotate(new Vector3(0.0f, rot * rotMultiplier, 0.0f));
    }
}
