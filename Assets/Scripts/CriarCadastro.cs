using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CriarCadastro : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField confirmPasswordInputField;

    [SerializeField] private Button cadastroButton;
    [SerializeField] private Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CriarCadastroButton()
    {

        if (nameInputField.text != "" && emailInputField.text != "" && passwordInputField.text != "" && confirmPasswordInputField.text != "" && passwordInputField.text == confirmPasswordInputField.text)
        {
            StartCoroutine(Main.instance.Web.RegisterCooldown(nameInputField.text, emailInputField.text, passwordInputField.text));
            backButton.onClick.Invoke();
        }
    }

}
