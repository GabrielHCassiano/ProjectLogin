using System.Collections;
using UnityEngine;

public class Main : MonoBehaviour
{

    public static Main instance;

    private Web web;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        web = GetComponent<Web>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Web Web
    { 
        get { return web; }
        set { web = value; }
    }


}
