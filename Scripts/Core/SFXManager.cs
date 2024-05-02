using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class SFXManager : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _audioClips;
        private List<AudioSource> _audioSourceReserve = new();

        public void PlaySound(SFX soundId)
        {
            var player = _audioSourceReserve.FirstOrDefault(source => !source.isPlaying);
            if (player == null)
            {
                player = gameObject.AddComponent<AudioSource>();
                _audioSourceReserve.Add(player);
            }
            
            player.playOnAwake = false;
            player.clip = _audioClips[(int)soundId];
            player.pitch = Random.Range(0.9f, 1.1f);
            player.Play();
        }
    }

    public enum SFX
    {
        Step = 0,
        MoveBox = 1,
        PressButton = 2,
        Undo = 3,
    }
}