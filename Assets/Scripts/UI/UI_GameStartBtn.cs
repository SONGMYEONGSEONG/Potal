using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameStartBtn : MonoBehaviour
{
    public GameObject StageSelect;

    public void Awake()
    {
        SoundManager.Instance.StopBgm();
        SoundManager.Instance.PlayBgm("Title");
    }
    public void GameStart()
    {
        StageSelect.SetActive(true);
    }
}
