// Normally we will start by reading lines from an input file
var input = File.ReadAllLines(args.FirstOrDefault() ?? "input.txt");
var freshRanges = input.TakeWhile(x => x != "")
    .Select(x =>
    {
        var parts = x.Split('-');
        return new Range(parts[0].ToLong(), parts[1].ToLong());
    }).ToArray();
WriteLine($"Built fresh ingredients list. {freshRanges.Length}");
var ingredients = input.SkipWhile(x => x != "").Skip(1).Select(x => x.ToLong());
var freshCount = ingredients.Count(x => freshRanges.Any(s => s.Contains(x)));
WriteLine($"Fresh ingredients: {freshCount}");

var mergedRanges = new List<Range>();
foreach (var range in freshRanges)
{
    var fixedRange = range;
    var overlap = mergedRanges.Where(x => x.Overlaps(range)).ToArray();
    mergedRanges = mergedRanges.Except(overlap).ToList();
    foreach (var ol in overlap)
    {
        fixedRange = fixedRange.Combine(ol);
    }
    
    mergedRanges.Add(fixedRange);
}

mergedRanges = mergedRanges.OrderBy(x =>x.Start).ToList();
var prev = mergedRanges.First();
foreach(Range r in mergedRanges.OrderBy(x => x.Start))
{
    WriteLine($"{r.Start,20} ->{r.End,20} ({r.IncludedIdCount,20}). Distance: {prev.End - r.Start}");
    prev = r;
}

long freshTotal = mergedRanges.Sum(m => m.IncludedIdCount);
WriteLine($"Merged ranges: {mergedRanges.Count}");
WriteLine($"Sum of ingredients included {freshTotal}");


record Range(long Start, long End)
{
    public long IncludedIdCount => (End - Start) + 1;

    public bool Contains(long ingredient) => ingredient >= Start && ingredient <= End;

    public bool Overlaps(Range other)
    {
        return other.Contains(End) || other.Contains(Start);
    }

    public Range Combine(Range other)
    {
        // this is fully contained in other
        if (Start >= other.Start && End <= other.End)
            return other;

        // other is fully contained in this
        if (Start <= other.Start && End >= other.End)
            return this;
        
        return new Range(Math.Min(Start, other.Start), Math.Max(End, other.End));
    }
}