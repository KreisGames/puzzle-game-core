using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Environment;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Enviroment
{
    public class RoomManager : MonoBehaviour
    {
        [FormerlySerializedAs("grid")] [SerializeField] private Grid _grid;

        public Room _currentRoom;
        
        public void SetAdjacentTo(Vector2 position, TileState state)
        {
            MarkAllState(position, state);
            MarkAllState(position + Vector2.down, state);
            MarkAllState(position + Vector2.up, state);
            MarkAllState(position + Vector2.left, state);
            MarkAllState(position + Vector2.right, state);
            MarkAllState(position + Vector2.right + Vector2.down, state);
            MarkAllState(position + Vector2.right + Vector2.up, state);
            MarkAllState(position + Vector2.left + Vector2.down, state);
            MarkAllState(position + Vector2.left + Vector2.up, state);
        }

        public List<Tile> GetTiles(Vector2 position)
        {
            LayerMask mask = LayerMask.GetMask("Tile");
            var results = Physics2D.RaycastAll(position, Vector2.zero, 1f, mask);
            return results.Select(result => result.transform.GetComponent<Tile>()).ToList();
        }

        public Tile GetTileSpecific(Vector2 position, Tags tags)
        {
            LayerMask mask = LayerMask.GetMask("Tile");
            var results = Physics2D.RaycastAll(position, Vector2.zero, 1f, mask);
            var tiles = results.Select(result => result.transform.GetComponent<Tile>());
            switch (tags)
            {
                case Tags.Pushable:
                    return tiles.FirstOrDefault(tile => tile._tileTags._pushable);
                case Tags.Stop:
                    return tiles.FirstOrDefault(tile => tile._tileTags._stop);
                case Tags.Interactable:
                    return tiles.FirstOrDefault(tile => tile._tileTags._interactable);
                case Tags.Floor:
                    return tiles.FirstOrDefault(tile => tile._tileTags._floor);
                case Tags.Trigger:
                    return tiles.FirstOrDefault(tile => tile._tileTags._trigger);
                case Tags.Pressurable:
                    return tiles.FirstOrDefault(tile => tile._tileTags._pressurable);
                default:
                    throw new ArgumentOutOfRangeException(nameof(tags), tags, null);
            }
        }
        
        private void MarkAllState(Vector2 recalculatedPosition, TileState state)
        {
            var results = new RaycastHit2D[4];
            LayerMask mask = LayerMask.GetMask("Tile");
            var countSame = Physics2D.RaycastNonAlloc(recalculatedPosition, Vector2.zero, results, 1f, mask);
            for (var i = 0; i < countSame; i++)
            {
                var tileComponent = results[i].collider.gameObject.GetComponent<Tile>();
                tileComponent.SetState(state);
            }
        }
    }
}