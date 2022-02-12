using UnityEngine;

namespace Week04
{
    [DisallowMultipleComponent]
    public abstract class Projectile : MonoBehaviour
    {
        [Header("Launch Settings")]
        [SerializeField]
        protected Vector3 initVel = Vector3.zero;

        [SerializeField]
        protected Vector3 initForce = Vector3.zero;

        protected bool IsLaunched { get; private set; }

        public void Launch()
        {
            if (IsLaunched) return;
            LaunchImplementation();
            IsLaunched = true;
        }

        protected abstract void LaunchImplementation();
    }
}