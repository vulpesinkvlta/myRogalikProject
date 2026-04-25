using UnityEngine;

namespace Core
{
    public interface IInputService
    {
        Vector2 MoveDirection { get; }
        Vector2 AttackDirection { get; }
        bool HasAttackInput { get; }
    }
}
