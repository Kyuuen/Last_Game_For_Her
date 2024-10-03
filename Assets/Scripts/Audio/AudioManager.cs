using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] Sound[] jumpSound, sfxSound, clickSound;
    [SerializeField] AudioSource jumpSource, sfxSource, clickSource, themeSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayJumpSound()
    {
        string sound = "jump";
        Sound s = Array.Find(jumpSound, x => x.name == sound);

        if (s == null)
        {
            Debug.Log($"Sound not found: ");
        }
        else
        {
            jumpSource.clip = s.clip;
            jumpSource.Play();
        }
    }

    public void PlaySFXSound(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }

    public void PlayClickSound()
    {
        string name = "click";
        Sound s = Array.Find(clickSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            clickSource.clip = s.clip;
            clickSource.Play();
        }
    }

    public void PlayTheme()
    {
        themeSource.Play();
    }

    public void StopTheme()
    {
        themeSource.Stop();
    }
}
