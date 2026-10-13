using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public Vector3 localOffset = new Vector3(0f, 3f, -7f);

    public float positionSmoothSpeed = 10f;
    public float rotationSmoothSpeed = 10f;

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.TransformPoint(localOffset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, positionSmoothSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotationSmoothSpeed * Time.deltaTime);
    }
}