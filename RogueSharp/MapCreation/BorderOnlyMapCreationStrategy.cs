namespace RogueSharp.MapCreation
{
   /// <summary>
   /// The BorderOnlyMapCreationStrategy creates a Map of the specified type by making an empty map with only the outermost border being solid walls
   /// </summary>
   /// <typeparam name="TMap">The type of IMap that will be created</typeparam>
   /// <remarks>
   /// Constructs a new BorderOnlyMapCreationStrategy with the specified parameters
   /// </remarks>
   /// <param name="width">The width of the Map to be created</param>
   /// <param name="height">The height of the Map to be created</param>
   public class BorderOnlyMapCreationStrategy<TMap>( int width, int height ) : BorderOnlyMapCreationStrategy<TMap, Cell>( width, height ), IMapCreationStrategy<TMap> where TMap : IMap<Cell>, new()
   {
   }

   /// <summary>
   /// The BorderOnlyMapCreationStrategy creates a Map of the specified type by making an empty map with only the outermost border being solid walls
   /// </summary>
   /// <typeparam name="TMap">The type of IMap that will be created</typeparam>
   /// <typeparam name="TCell">The type of ICell that the Map will use</typeparam>
   /// <remarks>
   /// Constructs a new BorderOnlyMapCreationStrategy with the specified parameters
   /// </remarks>
   /// <param name="width">The width of the Map to be created</param>
   /// <param name="height">The height of the Map to be created</param>
   public class BorderOnlyMapCreationStrategy<TMap, TCell>( int width, int height ) : IMapCreationStrategy<TMap, TCell> where TMap : IMap<TCell>, new() where TCell : ICell
   {
      private readonly int _height = height;
      private readonly int _width = width;

      /// <summary>
      /// Creates a Map of the specified type by making an empty map with only the outermost border being solid walls
      /// </summary>
      /// <returns>An IMap of the specified type</returns>
      public TMap CreateMap()
      {
         var map = new TMap();
         map.Initialize( _width, _height );
         map.Clear( true, true );

         foreach ( TCell cell in map.GetCellsInRows( 0, _height - 1 ) )
         {
            map.SetCellProperties( cell.X, cell.Y, false, false );
         }

         foreach ( TCell cell in map.GetCellsInColumns( 0, _width - 1 ) )
         {
            map.SetCellProperties( cell.X, cell.Y, false, false );
         }

         return map;
      }
   }
}