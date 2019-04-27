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
    float width, height; // Plane size
    bool moving = true;

    // Start is called before the first frame update
    void Start()
    {
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

        var coin = GameObject.Find("Coin");
        var coinCamera = coin.Find("Camera");

        // Move camera to look in direction of the coin's movement.
        var coinDirection = coin.GetComponent<Rigidbody>().velocity.normalized;
        coinCamera.transform.position = coin.transform.position - coinDirection * 3f;
        coinCamera.transform.LookAt(coin.transform.position + coinDirection);

        // Get coin's position relative to arena's.
        var relX = Mathf.Abs(coin.transform.position.x) / width * 2f;
        var relZ = Mathf.Abs(coin.transform.position.z) / height * 2f;

        // Rotate the plane, but the closer the coin is to its border, the less significant the rotation is.
        var body = gameObject.GetComponent<Rigidbody>();
        body.MoveRotation(Quaternion.Euler(factor * left * (1f - relZ), 0f, -factor * fwd * (1f - relX)));
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

    public void StartMoving() => moving = true;
}
