using UnityEngine;
using System.Collections;

public class God_SCR : MonoBehaviour
{
    /* Publics */
    public string platform;

    /* Privates */

    // Use this for initialization
    void Start()
    {
        // Init the platform
        if (platform == "")
        {
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
            {
                platform = "Editor";
            }

            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                platform = "Ios";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
