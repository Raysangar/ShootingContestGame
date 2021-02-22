using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileCurve : ProjectileBase
{
    protected override void Update()
    {
        base.Update();
        if (rotate)
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * force);
    }

    [SerializeField] float force;
    [SerializeField] bool rotate;
    [SerializeField] float rotationSpeed;
}
