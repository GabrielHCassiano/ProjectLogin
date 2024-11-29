using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayMusic : MonoBehaviour
{
    private int idMusic;

    private string arquivo;

    [SerializeField] private AudioClip musicClip;

    [SerializeField] private TextMeshProUGUI nameMusic;
    [SerializeField] private TextMeshProUGUI artistsMusic;
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

    public string Arquivo
    {
        get { return arquivo; }
        set { arquivo = value; }
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

    public TextMeshProUGUI ArtistsMusic
    {
        get { return artistsMusic; }
        set { artistsMusic = value; }
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

        musicManager.MusicSource.time = 0;
        musicManager.CurrentIdMusic = idMusic;
    }

    public void SetStatus()
    {
        musicManager.NameMusic.text = nameMusic.text;
        musicManager.NameMusicStatus.text = nameMusic.text;
        musicManager.ArtistsMusic.text = artistsMusic.text;
        musicManager.AlbumMusic.text = albumText;
        musicManager.GenresMusic.text = genresText;
        musicManager.PicturesMusic.texture = picturesImage;
        musicManager.LyricsMusic.text = lyricsText;
    }

    public void DeleteMusic()
    {
        if (musicManager.CurrentIdMusic == idMusic)
        {
            musicManager.MusicSource.time = 0;
            musicManager.MusicSource.clip = null;
            musicManager.NameMusic.text = "Sem Nome";
            musicManager.NameMusicStatus.text = "Sem Nome";
            musicManager.ArtistsMusic.text = "Sem Artista";
            musicManager.AlbumMusic.text = "Sem Album";
            musicManager.GenresMusic.text = "Sem Gênero";
            musicManager.PicturesMusic.texture = musicManager.SemImagem;
            musicManager.LyricsMusic.text = "Sem Letra";
        }

        print("remover");

        musicManager.MusicPlaylist.Remove(this);

        for (int i = idMusic; i < musicManager.MusicPlaylist.Count; i++)
        {
            print(musicManager.MusicPlaylist[i].idMusic);
            musicManager.MusicPlaylist[i].idMusic -= 1;
        }
        StartCoroutine(Main.instance.Web.DeleteMusic(arquivo));

        Destroy(gameObject);
    }
}
