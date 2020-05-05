using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using UnityEngine.UI;

public class UdpReciever : MonoBehaviour
{
    static string localIpString = "127.0.0.10";
    static int localPort = 8887;
    static IPAddress localAddress = IPAddress.Parse(localIpString);
    IPEndPoint localEP = new IPEndPoint(localAddress, localPort);

    static int unityPort = 8888;
    IPEndPoint unityEP = new IPEndPoint(localAddress, unityPort);
    static bool isReceiving;
    static UdpClient udpUnity;
    Thread thread;
    GameObject window;
    static uWindowCapture.UwcWindowTexture windowScript;



    void Start()
    {
        udpUnity = new UdpClient(unityEP);
        udpUnity.Client.ReceiveTimeout = 20;
        udpUnity.Connect(localEP);
        isReceiving = true;
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
        Debug.Log("start");
        window = GameObject.Find("Window");
        windowScript = window.GetComponent<uWindowCapture.UwcWindowTexture>();
        //Debug.Log(windowScript.partialWindowTitle);
    }

    static public byte[] unityData;


    void OnApplicationQuit()
    {
        isReceiving = false;
        if (thread != null) thread.Abort();
        if (udpUnity != null) udpUnity.Close();
    }

    private static void ThreadMethod()
    {
        while (isReceiving)
        {
            try
            {
                IPEndPoint remoteEP = null;
                byte[] data = udpUnity.Receive(ref remoteEP);
                string targetWindow = System.Text.Encoding.ASCII.GetString(data);
                Debug.Log(targetWindow);
                windowScript.partialWindowTitle = targetWindow;
                Debug.Log(targetWindow);
                Debug.Log(windowScript.partialWindowTitle);
                windowScript.RequestWindowUpdate();
                
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
    }
}
