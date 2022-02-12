using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectilePhysics : Projectile
{
    [SerializeField]
    private ForceMode forceMode;

    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    protected override void LaunchImplementation()
    {
        _rigidbody.isKinematic = false;

        _rigidbody.velocity = initVel;
        switch (forceMode)
        {
            case ForceMode.Force:
                _rigidbody.AddForce(initForce, ForceMode.Force);
                break;
            case ForceMode.Impulse:
                _rigidbody.AddForce(initForce, ForceMode.Impulse);
                break;
            case ForceMode.Acceleration:
                _rigidbody.AddForce(initForce, ForceMode.Acceleration);
                break;
            case ForceMode.VelocityChange:
                _rigidbody.AddForce(initForce, ForceMode.VelocityChange);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(forceMode), forceMode, "Invalid force mode");
        }
    }
}