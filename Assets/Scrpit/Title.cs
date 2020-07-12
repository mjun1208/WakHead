using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject ServerButton;
    public GameObject ClientButton;

    public void ClickButton()
    {
        ServerButton.SetActive(false);
        ClientButton.SetActive(false);
    }
}
