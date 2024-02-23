namespace RogueSharp.Algorithms
{
   /// <summary>
   /// The DirectedEdge class represents a weighted edge in an edge-weighted directed graph.
   /// </summary>
   /// <seealso href="http://algs4.cs.princeton.edu/44sp/DirectedEdge.java.html">DirectedEdge class from Princeton University's Java Algorithms</seealso>
   /// <remarks>
   /// Constructs a directed edge from one specified vertex to another with the given weight
   /// </remarks>
   /// <param name="from">The start vertex</param>
   /// <param name="to">The destination vertex</param>
   /// <param name="weight">The weight of the DirectedEdge</param>
   public class DirectedEdge( int from, int to, double weight )
   {

      /// <summary>
      /// Returns the destination vertex of the DirectedEdge
      /// </summary>
      public int From { get; set; } = from;

      /// <summary>
      /// Returns the start vertex of the DirectedEdge
      /// </summary>
      public int To { get; set; } = to;

      /// <summary>
      /// Returns the weight of the DirectedEdge
      /// </summary>
      public double Weight { get; set; } = weight;

      /// <summary>
      /// Returns a string that represents the current DirectedEdge
      /// </summary>
      /// <returns>
      /// A string that represents the current DirectedEdge
      /// </returns>
      public override string ToString() => $"From: {From}, To: {To}, Weight: {Weight}";
   }
}