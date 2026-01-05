using UnityEngine;

public class MapFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 100f, 0f);
    public float smoothSpeed = 5f;
    public float minY = 50f;

    void LateUpdate()
    {
        if (player == null) return;
        Vector3 target = player.position + offset;
        if (target.y < minY) target.y = minY;
        transform.position = Vector3.Lerp(transform.position, target, Time.unscaledDeltaTime * smoothSpeed);
    }
}
