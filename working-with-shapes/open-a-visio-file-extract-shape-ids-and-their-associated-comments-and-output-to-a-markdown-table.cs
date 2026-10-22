using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputMarkdownFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : null;

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare a StringBuilder for the markdown table.
            StringBuilder mdBuilder = new StringBuilder();
            mdBuilder.AppendLine("| Shape ID | Comment |");
            mdBuilder.AppendLine("|---|---|");

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Access page‑level annotations (comments).
                var annotations = page.PageSheet.Annotations;
                if (annotations == null) continue;

                // Enumerate each annotation.
                foreach (Annotation ann in annotations)
                {
                    // ShapeID links the comment to a shape.
                    int shapeId = ann.ShapeID;

                    // Retrieve the comment text; handle possible nulls.
                    string comment = ann.Comment?.Value ?? string.Empty;

                    // Escape markdown‑sensitive characters.
                    comment = comment.Replace("|", "\\|")
                                     .Replace("\r\n", "<br>")
                                     .Replace("\n", "<br>")
                                     .Replace("\r", "<br>");

                    // Append a row to the markdown table.
                    mdBuilder.AppendLine($"| {shapeId} | {comment} |");
                }
            }

            // Output the markdown either to a file or to the console.
            if (!string.IsNullOrEmpty(outputPath))
            {
                // Ensure the directory for the output file exists.
                string dir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.WriteAllText(outputPath, mdBuilder.ToString());
                Console.WriteLine($"Markdown table written to \"{outputPath}\".");
            }
            else
            {
                Console.WriteLine(mdBuilder.ToString());
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}