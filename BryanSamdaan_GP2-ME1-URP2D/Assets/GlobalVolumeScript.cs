using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVolumeScript : MonoBehaviour
{
    public static GlobalVolumeScript Instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
