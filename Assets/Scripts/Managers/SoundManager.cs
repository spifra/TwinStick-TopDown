using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                return null;
            }
        }
    }
    #endregion

    private List<StudioEventEmitter> soundsList = new List<StudioEventEmitter>();

    private void Awake()
    {
        instance = this;
        soundsList = Resources.LoadAll<StudioEventEmitter>("Sounds").ToList();
    }


    public void PlaySound(string name)
    {
        StudioEventEmitter sound = soundsList.Where(x => x.name == name).First();

        sound.Play();

    }

}
