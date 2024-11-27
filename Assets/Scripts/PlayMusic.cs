using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayMusic : MonoBehaviour
{
    private int idMusic;

    [SerializeField] private AudioClip musicClip;

    [SerializeField] private TextMeshProUGUI nameMusic;
    [SerializeField] private TextMeshProUGUI groupingMusic;
    [SerializeField] private string albumText;
    [SerializeField] private string genresText;
    [SerializeField] private Texture2D picturesImage;
    [SerializeField] private string lyricsText;

    [SerializeField] private TextMeshProUGUI timeMusic;

    private MusicManager musicManager;

    // Start is called before the first frame update
    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        StatusMusic();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StatusMusic()
    {
        float minutes = (int) (musicManager.MusicPlaylist[idMusic].musicClip.length / 60f);
        float seconds = (int) (musicManager.MusicPlaylist[idMusic].musicClip.length - minutes * 60f);

        timeMusic.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public int IdMusic
    {
        get { return idMusic; }
        set { idMusic = value; }
    }

    public AudioClip MusicClip
    {
        get { return musicClip; }
        set { musicClip = value; }
    }

    public TextMeshProUGUI NameMusic
    {
        get { return nameMusic; }
        set { nameMusic = value; }
    }

    public TextMeshProUGUI GroupingMusic
    {
        get { return groupingMusic; }
        set { groupingMusic = value; }
    }

    public string AlbumText
    {
        get { return albumText; }
        set { albumText = value; }
    }
    public string GenresText
    {
        get { return genresText; }
        set { genresText = value; }
    }

    public Texture2D PicturesImage
    {
        get { return picturesImage; }
        set { picturesImage = value; }
    }

    public string LyricsText
    {
        get { return lyricsText; }
        set { lyricsText = value; }
    }
    
    public void StartMusicLogic()
    {
        musicManager.MusicSource.clip = musicManager.MusicPlaylist[idMusic].musicClip;
        musicManager.PlayMusicLogic();
        SetStatus();

        musicManager.CurrentIdMusic = idMusic;
    }

    public void SetStatus()
    {
        musicManager.NameMusic.text = nameMusic.text;
        musicManager.NameMusicStatus.text = nameMusic.text;
        musicManager.GroupingMusic.text = groupingMusic.text;
        musicManager.AlbumMusic.text = albumText;
        musicManager.GenresMusic.text = genresText;
        musicManager.PicturesMusic.texture = picturesImage;
        musicManager.LyricsMusic.text = lyricsText;
    }

}
