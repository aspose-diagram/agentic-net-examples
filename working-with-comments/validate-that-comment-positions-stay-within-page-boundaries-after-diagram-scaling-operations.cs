using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least one argument: input Visio file path.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [scaleFactor]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Optional scaling factor (default = 1.0).
        double scaleFactor = 1.0;
        if (args.Length >= 2 && double.TryParse(args[1], out double parsedScale) && parsedScale > 0)
        {
            scaleFactor = parsedScale;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Apply the scaling factor to the page's print properties.
                    page.PageSheet.PrintProps.ScaleX.Value = scaleFactor; // Fixed: assign to .Value
                    page.PageSheet.PrintProps.ScaleY.Value = scaleFactor; // Fixed: assign to .Value

                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;   // Fixed: use .Value
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value; // Fixed: use .Value

                    // Iterate through all annotations (comments) on the page.
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the original comment coordinates.
                        double originalX = annotation.X.Value; // Fixed: use .Value
                        double originalY = annotation.Y.Value; // Fixed: use .Value

                        // Apply scaling to the coordinates.
                        double scaledX = originalX * scaleFactor;
                        double scaledY = originalY * scaleFactor;

                        // Validate that the scaled coordinates are within page bounds.
                        bool withinWidth = scaledX >= 0 && scaledX <= pageWidth;
                        bool withinHeight = scaledY >= 0 && scaledY <= pageHeight;

                        // Use page name if available; otherwise fallback to page ID.
                        string pageIdentifier = page.Name != null ? page.Name.ToString() : $"ID_{page.ID}";

                        // Use marker index if available; otherwise fallback to 0.
                        int markerId = annotation.MarkerIndex.Value; // Fixed: use .Value

                        if (withinWidth && withinHeight)
                        {
                            Console.WriteLine($"Page '{pageIdentifier}': Comment #{markerId} is within bounds (X={scaledX}, Y={scaledY}).");
                        }
                        else
                        {
                            Console.WriteLine($"Page '{pageIdentifier}': Comment #{markerId} OUT OF BOUNDS (X={scaledX}, Y={scaledY}).");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
