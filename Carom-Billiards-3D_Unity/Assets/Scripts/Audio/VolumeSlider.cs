using UnityEngine;
using UnityEngine.Audio;

namespace CaromBilliard
{
    public class VolumeSlider : MonoBehaviour
    {
        public AudioMixer mixer;

        public void SetVolume (float volumeValue)
        {
            mixer.SetFloat("Volume", volumeValue);
        }
    }
}
