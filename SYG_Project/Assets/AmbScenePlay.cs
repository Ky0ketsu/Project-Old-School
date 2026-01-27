using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbScenePlay : MonoBehaviour
{
    [SerializeField]SO_Playlist scenePlaylistAmbiance;

    void OnEnable()
    {
        EVENTS.OnSceneLoaded += SetPlaylist;
        EVENTS.OnGameStart += SetPlaylist;
    }

    void OnDisable()
    {
        EVENTS.OnSceneLoaded -= SetPlaylist;
        EVENTS.OnGameStart -= SetPlaylist;
    }

    void SetPlaylist(int sceneLoaded)
    {
        SetPlaylist();
    }

    void SetPlaylist()
    {
        MUSIC.PLAYER.SetPlaylist(scenePlaylistAmbiance);
    }
}
