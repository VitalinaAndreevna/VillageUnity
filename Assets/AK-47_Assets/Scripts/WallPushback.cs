using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WallPushback : MonoBehaviour
{
    public float pushForceMultiplier = 1f;  // ��������� ��� �������� ����
    public Transform headTransform;  // ��������� ������ (������)
    public Transform leftHandTransform;  // ��������� ����� ����
    public Transform rightHandTransform;  // ��������� ������ ����

    private Rigidbody playerRigidbody;

    void Start()
    {
        // �������� Rigidbody ������ ��� XRRig
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        // ���������, ���������� �� ����� ������ � ����� "Wall"
        if (other.CompareTag("Wall"))
        {
            // ��������� ��� ����� ����, ������� ����� ���������� �����
            ApplyPushback(headTransform, other);
            ApplyPushback(leftHandTransform, other);
            ApplyPushback(rightHandTransform, other);
        }
    }

    private void ApplyPushback(Transform bodyPart, Collider wallCollider)
    {
        // ���� ����� ���� ���������� �����
        if (wallCollider.bounds.Contains(bodyPart.position))
        {
            // �������� ����������� � ���� ������������� (��������� �������� �������� ����� ����)
            Vector3 penetrationDirection = (bodyPart.position - wallCollider.ClosestPoint(bodyPart.position)).normalized;
            Vector3 velocity = playerRigidbody.GetPointVelocity(bodyPart.position);
            float penetrationSpeed = Vector3.Dot(velocity, penetrationDirection);

            // ���� ����� �������� � �����, ��������� ���� ����������
            if (penetrationSpeed > 0)
            {
                Vector3 pushForce = -penetrationDirection * penetrationSpeed * pushForceMultiplier;
                playerRigidbody.AddForceAtPosition(pushForce, bodyPart.position, ForceMode.Impulse);
            }
        }
    }
}
