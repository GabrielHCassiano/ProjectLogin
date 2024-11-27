using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayMusic : MonoBehaviour
{
    private int idMusic;
    [SerializeField] private TextMeshProUGUI nameMusic;
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
        float minutes = (int) (musicManager.MusicPlaylist[idMusic].length / 60f);
        float seconds = (int) (musicManager.MusicPlaylist[idMusic].length - minutes * 60f);

        timeMusic.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public int IdMusic
    {
        get { return idMusic; }
        set { idMusic = value; }
    }

    public TextMeshProUGUI NameMusic
    {
        get { return nameMusic; }
        set { nameMusic = value; }
    }

    public void StartMusicLogic()
    {
        musicManager.MusicSource.clip = musicManager.MusicPlaylist[idMusic];
        musicManager.PlayMusicLogic();
        musicManager.NameMusic.text = nameMusic.text;
        musicManager.CurrentIdMusic = idMusic;
    }

}
