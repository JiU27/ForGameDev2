using System.Net.NetworkInformation;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState
    {
        SettingDirection,
        SettingPower,
        SpawningAndLaunchingRing
    }

    public GameState currentState = GameState.SettingDirection;
    public PointerController pointerController;
    void Update()
    {
        switch (currentState)
        {
            case GameState.SettingDirection:
                pointerController.UnlockPointer(); // Ensure pointer is unlocked
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentState = GameState.SettingPower;
                }
                break;

            case GameState.SettingPower:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentState = GameState.SpawningAndLaunchingRing;
                }
                break;

            case GameState.SpawningAndLaunchingRing:

                // Check if there are no active Ring instances in the scene
                SelfDestruct[] rings = FindObjectsOfType<SelfDestruct>(); // Get all Ring instances in the scene
                bool activeRingExists = false;

                foreach (SelfDestruct ring in rings)
                {
                    if (ring.isActive)
                    {
                        activeRingExists = true;
                        break;
                    }
                }

                if (!activeRingExists)
                {
                    currentState = GameState.SettingDirection;
                    Debug.Log("No active rings, switching state!");
                }
                break;
        }
    }
}
