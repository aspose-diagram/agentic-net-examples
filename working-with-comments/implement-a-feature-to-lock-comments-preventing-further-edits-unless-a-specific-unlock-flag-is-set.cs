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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> [unlock]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        bool unlockFlag = args.Length > 2 && args[2].Equals("unlock", StringComparison.OrdinalIgnoreCase);

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            Page page;
            if (diagram.Pages.Count > 0)
            {
                page = diagram.Pages[0];
            }
            else
            {
                // Create a new page and add it to the diagram
                page = new Page();
                diagram.Pages.Add(page);
            }

            // Add a comment if none exist on the page
            if (page.PageSheet.Annotations.Count == 0)
            {
                // Add a comment at arbitrary coordinates (e.g., 2.0, 2.0)
                page.AddComment(2.0, 2.0, "Initial comment (locked)");
                Console.WriteLine("Added initial comment to the diagram.");
            }

            // Process comment editing based on the unlock flag
            foreach (Annotation annotation in page.PageSheet.Annotations)
            {
                // If unlock flag is not set, treat comment as locked and skip editing
                if (!unlockFlag)
                {
                    Console.WriteLine($"Comment ID {annotation.MarkerIndex.Value} is locked. Skipping edit.");
                    continue;
                }

                // Unlock is active: perform edit
                string oldText = annotation.Comment.Value;
                string newText = "Edited comment after unlock";
                annotation.Comment.Value = newText;
                Console.WriteLine($"Edited comment ID {annotation.MarkerIndex.Value} from \"{oldText}\" to \"{newText}\".");

                // For demonstration, edit only the first comment
                break;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or runtime errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}