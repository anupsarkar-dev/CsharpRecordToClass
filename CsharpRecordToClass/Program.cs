using System.Text.RegularExpressions;
using System.Text;

string recordDeclaration = @"""
 public record ImeiDetailsDto
	(
	    int Id,
	    string? DeviceType,
	    DateTime AllocationDate,
	    DateTime RegisterDate,
	    string Manufacturer,
	    string Brand,
	    string Model,
	    short? SimSlot,
	    short? ImeiQuantitySupport,
	    string? NonRemovableEuicc,
	    bool Blocked,
	    bool Counterfeit,
	) ;
 """;


Console.WriteLine(RecortToClass(recordDeclaration));

 string RecortToClass(string input)
{
    string recordNamePattern = @"\s+record\s+(\w+)";
    string propertyPattern = @"\s+(\w+\??)\s+(\w+)\s*[,]?\s*$";

    var result = new StringBuilder();

    Match recordNameMatch = Regex.Match(input, recordNamePattern);
    if (recordNameMatch.Success && recordNameMatch.Groups.Count == 2)
    {
        string recordName = recordNameMatch.Groups[1].Value;
        result.Append($"public class {recordName}");

    }

    // Extract properties
    MatchCollection matches = Regex.Matches(input, propertyPattern, RegexOptions.Multiline);

    result.Append($"\n {{");
    foreach (Match match in matches)
    {

        string type = match.Groups[1].Value; // property type
        string name = match.Groups[2].Value; // property name

        if (type.ToLower() != "record")
            result.Append($"\n \t public {type} {name} {{ get; set; }}");
    }

    result.Append($"\n}}");
    return result.ToString();
}