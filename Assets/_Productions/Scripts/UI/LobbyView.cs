using System.Threading.Tasks;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyView : MonoBehaviour
{
    public string nickname = "Player";
    public GameLauncher launcher;
    
    private GameMode gameMode;
    public FusionEvent onPlayerJoinedEvent;
    public FusionEvent onPlayerLeftEvent;
    public FusionEvent onShutdownEvent;
    public FusionEvent onPlayerDataSpawnedEvent;

    [Space]
    [SerializeField] private GameObject initPainel;
    [SerializeField] private GameObject lobbyPainel;
    [SerializeField] private TextMeshProUGUI lobbyPlayerText;
    [SerializeField] private TextMeshProUGUI lobbyRoomName;
    [SerializeField] private Button startButton;
    [Space]
    [SerializeField] private GameObject modeButtons;
    [SerializeField] private TMP_InputField nickText;
    [SerializeField] private TMP_InputField room;

    private void OnEnable()
    {
        onPlayerJoinedEvent.RegisterResponse(ShowLobbyCanvas);
        onShutdownEvent.RegisterResponse(ResetCanvas);
        onPlayerLeftEvent.RegisterResponse(UpdateLobbyList);
        onPlayerDataSpawnedEvent.RegisterResponse(UpdateLobbyList);
    }

    private void OnDisable()
    {
        onPlayerJoinedEvent.RemoveResponse(ShowLobbyCanvas);
        onShutdownEvent.RemoveResponse(ResetCanvas);
        onPlayerLeftEvent.RemoveResponse(UpdateLobbyList);
        onPlayerDataSpawnedEvent.RemoveResponse(UpdateLobbyList);
    }

    //Called from button
    public void SetGameMode(int gameMode)
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Lobby);
        this.gameMode = (GameMode)gameMode;
        modeButtons.SetActive(false);
        nickText.transform.parent.gameObject.SetActive(true);
    }

    //Called from button
    public void StartLauncher()
    {
        launcher = FindObjectOfType<GameLauncher>();
        nickname = nickText.text;
        PlayerPrefs.SetString("Nick", nickname);
        launcher.Launch(gameMode, room.text);
        nickText.transform.parent.gameObject.SetActive(false);
    }

    //Called from button
    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }

    //Called from button
    public void LeaveLobby()
    {
        _ = LeaveLobbyAsync();
    }

    //Called from button
    public void StartButton()
    {
        FusionHelper.LocalRunner.SessionInfo.IsOpen = false;
        FusionHelper.LocalRunner.SessionInfo.IsVisible = false;
        LoadingManager.Instance.LoadNextLevel(FusionHelper.LocalRunner);
    }

    private async Task LeaveLobbyAsync()
    {
        if (FusionHelper.LocalRunner.IsServer)
        {
            CloseLobby();
        }
        await FusionHelper.LocalRunner?.Shutdown(destroyGameObject: false);
    }

    public void CloseLobby()
    {
        foreach(var player in FusionHelper.LocalRunner.ActivePlayers)
        {
            if (player!= FusionHelper.LocalRunner.LocalPlayer)
                FusionHelper.LocalRunner.Disconnect(player);
        }
    }

    private void ResetCanvas(PlayerRef player, NetworkRunner runner)
    {
        initPainel.SetActive(true);
        modeButtons.SetActive(true);
        lobbyPainel.SetActive(false);
        startButton.gameObject.SetActive(runner.IsServer);
    }

    public void ShowLobbyCanvas(PlayerRef player, NetworkRunner runner)
    {
        initPainel.SetActive(false);
        lobbyPainel.SetActive(true);
    }

    public void UpdateLobbyList(PlayerRef playerRef, NetworkRunner runner)
    {
        startButton.gameObject.SetActive(runner.IsServer);
        string players = default;
        string isLocal;
        foreach(var player in runner.ActivePlayers)
        {
            isLocal = player == runner.LocalPlayer ? " (You)" : string.Empty;
            players += GameManager.Instance.GetPlayerData(player, runner)?.Nick + isLocal + " \n";
        }
        lobbyPlayerText.text = players;
        lobbyRoomName.text = $"Room: {runner.SessionInfo.Name}";
    }
}
