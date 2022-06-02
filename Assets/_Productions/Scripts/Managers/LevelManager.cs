using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;

public class LevelManager : NetworkSceneManagerBase
{
    [HideInInspector]
    public FusionLauncher launcher;
    [SerializeField] private LoadingManager loadingManager;
    private Scene loadedScene;

    public void ResetLoadedScene()
    {
        loadingManager.ResetLastLevelsIndex();
        loadedScene = default;
    }

    protected override IEnumerator SwitchScene(SceneRef prevScene, SceneRef newScene, FinishedLoadingDelegate finished)
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Loading);
        loadingManager.StartLoadingScreen();
        Debug.Log($"Switching Scene from {prevScene} to {newScene}");
        if (newScene <= 0)
        {
            finished(new List<NetworkObject>());
            yield break;
        }

        yield return new WaitForSeconds(1.0f);

        launcher.SetConnectionStatus(FusionLauncher.ConnectionStatus.Loading, "");

        yield return null;
        Debug.Log($"Start loading scene {newScene} in single peer mode");

        if (loadedScene != default)
        {
            Debug.Log($"Unloading Scene {loadedScene.buildIndex}");
            yield return SceneManager.UnloadSceneAsync(loadedScene);
        }

        loadedScene = default;
        Debug.Log($"Loading scene {newScene}");

        List<NetworkObject> sceneObjects = new List<NetworkObject>();
        if (newScene >= 0)
        {
            yield return SceneManager.LoadSceneAsync(newScene);
            loadedScene = SceneManager.GetSceneByBuildIndex(newScene);
            Debug.Log($"Loaded scene {newScene}: {loadedScene}");
            sceneObjects = FindNetworkObjects(loadedScene, disable: false);
        }

        // Delay one frame
        yield return null;

        launcher.SetConnectionStatus(FusionLauncher.ConnectionStatus.Loaded, "");

        yield return new WaitForSeconds(1);

        Debug.Log($"Switched Scene from {prevScene} to {newScene} - loaded {sceneObjects.Count} scene objects");
        finished(sceneObjects);
        yield return new WaitForSeconds(1f);
        loadingManager.FinishLoadingScreen();
    }
}