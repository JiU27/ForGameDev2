using UnityEngine;
using TMPro;

public class PointerController : MonoBehaviour
{
    public GameController gameController;
    public TextMeshProUGUI angleText;
    public float rotationSpeed = 90f;
    public float maxAngle = 45f;
    public float minAngle = -45f;

    private bool isLocked = false;
    private float currentAngle = 0f;
    private int rotationDirection = 1;
    private Vector3 lockedDirection;
    private Vector3 lineEndpoint;

    void Start()
    {
        
        transform.Rotate(new Vector3(-50f, 0f, 0f));
        lockedDirection = transform.forward;
    }

    void FixedUpdate()
    {
        if (gameController.currentState == GameController.GameState.SettingDirection && !isLocked)
        {
            if (isLocked)
            {
                // 如果上一个状态锁定了力量，那么在回到这个状态时重置它。
                //ResetAngle();
            }

            currentAngle += rotationSpeed * Time.deltaTime * rotationDirection;
            if (currentAngle >= maxAngle || currentAngle <= minAngle)
            {
                rotationDirection *= -1;
            }

            transform.rotation = Quaternion.Euler(-50f, 0f, currentAngle);

            if (angleText != null)
            {
                angleText.text = "Angle: " + currentAngle.ToString("F1") + "°";
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                LockPointer();
            }
        }

        
    }

    public void LockPointer()
    {
        isLocked = true;
        lockedDirection = transform.forward;

        // Rotate the lockedDirection 180 degrees around the y-axis
        Quaternion yFlip = Quaternion.Euler(0f, 180f, 0f);
        lockedDirection = yFlip * lockedDirection;

        

        Debug.Log("Pointer Locked! Expected line endpoint: " + lineEndpoint);  // Logging

        if (angleText != null)
        {
            angleText.text = "Locked Angle: " + currentAngle.ToString("F1") + "°";
        }
    }

    public Vector3 GetLockedDirection()
    {
        return lockedDirection;
    }

    public void ResetAngle()
    {
        isLocked = false;
        currentAngle = 0f;

        
        if (angleText != null)
        {
            angleText.text = "Angle: " + currentAngle.ToString("F1");
        }
    }

    public void UnlockPointer()
    {
        isLocked = false;
    }

}
