using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("X");
        float y = Input.GetAxis("Y");
    }
}
