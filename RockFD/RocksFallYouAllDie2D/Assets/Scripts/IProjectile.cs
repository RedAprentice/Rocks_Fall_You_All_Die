using UnityEngine;

public interface IProjectile
{

    void SpawningBehavior(Vector3 position, Vector3 direction, int damage, float velocity, float range);

}
