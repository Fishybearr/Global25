using UnityEngine;
using UnityEngine.UIElements;

public class GlobalSettings : MonoBehaviour
{
    public float musicVolume = 1.0f;
    public float sfxVolume = 1.0f;
    
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
