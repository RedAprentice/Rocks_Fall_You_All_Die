using UnityEngine;

public interface IProjectile
{

    void SpawningBehavior(Vector2 position, Vector2 direction, int damage, float velocity, float range);

}
