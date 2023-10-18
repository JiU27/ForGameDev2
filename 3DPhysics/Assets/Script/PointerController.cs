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

    void Start()
    {
        transform.Rotate(new Vector3(-60f, 0f, 0f));
        lockedDirection = transform.forward;
    }

    void FixedUpdate()
    {
        if (gameController.currentState == GameController.GameState.SettingDirection && !isLocked)
        {
            currentAngle += rotationSpeed * Time.deltaTime * rotationDirection;
            if (currentAngle >= maxAngle || currentAngle <= minAngle)
            {
                rotationDirection *= -1;
            }

            transform.rotation = Quaternion.Euler(-60f, 0f, currentAngle);
            Debug.DrawRay(transform.position, transform.forward);

            if (angleText != null)
            {
                angleText.text = "Angle: " + currentAngle.ToString("F1") + "бу";
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
        lockedDirection = transform.up;

        

        if (angleText != null)
        {
            angleText.text = "Locked Angle: " + currentAngle.ToString("F1") + "бу";
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
