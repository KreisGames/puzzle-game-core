using UnityEngine;

namespace Core
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioSource _audioSource;

        public void StartMusic()
        {
            _audioSource.clip = _audioClip;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}