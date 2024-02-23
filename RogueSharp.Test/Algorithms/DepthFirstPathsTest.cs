using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RogueSharp.Algorithms;

namespace RogueSharp.Test.Algorithms
{
   [TestClass]
   public class DepthFirstPathsTest
   {
      [TestMethod]
      public void Constructor_WhenGraphIsNull_WillThrowArgumentException() => Assert.ThrowsException<ArgumentException>( () => new DepthFirstPaths( null, 3 ) );

      [TestMethod]
      public void HasPathTo_WhenPathExistsBetweenVertices_WillReturnTrue()
      {
         Graph graph = new( 5 );
         graph.AddEdge( 0, 1 );
         graph.AddEdge( 1, 2 );
         graph.AddEdge( 1, 4 );
         graph.AddEdge( 2, 3 );
         graph.AddEdge( 3, 4 );
         DepthFirstPaths paths = new( graph, 3 );

         Assert.IsTrue( paths.HasPathTo( 0 ) );
      }

      [TestMethod]
      public void HasPathTo_WhenPathDoesNotExistBetweenVertices_WillReturnFalse()
      {
         Graph graph = new( 5 );
         graph.AddEdge( 0, 1 );
         graph.AddEdge( 1, 2 );
         graph.AddEdge( 1, 4 );
         DepthFirstPaths paths = new( graph, 0 );

         Assert.IsFalse( paths.HasPathTo( 3 ) );
      }

      [TestMethod]
      public void PathTo_WhenPathExistsBetweenVertices_WillReturnVerticesInPath()
      {
         Graph graph = new( 5 );
         graph.AddEdge( 0, 1 );
         graph.AddEdge( 1, 2 );
         graph.AddEdge( 1, 4 );
         graph.AddEdge( 2, 3 );
         graph.AddEdge( 3, 4 );
         DepthFirstPaths paths = new( graph, 3 );

         int[] pathVertices = paths.PathTo( 0 ).ToArray();

         Assert.AreEqual( 2, pathVertices[0] );
         Assert.AreEqual( 1, pathVertices[1] );
         Assert.AreEqual( 0, pathVertices[2] );
         Assert.AreEqual( 3, pathVertices.Length );
      }

      [TestMethod]
      public void PathTo_WhenPathDoesNotExistBetweenVertices_WillReturnEmptyCollection()
      {
         Graph graph = new( 5 );
         graph.AddEdge( 0, 1 );
         graph.AddEdge( 1, 2 );
         graph.AddEdge( 1, 4 );
         DepthFirstPaths paths = new( graph, 0 );

         System.Collections.Generic.IEnumerable<int> result = paths.PathTo( 3 );

         Assert.IsNotNull( result ); // Ensure the result is not null
         Assert.IsFalse( result.Any() ); // Ensure the collection is empty
      }
   }
}