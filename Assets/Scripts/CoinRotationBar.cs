using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinRotationBar : MonoBehaviour
{
    public GameObject coin;
    public float coinMaxPower = 120;
    CoinController controller;
    Slider lifeBar;
    bool active = true;

    void Awake()
    {
        controller = coin.GetComponentInChildren<CoinController>();
        if (controller == null) Debug.LogError("CoinRotationBar class assumed that Coin has CoinController class attached");

        lifeBar = gameObject.GetComponentInChildren<Slider>();
    }

    void Update()
    {
        lifeBar.value = controller.power / coinMaxPower;

        if (lifeBar.value < 0.01f) SetBarActive(false);
        else SetBarActive(true);
    }

    void SetBarActive(bool value)
    {
        if (active != value)
            active = value;
        else
            return;

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}
