using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float lives = 1;
    public float timer;
    public bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                timer = 0;
                gameObject.GetComponent<Collider>().isTrigger = true;
                hit = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        lives = lives - other.gameObject.GetComponent<CoinController>().power / 100;
        if (lives <= 0) Destroy(gameObject);
        gameObject.GetComponent<Collider>().isTrigger = false;
        hit = true;
    }
}
