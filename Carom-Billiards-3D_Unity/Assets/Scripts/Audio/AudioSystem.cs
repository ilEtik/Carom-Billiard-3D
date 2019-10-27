using UnityEngine;

namespace CaromBilliard
{
    public class AudioSystem : MonoBehaviour, IServiceLocator
    {
        void IServiceLocator.ProvideService() { }

        void IServiceLocator.GetService()
        {
            playerController = ServiceLocator.GetService<PlayerController>();
            ballsManager = ServiceLocator.GetService<BallsManager>();
        }

        public AudioClip ballHitClip, tableHitClip;

        private float volumeScale;

        private PlayerController playerController;
        private BallsManager ballsManager;

        private void Start()
        {
            playerController.OnApplyForce += (a) => volumeScale = a;

            for (int i = 0; i < ballsManager.Balls.Length; i++)
                ballsManager.Balls[i].OnHit += PlayHit;
        }

        private void PlayHit(GameObject sourceObj, GameObject objectToHit)
        {
            if (objectToHit.tag == "Ball" || objectToHit.tag == "Player")
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
