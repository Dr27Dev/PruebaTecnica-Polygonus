using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    public Action<Vector2> OnMove;
    public Action<bool> OnFire_A, OnFire_B;
    public Action OnPause, OnReset;
    private InputManager inputManager;

    private bool isFiring_A, isFiring_B;

    private void Awake()
    {
        Instance = this;
        inputManager = new InputManager();
        EnableInputs();
        isFiring_A = false;
        isFiring_B = false;
    }

    private void FixedUpdate() { HandleMove(); HandleFire(); }

    private void HandleMove() { if (OnMove != null) OnMove(inputManager.Player.Move.ReadValue<Vector2>()); }

    private void HandleFire()
    {
        OnFire_A(false); OnFire_B(false);
        if (isFiring_A && !isFiring_B) { if (OnFire_A != null) OnFire_A(true); }
        if (isFiring_B && !isFiring_A) { if (OnFire_B != null) OnFire_B(true); }
    }
    
    #region InputCallbacks
    private void StartFire_A(InputAction.CallbackContext ctx) { isFiring_A = true; }
    private void StartFire_B(InputAction.CallbackContext ctx) { isFiring_B = true; }
    private void StopFire_A(InputAction.CallbackContext ctx) { isFiring_A = false; }
    private void StopFire_B(InputAction.CallbackContext ctx) { isFiring_B = false; }
    private void TriggerPause(InputAction.CallbackContext ctx) { if (OnPause != null) OnPause(); }
    private void TriggerReset(InputAction.CallbackContext ctx) { if (OnReset != null) OnReset(); }
    #endregion
    
    private void EnableInputs()
    {
        inputManager.Player.Enable();
        inputManager.Player.Fire_A.performed += StartFire_A;
        inputManager.Player.Fire_B.performed += StartFire_B;
        inputManager.Player.Fire_A.canceled += StopFire_A;
        inputManager.Player.Fire_B.canceled += StopFire_B;
        inputManager.Player.Pause.performed += TriggerPause;
        inputManager.Player.Reset.performed += TriggerReset;
    }

    private void OnDisable()
    {
        inputManager.Player.Disable();
    }
}
