using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Access the annotations (comments) collection via PageSheet
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Ensure the comment text is not null
                    string commentText = annotation.Comment?.Value ?? string.Empty;

                    // Criteria: comment contains the word "TODO"
                    if (commentText.Contains("TODO", StringComparison.OrdinalIgnoreCase))
                    {
                        // Apply a custom tag by prefixing the comment text
                        // (Tag class does not exist, so we embed the tag in the text)
                        annotation.Comment.Value = "[Review] " + commentText;
                    }
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}