

namespace PrintExecutionService.Services
{
  public class PrintIdIdentifier
  {
    public List<int> GetPrintIds(string args)
    {
      var input = args.Split(" ").ToList();
      var ints = input.Select(t => int.Parse(t)).ToList<int>();
      var values = new List<int>();
      if (ints.Count == 1 && ints.Any(i => i >= 100000))
      {
        return new List<int>() { ints.First() };
      }
      else if (ints.Count > 1 && ints.Any(i => i < 100000))
      {
        var loopCount = ints.First(i => i < 100000);
        var MinimumId = ints.First(i => i >= 100000);
        for (var i = 0; i < loopCount; i++)
        {
          values.Add(MinimumId);
          MinimumId++;
        }
        return values;
      }
      else if (ints.Count > 1 && ints.All(i => i >= 100000))
      {
        foreach (var i in ints)
        {
          values.Add(i);
        }
        return values;
      }
      else
      {
        return values;
      }
    }
  }
}