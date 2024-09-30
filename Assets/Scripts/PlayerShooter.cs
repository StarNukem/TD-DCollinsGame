using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Transform playerBody;

    private void Update()
    {
        AimTowardsMouse();
    }

    private void AimTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = (mousePosition - playerBody.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        playerBody.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}