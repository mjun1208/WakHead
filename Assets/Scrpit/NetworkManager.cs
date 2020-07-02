using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void JoinGame()
    {
        StartCoroutine(ConnectToServer());
    }

    IEnumerator ConnectToServer()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
