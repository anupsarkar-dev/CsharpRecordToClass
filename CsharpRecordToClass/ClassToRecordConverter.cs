string classDeclaration = """
 public class StaticRouteDto
 {
     public int Id { get; set; }

     public string Value { get; set; }  

     public string RouteType { get; set; }

     public string FilterType { get; set; }

     public string ResponseType { get; set; }

     public string? ResponseTypeValue { get; set; }
 }


""";


string classNamePattern = @"\s+class\s+(\w+)";
string propertyPattern = @"\s+(public|private|protected|internal)\s+([\w<>,\s]+\??)\s+(\w+)\s*{\s*get;\s*set;\s*}";

// Extract class name
Match classNameMatch = Regex.Match(classDeclaration, classNamePattern);
if (classNameMatch.Success && classNameMatch.Groups.Count == 2)
{
    string className = classNameMatch.Groups[1].Value;

    StringBuilder recordBuilder = new StringBuilder();
    recordBuilder.AppendLine($"public record {className}");

    // Extract properties
    MatchCollection propertyMatches = Regex.Matches(classDeclaration, propertyPattern);
    if (propertyMatches.Count > 0)
    {
        recordBuilder.AppendLine("(");

        foreach (Match match in propertyMatches)
        {
            if (match.Groups.Count == 4)
            {
                string type = match.Groups[2].Value;
                string name = match.Groups[3].Value;

                recordBuilder.AppendLine($"    {type} {name},");
            }
        }

        // Remove the trailing comma from the last property
        recordBuilder.Remove(recordBuilder.Length - 3, 1);
        recordBuilder.AppendLine(");");
    }
    else
    {
        recordBuilder.AppendLine(";");
    }

    Console.WriteLine(recordBuilder.ToString());
}
 
