using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file path, keyword, optional output path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <keyword> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string keyword = args[1];
        string outputPath = args.Length >= 3 ? args[2] : "output.vsdx";

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect annotations that contain the keyword
                    List<Annotation> toRemove = new List<Annotation>();
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        string commentText = annotation.Comment?.Value ?? string.Empty;
                        if (commentText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            toRemove.Add(annotation);
                        }
                    }

                    // Remove the collected annotations from the page
                    foreach (Annotation annotation in toRemove)
                    {
                        page.PageSheet.Annotations.Remove(annotation);
                    }
                }

                // Verify that no remaining annotations contain the keyword
                bool keywordFound = false;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        string commentText = annotation.Comment?.Value ?? string.Empty;
                        if (commentText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            keywordFound = true;
                            break;
                        }
                    }
                    if (keywordFound) break;
                }

                if (keywordFound)
                {
                    Console.Error.WriteLine($"Error: Some comments containing the keyword \"{keyword}\" still exist after deletion.");
                    return;
                }
                else
                {
                    Console.WriteLine($"All comments containing the keyword \"{keyword}\" have been successfully removed.");
                }

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Modified diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}