using System;
using System.Linq;
using Core.UndoFeature;
using Gameplay;
using Gameplay.Actors;
using Gameplay.Enviroment;
using Gameplay.Environment;
using Gameplay.Environment.Triggers;
using UI.Core;
using UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private static bool _initialized;

        private GameState _state = GameState.MainMenu;

        [FormerlySerializedAs("guiRoot")] [SerializeField] private GUIRoot _guiRoot;
        [SerializeField] private ControlsManager _controlsManager;
        [SerializeField] private MusicManager _musicManager;
        [SerializeField] private SFXManager _sfxManager;
        
        private Player _player;
        private RoomManager _roomManager;
        
        private void Awake()
        {
            if (_initialized)
            {
                Destroy(gameObject);
            }
            
            _initialized = true;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            SetState(GameState.MainMenu);
        }

        private void Update()
        {
            _controlsManager.TriggerUpdate();
            
            switch (_state)
            {
                case GameState.MainMenu:
                    MainMenuState();
                    break;
                case GameState.Playing:
                    PlayingState();
                    break;
                case GameState.Intro:
                    IntroState();
                    break;
                case GameState.Ending:
                    EndingState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return;

            void MainMenuState()
            {
                if (_controlsManager.ControlDataInstance.Action)
                {
                    SetState(GameState.Intro);
                }
            }
            
            void PlayingState()
            {
                var undoRecording = new UndoRecording
                {
                    Success = false,
                };
                if (_controlsManager.ControlDataInstance.Up)
                {
                    TryMovePlayer(Vector2.up, ref undoRecording);
                }
                else if (_controlsManager.ControlDataInstance.Down)
                {
                    TryMovePlayer(Vector2.down, ref undoRecording);
                }
                else if (_controlsManager.ControlDataInstance.Left)
                {
                    TryMovePlayer(Vector2.left, ref undoRecording);
                }
                else if (_controlsManager.ControlDataInstance.Right)
                {
                    TryMovePlayer(Vector2.right, ref undoRecording);
                }
                else if (_controlsManager.ControlDataInstance.Undo)
                {
                    var (undoSuccess, undo) = UndoManager.GetLastUndo();
                    
                    if (!undoSuccess) return;

                    Undo(ref undo);
                    _sfxManager.PlaySound(SFX.Undo);
                }

                if (undoRecording.Success)
                {
                    UndoManager.AddRecording(undoRecording);
                    if (undoRecording.ActorType == ActorType.Player)
                    {
                        var tile = _roomManager.GetTileSpecific(_player.transform.position, Tags.Trigger);
                        if (tile == null) return;
                        var trigger = tile.GetComponent<CompositeTrigger>();
                        trigger.Trigger();
                    }
                }
            }
            
            void IntroState()
            {
                _player ??= FindObjectOfType<Player>();
                _roomManager ??= FindObjectOfType<RoomManager>();
                
                var startPosition = _roomManager._currentRoom._startPoint.position;
                MovePlayer(startPosition, true);
                
                SetState(GameState.Playing);
            }

            void EndingState()
            {
                _guiRoot.GetUI<EndScreen>().Show();
            }
        }

        public void ResetUndo()
        {
            UndoManager.ResetRecordings();
        }

        private void Undo(ref UndoRecording undoRecording)
        {
            switch (undoRecording.ActorType)
            {
                case ActorType.Player:
                    var position = _player.transform.position;
                    _roomManager.SetAdjacentTo(position, TileState.Dark);
                    MovePlayer((Vector2)position - undoRecording.Movement, true);
                    break;
                case ActorType.Object:
                    var obj = undoRecording.Object;
                    MoveObject(obj, obj.transform.position - (Vector3)undoRecording.Movement);
                    obj.GetComponent<Tile>().SetState(TileState.Lighted);
                    break;
                //TODO add interactable support if needed
                case ActorType.Interactable:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TryMovePlayer(Vector2 direction, ref UndoRecording undoRecording)
        {
            var currPosition = _player.transform.position;
            var position = (Vector2)currPosition + direction;
            var tiles = _roomManager.GetTiles(position);
            if (tiles.Count == 0)
            {
                return;
            }

            foreach (var tile in tiles)
            {
                if (tile._tileTags._stop)
                {
                    return;
                }

                if (tile._tileTags._pushable)
                {
                    var result = TryMoveObject(tile.gameObject, direction, ref undoRecording);
                    if (!result) return;
                    tile.SetState(TileState.Dark);
                    return;
                }
            }
                    
            MovePlayer(position, false);
            _sfxManager.PlaySound(SFX.Step);
            
            undoRecording.Record(ActorType.Player, direction);
        }

        private void MovePlayer(Vector2 position, bool suppressMemorized)
        {
            DisableTriggerAtPosition(_player.transform.position);
            var transformPl = _player.transform;
            if (!suppressMemorized)
            {
                _roomManager.SetAdjacentTo(transformPl.position, TileState.Dark);
            }

            transformPl.position = position;
            _roomManager.SetAdjacentTo(position, TileState.Lighted);
            TryTriggerAtPosition(position);
        }

        private void TryTriggerAtPosition(Vector2 position)
        {
            var tile = _roomManager.GetTileSpecific(position, Tags.Pressurable);
            if (tile == null) return;
            tile.GetComponent<CompositeTrigger>().Trigger();
            _sfxManager.PlaySound(SFX.PressButton);
        }

        private void DisableTriggerAtPosition(Vector2 position)
        {
            var tile = _roomManager.GetTileSpecific(position, Tags.Pressurable);
            if (tile == null) return;
            tile.GetComponent<CompositeTrigger>().DisableTrigger();
        }

        private bool TryMoveObject(GameObject obj, Vector2 direction, ref UndoRecording undoRecording)
        {
            var position = obj.transform.position;
            var newPos = (Vector2)position + direction;
            var tiles = _roomManager.GetTiles(newPos);
            if (tiles.Count == 0)
            {
                return false;
            }

            if (tiles.Any(tile => tile._tileTags._stop || tile._tileTags._pushable))
            {
                return false;
            }

            MoveObject(obj, newPos);
            _sfxManager.PlaySound(SFX.MoveBox);

            undoRecording.Record(ActorType.Object, direction, obj);
            return true;
        }

        private void MoveObject(GameObject obj, Vector3 newPos)
        {
            DisableTriggerAtPosition(obj.transform.position);
            obj.transform.position = newPos;
            TryTriggerAtPosition(newPos);
        }

        private void OnStateChanged()
        {
            switch (_state)
            {
                case GameState.MainMenu:
                    MainMenuState();
                    break;
                case GameState.Playing:
                    PlayingState();
                    break;
                case GameState.Intro:
                    IntroState();
                    break;
                case GameState.Ending:
                    EndingState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return;

            void MainMenuState()
            {
                _musicManager.StartMusic();
                var mainMenu = _guiRoot.GetUI<MainMenu>();
                mainMenu.Show();
            }
            
            void PlayingState()
            {
                
            }

            void IntroState()
            {
                _guiRoot.GetUI<MainMenu>().Hide();
                SceneManager.LoadScene("Game");
            }

            void EndingState()
            {
                SceneManager.LoadScene("Ending");
            }
        }

        private void SetState(GameState state)
        {
            _state = state;
            OnStateChanged();
        }

        public void EndGame()
        {
            SetState(GameState.Ending);
        }

        public GameState GetState()
        {
            return _state;
        }
    }
}
