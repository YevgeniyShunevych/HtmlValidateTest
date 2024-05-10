using Atata.Cli.HtmlValidate;
using Atata.HtmlValidation;

namespace HtmlValidateTest;

class Program
{
    static void Main(string[] args)
    {
        var validator = new HtmlValidator(new HtmlValidationOptions
        {
            OutputFormatter = HtmlValidateFormatter.Names.Json,
            ResultFileFormatter = HtmlValidateFormatter.Names.Json,
            SaveResultToFile = true,
            WorkingDirectory = "output"
        });
        // read the reddit-frontpage.html file into a string
        string html = File.ReadAllText("reddit-frontpage.html");

        // These are just to show that the html file was read correctly
        Console.WriteLine(html.Substring(0, 10));
        Console.WriteLine(html.Substring(html.Length - 10));

        var validationResult = validator.Validate(html);

        // These are just to show that the validation result was cut off.
        Console.WriteLine(validationResult.Output.Substring(0, 10));
        Console.WriteLine(validationResult.Output.Substring(validationResult.Output.Length - 10));

        Console.WriteLine($"Length of output: {validationResult.Output.Length}");
    }
}
