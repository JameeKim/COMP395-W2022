using UnityEngine;

public class ProjectileEuler : Projectile
{
    [Header("Physical Properties")]
    [SerializeField]
    private float mass = 1;

    [Header("Old Status")]
    [SerializeField]
    private Vector3 oldPos;

    [SerializeField]
    private Vector3 oldVel;

    [SerializeField]
    private Vector3 oldAcc;

    void FixedUpdate()
    {
        if (!IsLaunched) return;

        float dt = Time.fixedDeltaTime;

        Vector3 newAcc = oldAcc;
        Vector3 newVel = newAcc * dt + oldVel;
        Vector3 newPos = newVel * dt + oldPos;

        transform.position = newPos;

        oldAcc = Physics.gravity;
        oldVel = newVel;
        oldPos = newPos;
    }

    protected override void LaunchImplementation()
    {
        oldPos = transform.position;
        oldVel = initVel;
        oldAcc = Physics.gravity + initForce / mass;
    }
}