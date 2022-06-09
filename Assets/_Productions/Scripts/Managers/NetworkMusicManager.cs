using System;
using System.Linq;
using CarterGames.Assets.AudioManager;
using Fusion;
using UnityEngine;

public class NetworkMusicManager : NetworkBehaviour
{
    private static NetworkMusicManager _instance = null;

    public static NetworkMusicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<NetworkMusicManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.AddComponent<NetworkMusicManager>();
                    go.name = typeof(NetworkMusicManager).ToString();
                }
            }

            return _instance;
        }
    }

    public void Play(string request, int loop = 0, float volume = 1f)
    {
        Debug.Log($"Receive local play");
        RPC_PlayMusic(request, loop, volume);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_PlayMusic(string request, int loop = 0, float volume = 1f)
    {
        Debug.Log($"Receive RPC play");
        AudioClip clip = null;
        foreach (var lib in AudioManager.instance.AudioManagerFile.library.Where(lib => lib.key == request))
        {
            clip = lib.value;
        }

        MusicPlayer.instance.ShouldLoop = Convert.ToBoolean(loop);
        MusicPlayer.instance.SetVolume(volume);
        MusicPlayer.instance.PlayTrack(clip, TransitionType.CrossFade, 3f);
    }
}
