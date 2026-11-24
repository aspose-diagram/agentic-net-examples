using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input folder and output folder
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Validate input folder
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
                return;
            }
        }

        // Get all Visio files in the input folder (common extensions)
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in diagramFiles)
        {
            // Filter supported Visio extensions
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx")
            {
                continue; // Skip non‑Visio files
            }

            // Guard against missing file (should not happen with GetFiles)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load diagram inside a using block as required
                using (Diagram diagram = new Diagram(filePath))
                {
                    bool hasAnyComments = false;
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Ensure the page sheet and annotations collection are available
                        if (page.PageSheet?.Annotations != null && page.PageSheet.Annotations.Count > 0)
                        {
                            hasAnyComments = true;
                            // Prepare output file for this diagram
                            string outputFile = Path.Combine(outputFolder,
                                $"{Path.GetFileNameWithoutExtension(filePath)}_comments.txt");

                            // Append comments to the output file
                            using (StreamWriter writer = new StreamWriter(outputFile, true))
                            {
                                foreach (Annotation ann in page.PageSheet.Annotations)
                                {
                                    string commentText = ann.Comment?.Value ?? string.Empty;
                                    long markerId = ann.MarkerIndex?.Value ?? 0;
                                    long reviewerId = ann.ReviewerID?.Value ?? 0;
                                    writer.WriteLine($"Page: {page.Name}, CommentID: {markerId}, ReviewerID: {reviewerId}, Text: {commentText}");
                                }
                            }
                        }
                    }

                    // If no comments were found, optionally inform the user
                    if (!hasAnyComments)
                    {
                        Console.WriteLine($"No comments found in diagram: {Path.GetFileName(filePath)}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Gracefully skip diagrams that cannot be processed (e.g., lack comment support)
                Console.Error.WriteLine($"Skipping file '{filePath}': {ex.Message}");
                continue;
            }
        }
    }
}