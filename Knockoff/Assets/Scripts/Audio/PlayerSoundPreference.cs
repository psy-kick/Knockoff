using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundPreference : MonoBehaviour
{
    //This script is responsbile for saving and loading player sound preferences
    public static void SoundVolumeLevelPreference(float sVol)
    {
        PlayerPrefs.SetFloat("sVol", sVol);
    }

    public static void MusicVolumeLevelPreference(float mVol)
    {
        PlayerPrefs.SetFloat("mVol", mVol);
    }

    public static void MusicOnOffPreference(int play) //play if 0 . stop if 1
    {
        PlayerPrefs.SetInt("mOnOff", play);
    }

    public static void SoundOnOffPreference(int play) //play if 0 . stop if 1
    {
        PlayerPrefs.SetInt("sOnOff", play);
    }

    public static float GetSoundVolumeLevelPreference()
    {
        return PlayerPrefs.GetFloat("sVol", 0.8f);
    }

    public static float GetMusicVolumeLevelPreference()
    {
        return PlayerPrefs.GetFloat("mVol", 0.75f);
    }

    public static int GetMusicOnOffPreference()
    {
        return PlayerPrefs.GetInt("mOnOff", 0); // if null then return 0. 0 indicates play music
    }

    public static int GetSoundOnOffPreference()
    {
        return PlayerPrefs.GetInt("sOnOff", 0);// if null then return 0. 0 indicates play music
    }
}

