var input = File.ReadAllLines(args.FirstOrDefault() ?? "input.txt");

var parts = input.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();
int length = parts[0].Length;
var problems = Enumerable.Range(0, length).Select(idx => parts.Select(p => p[idx]).ToArray())
    .Select(raw => new Problem(raw)).ToArray();

var sum = problems.Sum(p => p.Answer);
WriteLine(sum);

// Part 2 
var part2Problems = GetPart2Problems(input).ToArray();
part2Problems.DumpConsole();
WriteLine(part2Problems.Sum(p => p.Answer));
IEnumerable<ProblemPart2> GetPart2Problems(string[] data)
{
    var digitPositions = data.Take(data.Length-1).ToArray();
    List<long> values = new List<long>();
    for (int n = data[0].Length - 1; n >= 0; n--)
    {
        var str = digitPositions.Select(line => line[n]).ToArray();
        if (str.All(ch => ch == ' '))
            continue;
        var num = new string(str).ToLong();
        values.Add(num);
        if (data.Last()[n] != ' ')
        {
            var op = data.Last()[n];
            yield return new ProblemPart2(values.ToArray(), op.ToString());
            values = new List<long>();
        }
    }
}

record Problem(string[] Raw)
{
    private Func<long, long, long> Operand => Raw.Last() switch
    {
        "+" => (x, y) => x + y,
        "*" => (x, y) => x * y,
        _ => throw new InvalidOperationException($"Unknown operand {Raw.Last()}")
    };

    private long[] Input => Raw.Take(Raw.Length - 1).Select(long.Parse).ToArray();

    public long Answer => Input.Aggregate(Operand);
}

record ProblemPart2(long[] Numbers, string Operator)
{
    private Func<long, long, long> Operand => Operator switch
    {
        "+" => (x, y) => x + y,
        "*" => (x, y) => x * y,
        _ => throw new InvalidOperationException($"Unknown operand {Operator}")
    };
    
    public long Answer => Numbers.Aggregate(Operand);
}