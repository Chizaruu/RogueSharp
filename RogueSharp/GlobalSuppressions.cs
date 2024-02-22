// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the
// Code Analysis results, point to "Suppress Message", and click
// "In Suppression File".
// You do not need to add suppressions to this file manually.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage( "Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "~M:RogueSharp.Rectangle.Intersects(RogueSharp.Rectangle@,System.Boolean@)" )]
[assembly: SuppressMessage( "Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "~M:RogueSharp.Rectangle.Intersect(RogueSharp.Rectangle@,RogueSharp.Rectangle@,RogueSharp.Rectangle@)" )]
[assembly: SuppressMessage( "Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member", Target = "~M:RogueSharp.Rectangle.Intersect(RogueSharp.Rectangle@,RogueSharp.Rectangle@,RogueSharp.Rectangle@)" )]
[assembly: SuppressMessage( "Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "~M:RogueSharp.Rectangle.Union(RogueSharp.Rectangle@,RogueSharp.Rectangle@,RogueSharp.Rectangle@)" )]
[assembly: SuppressMessage( "Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member", Target = "~M:RogueSharp.Rectangle.Union(RogueSharp.Rectangle@,RogueSharp.Rectangle@,RogueSharp.Rectangle@)" )]
[assembly: SuppressMessage( "Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "~F:RogueSharp.Random.Singleton.DefaultRandom" )]
[assembly: SuppressMessage( "Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", MessageId = "maxValue+1", Scope = "member", Target = "~M:RogueSharp.Random.DotNetRandom.Next(System.Int32,System.Int32)~System.Int32" )]
[assembly: SuppressMessage( "Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "~N:RogueSharp.DiceNotation.Terms" )]
[assembly: SuppressMessage( "Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "~N:RogueSharp.DiceNotation.Exceptions" )]
[assembly: SuppressMessage( "Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Next", Scope = "member", Target = "~M:RogueSharp.Random.IRandom.Next(System.Int32,System.Int32)~System.Int32" )]
[assembly: SuppressMessage( "Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Next", Scope = "member", Target = "~M:RogueSharp.Random.IRandom.Next(System.Int32)~System.Int32" )]
[assembly: SuppressMessage( "Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "~P:RogueSharp.Random.RandomState.Seed" )]
[assembly: SuppressMessage( "Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "~P:RogueSharp.MapState.Cells" )]
[assembly: SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Scope = "member", Target = "~M:RogueSharp.Rectangle.Union(RogueSharp.Rectangle@,RogueSharp.Rectangle@,RogueSharp.Rectangle@)" )]
[assembly: SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Scope = "member", Target = "~M:RogueSharp.Rectangle.Intersects(RogueSharp.Rectangle@,System.Boolean@)" )]
[assembly: SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "~M:RogueSharp.Rectangle.Intersects(RogueSharp.Rectangle@,System.Boolean@)" )]
[assembly: SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Scope = "member", Target = "~M:RogueSharp.Rectangle.Intersect(RogueSharp.Rectangle@,RogueSharp.Rectangle@,RogueSharp.Rectangle@)" )]
[assembly: SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Scope = "member", Target = "~M:RogueSharp.Rectangle.Intersect(RogueSharp.Rectangle@,RogueSharp.Rectangle@,RogueSharp.Rectangle@)" )]
[assembly: SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Scope = "member", Target = "~M:RogueSharp.Rectangle.Intersect(RogueSharp.Rectangle@,RogueSharp.Rectangle@,RogueSharp.Rectangle@)" )]
[assembly: SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Scope = "member", Target = "~M:RogueSharp.Rectangle.Union(RogueSharp.Rectangle@,RogueSharp.Rectangle@,RogueSharp.Rectangle@)" )]
[assembly: SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Scope = "member", Target = "~M:RogueSharp.Rectangle.Union(RogueSharp.Rectangle@,RogueSharp.Rectangle@,RogueSharp.Rectangle@)" )]
[assembly: SuppressMessage( "Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Scope = "type", Target = "~T:RogueSharp.Algorithms.IndexMinPriorityQueue`1" )]
[assembly: SuppressMessage( "Microsoft.Usage", "CA2243:AttributeStringLiteralsShouldParseCorrectly" )]
[assembly: SuppressMessage( "Usage", "CA2237:Mark ISerializable types with serializable", Justification = "Not available in .NET Standard 1.0", Scope = "type", Target = "~T:RogueSharp.DiceNotation.Exceptions.ImpossibleDieException" )]
[assembly: SuppressMessage( "Usage", "CA2237:Mark ISerializable types with serializable", Justification = "Not available in .NET Standard 1.0", Scope = "type", Target = "~T:RogueSharp.DiceNotation.Exceptions.InvalidChooseException" )]
[assembly: SuppressMessage( "Usage", "CA2237:Mark ISerializable types with serializable", Justification = "Not available in .NET Standard 1.0", Scope = "type", Target = "~T:RogueSharp.DiceNotation.Exceptions.InvalidMultiplicityException" )]
[assembly: SuppressMessage( "Usage", "CA2237:Mark ISerializable types with serializable", Justification = "Not available in .NET Standard 1.0", Scope = "type", Target = "~T:RogueSharp.NoMoreStepsException" )]
[assembly: SuppressMessage( "Usage", "CA2237:Mark ISerializable types with serializable", Justification = "Not available in .NET Standard 1.0", Scope = "type", Target = "~T:RogueSharp.PathNotFoundException" )]
[assembly: SuppressMessage( "Globalization", "CA1304:Specify CultureInfo", Justification = "Not available in .NET Standard 1.0", Scope = "member", Target = "~M:RogueSharp.DiceNotation.DiceParser.Parse(System.String)~RogueSharp.DiceNotation.DiceExpression" )]
[assembly: SuppressMessage( "Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "<Pending>", Scope = "member", Target = "~M:RogueSharp.Map.#ctor(System.Int32,System.Int32)" )]
[assembly: SuppressMessage( "Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Does not waste space", Scope = "member", Target = "~F:RogueSharp.GoalMap`1._cellWeights" )]
[assembly: SuppressMessage( "Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Does not waste space", Scope = "member", Target = "~M:RogueSharp.GoalMap`1.#ctor(RogueSharp.IMap{`0},System.Boolean)" )]
[assembly: SuppressMessage( "Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Does not waste space", Scope = "member", Target = "~M:RogueSharp.Map`1.Init(System.Int32,System.Int32)" )]
[assembly: SuppressMessage( "Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Does not waste space", Scope = "member", Target = "~F:RogueSharp.Map`1._cells" )]