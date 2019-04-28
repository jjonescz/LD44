using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDirection : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject model = gameObject.Find("CoinModel");
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void FixedUpdate()
    {
        float rotMultiplier = 3.0f;
        float rot = Input.GetAxis("X");

        gameObject.transform.Rotate(new Vector3(0.0f, rot * rotMultiplier, 0.0f));
    }
}
