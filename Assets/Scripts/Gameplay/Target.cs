using UnityEngine;

public class Target : MonoBehaviour
{
    public static System.Action<int> OnHit;

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(pointsValue);
        targetParent.SetActive(false);
        AudioSource.PlayClipAtPoint(onShootSound, targetParent.transform.position);
    }

    [SerializeField] GameObject targetParent;
    [SerializeField] AudioClip onShootSound;
    [SerializeField] Transform pointsFeedbackPosition;
    [SerializeField] int pointsValue;
}
