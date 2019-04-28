using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHider
{
    public GameObject coin;
    public GameObject cam;

    List<GameObject> lastHitWalls;

    // Corners of coin
    Vector3[] coinCorners = {   new Vector3(0.0f, 0.5f, 0.0f),
                                new Vector3(0.0f, -0.5f, 0.0f),
                                new Vector3(0.0f, 0.0f, 0.5f),
                                new Vector3(0.0f, 0.0f, -0.5f)};

    public WallHider(GameObject coin, GameObject coinCam)
    {
        this.coin = coin;
        cam = coinCam;
    }

    public void HideWalls()
    {
        ShowHiddenWalls();
        lastHitWalls = new List<GameObject>();

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        Vector3 point1 = cam.transform.position;
        Vector3 direction = coin.transform.position - cam.transform.position;
        float maxDistance = direction.magnitude;

        foreach (var corner in coinCorners)
        {
            RaycastHit[] hits = Physics.RaycastAll(point1, direction + corner, maxDistance, layerMask);

            Debug.DrawRay(point1, (direction + corner).normalized * maxDistance, Color.yellow);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("ArenaWall") || hit.collider.gameObject.CompareTag("BadCoin"))
                {
                    MeshRenderer mr = hit.collider.gameObject.GetComponent<MeshRenderer>();
                    // Don't bother with objects with disabled mesh renderer
                    if (mr == null || mr.enabled == false) continue;
                    mr.enabled = false;
                    lastHitWalls.Add(hit.collider.gameObject);
                }
            }
        }
    }

    public void ShowHiddenWalls()
    {
        if (lastHitWalls != null)
            foreach (GameObject wall in lastHitWalls)
            {
                wall.GetComponent<MeshRenderer>().enabled = true;
            }
        lastHitWalls = null;
    }
}
