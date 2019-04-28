using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float lives = 1;
    BadCoinSpawn enemyCont;
    AudioSource coinAudi;

    // Start is called before the first frame update
    void Start()
    {
        enemyCont = GameObject.Find("BadCoinControll").GetComponent<BadCoinSpawn>();
        coinAudi = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            lives = lives - Mathf.Min(other.gameObject.GetComponentInChildren<CoinController>().power / 120, 0.7f);
            coinAudi.Play();
        }
        if (lives <= 0)
        {
            enemyCont.DestroyEnemy(gameObject);
            Destroy(gameObject);
        }
    }
}
