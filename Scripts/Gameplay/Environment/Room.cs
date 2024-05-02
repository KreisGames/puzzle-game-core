using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Environment
{
    public class Room : MonoBehaviour
    {
        [FormerlySerializedAs("startPoint")] public Transform _startPoint;
    }
}