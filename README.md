# CsharpRecordToClass
Convert your C# record type to class type


Often while creating POCO or DTO type , we use C# record or class type.
Many times we have to manually convert C# class to record type or vice versa which is time consuming.

Lets automate this:

for example we have a record type:
Now we want to convert it to a class or vice versa

Here is the simple C# program to do it:

```
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
```
