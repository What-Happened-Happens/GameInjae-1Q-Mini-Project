using Assets.Script.Clone_Kim;
using Assets.Script.PlayerObserver;
using Script.Interface;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    private PlayerMovement_Kim _playerMovement;
    private PlayerJump_Kim _playerJump;
    private PlayerInput_Kim _playerInput;
    private CloneManager _cloneManager;
    private CloneReplay _cloneReplay;

    // Record
    private RecordManager _recManager;
    private PlaybackManager _playbackManager;
    int _phaseTwoRemaining;

    void Awake()
    {
        // Move Component 
        _playerMovement = GetComponent<PlayerMovement_Kim>();
        _playerJump = GetComponent<PlayerJump_Kim>();
        _playerInput = GetComponent<PlayerInput_Kim>();
        _cloneManager = GetComponent<CloneManager>();

        // Observer test code         
        _recManager = GetComponent<RecordManager>();
        _playbackManager = GetComponent<PlaybackManager>();
        _cloneManager = GetComponent<CloneManager>();

        _recManager.OnPhaseOneFinished += OnPhaseOneRecorded;
        _recManager.OnPhaseTwoFinished += OnPhaseTwoRecorded;
    }

    void Update()
    {
          float dir = PlayerInput_Kim.MoveInput;
          _playerMovement.Move(dir);
          GetComponent<PlayerSubject>().Notify(
             new PlayerEvent
             {
                 type = PlayerEventType.MOVE,
                 position = transform.position,
                 direction = dir,
                 deltaTime = GetComponent<Timer>().GetDeltaTime()
             }
         );
      
        
        if (PlayerInput_Kim.JumpPressed && _playerJump.CanJump)
        {
            _playerJump.Jump();
            GetComponent<PlayerSubject>().Notify(
                 new PlayerEvent
                 {
                     type = PlayerEventType.JUMP,
                     position = transform.position,
                     deltaTime = GetComponent<Timer>().GetDeltaTime()
                 });
        }

        if (_recManager.RecordedEvents != null)
        {
            _playbackManager.Replay(_recManager.RecordedEvents); 
        }

        if (PlayerInput_Kim.TimePressed)
        {
            if (!_cloneReplay && _recManager.RecordedEvents.Count > 0 && _phaseTwoRemaining == 0)
            {
                _cloneReplay.PauseAndHide();

                _recManager.StartPhase(2, _cloneReplay.NextIndex);
            }
        }
    }
    
    void OnPhaseOneRecorded()
    {
        var Events = _recManager.RecordedEvents; 

        _cloneReplay = _playbackManager.Replay(Events);
        transform.position = _recManager._recordStartPosition;
    }
    void OnPhaseTwoRecorded()
    {
        var rec = _recManager.RecordedEvents;
        _phaseTwoRemaining = 2;
        var clone_two = _playbackManager.Replay(rec);
        clone_two.OnReplayFinished += _recManager.OnPhaseTwoFinished;
    }

}
