using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WallPushback : MonoBehaviour
{
    public float pushForceMultiplier = 1f;  // Множитель для контроля силы
    public Transform headTransform;  // Трансформ головы (камеры)
    public Transform leftHandTransform;  // Трансформ левой руки
    public Transform rightHandTransform;  // Трансформ правой руки

    private Rigidbody playerRigidbody;

    void Start()
    {
        // Получаем Rigidbody игрока или XRRig
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        // Проверяем, пересекает ли игрок объект с тегом "Wall"
        if (other.CompareTag("Wall"))
        {
            // Проверяем все части тела, которые могут пересекать стену
            ApplyPushback(headTransform, other);
            ApplyPushback(leftHandTransform, other);
            ApplyPushback(rightHandTransform, other);
        }
    }

    private void ApplyPushback(Transform bodyPart, Collider wallCollider)
    {
        // Если часть тела пересекает стену
        if (wallCollider.bounds.Contains(bodyPart.position))
        {
            // Получаем направление и силу проникновения (вычисляем скорость движения части тела)
            Vector3 penetrationDirection = (bodyPart.position - wallCollider.ClosestPoint(bodyPart.position)).normalized;
            Vector3 velocity = playerRigidbody.GetPointVelocity(bodyPart.position);
            float penetrationSpeed = Vector3.Dot(velocity, penetrationDirection);

            // Если игрок движется в стену, применяем силу вытеснения
            if (penetrationSpeed > 0)
            {
                Vector3 pushForce = -penetrationDirection * penetrationSpeed * pushForceMultiplier;
                playerRigidbody.AddForceAtPosition(pushForce, bodyPart.position, ForceMode.Impulse);
            }
        }
    }
}
