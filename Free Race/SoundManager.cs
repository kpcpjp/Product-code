using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Chương trình cho thanh chỉnh âm thanh
public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    // Update is called once per frame
    void Update()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.3f);
            Load();
        } else Load();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
