using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject PhotonInit;
    
    public void ClickStartButton()
    {
        StartButton.SetActive(false);
        PhotonInit.SetActive(true);
    }
}
