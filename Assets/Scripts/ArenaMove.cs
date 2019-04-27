using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectHelpers
{
    public static GameObject Find(this GameObject obj, string name)
        => obj.transform.Find(name).gameObject;
}

public class ArenaMove : MonoBehaviour
{
    GameObject coin, coinCamera;
    float width, height; // Plane size
    bool canMove = false;
    bool moving;

    // Start is called before the first frame update
    void Start()
    {
        moving = canMove;

        coin = GameObject.Find("Coin");
        coinCamera = coin.Find("Camera");

        // Plane is 10x10 mesh.
        width = gameObject.transform.localScale.x * 10f;
        height = gameObject.transform.localScale.z * 10f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!moving)
            return;

        const float factor = 3;

        float fwd = Input.GetAxis("X");
        float left = Input.GetAxis("Y");

        // Get coin's position relative to arena's.
        var relX = Mathf.Abs(coin.transform.position.x) / width * 2f;
        var relZ = Mathf.Abs(coin.transform.position.z) / height * 2f;

        // Rotate the plane, but the closer the coin is to its border, the less significant the rotation is.
        var body = gameObject.GetComponent<Rigidbody>();
        body.MoveRotation(Quaternion.Euler(factor * left * (1f - relZ), 0f, -factor * fwd * (1f - relX)));
    }

    void Update()
    {
        const float maxDist = 0.5f;

        // Move camera to look in direction of the coin's movement.
        var coinDirection = coin.GetComponent<Rigidbody>().velocity.normalized;
        if (coinDirection != Vector3.zero)
        {
            Vector3 newPosition = new Vector3(0f, 3f, 0f) - coinDirection * 10f;
            coinCamera.transform.position = Vector3.MoveTowards(coinCamera.transform.position,
                coin.transform.position + newPosition, maxDist);
            coinCamera.transform.LookAt(coin.transform.position);
        }
    }

    public void StopMoving()
    {
        if (!moving)
            return;
        moving = false;

        // Reset rotation.
        var body = gameObject.GetComponent<Rigidbody>();
        body.MoveRotation(Quaternion.Euler(0f, 0f, 0f));
    }

    public void StartMoving()
    {
        if (canMove)
            moving = true;
    }
}
