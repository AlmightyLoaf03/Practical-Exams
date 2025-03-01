using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource audioSource;
    public AudioClip backGroundMusic;
    [SerializeField] private Slider musicSlider;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(backGroundMusic != null)
        {
            PlayBackgroundMusic(false, backGroundMusic);
        }    

        musicSlider.onValueChanged.AddListener(delegate { SetVolume(musicSlider.value); });

    }

    public static void SetVolume(float volume)
    {
        Instance.audioSource.volume = volume;
    }

    public static void PlayBackgroundMusic(bool resetSong, AudioClip audioClip = null)
    {
        if(audioClip != null)
        {
            Instance.audioSource.clip = audioClip;
        }
        if (Instance.audioSource.clip != null)
        {
            if(resetSong)
            {
                Instance.audioSource.Stop();
            }
            Instance.audioSource.Play();
        }
    }

    public static void PauseBackgroundMusic()
    {
        Instance.audioSource.Pause();
    }

    
}
