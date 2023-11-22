using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCarrier : MonoBehaviour
{
    private static AudioCarrier instance;
    //-----------------
    //---PROPERTIES----
    public static AudioCarrier Instance
    {
        get { return instance; }
    }
    //################ #################
    //------------UNITY F--------------
    //################ #################
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}
