using UnityEngine;

namespace Week04
{
    public class GameController : MonoBehaviour
    {
        [System.Serializable]
        private struct ProjectileInfo
        {
            public Transform spawnPosition;
            public GameObject projectilePrefab;
            public Projectile SpawnedProjectile { get; set; }
        }

        [SerializeField]
        private ProjectileInfo[] projectiles;

        public void ResetProjectiles()
        {
            for (int i = 0; i < projectiles.Length; i++)
            {
                if (projectiles[i].SpawnedProjectile)
                {
                    Destroy(projectiles[i].SpawnedProjectile.gameObject);
                }

                GameObject newProjectile = Instantiate(
                    projectiles[i].projectilePrefab,
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
