using Gameplay.Damage;
using Gameplay.Survival;
using UnityEngine;

public sealed class MineExploseView : MonoBehaviour, IDamagingView
{
    public DamageModel DamageModel { get; private set; }

    public void Init(DamageModel damageModel)
    {
        DamageModel = damageModel;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CollisionEnter(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEnter(collision.gameObject);
    }

    public void DealDamage(IDamageableView damageable)
    {
        damageable.TakeDamage(DamageModel);
    }

    private void CollisionEnter(GameObject go)
    {
        var damageable = go.GetComponent<IDamageableView>();
        if (damageable is not null)
        {
            DealDamage(damageable);
        }
    }
}