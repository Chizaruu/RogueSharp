using System;
using System.Collections.Generic;

namespace RogueSharp.Algorithms
{
   /// <summary>
   /// The AStarShortestPath class represents a data type for finding the shortest path between two Cells on a Map
   /// </summary>
   public class AStarShortestPath<TCell> where TCell : ICell
   {
      private readonly double? _diagonalCost;

      /// <summary>
      /// Construct a new class for computing the shortest path between two Cells on a Map using the A* algorithm
      /// Using this constructor will not allow diagonal movement. Use the overloaded constructor with diagonalCost if diagonal movement is allowed.
      /// </summary>
      public AStarShortestPath()
      {
      }

      /// <summary>
      /// Construct a new class for computing the shortest path between two Cells on a Map using the A* algorithm
      /// </summary>
      /// <param name="diagonalCost">
      /// The cost of diagonal movement compared to horizontal or vertical movement.
      /// Use 1.0 if you want the same cost for all movements.
      /// On a standard cartesian map, it should be sqrt(2) (1.41)
      /// </param>
      public AStarShortestPath( double diagonalCost )
      {
         _diagonalCost = diagonalCost;
      }

      /// <summary>
      /// Returns an List of Cells representing a shortest path from the specified source to the specified destination
      /// </summary>
      /// <param name="source">The source Cell to find a shortest path from</param>
      /// <param name="destination">The destination Cell to find a shortest path to</param>
      /// <param name="map">The Map on which to find the shortest path between Cells</param>
      /// <returns>List of Cells representing a shortest path from the specified source to the specified destination. If no path is found null will be returned</returns>
      public List<TCell> FindPath( TCell source, TCell destination, IMap<TCell> map )
      {
         var openNodes = new IndexMinPriorityQueue<PathNode>( map.Height * map.Width );
         var isNodeClosed = new bool[map.Height * map.Width];
         openNodes.Insert( map.IndexFor( source ), new PathNode( source.X, source.Y, 0, CalculateDistance( source, destination, _diagonalCost ), null ) );

         while ( !openNodes.IsEmpty() )
         {
            var currentNode = openNodes.MinKey();
            var currentIndex = openNodes.DeleteMin();
            isNodeClosed[currentIndex] = true;

            var currentCell = map.CellFor( currentIndex );
            if ( currentCell.Equals( destination ) )
            {
               var path = new List<TCell>();
               while ( currentNode != null )
               {
                  path.Add( map.GetCell( currentNode.X, currentNode.Y ) );
                  currentNode = currentNode.Parent;
               }
               path.Reverse();
               return path;
            }

            foreach ( var neighbor in map.GetAdjacentCells( currentCell.X, currentCell.Y, _diagonalCost.HasValue ) )
            {
               var neighborIndex = map.IndexFor( neighbor );
               if ( !neighbor.IsWalkable || isNodeClosed[neighborIndex] )
                  continue;

               var isNeighborInOpen = openNodes.Contains( neighborIndex );
               var tentativeGScore = currentNode.DistanceFromStart + 1;
               if ( isNeighborInOpen && tentativeGScore >= openNodes.KeyAt( neighborIndex ).DistanceFromStart )
                  continue;

               var neighborNode = new PathNode( neighbor.X, neighbor.Y, tentativeGScore, CalculateDistance( neighbor, destination, _diagonalCost ), currentNode );
               if ( isNeighborInOpen )
               {
                  openNodes.ChangeKey( neighborIndex, neighborNode );
               }
               else
               {
                  openNodes.Insert( neighborIndex, neighborNode );
               }
            }
         }
         return null;
      }

      private static double CalculateDistance( TCell source, TCell destination )
      {
         int dx = Math.Abs( source.X - destination.X );
         int dy = Math.Abs( source.Y - destination.Y );

         return dx + dy;
      }

      private static double CalculateDistance( TCell source, TCell destination, double? diagonalCost )
      {
         var dx = Math.Abs( source.X - destination.X );
         var dy = Math.Abs( source.Y - destination.Y );
         if ( !diagonalCost.HasValue )
         {
            return dx + dy; // Manhattan distance for non-diagonal
         }

         return diagonalCost.Value * Math.Max( dx, dy ); // Diagonal shortcut
      }

      // G cost = distance from starting node
      // H cost = (heuristic) distance from end node
      private sealed record PathNode( int X, int Y, double DistanceFromStart, double HeuristicDistanceFromEnd, PathNode Parent ) : IComparable<PathNode>
      {
         public double Cost => DistanceFromStart + HeuristicDistanceFromEnd;
         public int CompareTo( PathNode other ) => Cost.CompareTo( other?.Cost ?? double.MaxValue );
      }
   }
}