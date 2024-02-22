﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RogueSharp.MapCreation;
using RogueSharp.Random;

namespace RogueSharp.Test
{
   [TestClass]
   public partial class MapTest
   {
      [TestMethod]
      public void Constructor_MapCreatedWithWidth40Height20_ExpectedWidthAndHeight()
      {
         int expectedWidth = 40;
         int expectedHeight = 20;

         Map map = new( expectedWidth, expectedHeight );

         Assert.AreEqual( expectedWidth, map.Width );
         Assert.AreEqual( expectedHeight, map.Height );
      }

      [TestMethod]
      public void Clear_IsTransparentTrueIsWalkableFalse_AllCellsHaveExpectedValues()
      {
         Map map = new( 10, 10 );

         map.Clear( true, false );

         foreach ( ICell cell in map.GetAllCells() )
         {
            Assert.IsTrue( map.IsTransparent( cell.X, cell.Y ) );
            Assert.IsFalse( map.IsWalkable( cell.X, cell.Y ) );
         }
      }

      [TestMethod]
      public void Create_MapCreatedWithCaveCreationStrategy_ExpectedMap()
      {
         int expectedWidth = 50;
         int expectedHeight = 20;
         IRandom random = new DotNetRandom( 27 );
         IMapCreationStrategy<Map> mapCreationStrategy = new CaveMapCreationStrategy<Map>( expectedWidth, expectedHeight, 45, 3, 2, random );
         string expectedMapRepresentation = @"##################################################
                                              ########...###################.#.#####....########
                                              ######.......##############......#####....########
                                              ####........###############......####......#######
                                              ###.........#################...######........####
                                              ##........#################.##..#####.....########
                                              ###.......#######.#.######..##..#######.#..#######
                                              ####......######.......#........#########..#######
                                              #####.......#....##....###......#########..#######
                                              ######........#####..............#########..######
                                              #..###......########....######...#########..######
                                              #...##......########.....#####.....#.######..#####
                                              #............##########..######......######..#####
                                              #...........########.....######.#....#######..####
                                              ##...........#######.....###################..####
                                              #............#.#####........#################..###
                                              ##.............#####................#########..###
                                              ###.............#####........######...........####
                                              ####.#....#.#.#######.#.#...######################
                                              ##################################################";

         IMap actualMap = Map.Create( mapCreationStrategy );
         Trace.Write( actualMap );

         Assert.AreEqual( expectedWidth, actualMap.Width );
         Assert.AreEqual( expectedHeight, actualMap.Height );
         Assert.AreEqual( RemoveWhiteSpace( expectedMapRepresentation ), RemoveWhiteSpace( actualMap.ToString() ) );
      }

      [TestMethod]
      public void Create_DerivedMapCreatedWithCaveCreationStrategy_ExpectedMap()
      {
         int expectedWidth = 50;
         int expectedHeight = 20;
         IRandom random = new DotNetRandom( 27 );
         IMapCreationStrategy<CaveMap> mapCreationStrategy = new CaveMapCreationStrategy<CaveMap>( expectedWidth, expectedHeight, 45, 3, 2, random );
         string expectedMapRepresentation = @"##################################################
                                              ########...###################.#.#####....########
                                              ######.......##############......#####....########
                                              ####........###############......####......#######
                                              ###.........#################...######........####
                                              ##........#################.##..#####.....########
                                              ###.......#######.#.######..##..#######.#..#######
                                              ####......######.......#........#########..#######
                                              #####.......#....##....###......#########..#######
                                              ######........#####..............#########..######
                                              #..###......########....######...#########..######
                                              #...##......########.....#####.....#.######..#####
                                              #............##########..######......######..#####
                                              #...........########.....######.#....#######..####
                                              ##...........#######.....###################..####
                                              #............#.#####........#################..###
                                              ##.............#####................#########..###
                                              ###.............#####........######...........####
                                              ####.#....#.#.#######.#.#...######################
                                              ##################################################";

         CaveMap actualMap = Map.Create( mapCreationStrategy );
         Trace.Write( actualMap );

         Assert.AreEqual( expectedWidth, actualMap.Width );
         Assert.AreEqual( expectedHeight, actualMap.Height );
         Assert.AreEqual( RemoveWhiteSpace( expectedMapRepresentation ), RemoveWhiteSpace( actualMap.ToString() ) );
      }

      public class CaveMap : Map
      {
      }

      [TestMethod]
      public void Create_MapCreatedWithWidth40Height20BorderOnlyStrategy_ExpectedMap()
      {
         int expectedWidth = 40;
         int expectedHeight = 20;
         IMapCreationStrategy<Map> mapCreationStrategy = new BorderOnlyMapCreationStrategy<Map>( expectedWidth, expectedHeight );

         IMap map = Map.Create( mapCreationStrategy );
         Trace.Write( map );

         Assert.AreEqual( expectedWidth, map.Width );
         Assert.AreEqual( expectedHeight, map.Height );

         for ( int x = 0; x < map.Width; x++ )
         {
            Assert.IsFalse( map.IsTransparent( x, 0 ) );
            Assert.IsFalse( map.IsWalkable( x, 0 ) );
            Assert.IsFalse( map.IsTransparent( x, map.Height - 1 ) );
            Assert.IsFalse( map.IsWalkable( x, map.Height - 1 ) );
         }

         for ( int y = 0; y < map.Height; y++ )
         {
            Assert.IsFalse( map.IsTransparent( 0, y ) );
            Assert.IsFalse( map.IsWalkable( 0, y ) );
            Assert.IsFalse( map.IsTransparent( map.Width - 1, y ) );
            Assert.IsFalse( map.IsWalkable( map.Width - 1, y ) );
         }
      }

      [TestMethod]
      public void Create_MapCreatedWithRandomRoomsMapCreationStrategy_ExpectedMap()
      {
         int expectedWidth = 17;
         int expectedHeight = 10;
         IRandom random = new DotNetRandom( 13 );
         IMapCreationStrategy<Map> mapCreationStrategy = new RandomRoomsMapCreationStrategy<Map>( expectedWidth, expectedHeight, 30, 5, 3, random );
         string expectedMapRepresentation = @"#################
                                              #################
                                              ##...#######...##
                                              ##.............##
                                              ###.###....#...##
                                              ###...##.#####.##
                                              ###...##...###..#
                                              ####............#
                                              ##############..#
                                              #################";

         IMap actualMap = Map.Create( mapCreationStrategy );
         Trace.Write( actualMap );

         Assert.AreEqual( expectedWidth, actualMap.Width );
         Assert.AreEqual( expectedHeight, actualMap.Height );
         Assert.AreEqual( RemoveWhiteSpace( expectedMapRepresentation ), RemoveWhiteSpace( actualMap.ToString() ) );
      }

      [TestMethod]
      public void Create_StringDeserializeMapCreationStrategy_ExpectedMap()
      {
         string expectedMapRepresentation = @"####
                                              #..#
                                              #so#
                                              ####";

         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( expectedMapRepresentation );
         IMap actualMap = Map.Create( mapCreationStrategy );

         Assert.AreEqual( RemoveWhiteSpace( expectedMapRepresentation ), RemoveWhiteSpace( actualMap.ToString() ) );
      }

      [TestMethod]
      public void Clone_SmallMap_MapAndCloneHaveDifferentReferencesButSameValues()
      {
         string mapRepresentation = @"####
                                      #..#
                                      #so#
                                      ####";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap originalMap = Map.Create( mapCreationStrategy );

         IMap clonedMap = originalMap.Clone<Map>();

         Assert.AreNotEqual( originalMap, clonedMap );
         Assert.AreEqual( RemoveWhiteSpace( originalMap.ToString() ), RemoveWhiteSpace( clonedMap.ToString() ) );
      }

      [TestMethod]
      public void Copy_SourceMapWiderThanDestinationMap_ThrowsArgumentException()
      {
         Map sourceMap = new( 10, 10 );
         Map destinationMap = new( 5, 10 );

         try
         {
            destinationMap.Copy( sourceMap, 0, 0 );
         }
         catch ( Exception ex )
         {
            Assert.IsInstanceOfType( ex, typeof( ArgumentException ) );
            return;
         }

         Assert.Fail( "Exception should have been thrown" );
      }

      [TestMethod]
      public void Copy_SourceMapPlusTopLargerThanDestinationMap_ThrowsArgumentException()
      {
         IMap sourceMap = new Map( 10, 10 );
         IMap destinationMap = new Map( 10, 10 );

         try
         {
            sourceMap.Copy( destinationMap, 0, 1 );
         }
         catch ( Exception ex )
         {
            Assert.IsInstanceOfType( ex, typeof( ArgumentException ) );
            return;
         }

         Assert.Fail( "Exception should have been thrown" );
      }

      [TestMethod]
      public void Copy_SmallSourceMapIntoSpecificPostionOfDestinationMap_ExpectedDestinationMap()
      {
         string sourceMapRepresentation = @"..";
         IMapCreationStrategy<Map> sourceMapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( sourceMapRepresentation );
         IMap sourceMap = Map.Create( sourceMapCreationStrategy );
         string destinationMapRepresentation = @"####
                                                 #..#
                                                 #so#
                                                 ####";
         IMapCreationStrategy<Map> destinationMapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( destinationMapRepresentation );
         IMap destinationMap = Map.Create( destinationMapCreationStrategy );
         string expectedRepresentationAfterCopy = @"####
                                                    #..#
                                                    #..#
                                                    ####";

         destinationMap.Copy( sourceMap, 1, 2 );

         Assert.AreEqual( RemoveWhiteSpace( expectedRepresentationAfterCopy.ToString() ), RemoveWhiteSpace( destinationMap.ToString() ) );
      }

      [TestMethod]
      public void GetCellsAlongLine_TopRightToBottomLeft_ExpectedCells()
      {
         string mapRepresentation = @"####
                                      #..#
                                      #so#
                                      ####";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedPath = "#.s#";

         StringBuilder actualPath = new();
         foreach ( ICell cell in map.GetCellsAlongLine( 3, 0, 0, 3 ) )
         {
            actualPath.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedPath, actualPath.ToString() );
      }

      [TestMethod]
      public void GetCellsAlongLine_BottomRightToTopLeft_ExpectedCells()
      {
         string mapRepresentation = @"####
                                      #..#
                                      #so#
                                      ####";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedPath = "#o.#";

         StringBuilder actualPath = new();
         foreach ( ICell cell in map.GetCellsAlongLine( 3, 3, 0, 0 ) )
         {
            actualPath.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedPath, actualPath.ToString() );
      }

      [TestMethod]
      public void GetCellsAlongLine_ThirdRowLeftToRight_ExpectedCells()
      {
         string mapRepresentation = @"####
                                      #..#
                                      #so#
                                      ####";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedPath = "#so#";

         StringBuilder actualPath = new();
         foreach ( ICell cell in map.GetCellsAlongLine( 0, 2, 3, 2 ) )
         {
            actualPath.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedPath, actualPath.ToString() );
      }

      [TestMethod]
      public void GetCellsAlongLine_ThirdRowRightToLeft_ExpectedCells()
      {
         string mapRepresentation = @"####
                                      #..#
                                      #so#
                                      ####";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedPath = "#os#";

         StringBuilder actualPath = new();
         foreach ( ICell cell in map.GetCellsAlongLine( 3, 2, 0, 2 ) )
         {
            actualPath.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedPath, actualPath.ToString() );
      }

      [TestMethod]
      public void GetCellsAlongLine_ToDestinationOutsideMap_TrimsLineAtMapEdgeAndReturnsExpectedCells()
      {
         string mapRepresentation = @"####
                                      #..#
                                      #so#
                                      ####";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedPath = "#.o#";

         StringBuilder actualPath = new();
         foreach ( ICell cell in map.GetCellsAlongLine( 0, 0, 10, 10 ) )
         {
            actualPath.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedPath, actualPath.ToString() );
      }

      [TestMethod]
      public void GetCellsInCircle_SmallMap_ExpectedCells()
      {
         string mapRepresentation = @"#################
                                      #################
                                      ##...#######...##
                                      ##.............##
                                      ###.###....#...##
                                      ###...##.#####.##
                                      ###...##...###..#
                                      ####............#
                                      ##############..#
                                      #################";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedCells = "##########..#####.....##..#..##.#.#.#";

         IEnumerable<ICell> cells = map.GetCellsInCircle( 3, 3, 3 )
            .OrderBy( c => c.X )
            .ThenBy( c => c.Y );
         var actualCells = new StringBuilder();
         foreach ( ICell cell in cells )
         {
            actualCells.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedCells, actualCells.ToString() );
      }

      [TestMethod]
      public void GetBorderCellsInCircle_SmallMap_ExpectedCells()
      {
         string mapRepresentation = @"#################
                                      #################
                                      ##...#######...##
                                      ##.............##
                                      ###.###....#...##
                                      ###...##.#####.##
                                      ###...##...###..#
                                      ####............#
                                      ##############..#
                                      #################";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedCells = "########.#.#.#.#";

         IEnumerable<ICell> cells = map.GetBorderCellsInCircle( 3, 3, 3 )
            .OrderBy( c => c.X )
            .ThenBy( c => c.Y );
         var actualCells = new StringBuilder();
         foreach ( ICell cell in cells )
         {
            actualCells.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedCells, actualCells.ToString() );
      }

      [TestMethod]
      public void GetBorderCellsInCircle_CenteredOnX0Y0InSmallMap_ExpectedCells()
      {
         string mapRepresentation = @"#################
                                      #################
                                      ##...#######...##
                                      ##.............##
                                      ###.###....#...##
                                      ###...##.#####.##
                                      ###...##...###..#
                                      ####............#
                                      ##############..#
                                      #################";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedCells = "##";

         IEnumerable<ICell> cells = map.GetBorderCellsInCircle( 0, 0, 1 )
            .OrderBy( c => c.X )
            .ThenBy( c => c.Y );
         var actualCells = new StringBuilder();
         foreach ( ICell cell in cells )
         {
            actualCells.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedCells, actualCells.ToString() );
      }

      [TestMethod]
      public void GetCellsInDiamond_SmallMap_ExpectedCells()
      {
         string mapRepresentation = @"#################
                                      #################
                                      ##...#######...##
                                      ##.............##
                                      ###.###....#...##
                                      ###...##.#####.##
                                      ###...##...###..#
                                      ####............#
                                      ##############..#
                                      #################";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedCells = "#..##......#.";

         IEnumerable<ICell> cells = map.GetCellsInDiamond( 3, 3, 2 )
            .OrderBy( c => c.X )
            .ThenBy( c => c.Y );
         var actualCells = new StringBuilder();
         foreach ( ICell cell in cells )
         {
            actualCells.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedCells, actualCells.ToString() );
      }

      [TestMethod]
      public void GetBorderCellsInDiamond_SmallMap_ExpectedCells()
      {
         string mapRepresentation = @"#################
                                      #################
                                      ##...#######...##
                                      ##.............##
                                      ###.###....#...##
                                      ###...##.#####.##
                                      ###...##...###..#
                                      ####............#
                                      ##############..#
                                      #################";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedCells = "#.##..#.";

         IEnumerable<ICell> cells = map.GetBorderCellsInDiamond( 3, 3, 2 )
            .OrderBy( c => c.X )
            .ThenBy( c => c.Y );
         var actualCells = new StringBuilder();
         foreach ( ICell cell in cells )
         {
            actualCells.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedCells, actualCells.ToString() );
      }

      [TestMethod]
      public void GetBorderCellsInDiamond_CenteredOnX0Y0InSmallMap_ExpectedCells()
      {
         string mapRepresentation = @"#################
                                      #################
                                      ##...#######...##
                                      ##.............##
                                      ###.###....#...##
                                      ###...##.#####.##
                                      ###...##...###..#
                                      ####............#
                                      ##############..#
                                      #################";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedCells = "##";

         IEnumerable<ICell> cells = map.GetBorderCellsInDiamond( 0, 0, 1 )
            .OrderBy( c => c.X )
            .ThenBy( c => c.Y );
         var actualCells = new StringBuilder();
         foreach ( ICell cell in cells )
         {
            actualCells.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedCells, actualCells.ToString() );
      }

      [TestMethod]
      public void GetCellsInSquare_SmallMap_ExpectedCells()
      {
         string mapRepresentation = @"#################
                                      #################
                                      ##...#######...##
                                      ##.............##
                                      ###.###....#...##
                                      ###...##.#####.##
                                      ###...##...###..#
                                      ####............#
                                      ##############..#
                                      #################";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedCells = "######..###....#..#.##.#.";

         IEnumerable<ICell> cells = map.GetCellsInSquare( 3, 3, 2 )
            .OrderBy( c => c.X )
            .ThenBy( c => c.Y );
         var actualCells = new StringBuilder();
         foreach ( ICell cell in cells )
         {
            actualCells.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedCells, actualCells.ToString() );
      }

      [TestMethod]
      public void GetCellsInRectangle_SmallMap_ExpectedCells()
      {
         string mapRepresentation = @"#################
                                      #################
                                      ##...#######...##
                                      ##.............##
                                      ###.###....#...##
                                      ###...##.#####.##
                                      ###...##...###..#
                                      ####............#
                                      ##############..#
                                      #################";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedCells = "..#.....##.#";

         IEnumerable<ICell> cells = map.GetCellsInRectangle( 2, 2, 4, 3 )
            .OrderBy( c => c.X )
            .ThenBy( c => c.Y );
         var actualCells = new StringBuilder();
         foreach ( ICell cell in cells )
         {
            actualCells.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedCells, actualCells.ToString() );
      }

      [TestMethod]
      public void GetCellsInRectangle_SmallMap_AllCells()
      {
         string mapRepresentation = @"#################
                                      #################
                                      ##...#######...##
                                      ##.............##
                                      ###.###....#...##
                                      ###...##.#####.##
                                      ###...##...###..#
                                      ####............#
                                      ##############..#
                                      #################";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );

         IEnumerable<ICell> cells = map.GetCellsInRectangle( 0, 0, map.Width, map.Height )
            .OrderBy( c => c.Y )
            .ThenBy( c => c.X );
         var actualCells = new StringBuilder();
         foreach ( ICell cell in cells )
         {
            actualCells.Append( cell.ToString() );
         }

         var expectedCells = new StringBuilder()
            .Append( "#################" )
            .Append( "#################" )
            .Append( "##...#######...##" )
            .Append( "##.............##" )
            .Append( "###.###....#...##" )
            .Append( "###...##.#####.##" )
            .Append( "###...##...###..#" )
            .Append( "####............#" )
            .Append( "##############..#" )
            .Append( "#################" )
            .ToString();

         Assert.AreEqual( expectedCells, actualCells.ToString() );
      }

      [TestMethod]
      public void GetBorderCellsInSquare_SmallMap_ExpectedCells()
      {
         string mapRepresentation = @"#################
                                      #################
                                      ##...#######...##
                                      ##.............##
                                      ###.###....#...##
                                      ###...##.#####.##
                                      ###...##...###..#
                                      ####............#
                                      ##############..#
                                      #################";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedCells = "########.#.##.#.";

         IEnumerable<ICell> cells = map.GetBorderCellsInSquare( 3, 3, 2 )
            .OrderBy( c => c.X )
            .ThenBy( c => c.Y );
         var actualCells = new StringBuilder();
         foreach ( ICell cell in cells )
         {
            actualCells.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedCells, actualCells.ToString() );
      }

      [TestMethod]
      public void GetBorderCellsInSquare_CenteredOnX0Y0InSmallMap_ExpectedCellsWithoutCenterCell()
      {
         string mapRepresentation = @"#################
                                      #################
                                      ##...#######...##
                                      ##.............##
                                      ###.###....#...##
                                      ###...##.#####.##
                                      ###...##...###..#
                                      ####............#
                                      ##############..#
                                      #################";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );
         string expectedCells = "###";

         IEnumerable<ICell> cells = map.GetBorderCellsInSquare( 0, 0, 1 )
            .OrderBy( c => c.X )
            .ThenBy( c => c.Y );
         var actualCells = new StringBuilder();
         foreach ( ICell cell in cells )
         {
            actualCells.Append( cell.ToString() );
         }

         Assert.AreEqual( expectedCells, actualCells.ToString() );
      }

      [TestMethod]
      public void GetAdjacentCells_TopLeftCorner_Returns2Cells()
      {
         string mapRepresentation = @"...
                                      ...
                                      ...";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );

         IEnumerable<Cell> cells = map.GetAdjacentCells( 0, 0 );

         Assert.AreEqual( 2, cells.Count() );
      }

      [TestMethod]
      public void GetAdjacentCells_BottomRightCorner_Returns2Cells()
      {
         string mapRepresentation = @"...
                                      ...
                                      ...";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );

         IEnumerable<Cell> cells = map.GetAdjacentCells( 2, 2 );

         Assert.AreEqual( 2, cells.Count() );
      }

      [TestMethod]
      public void GetAdjacentCells_X1Y1_Returns4Cells()
      {
         string mapRepresentation = @"...
                                      ...
                                      ...";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );

         IEnumerable<Cell> cells = map.GetAdjacentCells( 1, 1 );

         Assert.AreEqual( 4, cells.Count() );
      }

      [TestMethod]
      public void GetAdjacentCells_TopLeftCornerWithDiagonals_Returns3Cells()
      {
         string mapRepresentation = @"...
                                      ...
                                      ...";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );

         IEnumerable<Cell> cells = map.GetAdjacentCells( 0, 0, true );

         Assert.AreEqual( 3, cells.Count() );
      }

      [TestMethod]
      public void GetAdjacentCells_BottomRightCornerWithDiagonals_Returns3Cells()
      {
         string mapRepresentation = @"...
                                      ...
                                      ...";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );

         IEnumerable<Cell> cells = map.GetAdjacentCells( 2, 2, true );

         Assert.AreEqual( 3, cells.Count() );
      }

      [TestMethod]
      public void GetAdjacentCells_X1Y1WithDiagonals_Returns8Cells()
      {
         string mapRepresentation = @"...
                                      ...
                                      ...";
         IMapCreationStrategy<Map> mapCreationStrategy = new StringDeserializeMapCreationStrategy<Map>( mapRepresentation );
         IMap map = Map.Create( mapCreationStrategy );

         IEnumerable<Cell> cells = map.GetAdjacentCells( 1, 1, true );

         Assert.AreEqual( 8, cells.Count() );
      }

      private static string RemoveWhiteSpace( string source ) => MyRegex().Replace( source, string.Empty );

      [GeneratedRegex( @"\s+" )]
      private static partial Regex MyRegex();
   }
}
