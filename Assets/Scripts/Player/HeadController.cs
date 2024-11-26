using UnityEngine;

public class HeadController : MonoBehaviour
{
    public Transform neckBone; // Boyun kemiði referansý
    public float rotationLimit = 45f; // Rotasyon limiti

    private float currentXRotation = 0f;

    public void UpdateHeadRotation(float yRotation)
    {
        currentXRotation = Mathf.Clamp(currentXRotation - yRotation, -rotationLimit, rotationLimit);
        neckBone.localRotation = Quaternion.Euler(currentXRotation, neckBone.localRotation.eulerAngles.y, neckBone.localRotation.eulerAngles.z);
    }
}
