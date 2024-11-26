using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Logar : MonoBehaviour
{
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject musicPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoginButton()
    {
        StartCoroutine(Main.instance.Web.LoginCooldown(emailInputField.text, passwordInputField.text, this));
    }

    public void LoginStart()
    {
        musicPanel.SetActive(true);
        loginPanel.SetActive(false);
    }
}
