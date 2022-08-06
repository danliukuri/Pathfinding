using System.Collections.Generic;

namespace AStarPathfinding
{
    internal class PathNodeEqualityComparer<T> : EqualityComparer<PathNode<T>>
    {
        #region Fields
        private readonly PathNodeGraph<T>.EqualCallback _equalCallback;
        #endregion
        
        #region Methods
        public PathNodeEqualityComparer(PathNodeGraph<T>.EqualCallback equalCallback) => _equalCallback = equalCallback;

        public override int GetHashCode(PathNode<T> obj) => obj.GetHashCode();
        public override bool Equals(PathNode<T> first,  PathNode<T> second)
        {
            bool equals;
            if (first == null && second == null)
                equals = true;
            else if (first == null || second == null)
                equals = false;
            else if(ReferenceEquals(first, second))
                equals = true;
            else if (first.GetType() != second.GetType())
                equals = false;
            else
                equals = _equalCallback.Invoke(first.Value, second.Value);

            return equals;
        }
        #endregion
    }
}