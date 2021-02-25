using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public System.Action<ProjectileBase> OnDisappear;

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= LifetimeDuration)
            Disable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Disable();
    }

    private void Disable()
    {
        OnDisappear(this);
        Destroy(gameObject);
    }

    private float timer = 0;
    private const float LifetimeDuration = 5;
}
