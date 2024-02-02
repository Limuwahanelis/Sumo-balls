using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] InputActionReference _ballPushAction;
    [SerializeField] InputActionReference _cameraMouseRotationAction;
    [SerializeField] InputActionReference _cameraDeviceRotationAction;
    [SerializeField] BoolReference _skipTutorial;
    [SerializeField] TutorialCameraInputHandler _cameraInputHandler;
    [SerializeField] PushBallTask _ballPushTask;
    [SerializeField] GameObject _firstTutorial;
    // Start is called before the first frame update
    void Start()
    {
        //GlobalSettings.SetPause(true);
        if (_skipTutorial.value) 
        {
            Destroy(_cameraInputHandler);
            _ballPushTask.enabled = false;
            _firstTutorial.SetActive(false);
            return;
        } 
        _ballPushAction.action.Disable();
        _cameraMouseRotationAction.action.Disable();
        _cameraDeviceRotationAction.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCameraRotation(bool value)
    {
        if (value)
        {
            _cameraMouseRotationAction.action.Enable();
            _cameraDeviceRotationAction.action.Enable();
        }
        else
        {
            _cameraMouseRotationAction.action.Disable();
            _cameraDeviceRotationAction.action.Disable();
        }
        //GlobalSettings.SetPause(false);
    }
    public void SetBallPush(bool value)
    {
        if(value) _ballPushAction.action.Enable();
        else _ballPushAction.action.Disable();
    }

    public void SetPause(bool value)
    {
        GlobalSettings.SetPause(value);
    }
    public void CompleteTutorial()
    {
        GameDataManager.UpdateTutorial(true);
    }
}
