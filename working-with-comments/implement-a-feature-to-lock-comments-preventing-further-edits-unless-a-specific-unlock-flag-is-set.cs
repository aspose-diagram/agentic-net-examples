using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram path, output diagram path, optional unlock flag.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> [--unlock]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        bool unlockFlag = false;

        // Detect unlock flag.
        for (int i = 2; i < args.Length; i++)
        {
            if (args[i].Equals("--unlock", StringComparison.OrdinalIgnoreCase))
            {
                unlockFlag = true;
                break;
            }
        }

        // Guard: ensure input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Access the annotations (comments) collection via the PageSheet.
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // If the unlock flag is set, allow editing of the comment text.
                    if (unlockFlag)
                    {
                        // Example edit: append a suffix indicating the comment was unlocked.
                        string originalText = annotation.Comment.Value;
                        annotation.Comment.Value = $"{originalText} (unlocked)";
                    }
                    else
                    {
                        // Locking behavior: do not modify the comment.
                        // Optionally, you could set a visual indicator that the comment is locked.
                        // For demonstration, we prepend "[LOCKED]" if not already present.
                        string currentText = annotation.Comment.Value;
                        if (!currentText.StartsWith("[LOCKED] "))
                        {
                            annotation.Comment.Value = $"[LOCKED] {currentText}";
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram processed successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}