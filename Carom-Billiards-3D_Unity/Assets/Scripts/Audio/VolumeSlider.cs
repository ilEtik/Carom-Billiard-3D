using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace CaromBilliard
{
    /// <summary>
    /// Makes the Slider able to change the volume.
    /// </summary>
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

        /// <summary>
        /// Sets the volume of the AudioMixer.
        /// </summary>
        /// <param name="volumeValue"> Volume that the AudioMixer is set to. </param>
        public void SetVolume (float volumeValue)
        {
            mixer.SetFloat("Volume", volumeValue);
            PlayerPrefs.SetFloat(volumePlayerPrefs, volumeValue);
        }
    }
}
