using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace CaromBilliard
{
    public class VolumeSlider : MonoBehaviour
    {
        public AudioMixer mixer;
        private Slider volumeSlider;
        private string volumePlayerPrefs = "SoundVolume";

        private void Start() 
        {
            volumeSlider = GetComponentInChildren<Slider>();
            volumeSlider.value = PlayerPrefs.GetFloat(volumePlayerPrefs);
        }

        public void SetVolume (float volumeValue)
        {
            mixer.SetFloat("Volume", volumeValue);
            PlayerPrefs.SetFloat(volumePlayerPrefs, volumeValue);
        }
    }
}
