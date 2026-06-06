using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be validated.
                // If a command‑line argument is provided, use it; otherwise use a placeholder path.
                string filePath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram from the specified file.
                // The Diagram constructor handles loading; no LoadOptions are required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Retrieve the first page (index 0). Pages are zero‑based in the collection.
                    Page page = diagram.Pages[0];

                    // Get the page width in inches from the page's properties.
                    double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;

                    // Expected A4 width in inches (8.27 inches).
                    const double expectedA4Width = 8.27;

                    // Allow a tiny tolerance for floating‑point differences.
                    const double tolerance = 0.001;

                    // Validate the width.
                    if (Math.Abs(pageWidthInches - expectedA4Width) > tolerance)
                    {
                        throw new Exception($"Page width validation failed. Expected {expectedA4Width} inches, but found {pageWidthInches} inches.");
                    }
                    else
                    {
                        Console.WriteLine($"Page width validation succeeded. Width = {pageWidthInches} inches (A4).");
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }