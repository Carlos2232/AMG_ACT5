using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float turnSpeed = 40f;
    public float maxTilt = 25f;

    public int currentHits = 0;
    public int maxHits = 5;

    [Header("UI References")]
    public Image[] lifeImages;

    void Update()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.Self);

        float horizontalInput = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontalInput = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontalInput = 1f;
        }

        float turn = horizontalInput * turnSpeed * Time.deltaTime;
        transform.Rotate(0f, turn, 0f, Space.World);

        float targetTilt = -horizontalInput * maxTilt;
        float currentZ = Mathf.LerpAngle(transform.eulerAngles.z, targetTilt, Time.deltaTime * 5f);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, currentZ);
    }

    public void TakeHit()
    {
        currentHits++;
        Debug.Log($"Hit taken! {currentHits}/{maxHits}");

        int indexToDisable = currentHits - 1;
        if (lifeImages != null && indexToDisable < lifeImages.Length && lifeImages[indexToDisable] != null)
        {
            lifeImages[indexToDisable].gameObject.SetActive(false);
        }

        if (currentHits >= maxHits)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}