using AnotherFileBrowser.Windows;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ModelManager : MonoBehaviour
{
    [SerializeField] private GameObject modelParent;

    private GameObject modelMain;

    private float inputHorizontal;
    private float inputVertical;

    private float rotateObject;
    private float zoom = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputLogic();
        MoveObject();
    }

    public void MoveObject()
    {
        if (modelParent.GetComponentInChildren<Transform>() != null)
        {
            modelMain = modelParent.GetComponentInChildren<Transform>().gameObject;

            rotateObject += inputHorizontal;

            modelMain.transform.rotation = Quaternion.Euler(0, rotateObject, 0);

            zoom += inputVertical;
            zoom = Mathf.Clamp(zoom, 0, 150);

            modelMain.transform.localScale = new Vector3(zoom, zoom, zoom);
        }
    }

    public void InputLogic()
    {
        if (modelParent.GetComponentInChildren<Transform>() != null)
        {
            inputHorizontal = Input.GetAxis("Horizontal");
            inputVertical = Input.GetAxis("Vertical");
        }
    }

    public void AddModel()
    {
        var browser = new BrowserProperties();
        browser.filter = "3D files (*.fbx) | *.fbx";
        browser.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(browser, part =>
        {
            StartCoroutine(OpenFileCooldown(part));
        });
    }

    IEnumerator OpenFileCooldown(string path)
    {
        using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(path))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                print("error");
            else
            {
                var requestModel = DownloadHandlerAssetBundle.GetContent(request);

                Instantiate(requestModel, modelParent.transform);
            }
        }
    }
}
