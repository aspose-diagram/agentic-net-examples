using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as an argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Check if the shape contains foreign (OLE) data
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null)
                    {
                        // Retrieve the source file name/path of the embedded OLE object
                        string sourceFile = shape.ForeignData.ObjectSourceFullName ?? string.Empty;

                        // Prepare creation date information
                        string creationInfo = "N/A";

                        // If the source file path points to an existing file, get its creation time
                        if (!string.IsNullOrEmpty(sourceFile) && File.Exists(sourceFile))
                        {
                            try
                            {
                                DateTime fileCreation = File.GetCreationTime(sourceFile);
                                creationInfo = fileCreation.ToString("u");
                            }
                            catch (Exception ex)
                            {
                                // If accessing file info fails, log the error but continue
                                Console.Error.WriteLine($"Failed to get creation time for '{sourceFile}': {ex.Message}");
                            }
                        }

                        // Output the OLE object metadata
                        Console.WriteLine($"Page ID: {page.ID}, Shape ID: {shape.ID}");
                        Console.WriteLine($"  Source File: {sourceFile}");
                        Console.WriteLine($"  Source File Creation Date (UTC): {creationInfo}");
                        Console.WriteLine($"  Show As Icon: {(shape.ForeignData.ShowAsIcon == BOOL.True ? "True" : "False")}");
                        Console.WriteLine();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}