using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    public int moves = 1;

    private static Singleton instance = null;
    public static Singleton Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) SceneManager.LoadScene("MainMenu");
    }

    public void Win()
    {
        SceneManager.LoadScene("WinScene");
    }
}
