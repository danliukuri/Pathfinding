using System.Collections.Generic;

namespace AStarPathfinding
{
    /// <summary>
    /// Class which provide useful functions needed for pathfinding.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the node, any type that has a way connectedNode measure distance between its instances.
    /// </typeparam>
    public abstract class PathNodeGraph<T>
    {
        #region Fields
        private readonly Dictionary<T, PathNode<T>> _nodes = new Dictionary<T, PathNode<T>>();
        #endregion

        #region Methods
        /// <summary>
        /// Evaluates whether a target node has been reached.
        /// </summary>
        /// <param name="node">An evaluated node.</param>
        /// <param name="target">A target node.</param>
        /// <returns>True if target node is reached; otherwise, false.</returns>
        public abstract bool IsTargetReached(T node, T target);
        
        /// <summary>
        /// A heuristic function that estimates the cost of the
        /// cheapest path from a node to the <paramref name="target"/>.
        /// </summary>
        /// <param name="node">An estimated node.</param>
        /// <param name="target">A target node.</param>
        /// <returns>
        /// Cost of the cheapest path from a <paramref name="node"/> to the <paramref name="target"/>.
        /// </returns>
        public abstract float CostToTarget(T node, T target);
        /// <summary>
        /// A heuristic function that estimates the cost to moving from a <paramref name="node"/> to the
        /// <paramref name="connectedNode"/>.
        /// </summary>
        /// <param name="node">An estimated node which have connected nodes.</param>
        /// <param name="connectedNode">A connected node.</param>
        /// <returns>
        /// The weight of the edge from a <paramref name="node"/> to the <paramref name="connectedNode"/>
        /// </returns>
        public abstract float CostToConnectedNode(T node, T connectedNode);
        
        /// <summary>
        /// Provides connected nodes from which we can move to the given <paramref name="node"/>.
        /// </summary>
        /// <param name="node">A node which can have connected ones.</param>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerable`1"/> that contains connected nodes.</returns>
        public abstract IEnumerable<T> GetConnectedNodes(T node);
        
        internal void ResetNodes()
        {
            foreach (var node in _nodes.Values)
                node.G = float.MaxValue;
        }
        internal PathNode<T> WrapInPathNode(T value)
        {
            PathNode<T> vectorNode;
            if (!_nodes.ContainsKey(value))
            {
                vectorNode = new PathNode<T>(value);
                _nodes.Add(value, vectorNode);
            }
            else
                vectorNode = _nodes[value];

            return vectorNode;
        }
        #endregion
    }
}