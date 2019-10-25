using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace CaromBilliard
{
    public class AudioSystem : MonoBehaviour
    {
        public AudioClip ballHitClip, tableHitClip;

        private float volumeScale;

        private void Start()
        {
            PlayerController.OnApplyForce += (a) => volumeScale = a;

            for (int i = 0; i < BallsManager.Instance.Balls.Length; i++)
                BallsManager.Instance.Balls[i].OnHit += PlayHit;
        }

        private void PlayHit(GameObject sourceObj, GameObject objectToHit)
        {
            if (objectToHit.tag == "Ball")
            {
                var audio = objectToHit.GetComponent<AudioSource>();

                if (audio != null)
                {
                    var scale = objectToHit.GetComponent<Ball>().BallRb.velocity.sqrMagnitude / 100;
                    audio.PlayOneShot(ballHitClip, scale);
                }
            }
            else if (objectToHit.tag == "Table")
            {
                var audio = sourceObj.GetComponent<AudioSource>();

                if (audio != null)
                {
                    var scale = sourceObj.GetComponent<Ball>().BallRb.velocity.sqrMagnitude / 100;
                    audio.PlayOneShot(tableHitClip, scale);
                }
            }
        }
    }
}
