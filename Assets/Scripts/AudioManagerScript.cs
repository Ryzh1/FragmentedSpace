using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AudioManagerScript : MonoBehaviour
{
    public AudioClip MainMenuMusic;
    public AudioClip MainSceneMusic;
    public bool IsActive;
    private AudioSource _audioSource;
    

    // Start is called before the first frame update
    void Awake()
    {
        var audio = GameObject.FindGameObjectsWithTag("AudioManager").Select(x => x.GetComponent<AudioManagerScript>()).ToList();
        if(audio.Count < 2)
        {
            DontDestroyOnLoad(gameObject);
            IsActive = true;
        }
        else
        {
            audio.Where(x => !x.IsActive).ToList().ForEach(x => Destroy(x.gameObject));
        }
        _audioSource = gameObject.GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if ((SceneManager.GetActiveScene().buildIndex == 0  || SceneManager.GetActiveScene().buildIndex > 22) && _audioSource.clip != MainMenuMusic)
        {
            _audioSource.clip = MainMenuMusic;
            _audioSource.Play();
        }
        else if (_audioSource.clip != MainSceneMusic && (SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().buildIndex <= 22))
        {
            _audioSource.clip = MainSceneMusic;
            _audioSource.Play();
        }
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
