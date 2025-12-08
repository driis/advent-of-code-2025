// Normally we will start by reading lines from an input file
var input = File.ReadAllLines(args.FirstOrDefault() ?? "input.txt")
    .Select(line => line.ToArray()).ToArray();

long splits = 0;
for(int i = 0 ; i < input.Length - 1; i++)
{
    var line = input[i];
    var next = input[i+1];
    var beams = line.Index().Where(x => x.Item == 'S' || x.Item == '|').ToArray();
    foreach (var beam in beams)
    {
        if (next[beam.Index] == '^')
        {
            next[beam.Index - 1] = next[beam.Index + 1] = '|';
            splits++;
        }
        else
        {
            next[beam.Index] = '|';
        }
    }
}

WriteLine($"Number of splits: {splits}");

// Part 2
input = File.ReadAllLines(args.FirstOrDefault() ?? "input.txt")
    .Select(line => line.ToArray()).ToArray();

var start = input[0].Index().Single(x => x.Item == 'S');
WriteLine("Starting position: " + start.Index);
var timelines = CountTimelines(1, start.Index, input);
WriteLine($"Number of timelines: {timelines}");


long CountTimelines(int i, int pos, char[][] grid)
{
    if (i < grid.Length)
    {
        if (grid[i][pos] == '^')
        {
            return CountTimelines(i + 1, pos - 1, grid) +
                   CountTimelines(i + 1, pos + 1, grid);
        }

        return CountTimelines(i + 1, pos, grid);
    }
    return 1;
}  