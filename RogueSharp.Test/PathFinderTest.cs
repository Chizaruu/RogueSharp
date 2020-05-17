﻿using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RogueSharp.MapCreation;
using RogueSharp.Random;

namespace RogueSharp.Test
{
   [TestClass]
   public class PathFinderTest
   {
      [TestMethod]
      [ExpectedException( typeof( ArgumentNullException ) )]
      public void Constructor_NullMap_ThrowsArgumentNullException()
      {
         var pathFinder = new PathFinder( null );
      }

      [TestMethod]
      [ExpectedException( typeof( ArgumentNullException ) )]
      public void Constructor_NullMapWithDiagonalCostSet_ThrowsArgumentNullException()
      {
         var pathFinder = new PathFinder( null, 1.41 );
      }

      [TestMethod]
      [ExpectedException( typeof( ArgumentNullException ) )]
      public void ShortestPath_SourceIsNull_ThrowsArgumentNullException()
      {
         string mapRepresentation = @"########
                                      #....#.#
                                      #.#..#.#
                                      #.#..#.#
                                      #......#
                                      ########";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         PathFinder pathFinder = new PathFinder( map );
         ICell source = null;
         ICell destination = map.GetCell( 5, 4 );

         Path shortestPath = pathFinder.ShortestPath( source, destination );
      }

      [TestMethod]
      [ExpectedException( typeof( ArgumentNullException ) )]
      public void ShortestPath_DestinationIsNull_ThrowsArgumentNullException()
      {
         string mapRepresentation = @"########
                                      #....#.#
                                      #.#..#.#
                                      #.#..#.#
                                      #......#
                                      ########";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         PathFinder pathFinder = new PathFinder( map );
         ICell source = map.GetCell( 1, 4 );
         ICell destination = null;

         Path shortestPath = pathFinder.ShortestPath( source, destination );
      }

      [TestMethod]
      public void ShortestPath_DestinationReachableFromSource_ExpectedPath()
      {
         string mapRepresentation = @"########
                                      #....#.#
                                      #.#..#.#
                                      #.#..#.#
                                      #......#
                                      ########";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         PathFinder pathFinder = new PathFinder( map );
         ICell source = map.GetCell( 1, 4 );
         ICell destination = map.GetCell( 5, 4 );

         Path shortestPath = pathFinder.ShortestPath( source, destination );

         Assert.AreEqual( 5, shortestPath.Length );
         Assert.AreEqual( source, shortestPath.Start );
         Assert.AreEqual( destination, shortestPath.End );
         Assert.AreEqual( map.GetCell( 2, 4 ), shortestPath.StepForward() );
      }

      [TestMethod]
      public void ShortestPath_DestinationReachableFromSourceAndDiagonalMovementIsAllowed_ExpectedPath()
      {
         string mapRepresentation = @"########
                                      #....#.#
                                      #.#..#.#
                                      #.#..#.#
                                      #......#
                                      ########";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         PathFinder pathFinder = new PathFinder( map, 1.41 );
         ICell source = map.GetCell( 1, 1 );
         ICell destination = map.GetCell( 6, 4 );

         Path shortestPath = pathFinder.ShortestPath( source, destination );

         Assert.AreEqual( 6, shortestPath.Length );
         Assert.AreEqual( source, shortestPath.Start );
         Assert.AreEqual( destination, shortestPath.End );
         Assert.AreEqual( map.GetCell( 2, 1 ), shortestPath.StepForward() );
         Assert.AreEqual( map.GetCell( 3, 2 ), shortestPath.StepForward() );
      }

      [TestMethod]
      [ExpectedException( typeof( PathNotFoundException ) )]
      public void ShortestPath_SourceCellNotWalkable_ThrowsPathNotFoundException()
      {
         string mapRepresentation = @"########
                                      #....#.#
                                      #.#..#.#
                                      #.#..#.#
                                      #......#
                                      ########";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         PathFinder pathFinder = new PathFinder( map );
         ICell source = map.GetCell( 0, 1 );
         ICell destination = map.GetCell( 1, 1 );

         pathFinder.ShortestPath( source, destination );
      }

      [TestMethod]
      [ExpectedException( typeof( PathNotFoundException ) )]
      public void ShortestPath_DestinationNotWalkable_ThrowsPathNotFoundException()
      {
         string mapRepresentation = @"########
                                      #....#.#
                                      #.#..#.#
                                      #.#..#.#
                                      #......#
                                      ########";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         PathFinder pathFinder = new PathFinder( map );
         ICell source = map.GetCell( 1, 1 );
         ICell destination = map.GetCell( 0, 1 );

         pathFinder.ShortestPath( source, destination );
      }

      [TestMethod]
      [ExpectedException( typeof( PathNotFoundException ) )]
      public void ShortestPath_DestinationUnreachable_ThrowsPathNotFoundException()
      {
         string mapRepresentation = @"########
                                      #....#.#
                                      #.#..#.#
                                      #.#..#.#
                                      #....#.#
                                      ########";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         PathFinder pathFinder = new PathFinder( map );
         ICell source = map.GetCell( 1, 1 );
         ICell destination = map.GetCell( 6, 1 );

         pathFinder.ShortestPath( source, destination );
      }

      [TestMethod]
      public void TryFindShortestPath_DestinationReachableFromSource_ExpectedPath()
      {
         string mapRepresentation = @"########
                                      #....#.#
                                      #.#..#.#
                                      #.#..#.#
                                      #......#
                                      ########";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         PathFinder pathFinder = new PathFinder( map );
         ICell source = map.GetCell( 1, 4 );
         ICell destination = map.GetCell( 5, 4 );

         Path shortestPath = pathFinder.TryFindShortestPath( source, destination );

         Assert.AreEqual( 5, shortestPath.Length );
         Assert.AreEqual( source, shortestPath.Start );
         Assert.AreEqual( destination, shortestPath.End );
         Assert.AreEqual( map.GetCell( 2, 4 ), shortestPath.StepForward() );
      }

      [TestMethod]
      public void TryFindShortestPath_DestinationReachableFromSourceAndDiagonalMovementIsAllowed_ExpectedPath()
      {
         string mapRepresentation = @"########
                                      #....#.#
                                      #.#..#.#
                                      #.#..#.#
                                      #......#
                                      ########";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         PathFinder pathFinder = new PathFinder( map, 1.41 );
         ICell source = map.GetCell( 1, 1 );
         ICell destination = map.GetCell( 6, 4 );

         Path shortestPath = pathFinder.TryFindShortestPath( source, destination );

         Assert.AreEqual( 6, shortestPath.Length );
         Assert.AreEqual( source, shortestPath.Start );
         Assert.AreEqual( destination, shortestPath.End );
         Assert.AreEqual( map.GetCell( 2, 1 ), shortestPath.StepForward() );
         Assert.AreEqual( map.GetCell( 3, 2 ), shortestPath.StepForward() );
      }

      [TestMethod]
      [ExpectedException( typeof( ArgumentNullException ) )]
      public void TryFindShortestPath_SourceIsNull_ThrowsArgumentNullException()
      {
         string mapRepresentation = @"########
                                      #....#.#
                                      #.#..#.#
                                      #.#..#.#
                                      #......#
                                      ########";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         PathFinder pathFinder = new PathFinder( map );
         ICell source = null;
         ICell destination = map.GetCell( 5, 4 );

         Path shortestPath = pathFinder.TryFindShortestPath( source, destination );
      }

      [TestMethod]
      [ExpectedException( typeof( ArgumentNullException ) )]
      public void TryFindShortestPath_DestinationIsNull_ThrowsArgumentNullException()
      {
         string mapRepresentation = @"########
                                      #....#.#
                                      #.#..#.#
                                      #.#..#.#
                                      #......#
                                      ########";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         PathFinder pathFinder = new PathFinder( map );
         ICell source = map.GetCell( 1, 4 );
         ICell destination = null;

         Path shortestPath = pathFinder.TryFindShortestPath( source, destination );
      }

      [TestMethod]
      public void TryFindShortestPath_SourceCellNotWalkable_ReturnsNull()
      {
         string mapRepresentation = @"########
                                      #....#.#
                                      #.#..#.#
                                      #.#..#.#
                                      #......#
                                      ########";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         PathFinder pathFinder = new PathFinder( map );
         ICell source = map.GetCell( 0, 1 );
         ICell destination = map.GetCell( 1, 1 );

         Path shortestPath = pathFinder.TryFindShortestPath( source, destination );

         Assert.AreEqual( null, shortestPath );
      }

      [TestMethod]
      public void TryFindShortestPath_DestinationNotWalkable_ReturnsNull()
      {
         string mapRepresentation = @"########
                                      #....#.#
                                      #.#..#.#
                                      #.#..#.#
                                      #......#
                                      ########";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         PathFinder pathFinder = new PathFinder( map );
         ICell source = map.GetCell( 1, 1 );
         ICell destination = map.GetCell( 0, 1 );

         Path shortestPath = pathFinder.TryFindShortestPath( source, destination );

         Assert.AreEqual( null, shortestPath );
      }

      [TestMethod]
      public void TryFindShortestPath_DestinationUnreachable_ReturnsNull()
      {
         string mapRepresentation = @"########
                                      #....#.#
                                      #.#..#.#
                                      #.#..#.#
                                      #....#.#
                                      ########";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         PathFinder pathFinder = new PathFinder( map );
         ICell source = map.GetCell( 1, 1 );
         ICell destination = map.GetCell( 6, 1 );

         Path shortestPath = pathFinder.TryFindShortestPath( source, destination );

         Assert.AreEqual( null, shortestPath );
      }
	  
	  [TestMethod]
      public void TryFindShortestPath_Large200x400MapFromX5Y1ToX29Y187_ReturnsExpectedPath()
      {
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( Algorithms.TestSetup.TestHelpers.Map200x400 );
         IMap map = Map.Create( mapCreationStrategy );
         ICell source = map.GetCell( 5, 1 );
         ICell destination = map.GetCell( 29, 187 );
         PathFinder pathFinder = new PathFinder( map );

         Path shortestPath = pathFinder.TryFindShortestPath( source, destination );

         Assert.AreEqual( 705, shortestPath.Length );
      }

      [TestMethod]
      public void TryFindShortestPath_Large200x400MapTrying12KnownPaths_ReturnsExpectedPaths()
      {
         KnownSeriesRandom randomX = new KnownSeriesRandom( 150, 137, 51, 31, 40, 135, 116, 148, 83, 94, 153, 30, 63, 80, 31, 107, 64, 95, 6, 145, 105, 66, 96, 37 );
         KnownSeriesRandom randomY = new KnownSeriesRandom( 255, 359, 175, 279, 169, 293, 335, 208, 235, 327, 67, 234, 56, 272, 241, 215, 230, 377, 194, 301, 161, 348, 89, 171 );
         int[] pathLengths = { 822, 229, 598, 730, 344, 507, 398, 655, 737, 799, 683, 350 };
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( Algorithms.TestSetup.TestHelpers.Map200x400 );
         IMap map = Map.Create( mapCreationStrategy );
         for ( int i = 0; i < 12; i++ )
         {
            int x1 = randomX.Next( 199 );
            int y1 = randomY.Next( 399 );
            int x2 = randomX.Next( 199 );
            int y2 = randomY.Next( 399 );
            ICell source = map.GetCell( x1, y1 );
            ICell destination = map.GetCell( x2, y2 );

            Stopwatch timer = Stopwatch.StartNew();

            PathFinder pathFinder = new PathFinder( map );
            Path shortestPath = pathFinder.TryFindShortestPath( source, destination );

            Console.WriteLine( $"Path from `{x1}:{y1}` to `{x2}:{y2}` was {shortestPath?.Steps?.Count()} long and took Elapsed Milliseconds: {timer.ElapsedMilliseconds}" );
            Assert.AreEqual( pathLengths[i % 12], shortestPath?.Steps?.Count() );
         }

         // Sample Output
         //Path from `150:255` to `137:359` was 822 long and took Elapsed Milliseconds: 125
         //Path from `51:175` to `31:279` was 229 long and took Elapsed Milliseconds: 128
         //Path from `40:169` to `135:293` was 598 long and took Elapsed Milliseconds: 147
         //Path from `116:335` to `148:208` was 730 long and took Elapsed Milliseconds: 113
         //Path from `83:235` to `94:327` was 344 long and took Elapsed Milliseconds: 150
         //Path from `153:67` to `30:234` was 507 long and took Elapsed Milliseconds: 133
         //Path from `63:56` to `80:272` was 398 long and took Elapsed Milliseconds: 133
         //Path from `31:241` to `107:215` was 655 long and took Elapsed Milliseconds: 131
         //Path from `64:230` to `95:377` was 737 long and took Elapsed Milliseconds: 145
         //Path from `6:194` to `145:301` was 799 long and took Elapsed Milliseconds: 144
         //Path from `105:161` to `66:348` was 683 long and took Elapsed Milliseconds: 156
         //Path from `96:89` to `37:171` was 350 long and took Elapsed Milliseconds: 159
      }

      [TestMethod]
      public void TryFindShortestPath_Large200x400MapTrying12KnownPathsWithDiagonals_ReturnsExpectedPaths()
      {
         KnownSeriesRandom randomX = new KnownSeriesRandom( 150, 137, 51, 31, 40, 135, 116, 148, 83, 94, 153, 30, 63, 80, 31, 107, 64, 95, 6, 145, 105, 66, 96, 37 );
         KnownSeriesRandom randomY = new KnownSeriesRandom( 255, 359, 175, 279, 169, 293, 335, 208, 235, 327, 67, 234, 56, 272, 241, 215, 230, 377, 194, 301, 161, 348, 89, 171 );
         int[] pathLengths = { 749, 203, 557, 667, 328, 463, 371, 602, 692, 733, 626, 326 };
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( Algorithms.TestSetup.TestHelpers.Map200x400 );
         IMap map = Map.Create( mapCreationStrategy );
         for ( int i = 0; i < 12; i++ )
         {
            int x1 = randomX.Next( 199 );
            int y1 = randomY.Next( 399 );
            int x2 = randomX.Next( 199 );
            int y2 = randomY.Next( 399 );
            ICell source = map.GetCell( x1, y1 );
            ICell destination = map.GetCell( x2, y2 );

            Stopwatch timer = Stopwatch.StartNew();

            PathFinder pathFinder = new PathFinder( map, 1 );
            Path shortestPath = pathFinder.TryFindShortestPath( source, destination );

            Console.WriteLine( $"Path from `{x1}:{y1}` to `{x2}:{y2}` was {shortestPath?.Steps?.Count()} long and took Elapsed Milliseconds: {timer.ElapsedMilliseconds}" );
            Assert.AreEqual( pathLengths[i % 12], shortestPath?.Steps?.Count() );
         }

         // Sample Output
         //Path from `150:255` to `137:359` was 749 long and took Elapsed Milliseconds: 121
         //Path from `51:175` to `31:279` was 203 long and took Elapsed Milliseconds: 121
         //Path from `40:169` to `135:293` was 557 long and took Elapsed Milliseconds: 135
         //Path from `116:335` to `148:208` was 667 long and took Elapsed Milliseconds: 159
         //Path from `83:235` to `94:327` was 328 long and took Elapsed Milliseconds: 144
         //Path from `153:67` to `30:234` was 463 long and took Elapsed Milliseconds: 184
         //Path from `63:56` to `80:272` was 371 long and took Elapsed Milliseconds: 133
         //Path from `31:241` to `107:215` was 602 long and took Elapsed Milliseconds: 145
         //Path from `64:230` to `95:377` was 692 long and took Elapsed Milliseconds: 152
         //Path from `6:194` to `145:301` was 733 long and took Elapsed Milliseconds: 157
         //Path from `105:161` to `66:348` was 626 long and took Elapsed Milliseconds: 150
         //Path from `96:89` to `37:171` was 326 long and took Elapsed Milliseconds: 147
      }
   }
}
