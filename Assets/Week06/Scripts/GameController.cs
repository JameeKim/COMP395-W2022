using UnityEngine;
using Week04;

namespace Week06
{
    // NOT assigned to Week06 scene yet
    [DisallowMultipleComponent]
    public class GameController : MonoBehaviour
    {
        [System.Serializable]
        private struct ProjectileInfo
        {
            public Transform spawnPosition;
            public GameObject projectile;
            public Projectile SpawnedProjectile { get; set; }
        }

        [SerializeField]
        private ProjectileInfo[] projectiles;

        public void ResetProjectiles()
        {
            // TODO use the same game object instance for the projectiles (create a new version of Projectile class)
            for (int i = 0; i < projectiles.Length; i++)
            {
                if (projectiles[i].SpawnedProjectile)
                {
                    Destroy(projectiles[i].SpawnedProjectile.gameObject);
                }

                GameObject newProjectile = Instantiate(
                    projectiles[i].projectile,
                    projectiles[i].spawnPosition.position,
                    projectiles[i].spawnPosition.rotation);
                projectiles[i].SpawnedProjectile = newProjectile.GetComponent<Projectile>();
            }
        }

        public void LaunchProjectiles()
        {
            foreach (ProjectileInfo info in projectiles)
            {
                info.SpawnedProjectile.Launch();
            }
        }

        void Start()
        {
            ResetProjectiles();
        }
    }
}
