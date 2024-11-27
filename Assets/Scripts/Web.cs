using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Web : MonoBehaviour
{
    private string getUrl = "http://localhost/Web/GetUsers.php";
    private string loginUrl = "http://localhost/Web/Login.php";
    private string registerUrl = "http://localhost/Web/Register.php";
    private string uploadUrl = "http://localhost/Web/UploadMusic.php";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GetUsersCooldown()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(getUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                print("error");

            else
                print(request.downloadHandler.text);
        }
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

                if (request.downloadHandler.text == "connected Successfully Login Success.")
                    login.LoginStart();
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

    public IEnumerator UploadMusic(string path)
    {
        WWWForm form = new WWWForm();

        form.AddField("idUser", "1");
        form.AddBinaryData("music", File.ReadAllBytes(path), "Bomdia");

        using (UnityWebRequest request = UnityWebRequest.Post(uploadUrl, form))
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
