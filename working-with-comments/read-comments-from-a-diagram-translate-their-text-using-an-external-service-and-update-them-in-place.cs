using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Access the page-level annotations (comments).
                    var annotations = page.PageSheet?.Annotations;
                    if (annotations == null) continue;

                    // Process each annotation.
                    foreach (Annotation annotation in annotations)
                    {
                        // Retrieve the existing comment text.
                        string originalText = annotation.Comment?.Value ?? string.Empty;

                        // Skip empty comments.
                        if (string.IsNullOrWhiteSpace(originalText))
                            continue;

                        // Translate the comment text using an external service (simulated here).
                        string translatedText = TranslateText(originalText);

                        // Update the annotation with the translated text.
                        annotation.Comment.Value = translatedText;
                    }
                }

                // Save the updated diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram comments translated and saved successfully.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Simulates translation of text via an external service.
    /// Replace this stub with actual HTTP calls to a translation API if needed.
    /// </summary>
    /// <param name="text">The original text to translate.</param>
    /// <returns>The translated text.</returns>
    private static string TranslateText(string text)
    {
        // Placeholder translation logic.
        // In a real scenario, you might use HttpClient to call a translation API.
        return $"{text} (translated)";
    }
}