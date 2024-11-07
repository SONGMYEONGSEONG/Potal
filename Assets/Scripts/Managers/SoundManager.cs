using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource bgmPlayer;
    [SerializeField] private AudioSource sfxPlayer;

    [SerializeField] private AudioClip[] bgm;
    [SerializeField] private AudioClip[] sfx;

    protected override void Awake()
    {
        base.Awake();

        GameObject Bgmobj = new GameObject();
        Bgmobj.name = typeof(AudioSource).Name + "_BgmPlayer";
        bgmPlayer = Bgmobj.AddComponent<AudioSource>();
        bgmPlayer.volume = 0.1f;
        DontDestroyOnLoad(bgmPlayer);

        GameObject Sfxobj = new GameObject();
        Sfxobj.name = typeof(AudioSource).Name + "_SfxPlayer";
        sfxPlayer = Sfxobj.AddComponent<AudioSource>();
        sfxPlayer.volume = 0.1f;
        DontDestroyOnLoad(sfxPlayer);

        bgm = Resources.LoadAll<AudioClip>("Sounds/BGM");
        sfx = Resources.LoadAll<AudioClip>("Sounds/SFX");
    }

    public void PlayBgm(string bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgm[i].name == bgmName)
            {
                bgmPlayer.clip = bgm[i];
                bgmPlayer.Play();
                return;
            }
        }
    }

    public void StopBgm()
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = null;
    }

    public void PlaySFX(string sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (sfx[i].name == sfxName)
            {
                sfxPlayer.clip = sfx[i];
                sfxPlayer.PlayOneShot(sfx[i]);
                return;
            }
        }
        sfxPlayer.clip = null;
    }
}

