using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Week04;

namespace Week06
{
    public class ProjectileNumerical : Projectile
    {
        // TODO create ScriptableObjects of numerical simulation methods
        private enum NumericalSimulationStrategy
        {
            None,
            ExplicitEuler,
            SemiImplicitEuler,
            ExplicitRungeKutta4,
        }

        [Header("Calculation Strategy")]
        [SerializeField]
        private NumericalSimulationStrategy numericalStrategy = NumericalSimulationStrategy.ExplicitEuler;

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

        [SuppressMessage("ReSharper", "RedundantAssignment")]
        void FixedUpdate()
        {
            if (!IsLaunched) return;

            float dt = Time.fixedDeltaTime;
            Vector3 newAcc;
            Vector3 newVel;
            Vector3 newPos;

            switch (numericalStrategy)
            {
                case NumericalSimulationStrategy.None:
                    newAcc = Vector3.zero;
                    newVel = Vector3.zero;
                    newPos = Vector3.zero;
                    break;
                case NumericalSimulationStrategy.ExplicitEuler:
                    newAcc = oldAcc;
                    newVel = oldVel + oldAcc * dt;
                    newPos = oldPos + oldVel * dt;
                    break;
                case NumericalSimulationStrategy.SemiImplicitEuler:
                    newAcc = oldAcc;
                    newVel = oldVel + newAcc * dt;
                    newPos = oldPos + newVel * dt;
                    break;
                case NumericalSimulationStrategy.ExplicitRungeKutta4:
                    // TODO implement RungeKutta4 method
                    newAcc = Vector3.zero;
                    newVel = Vector3.zero;
                    newPos = Vector3.zero;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

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
}
