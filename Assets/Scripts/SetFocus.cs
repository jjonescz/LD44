using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attached to gameObject sets focus on this gameObject when its OnEnable() function is called
/// </summary>
public class SetFocus : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(EnableNextFrame());
    }

    IEnumerator EnableNextFrame()
    {
        yield return null;
        GetComponent<Selectable>().Select();
    }
}
