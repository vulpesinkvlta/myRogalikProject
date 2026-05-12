namespace Core
{
    public interface IEnemyAttack
    {
        bool CanAttack(float distanceToTarget);
        void TryAttack(IDamageable target);
    }
}
