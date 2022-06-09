using System;
using System.Collections;
using System.Collections.Generic;
using CarterGames.Assets.AudioManager;
using Fusion;
using UnityEngine;

public class ForestBehaviour : LevelBehaviour
{
    private static ForestBehaviour _instance = null;

    public static ForestBehaviour Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ForestBehaviour>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.AddComponent<ForestBehaviour>();
                    go.name = typeof(ForestBehaviour).ToString();
                }
            }

            return _instance;
        }
    }
    
    public Color sunColor;
    public Light sunLight;
    public GameObject nightSky;
    public GameObject morningSky;
    private int playerOnTent = 0;
    private Material skyMaterial;

    private void Awake()
    {
        skyMaterial = nightSky.GetComponentInChildren<Renderer>().material;
    }

    public override void Spawned()
    {
        base.Spawned();
        PlayAmbientMusic();
    }

    private void PlayAmbientMusic()
    {
        if (Object.HasStateAuthority)
        {
            NetworkMusicManager.Instance.Play("Forest BGM", 1);
        }
    }

    public void PlayerOnSleepTent(PlayerRef playerRef, Player player)
    {
        if (playerOnTent >= 2)
            return;
        
        playerOnTent++;

        if (playerOnTent >= 2)
        {
            Invoke("NextLevel", 5f);
        }
    }

    public void PlayerWakeFromTent(PlayerRef playerRef, Player player)
    {
        playerOnTent--;
        CancelInvoke("NextLevel");
    }

    public void TurnOnNightSky()
    {
        RPC_TurnOnNightSky();
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_TurnOnNightSky()
    {
        sunLight.color = sunColor;
        nightSky.SetActive(true);
        morningSky.SetActive(false);
    }
    
    public void EnableStars1()
    {
        RPC_EnableStars1();
    }
    
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_EnableStars1()
    {
        skyMaterial.SetFloat("_StarLayer3MaxRadius", 0.05f);
    }
    
    public void EnableStars2()
    {
        RPC_EnableStars2();
    }
    
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_EnableStars2()
    {
        skyMaterial.SetFloat("_StarLayer2MaxRadius", 0.005f);
    }
    
    public void EnableStars3()
    {
        RPC_EnableStars3();
    }
    
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_EnableStars3()
    {
        skyMaterial.SetFloat("_StarLayer1MaxRadius", 0.01f);
    }
    
    public void BiggerMoon()
    {
        RPC_BiggerMoon();
    }
    
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_BiggerMoon()
    {
        skyMaterial.SetFloat("_MoonRadius", 0.12f);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_FinishLevel()
    {
        if (FusionHelper.LocalRunner.IsClient) return;
        LoadingManager.Instance.LoadNextLevel(FusionHelper.LocalRunner);
    }
    
    private void NextLevel()
    {
        RPC_FinishLevel();
    }
}
