using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.ComponentModel;
using System.Text;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;


using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    //public static NetworkManager instance;

    //Socket listen_socket;
    //Socket client_socket;
    //bool IsConnect = false;


    //public Text Textlist;
    //public Text InputText;

    //public bool IsServer = false;

    //byte[] bytes = new byte[1024];
    //string data;

    //private void Awake()
    //{
    //    if (instance == null)
    //        instance = this;
    //    else
    //        Destroy(this.gameObject);

    //    DontDestroyOnLoad(gameObject);

    //    IsConnect = false;
    //}


    //private void Start()
    //{
        
    //}


    //public void StartConnect(string host, int port, int backlog)
    //{
    //    this.listen_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //    IPAddress address;

    //    if (host == "0.0.0.0")
    //        address = IPAddress.Any;
    //    else
    //        address = IPAddress.Parse(host);
    //    IPEndPoint endPoint = new IPEndPoint(address, port);

    //    try
    //    {
    //        listen_socket.Bind(endPoint);
    //        listen_socket.Listen(backlog);

    //        client_socket = listen_socket.Accept();

    //        // 연결되면 여기로 들어옴
    //        IsConnect = true;
    //        Textlist.text = "연결 시작";

    //        Thread listen_thread = new Thread(ReceiveText);
    //        listen_thread.Start();
    //    }
    //    catch (Exception e)
    //    {

    //    }
    //}

    //private void ReceiveText()
    //{
    //    while (IsConnect)
    //    {
    //        while (true)
    //        {
    //            byte[] temp_bytes = new byte[1024];
    //            int bytesRec = client_socket.Receive(temp_bytes);
    //            data = Encoding.UTF8.GetString(temp_bytes, 0, bytesRec);
    //            if (data.IndexOf("<eof>") > -1)
    //                break;
    //        }
    //    }

    //    data = data.Substring(0, data.Length -5);

    //    //넘겨주고
    //    Textlist.text = data;

    //    data = "";
    //}

    //public void ClickStartButton()
    //{
    //    IsServer = true;
    //    StartConnect("127.0.0.1", 7777, 10);
    //}

    //public void ClickConnectButton()
    //{
    //    IsServer = false;

    //    if (IsConnect)
    //        return;

    //    client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //    client_socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7777));

    //    IsConnect = true;
    //    //이제 이건 거시기임
    //    Textlist.text = "연결 시작";
    //    Thread listen_thread = new Thread(ReceiveText);
    //    listen_thread.Start();

    //}

    //public void Send()
    //{
    //    if (!IsConnect)
    //    {
    //        InputText.text = "";
    //        return;
    //    }

    //    byte[] msg = Encoding.UTF8.GetBytes(Textlist.text + "<eof>");
    //    int byteSent = client_socket.Send(msg);
    //    InputText.text = "";
    //}
}
