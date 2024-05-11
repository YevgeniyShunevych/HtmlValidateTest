using Atata.Cli.HtmlValidate;
using Atata.HtmlValidation;

string testFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "reddit-frontpage.html");

RunHtmlValidator();
Console.WriteLine();
RunHtmlValidateCli();

void RunHtmlValidator()
{
    Console.WriteLine("Use HtmlValidator");

    HtmlValidator validator = new(
        new HtmlValidationOptions
        {
            OutputFormatter = HtmlValidateFormatter.Names.Json,
            ResultFileFormatter = HtmlValidateFormatter.Names.Json,
            SaveResultToFile = true,
            WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HtmlValidator-output")
        });

    string testHtml = File.ReadAllText(testFilePath);

    var validationResult = validator.Validate(testHtml);

    Console.WriteLine($"Output file: {Path.GetFileName(validationResult.ResultFilePath)}");
    Console.WriteLine($"Length of output: {validationResult.Output.Length:N0}");
}

void RunHtmlValidateCli()
{
    Console.WriteLine("Use HtmlValidateCli");

    string cliWorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HtmlValidateCli-output");
    if (!Directory.Exists(cliWorkingDirectory))
        Directory.CreateDirectory(cliWorkingDirectory);

    HtmlValidateCli cli = new()
    {
        WorkingDirectory = cliWorkingDirectory,
    };

    string cliOutputFileName = $"{Guid.NewGuid()}.json";

    cli.Validate(
        testFilePath,
        new HtmlValidateOptions
        {
            Formatter = HtmlValidateFormatter.Json(cliOutputFileName)
        });

    FileInfo cliOutputFile = new(Path.Combine(cliWorkingDirectory, cliOutputFileName));

    Console.WriteLine($"Output file: {cliOutputFileName}");
    Console.WriteLine($"Length of output: {cliOutputFile.Length:N0}");
}
