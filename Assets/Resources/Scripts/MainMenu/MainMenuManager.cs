using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string localHost;
    public ushort port;

    public void Start()
    {
        localHost = "localhost";
        port = 0;
    }

    public void Play()
    {
        if (port != 0)
        {
            if (localHost == "localhost")
                localHost = "127.0.0.1";
            /*ServerManager.Host = localHost;
            ServerManager.Port = port;*/
            ServerCommandHandlers.Host = localHost;
            ServerCommandHandlers.Port = port;
            SceneManager.LoadScene(1);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeLocalHost(string str)
    {
        localHost = str;
    }

    public void ChangePort(string str)
    {
        if (!ushort.TryParse(str, out port))
        {
            port = 0;
        }
    }
}
