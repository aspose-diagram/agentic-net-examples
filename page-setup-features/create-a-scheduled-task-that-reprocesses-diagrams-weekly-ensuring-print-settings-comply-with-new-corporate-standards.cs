using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <DiagramsFolderPath>");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Supported Visio file extensions
        string[] extensions = new[]
        {
            ".vsdx", ".vsd", ".vdx", ".vssx", ".vss",
            ".vstx", ".vst", ".vtx"
        };

        // Retrieve all files (non‑recursive) and process supported diagrams
        string[] files = Directory.GetFiles(folderPath);
        foreach (string filePath in files)
        {
            // Process only supported extensions
            if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                continue;

            // Guard: ensure the file exists
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block (required for proper disposal)
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Ensure at least one window exists (required for some operations)
                    if (diagram.Windows.Count == 0)
                    {
                        Window w = new Window
                        {
                            WindowType = WindowTypeValue.Drawing,
                            WindowState = WindowStateValue.Maximized,
                            WindowWidth = 1100,
                            WindowHeight = 700
                        };
                        diagram.Windows.Add(w);
                    }

                    // Iterate through each page and apply corporate print settings
                    foreach (Page page in diagram.Pages)
                    {
                        // Access the page's print properties
                        PrintProps printProps = page.PageSheet.PrintProps;

                        // Set orientation to Landscape
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // Set scaling to 75 % (both X and Y)
                        printProps.ScaleX.Value = 0.75;
                        printProps.ScaleY.Value = 0.75;

                        // Enable "Fit to sheet" (OnPage) and use a single page layout
                        printProps.OnPage.Value = BOOL.True;

                        // Set standard margins (0.5 inch = 36 points)
                        double halfInch = 36.0; // points
                        printProps.PageTopMargin.Value = halfInch;
                        printProps.PageBottomMargin.Value = halfInch;
                        printProps.PageLeftMargin.Value = halfInch;
                        printProps.PageRightMargin.Value = halfInch;
                    }

                    // Save the updated diagram back to the original file (VSDX format)
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}
