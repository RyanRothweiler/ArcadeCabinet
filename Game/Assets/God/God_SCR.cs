using UnityEngine;
using System.Collections;

public class God_SCR : MonoBehaviour
{
    /* Publics */
    public string platform;

    public GameObject heartOne;
    public GameObject heartTwo;
    public GameObject heartThree;

    /* Privates */
    private GameObject player;

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

        player = GameObject.Find("Player");

        // Turn on the ui
        Camera.main.transform.FindChild("UI").gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        ManagePlayerHealth();
    }

    void ManagePlayerHealth()
    {
        int playerHealth = player.GetComponent<PlayerHealth_SCR>().health;

        if (playerHealth == 3)
        {
            heartOne.SetActive(true);
            heartTwo.SetActive(true);
            heartThree.SetActive(true);
        }

        if (playerHealth == 2)
        {
            heartOne.SetActive(false);
            heartTwo.SetActive(true);
            heartThree.SetActive(true);
        }

        if (playerHealth == 1)
        {
            heartOne.SetActive(false);
            heartTwo.SetActive(false);
            heartThree.SetActive(true);
        }

        if (playerHealth == 0)
        {
            heartOne.SetActive(false);
            heartTwo.SetActive(false);
            heartThree.SetActive(false);
        }
    }
}
