using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.ComponentModel;
using System.Text;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using UnityEngine.UI;
using TMPro;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
    }

    
}
