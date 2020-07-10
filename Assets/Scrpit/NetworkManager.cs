using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using UnityEngine.UI;
using TMPro;

public class NetworkManager : MonoBehaviour
{
    public void GoTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void Disconnect()
    {
        //PhotonNetwork.Disconnect();
    }
    
}
