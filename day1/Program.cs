// Normally we will start by reading lines from an input file
var input = File.ReadAllLines(args.FirstOrDefault() ?? "input.txt");

int dial = 50;
int code = 0;
int codeP2 = 0;
var movements = input.Select(line => Convert.ToInt32(line.Replace('R', '+').Replace('L', '-')));
bool test = args.FirstOrDefault()?.StartsWith("test") ?? false;
foreach (var move in movements)
{
    int prev = dial;
    dial = dial + move;
    if (dial > 100 || dial < 0)
    {
        int n = dial;// - prev;
        int add = Math.Abs(n) / 100 + (dial < 0 && prev > 0 ? 1 : 0);
        if (dial % 100 == 0) add -= 1;
        if (test)
        {
            WriteLine($"Dial moved from {prev} to {dial}, passing 0 {add} times.");
        }
        codeP2 += add;
    }

    dial %= 100;
    if (dial < 0)
        dial += 100;
    if (dial == 0)
    {
        code++;
        codeP2++;
    }
}
WriteLine($"Code after movements: {code}");
WriteLine($"Code after movements part 2: {codeP2}");