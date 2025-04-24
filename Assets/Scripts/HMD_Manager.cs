using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HMD_Manager : MonoBehaviour
{
    
    [SerializeField] GameObject xrPlayer;
    [SerializeField] GameObject FPSPlayer;


    void Start()
    {
        Debug.Log("Using Device:" + XRSettings.loadedDeviceName);
        if (XRSettings.isDeviceActive)
        {
            FPSPlayer.SetActive(false);
            xrPlayer.SetActive(true);
        }
        else
        {
            xrPlayer.SetActive(false);
            FPSPlayer.SetActive(true);
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
