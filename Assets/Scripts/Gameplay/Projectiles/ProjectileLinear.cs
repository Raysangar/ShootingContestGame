using UnityEngine;

public class ProjectileLinear : ProjectileBase
{
    protected override void Update()
    {
        base.Update();
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    [SerializeField] float speed;
}
