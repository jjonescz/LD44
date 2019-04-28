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
    GameObject coin;
    float width, height; // Plane size
    bool moving = true, stopping;
    float prevFwd, prevLeft;

    // Start is called before the first frame update
    void Start()
    {
        coin = GameObject.Find("Coin");

        // Plane is 10x10 mesh.
        width = gameObject.transform.localScale.x * 10f;
        height = gameObject.transform.localScale.z * 10f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!moving)
            return;

        const float factor = 4;
        const float delta = 0.05f;

        float fwd = Mathf.Clamp(Input.GetAxis("X"), prevFwd - delta, prevFwd + delta);
        float left = Mathf.Clamp(Input.GetAxis("Y"), prevLeft - delta, prevLeft + delta);
        prevFwd = fwd;
        prevLeft = left;

        // Get coin's position relative to arena's.
        var relX = Mathf.Abs(coin.transform.position.x) / width * 2f;
        var relZ = Mathf.Abs(coin.transform.position.z) / height * 2f;

        // Rotate the plane, but the closer the coin is to its border, the less significant the rotation is.
        var body = gameObject.GetComponent<Rigidbody>();
        body.MoveRotation(Quaternion.Euler(factor * left * (1f - relZ), 0f, -factor * fwd * (1f - relX)));
    }

    static float NormalizeAngle(float angle)
    {
        if (angle > 180f) return angle - 360f;
        return angle;
    }
    static Vector3 NormalizeAngles(Vector3 angles)
        => new Vector3(NormalizeAngle(angles.x), NormalizeAngle(angles.y), NormalizeAngle(angles.z));

    void Update()
    {
        if (!stopping)
            return;

        // Reset rotation.
        var body = gameObject.GetComponent<Rigidbody>();
        var rot = NormalizeAngles(body.rotation.eulerAngles);
        Vector3 d = Vector3.MoveTowards(rot, Vector3.zero, 0.1f);
        body.MoveRotation(Quaternion.Euler(d));

        if (body.rotation.eulerAngles == Vector3.zero)
            stopping = false;
    }

    public void StopMoving()
    {
        if (!moving)
            return;
        moving = false;
        stopping = true;
        prevFwd = 0f;
        prevLeft = 0f;
    }

    public void StartMoving()
    {
        stopping = false;
        moving = true;
    }
}
