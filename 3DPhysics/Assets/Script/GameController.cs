using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState
    {
        SettingDirection,
        SettingPower,
        WaitingForLaunch,
        SpawningAndLaunchingRing
    }

    public GameState currentState = GameState.SettingDirection;
    public PointerController pointerController;
    public PowerIndicatorController powerIndicatorController;
    private List<SelfDestruct> rings = new List<SelfDestruct>();

    public float spaceCooldownDuration = 0.5f;  // 设置为0.5秒，您可以根据需要调整
    private float spaceCooldownTimer = 0f;

    void Start()
    {
        // Initialize the list with the rings present at the start (if any)
        rings.AddRange(FindObjectsOfType<SelfDestruct>());
    }
    void Update()
    {
        if (spaceCooldownTimer > 0f)
        {
            spaceCooldownTimer -= Time.deltaTime;
        }

        switch (currentState)
        {
            case GameState.SettingDirection:
                pointerController.UnlockPointer(); // Ensure pointer is unlocked
                if (Input.GetKeyDown(KeyCode.Space) && spaceCooldownTimer <= 0f)
                {
                    currentState = GameState.SettingPower;
                    spaceCooldownTimer = spaceCooldownDuration;  // Set the cooldown
                }
                break;

            case GameState.SettingPower:
                if (Input.GetKeyDown(KeyCode.Space) && spaceCooldownTimer <= 0f)
                {
                    currentState = GameState.WaitingForLaunch;
                    spaceCooldownTimer = spaceCooldownDuration;  // Set the cooldown
                    powerIndicatorController.LockPower();

                }
                break;

            case GameState.WaitingForLaunch:
                currentState = GameState.SpawningAndLaunchingRing;
                break;

            case GameState.SpawningAndLaunchingRing:
                bool activeRingExists = rings.Exists(ring => ring.isActive);
                if (!activeRingExists)
                {
                    currentState = GameState.SettingDirection;
                    Debug.Log("No active rings, switching state!");
                }
                break;
        }
    }
    public void RegisterRing(SelfDestruct ring)
    {
        rings.Add(ring);
    }

    public void UnregisterRing(SelfDestruct ring)
    {
        rings.Remove(ring);
    }
    //public void LaunchRing()
    //{
        //currentState = GameState.SpawningAndLaunchingRing;
    //}

}
