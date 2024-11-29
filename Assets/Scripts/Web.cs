using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

public class Web : MonoBehaviour
{
    private string getIdUrl = "http://localhost/Web/GetId.php";
    private string getUserUrl = "http://localhost/Web/GetUser.php";
    private string loginUrl = "http://localhost/Web/Login.php";
    private string registerUrl = "http://localhost/Web/Register.php";
    private string uploadMusicUrl = "http://localhost/Web/UploadMusic.php";
    private string getMusicUrl = "http://localhost/Web/GetMusic.php";
    private string deleteMusicUrl = "http://localhost/Web/DeleteMusic.php";

    [SerializeField] private string usernameLogin;
    [SerializeField] private string emailLogin;
    [SerializeField] private string idLogin;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string Username
    {   
       get { return usernameLogin; }
       set { usernameLogin = value; }
    }

    public string EmailLogin
    {
        get { return emailLogin; }
        set { emailLogin = value; }
    }

    public string Id
    {
        get { return idLogin; }
        set { idLogin = value; }
    }

    public IEnumerator LoginCooldown(string email, string password, Logar login)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginEmail", email);
        form.AddField("loginPass", password);

        using (UnityWebRequest request = UnityWebRequest.Post(loginUrl, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                print("error");

            else
            {
                print(request.downloadHandler.text);

                if (request.downloadHandler.text == "Login Success.")
                {
                    login.LoginStart();

                    emailLogin = email;

                    StartCoroutine(GetUserCooldown(emailLogin));
                    StartCoroutine(GetIdCooldown(emailLogin));

                }
            }

        }
    }

    public IEnumerator GetIdCooldown(string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("EmailUser", email);

        using (UnityWebRequest request = UnityWebRequest.Post(getIdUrl, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                print("error");
            }
            else
            {
                idLogin = request.downloadHandler.text;
            }
        }
    }

    public IEnumerator GetUserCooldown(string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("EmailUser", email);

        using (UnityWebRequest request = UnityWebRequest.Post(getUserUrl, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                print("error");
            }
            else
            {
                usernameLogin = request.downloadHandler.text;
            }
        }
    }

    public IEnumerator RegisterCooldown(string username, string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("registerUser", username);
        form.AddField("registerEmail", email);
        form.AddField("registerPass", password);

        using (UnityWebRequest request = UnityWebRequest.Post(registerUrl, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                print("error");

            else
                print(request.downloadHandler.text);
        }
    }

    public IEnumerator GetMusic(MusicManager musicManager)
    {
        WWWForm form = new WWWForm();

        form.AddField("idUser", idLogin);

        using (UnityWebRequest request = UnityWebRequest.Post(getMusicUrl, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                print("error");
            }
            else
            {
                musicManager.GetMusics(request.downloadHandler.text);
                //StartCoroutine(musicManager.GetMusics(request.downloadHandler.text));
            }
        }
    }

    public IEnumerator UploadMusic(string path, PlayMusic playMusic)
    {
        WWWForm form = new WWWForm();

        form.AddField("idUser", idLogin);
        form.AddBinaryData("arquivo", File.ReadAllBytes(path));

        using (UnityWebRequest request = UnityWebRequest.Post(uploadMusicUrl, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                print("error");
            }
            else
            {
                print(request.downloadHandler.text);
                playMusic.Arquivo = request.downloadHandler.text;
            }
        }
    }

    public IEnumerator DeleteMusic(string arquivo)
    {
        WWWForm form = new WWWForm();

        string[] arquivoMain = arquivo.Split('/');

        print(arquivoMain[arquivoMain.Length-1]);

        form.AddField("idArquivo", arquivoMain[arquivoMain.Length-1]);


        using (UnityWebRequest request = UnityWebRequest.Post(deleteMusicUrl, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                print("error");
            }
            else
            {
                print(request.downloadHandler.text);
            }
        }
    }

}
