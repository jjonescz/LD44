using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeBar : MonoBehaviour
{
    EnemyController controller;
    Vector3 lifeBarStartPos;
    float enemyFullLife;

    void Awake()
    {
        controller = gameObject.transform.parent.GetComponent<EnemyController>();
        if (controller == null) Debug.LogError("LifeBar class assumed that EnemyCoin has EnemyController class attached");
    }

    void Start()
    {
        lifeBarStartPos = gameObject.transform.localPosition;
        enemyFullLife = controller.lives;
    }
    
    void Update()
    {
        Vector3 scale = gameObject.transform.localScale;
        scale.x = controller.lives / enemyFullLife;
        gameObject.transform.localScale = scale;

        Vector3 position = lifeBarStartPos;
        position.x -= (enemyFullLife - controller.lives) / 2;
        gameObject.transform.localPosition = position;
    }
}
