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

        // Spawn the ring.
        currentRing = Instantiate(ringPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody ringRb = currentRing.GetComponent<Rigidbody>();

        // Launch the ring.
        Vector3 launchDirection = pointerController.GetLockedDirection();
        float launchPower = powerIndicatorController.GetLockedPower();

        // Launch the ring.
        ringRb.AddForce(launchDirection * launchPower, ForceMode.Impulse);
        //currentRing = null;

        

        Debug.Log($"Launched ring with direction: {launchDirection}, and power: {launchPower}");
    }
}
