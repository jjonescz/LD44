using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    [SerializeField] GameObject coin;
    [SerializeField] GameObject arena;
    [SerializeField] Canvas canvas;

    GameObject coinCamera;
    GameObject arenaCamera;

    CoinDirection coinDirection;
    ArenaMove arenaController;
    Animator coinRotationAnim;
    RandomKeys randomKeys;

    // TODO: Change these names if you come up with better ones
    enum GameState { ShowArena, CoinDirection, CoinForce, ControlArena }
    GameState currentState;

    void Awake()
    {
        coinCamera = coin.transform.Find("Camera").gameObject;
        arenaCamera  = arena.transform.Find("Camera").gameObject;

        coinDirection = coin.GetComponent<CoinDirection>();
        arenaController = arena.GetComponent<ArenaMove>();
        coinRotationAnim = coin.GetComponentInChildren<Animator>();
        randomKeys = canvas.GetComponentInChildren<RandomKeys>();

        currentState = GameState.ShowArena;
    }
    
    void Start()
    {
        // disable all cameras to be sure
        coinCamera.SetActive(false);
        arenaCamera.SetActive(false);

        // disable all scripts to be sure
        coinDirection.enabled = false;
        arenaController.enabled = false;
        coinRotationAnim.enabled = false;
        randomKeys.enabled = false;

        ShowArena();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (currentState)
            {
                case (GameState.ShowArena):
                    StartChoosingCoinDirection();
                    break;
                case (GameState.CoinDirection):
                    StartChoosingCoinForce();
                    break;
                case (GameState.CoinForce):
                    // GameFlow doesn't handle this
                    // RandomKeys handles this itself
                    break;
                case (GameState.ControlArena):
                    StartChoosingCoinDirection();
                    break;
            }

        }
    }

    void ShowArena()
    {
        arenaCamera.SetActive(true);
    }

    void StartChoosingCoinDirection()
    {
        arenaCamera.SetActive(false);
        coinCamera.SetActive(true);

        coinDirection.enabled = true;
        arenaController.enabled = false;
        coinRotationAnim.enabled = false;

        currentState = GameState.CoinDirection;
    }

    void StartChoosingCoinForce()
    {
        coinDirection.enabled = false;
        randomKeys.enabled = true;

        currentState = GameState.CoinForce;
    }

    /// <summary>
    /// Call to start the game!
    /// </summary>
    public void ControlArena()
    {
        arenaController.enabled = true;
        coinRotationAnim.enabled = true;

        currentState = GameState.ControlArena;
    }
}
