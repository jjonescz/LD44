using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomKeys : MonoBehaviour
{
    public CoinController coinController;
    List<GameObject> keys = new List<GameObject>();
    public GameObject predloha;
    List<int> used = new List<int>();
    List<string> symbols = new List<string>();
    List<bool> move = new List<bool>();
    List<bool> active = new List<bool>();
    List<int> number = new List<int>();
    List<GameObject> points = new List<GameObject>();

    float power;

    // Start is called before the first frame update
    void Start()
    {
        symbols.Add("R");
        symbols.Add("T");
        symbols.Add("U");
        symbols.Add("I");
        symbols.Add("O");
        symbols.Add("P");
        symbols.Add("F");
        symbols.Add("G");
        symbols.Add("H");
        symbols.Add("J");
        symbols.Add("K");
        symbols.Add("L");
        symbols.Add("V");
        symbols.Add("B");
        symbols.Add("N");
        symbols.Add("M");
        
        for (int i = 0; i < 16; i++)
        {
            used.Add(i);
            active.Add(false);
            number.Add(0);
        }

        for (int i = 0; i < 10; i++)
        {
            int rand = Random.Range(0, used.Count);

            GameObject key = Instantiate(predloha, transform.position, transform.rotation, transform);

            key.SetActive(true);

            key.GetComponentInChildren<Text>().text = symbols[used[rand]];

            number[used[rand]] = i;

            active[used[rand]] = true;

            keys.Add(key);

            move.Add(true);

            used.RemoveAt(rand);
        }
    }

    // Update is called once per frame
    void Update()
    {
            for (int i = 0; i < keys.Count; i++)
            {
                int x = Random.Range(-1, 2);
                int y = Random.Range(-1, 2);
                int jump = 10;

                if (move[i])
                {
                    Vector3 pos = new Vector3(keys[i].transform.position.x + x * jump, keys[i].transform.position.y + y * jump, keys[i].transform.position.z);
                    Vector2 localpos = new Vector2(keys[i].transform.localPosition.x + x * jump, keys[i].transform.localPosition.y + y * jump);
                    if (localpos.magnitude < 150) keys[i].transform.position = pos;
                }
            }

        for (int i = 0; i < symbols.Count; i++)
        {
            if (Input.GetButtonDown(symbols[i]) && active[i])
            {
                move[number[i]] = false;
                active[i] = false;
                points.Add(keys[number[i]]);
                if (points.Count == 3)
                {
                    Vector2 middle = new Vector2((points[1].transform.localPosition.x + points[2].transform.localPosition.x) / 2, (points[1].transform.localPosition.y + points[2].transform.localPosition.y) / 2);
                    Vector2 center = new Vector2((points[0].transform.localPosition.x + middle.x) / 2, (points[0].transform.localPosition.y + middle.y) / 2);
                    power = middle.magnitude;
                    coinController.StartMoveWithPower(power);
                    
                }

            }

        }
    }
}