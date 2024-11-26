using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnotherFileBrowser;
using AnotherFileBrowser.Windows;
using UnityEngine.Networking;
using UnityEditor.PackageManager.Requests;
using System.Windows.Forms;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private GameObject musicPrefab;
    [SerializeField] private GameObject parentMusic;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                


                var requestMusic = DownloadHandlerAudioClip.GetContent(request);
                PlayMusic playMusic = Instantiate(musicPrefab, parentMusic.transform).GetComponent<PlayMusic>();
                playMusic.NameMusic.text = requestMusic.name;
                playMusic.MusicSource.clip = requestMusic;

                StartCoroutine(Main.instance.Web.UploadMusic(path));
            }
        }
    }
}
