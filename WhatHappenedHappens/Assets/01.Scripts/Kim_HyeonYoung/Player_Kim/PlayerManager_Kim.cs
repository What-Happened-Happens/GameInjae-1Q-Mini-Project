using Assets.Script.PlayerObserver;
using Script.Interface;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager_Kim : MonoBehaviour
{ 
    private PlayerMovement_Kim  _playerMovement;
    private PlayerJump_Kim      _playerJump;
    private PlayerInput_Kim     _playerInput;
    private CloneManager        _cloneManager;

    // Observer test code 
    private const float RECORD_DURATION = 5f;
    private bool _isRecording = false;
    private Vector3 _recordStartPosition;

    private ITimer           _timer;
    PlayerSubject            _subject;
    private MovementRecorder _movingRecorder;
    private CloneReplay      _cloneReplay;

    // Record Replay List 
    List<CloneReplay> _activeReplayers = new List<CloneReplay>();
   

    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement_Kim>();    
        _playerJump     = GetComponent<PlayerJump_Kim>();
        _playerInput    = GetComponent<PlayerInput_Kim>();
        _cloneManager   = GetComponent<CloneManager>();

        // Observer test code 
        _timer = GetComponent<Timer>();
        _subject = GetComponent<PlayerSubject>();
        _cloneReplay = GetComponent<CloneReplay>();
    }
    
    void Update()
    {
        // Observer test code 
        _timer.Update();
        Debug.Log($"Current Timer : {RECORD_DURATION}");
        float dir = PlayerInput_Kim.MoveInput;
        _playerMovement.Move(dir);
        _subject.Notify(new PlayerEvent { type = PlayerEventType.MOVE, position = transform.position,
                                          direction = dir, deltaTime = _timer.GetDeltaTime() }); 
     
        if (PlayerInput_Kim.JumpPressed && _playerJump.CanJump)
        {
            _playerJump.Jump();
            _subject.Notify(new PlayerEvent { type = PlayerEventType.JUMP, position = transform.position, 
                                              deltaTime = _timer.GetDeltaTime() });
        }

        // R : Player Recording Start : 10second 
        if (PlayerInput_Kim.TimePressed && !_isRecording)
        {
            Debug.Log($"Player Recording Start! : {_isRecording}");
            _isRecording = true;
            _recordStartPosition = transform.position;   // back Position 
            _movingRecorder = new MovementRecorder();    
            _subject.Attach(_movingRecorder);          
            _timer.TimeInit(true);           
        }

        // Player Recording End && Clone Created 
        if(_isRecording && _timer.IsElapsed(RECORD_DURATION) && _cloneManager.IsCloneCreated)
        {                  
            _isRecording = false;
            _subject.Detach(_movingRecorder);

            GameObject clone = _cloneManager.CreateClone(); // Clone Created 
            CloneReplay replayer; 

            if (!clone.TryGetComponent<CloneReplay>(out replayer))
            {
                replayer = clone.AddComponent<CloneReplay>();
            }

            replayer.SetEvents(_movingRecorder.Events);
            _activeReplayers.Add(replayer);
            
            foreach (var cr in _activeReplayers)// clone Stop clear
                cr.Resume();

            // replayer position Back 
            transform.position = _recordStartPosition;

         }

        // Player clone Delete : E 
        if (PlayerInput_Kim.ReloadPressed && _cloneManager.IsCloneCreated == false && _timer.IsElapsed(5f))
        {
            Debug.Log($"Player Clone Remove! : {_cloneManager.IsCloneCreated}");
            _cloneManager.RemoveClone();
        }
    }
}
