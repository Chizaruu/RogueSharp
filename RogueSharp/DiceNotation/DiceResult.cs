using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RogueSharp.Random;

namespace RogueSharp.DiceNotation
{
   /// <summary>
   /// The DiceResult class represents the result of rolling a DiceExpression
   /// </summary>
   /// <remarks>
   /// Construct a new DiceResult from the specified values
   /// </remarks>
   /// <param name="results">An IEnumerable of TermResult that represents one result for each DiceTerm in the DiceExpression</param>
   /// <param name="randomUsed">The random number generator used to get this result</param>
   public class DiceResult( IEnumerable<TermResult> results, IRandom randomUsed )
   {
      /// <summary>
      /// The random number generator used to get this result
      /// </summary>
      public IRandom RandomUsed { get; private set; } = randomUsed;

      /// <summary>
      /// A Collection of TermResults that represents one result for each DiceTerm in the DiceExpression
      /// </summary>
      public ReadOnlyCollection<TermResult> Results { get; private set; } = new ReadOnlyCollection<TermResult>( results.ToList() );

      /// <summary>
      /// The total result of the the roll
      /// </summary>
      public int Value { get; private set; } = results.Sum( r => r.Value * r.Scalar );
   }
}