// Normally we will start by reading lines from an input file
var input = File.ReadAllLines(args.FirstOrDefault() ?? "input.txt");

var width = input[0].Length;
char[] border = new string('.', width + 2).ToArray();
var borderedMap = input.Select(line => $".{line}.".ToArray())
    .Prepend(border)
    .Append(border)
    .ToArray();

// Part 1
int count = RemoveRolls(borderedMap);
WriteLine($"Free rolls: {count}");

// Part 2
int sum = count;
while (count > 0)
{
    count = RemoveRolls(borderedMap);
    sum += count;
}
WriteLine($"Total removed rolls: {sum}");

int RemoveRolls(char[][] map)
{
    int count = 0;
    for (int y = 1; y < map.Length - 1; y++)
    {
        for(int x = 1 ; x < map[y].Length - 1; x++)
        {
            char current = map[y][x];
            if (current == '@')
            {
                char[] neighbors = [
                    map[y-1][x - 1], map[y-1][x], map[y-1][x + 1],
                    map[y][x - 1],                map[y][x + 1],
                    map[y+1][x - 1], map[y+1][x], map[y+1][x + 1]];
                int rollCount = neighbors.Count(ch => ch == '@');
                if (rollCount < 4)
                {
                    map[y][x] = 'x';
                    count++;    
                }
            }
        }
    }

    return count;
}