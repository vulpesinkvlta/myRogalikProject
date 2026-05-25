namespace Core
{
    public static class DoorDirectionExtensions
    {
        public static DoorDirection GetOpposite(this DoorDirection direction)
        {
            switch (direction)
            {
                case DoorDirection.Up:
                    return DoorDirection.Down;

                case DoorDirection.Down:
                    return DoorDirection.Up;

                case DoorDirection.Left:
                    return DoorDirection.Right;

                case DoorDirection.Right:
                    return DoorDirection.Left;

                default:
                    throw new System.ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}