using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinSceneScript : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "You won in " + Singleton.Instance.moves.ToString() + " moves.";
    }
}
