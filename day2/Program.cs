using System.Net.NetworkInformation;

var input = File.ReadAllLines(args.FirstOrDefault() ?? "input.txt").Single();
var rangeStrings = input.Split(',');
var ranges = rangeStrings.Select(rs =>
{
    var parts = rs.Split('-');
    return (Start: Convert.ToInt64(parts[0]), End: Convert.ToInt64(parts[1]));
}).ToArray(); 

var valuesToCheck = ranges.SelectMany(r => ExplodeRange(r.Start, r.End)).ToArray();
var silly = valuesToCheck.Where(IsRepeated);
WriteLine($"Sum of silly codes: {silly.Sum()}");

var sillyPart2 = valuesToCheck.Where(IsRepeatedAnyLength);
WriteLine($"Sum of silly codes part 2: {sillyPart2.Sum()}");

IEnumerable<long> ExplodeRange(long begin, long end)
{
    for(long n = begin; n <= end ; n++)
        yield return n;
}

bool IsRepeated(long n) 
{
    string str = n.ToString();
    var part1 = str[..(str.Length / 2)];
    var part2 = str[(str.Length / 2)..];
    return part1 == part2;
}

bool IsRepeatedAnyLength(long n)
{
    string str = n.ToString();
    int lengthMax = str.Length / 2;
    for(int length = 1; length <= lengthMax; length++)
    {
        if (str.Length % length != 0)
            continue;
        var parts = Enumerable.Range(0, str.Length / length)
            .Select(i => str.Substring(i * length, length))
            .ToArray();

        if (parts.All(p => p == parts[0]))
            return true;
    }

    return false;
}