using UnityEngine;

public class Target : MonoBehaviour
{
    public System.Action<int> OnHit;

    public void ResetParent()
    {
        targetParent.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(pointsValue);
        targetParent.SetActive(false);
        AudioSource.PlayClipAtPoint(onShootSound, targetParent.transform.position);
    }

    [SerializeField] GameObject targetParent;
    [SerializeField] AudioClip onShootSound;
    [SerializeField] int pointsValue;
}
