using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private TextMeshProUGUI nameMusic;
    [SerializeField] private Slider musicSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StatusMusic();
    }

    public AudioSource MusicSource
    {
        get { return musicSource; } 
        set { musicSource = value; }
    }

    public TextMeshProUGUI NameMusic
    {
        get { return nameMusic; }
        set { nameMusic = value; }
    }

    public Slider MusicSlider
    {
        get { return musicSlider; }
        set { musicSlider = value; }
    }

    public void StatusMusic()
    {
        musicSlider.value = musicSource.time / musicSource.clip.length;
    }

    public void PlayMusicLogic()
    {
        musicSource.Play();
    }

    public void StopMusicLogic()
    {
        musicSource.Stop();
    }

    public void ChangeAudioTime()
    {
        musicSource.time = musicSource.clip.length * musicSlider.value;
    }

}
