#undef DEBUG_POSSIBLE_PLACES

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadCoinSpawn : MonoBehaviour
{
    public List<GameObject> enemy = new List<GameObject>();
    public GameObject BadCoin;
    List<Vector3> possiblePlaces = new List<Vector3>();
    public int countpm = 8;

    // Start is called before the first frame update
    void Start()
    {
        possiblePlaces.Add(new Vector3(-8.3f, 0.5f, -0.6f));
        possiblePlaces.Add(new Vector3(0.6f, 0.5f, 9.3f));
        possiblePlaces.Add(new Vector3(10.7f, 0.5f, -2.6f));
        possiblePlaces.Add(new Vector3(-8.1f, 0.5f, 11.7f));
        possiblePlaces.Add(new Vector3(-7.6f, 0.5f, 6.8f));
        possiblePlaces.Add(new Vector3(-2.5f, 0.5f, -4f));
        possiblePlaces.Add(new Vector3(-1.5f, 0.5f, 6.4f));
        possiblePlaces.Add(new Vector3(7.7f, 0.5f, 7.1f));
        possiblePlaces.Add(new Vector3(16.3f, 0.5f, -1.2f));
        possiblePlaces.Add(new Vector3(-8.5f, 0.5f, -12f));
        possiblePlaces.Add(new Vector3(-10.5f, 0.5f, -7.2f));
        possiblePlaces.Add(new Vector3(-14.1f, 0.5f, 1.3f));
        possiblePlaces.Add(new Vector3(6.8f, 0.5f, 2.2f));
        possiblePlaces.Add(new Vector3(-5f, 0.5f, 1.8f));
        possiblePlaces.Add(new Vector3(3.6f, 0.5f, -9.3f));
        possiblePlaces.Add(new Vector3(-12.3f, 0.5f, 9.7f));
        possiblePlaces.Add(new Vector3(-15.1f, 0.5f, -5.2f));
        possiblePlaces.Add(new Vector3(4.2f, 0.5f, 11.7f));
        possiblePlaces.Add(new Vector3(9.8f, 0.5f, 9.8f));
        possiblePlaces.Add(new Vector3(-0.7f, 0.5f, -10.9f));
        possiblePlaces.Add(new Vector3(7.2f, 0.5f, -8.3f));
        possiblePlaces.Add(new Vector3(15.3f, 0.5f, -10.4f));
        possiblePlaces.Add(new Vector3(15.1f, 0.5f, 6.2f));
        possiblePlaces.Add(new Vector3(-16f, 0.5f, 5.8f));
        possiblePlaces.Add(new Vector3(3.1f, 0.5f, -6f));

#if DEBUG_POSSIBLE_PLACES
        foreach (var place in possiblePlaces)
            enemy.Add(Instantiate(BadCoin, place, transform.rotation, transform));
#else
        int count = System.Math.Max(1, countpm + Random.Range(-1, 2));
        for (int i = 0; i < count; i++)
        {
            int rnd = Random.Range(0, possiblePlaces.Count);
            GameObject bad = Instantiate(BadCoin, possiblePlaces[rnd], transform.rotation, transform);
            enemy.Add(bad);
            possiblePlaces.RemoveAt(rnd);
        }
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.Count <= 0) StartCoroutine(ShowWinScreenAfterSeconds(1));
    }

    IEnumerator ShowWinScreenAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Singleton.Instance.Win();
    }

    public void DestroyEnemy(GameObject bad)
    {
        enemy.Remove(bad);
    }
}