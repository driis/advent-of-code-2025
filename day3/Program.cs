// Normally we will start by reading lines from an input file
var input = File.ReadAllLines(args.FirstOrDefault() ?? "input.txt");
bool test = args.FirstOrDefault()?.StartsWith("test") ?? false;
var sum = input.Select(bank => Joltage(bank,2)).Sum();
WriteLine(sum);

var sumPart2 = input.Select(bank => Joltage(bank, 12)).Sum();
WriteLine(sumPart2);

long Joltage(string bank, int length)
{
    var chars = JoltageRecur(bank, length);
    return new string(chars).ToLong();
}

char[] JoltageRecur(string bank, int length)
{
    if (length == 0)
        return [];
    var nextCh = bank[..^(length-1)].Max();
    var nextIndex = bank.IndexOf(nextCh) + 1;
    var remaining = bank[nextIndex..];
    char[] next = [nextCh];
    return next.Concat(JoltageRecur(remaining, length - 1)).ToArray();
}