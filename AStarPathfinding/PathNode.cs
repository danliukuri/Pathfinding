namespace AStarPathfinding
{
    internal class PathNode<T>
    {
        #region Properties
        public T Value { get; }
        public PathNode<T> PreviousPathNode { get; set; }

        /// <summary>The cost of the path from the start node to this one.</summary>
        public float G { get; set; } = float.MaxValue;

        /// <summary>The heuristic cost of the cheapest path from this node to the goal one.</summary>
        public float H { get; set; }

        /// <summary>
        /// Sum of the cost of the path(<see cref="G">G</see>) and the estimate of the cost
        /// required to extend the path all the way to the goal(<see cref="H">H</see>).
        /// </summary>
        public float F { get; set; } = float.MaxValue;
        #endregion

        #region Methods
        public PathNode(T value) => Value = value;
        public void UpdatePathCharacteristics(PathNode<T> previousPathNode, float g, float h)
        {
            PreviousPathNode = previousPathNode;
            G = g;
            H = h;
            F = g + h;
        }
        #endregion
    }
}