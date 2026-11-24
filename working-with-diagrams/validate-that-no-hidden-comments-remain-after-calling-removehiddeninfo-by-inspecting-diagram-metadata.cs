using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Determine output path (optional second argument or default).
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "_cleaned.vsdx");

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Remove hidden information (shapes and masters as an example).
                diagram.RemoveHiddenInformation((int)(RemoveHiddenInfoItem.Shapes | RemoveHiddenInfoItem.Masters));

                // Validate that no comments (annotations) remain in the diagram.
                bool hasComments = false;
                foreach (Page page in diagram.Pages)
                {
                    // Access page-level annotations (comments).
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // If any annotation exists, consider it a remaining comment.
                        hasComments = true;
                        // Optionally, output details for debugging.
                        Console.WriteLine($"Found comment on page '{page.Name ?? "Unnamed"}' by reviewer ID {annotation.ReviewerID?.Value ?? 0}.");
                    }
                }

                if (hasComments)
                {
                    // Validation failed: hidden comments still present.
                    throw new Exception("Validation failed: hidden comments are still present after removing hidden information.");
                }
                else
                {
                    Console.WriteLine("Validation succeeded: no comments remain after removing hidden information.");
                }

                // Save the cleaned diagram to the output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Cleaned diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}