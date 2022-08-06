using System.Collections.Generic;
using System.Linq;

namespace AStarPathfinding
{
    /// <summary>
    /// A* (pronounced "A-star") is a graph traversal and path search algorithm, which is often used in many fields of
    /// computer science due to its completeness, optimality, and optimal efficiency.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the node, any type that has a way to measure distance between its instances.
    /// </typeparam>
    public class AStar<T>
    {
        #region Fields
        private readonly PathNodeGraph<T> _pathNodeGraph;
        #endregion
    
        #region Methods
        public AStar(PathNodeGraph<T> pathNodeGraph) => _pathNodeGraph = pathNodeGraph;
        
        /// <summary>
        /// Finds shortest path from <paramref name="start"/> to <paramref name="target"/>.
        /// </summary>
        /// <param name="start">Start of path.</param>
        /// <param name="target">End of path.</param>
        /// <returns>Default value of list if source is empty; otherwise, a reversed list of path nodes.</returns>
        public List<T> ShortestReversedPathOrDefault(T start, T target)
        {
            var startNode = _pathNodeGraph.WrapInPathNode(start);
            var lastNodeInPath = EndOfShortestPathOrDefault(startNode, target);
            return lastNodeInPath == default ? default : ShortestReversedPath(lastNodeInPath);
        }
        private static List<T> ShortestReversedPath(PathNode<T> lastNodeInPath)
        {
            var path = new List<T> { lastNodeInPath.Value };
            var pathStep = lastNodeInPath;
            while (pathStep.PreviousPathNode != default)
            {
                pathStep = pathStep.PreviousPathNode;
                path.Add(pathStep.Value);
            }
            return path;
        }
        
        private PathNode<T> EndOfShortestPathOrDefault(PathNode<T> startNode, T target)
        {
            _pathNodeGraph.ResetNodes();
            var openSet = new HashSet<PathNode<T>>();
            var closedSet = new HashSet<T>();

            startNode.UpdatePathCharacteristics(default, default, _pathNodeGraph.CostToTarget(startNode.Value, target));
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                var currentNode = openSet.Aggregate((node1,node2) => node1.F < node2.F ? node1 : node2);
                if (_pathNodeGraph.IsTargetReached(currentNode.Value, target))
                    return currentNode;

                openSet.Remove(currentNode);
                closedSet.Add(currentNode.Value);

                foreach (var currentNeighbor in _pathNodeGraph.GetConnectedNodes(currentNode.Value))
                    if(!closedSet.Contains(currentNeighbor))
                    {
                        var currentNodeNeighbor = _pathNodeGraph.WrapInPathNode(currentNeighbor);
                        var tentativeG = currentNode.G +
                                         _pathNodeGraph.CostToConnectedNode(currentNode.Value, currentNeighbor);
                        if (tentativeG < currentNodeNeighbor.G)
                        {
                            currentNodeNeighbor.UpdatePathCharacteristics(currentNode, tentativeG,
                                _pathNodeGraph.CostToTarget(currentNeighbor, target));

                            if (!openSet.Contains(currentNodeNeighbor))
                                openSet.Add(currentNodeNeighbor);
                        }
                    }
            }
            return default;
        }
        #endregion
    }
}