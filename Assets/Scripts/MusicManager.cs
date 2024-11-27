using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnotherFileBrowser.Windows;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using TagLib;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private GameObject musicPrefab;
    [SerializeField] private GameObject parentMusic;

    [SerializeField] private List<AudioClip> musicPlaylist;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private TextMeshProUGUI nameMusic;
    [SerializeField] private Slider musicSlider;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject stopButton;

    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Sprite[] volumeImages;
    [SerializeField] private Image volumeImageMain;

    [SerializeField] private int currentIdMusic = 0;

    private float currentTime;

    private bool inMute = false;

    private bool inStop = false;
    // Start is called before the first frame update
    void Start()
    {
        sliderMaster.value = PlayerPrefs.GetFloat("Master", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        StatusMusic();
        VolumeImage();
    }

    public List<AudioClip> MusicPlaylist
    {
        get { return musicPlaylist; }
        set { musicPlaylist = value; }
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

    public int CurrentIdMusic
    {
        get { return currentIdMusic; }
        set { currentIdMusic = value; }
    }

    public void StatusMusic()
    {
        if (musicSource.clip != null && !inStop)
        {
            musicSlider.value = musicSource.time / musicSource.clip.length;
        }
        else if (musicSource.clip != null && inStop)
        {
            musicSlider.value = currentTime / musicSource.clip.length;
        }
    }

    public void VolumeImage()
    {
        if (!inMute)
        {
            if (sliderMaster.value > 0.75f)
                volumeImageMain.sprite = volumeImages[3];
            else if (sliderMaster.value > 0.35f)
                volumeImageMain.sprite = volumeImages[2];
            else if (sliderMaster.value > 0.0001f)
                volumeImageMain.sprite = volumeImages[1];
            else if (sliderMaster.value == 0.0001f)
                volumeImageMain.sprite = volumeImages[0];
        }
        else
        {
            volumeImageMain.sprite = volumeImages[0];
        }
    }

    public void Mute()
    {
        if (musicSource.clip != null && sliderMaster.value > 0.0001f)
        {
            inMute = !inMute;
            musicSource.mute = inMute;
        }
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Master", volume);
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void PlayMusicLogic()
    {
        if (musicSource.clip != null)
        {
            inStop = false;
            musicSource.Play();
            playButton.SetActive(false);
            stopButton.SetActive(true);
            musicSource.time = currentTime;
        }
    }

    public void StopMusicLogic()
    {
        if (musicSource.clip != null)
        {
            inStop = true;
            currentTime = musicSource.time;
            musicSource.Stop();
            playButton.SetActive(true);
            stopButton.SetActive(false);
            musicSource.time = currentTime;
        }
    }

    public void ChangeAudioTime()
    {
        if (musicSource.clip != null)
        {
            if (!inStop)
                musicSource.time = musicSource.clip.length * musicSlider.value;
            else
                currentTime = musicSource.clip.length * musicSlider.value;
        }
    }

    public void BackSkipMusicLogic()
    {
        if (musicSource.clip != null && musicPlaylist.Count > 1)
        {
            if (currentIdMusic - 1 < 0)
            {
                musicSource.clip = musicPlaylist[musicPlaylist.Count - 1];
                currentIdMusic = musicPlaylist.Count - 1;
            }
            else
            {
                musicSource.clip = musicPlaylist[currentIdMusic - 1];
                currentIdMusic -= 1;
            }

            PlayMusicLogic();
            musicSource.time = 0;
        }
    }

    public void SkipMusicLogic()
    {
        if (musicSource.clip != null && musicPlaylist.Count > 1)
        {
            if (currentIdMusic + 1 > musicPlaylist.Count - 1)
            {
                musicSource.clip = musicPlaylist[0];
                currentIdMusic = 0;
            }
            else
            {
                musicSource.clip = musicPlaylist[currentIdMusic + 1];
                currentIdMusic += 1;
            }

            PlayMusicLogic();
            musicSource.time = 0;
        }
    }

    public void AddMusic()
    {
        var browser = new BrowserProperties();
        browser.filter = "Audio files (*.wav, *.mp3, *.ogg, *.flac, *.aiff, *.aif, *.mod, *.it, *.s3m, *.xm) | *.wav; *.mp3; *.ogg; *.flac; *.aiff; *.aif; *.mod; *.it; *.s3m; *.xm";
        browser.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(browser, part =>
        {
            StartCoroutine(OpenFileCooldown(part));
        });
    }

    IEnumerator OpenFileCooldown(string path)
    {
        using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                print("error");
            else
            {
                var tagFile = TagLib.File.Create(path);

                var requestMusic = DownloadHandlerAudioClip.GetContent(request);
                PlayMusic playMusic = Instantiate(musicPrefab, parentMusic.transform).GetComponent<PlayMusic>();
                playMusic.NameMusic.text = tagFile.Tag.Title;
                playMusic.IdMusic = musicPlaylist.Count;
                musicPlaylist.Add(requestMusic);

                StartCoroutine(Main.instance.Web.UploadMusic(path));
            }
        }
    }
}
