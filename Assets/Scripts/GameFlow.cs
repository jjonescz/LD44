using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{
    public GameObject coin;
    public GameObject arena;
    public Canvas canvas;
    public GameObject messagePanelPrefab;
    public GameObject gameCam;

    GameObject coinCam;
    GameObject mapCam;

    CoinDirection coinDirection;
    ArenaMove arenaController;
    Animator coinRotationAnim;
    RandomKeys randomKeys;
    bool helpShown;

    WallHider wallHider;
    
    enum GameState { ShowArena, CoinDirection, CoinForce, ControlArena }
    GameState currentState;

    void Awake()
    {
        coinCam = coin.Find("Camera");
        mapCam  = arena.Find("MapCamera");

        coinDirection = coin.GetComponent<CoinDirection>();
        arenaController = arena.GetComponent<ArenaMove>();
        coinRotationAnim = coin.GetComponentInChildren<Animator>();
        randomKeys = canvas.GetComponentInChildren<RandomKeys>();

        wallHider = new WallHider(coin, coinCam);

        currentState = GameState.ShowArena;
    }
    
    void Start()
    {
        // disable all cameras to be sure
        coinCam.SetActive(false);
        mapCam.SetActive(false);
        gameCam.SetActive(false);

        // disable all scripts to be sure
        coinDirection.enabled = false;
        arenaController.enabled = false;
        coinRotationAnim.enabled = false;
        randomKeys.enabled = false;

        ShowArena();
    }

    private void Update()
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
                    // GameFlow doesn't handle this
                    // CoinController handles this itself
                    break;
            }
        }

        if (currentState == GameState.CoinForce) wallHider.HideWalls();
    }

    void ShowArena()
    {
        mapCam.SetActive(true);
        ShowMessage("Press Enter to start the game.");
    }

    public void StartChoosingCoinDirection()
    {
        gameCam.SetActive(false);
        mapCam.SetActive(false);
        coinCam.SetActive(true);

        coinDirection.enabled = true;
        arenaController.enabled = false;
        coinRotationAnim.enabled = false;

        coinCam.GetComponent<Camera>().fieldOfView = 60f;

        currentState = GameState.CoinDirection;

        ShowMessage("Use arrows to turn the coin. When ready, press Enter.");
    }

    void StartChoosingCoinForce()
    {
        coinDirection.enabled = false;
        randomKeys.enabled = true;

        coinCam.GetComponent<Camera>().fieldOfView = 22f;

        currentState = GameState.CoinForce;

        ShowMessage("Press three of the letters you see on screen. The center of these letters will determine where you spin the coin.");
    }

    /// <summary>
    /// Call to start the game!
    /// </summary>
    public void ControlArena()
    {
        HideLastMessage();
        wallHider.ShowHiddenWalls();

        randomKeys.enabled = false;
        arenaController.enabled = true;
        coinRotationAnim.enabled = true;

        coinCam.SetActive(false);
        gameCam.SetActive(true);

        currentState = GameState.ControlArena;

        ShowMessage("Use arrows to tilt the arena.");
        helpShown = true;
    }

    /// <summary>
    /// Call to start the game again!
    /// </summary>
    public void RestartCoin()
    {
        StartChoosingCoinDirection();
    }

    GameObject panel;

    // Use this to show message to the player
    void ShowMessage(string message)
    {
        HideLastMessage();

        // Don't show the help twice.
        if (helpShown)
            return;

        panel = Instantiate(messagePanelPrefab, canvas.transform);
        panel.GetComponentInChildren<Text>().text = message;
    }

    void HideLastMessage()
    {
        if (panel != null) Destroy(panel);
    }
}
