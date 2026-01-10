using UnityEngine;

public class EnemyAreaLimiter : MonoBehaviour
{
    public Transform leftBound;
    public Transform rightBound;
    public bool lockY = true;

    private float lockedY;

    private void Start()
    {
        lockedY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (leftBound == null || rightBound == null) return;

        float minX = Mathf.Min(leftBound.position.x, rightBound.position.x);
        float maxX = Mathf.Max(leftBound.position.x, rightBound.position.x);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        if (lockY) pos.y = lockedY;

        transform.position = pos;
    }
}
