using UnityEngine;

public class RingLauncher : MonoBehaviour
{

    public GameController gameController;
    public PointerController pointerController;
    public PowerIndicatorController powerIndicatorController;
    public GameObject ringPrefab;
    public Transform spawnPoint;

    private GameObject currentRing = null;

    void Update()
    {
        if (gameController.currentState == GameController.GameState.SpawningAndLaunchingRing)
        {
            if (currentRing == null)
                SpawnAndLaunchRing();
        }
    }

    void SpawnAndLaunchRing()
    {

        Debug.Log("Spawning and launching the ring...");

        currentRing = Instantiate(ringPrefab, spawnPoint.position, Quaternion.Euler(-90, 0, 0));
        Rigidbody ringRb = currentRing.GetComponent<Rigidbody>();

        Vector3 launchDirection = pointerController.GetLockedDirection().normalized;
        float launchPower = powerIndicatorController.GetLockedPower();

        Debug.Log($"Before launching - Direction: {launchDirection}, Power: {launchPower}");

        ringRb.AddForce(launchDirection * launchPower, ForceMode.Impulse);


        // Register the ring to the GameController's list
        gameController.RegisterRing(currentRing.GetComponent<SelfDestruct>());

        Debug.Log($"Launched ring with direction: {launchDirection}, and power: {launchPower}");
    }
}
