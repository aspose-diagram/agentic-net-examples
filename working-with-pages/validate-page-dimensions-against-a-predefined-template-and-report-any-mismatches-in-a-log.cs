using System;
using Aspose.Diagram;

class Program
    {
        // Simple template definition: expected page dimensions in inches.
        // Key: page name (or empty string for any page), Value: tuple of width and height.
        private static readonly System.Collections.Generic.Dictionary<string, (double Width, double Height)> PageTemplates
            = new System.Collections.Generic.Dictionary<string, (double, double)>(StringComparer.OrdinalIgnoreCase)
        {
            // Example: A4 size for all pages (use empty key to apply to any page name)
            { "", (8.27, 11.69) } // Width, Height in inches
        };

        static void Main(string[] args)
        {
            try
            {

                if (args.Length == 0)
                {
                    Console.WriteLine("Usage: DiagramPageValidator <diagram-file-path>");
                    return;
                }

                string diagramPath = args[0];

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    bool anyMismatch = false;

                    // Iterate through each page explicitly typed as Aspose.Diagram.Page
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve actual dimensions (in inches)
                        double actualWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double actualHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Determine expected dimensions based on template
                        // First try exact page name, then fallback to generic entry ("")
                        (double expectedWidth, double expectedHeight) expected;

                        if (!PageTemplates.TryGetValue(page.Name, out expected))
                        {
                            // Use generic template if specific name not found
                            PageTemplates.TryGetValue(string.Empty, out expected);
                        }

                        // If no template entry exists, skip validation for this page
                        if (expected == default)
                        {
                            Console.WriteLine($"No template defined for page \"{page.Name}\". Skipping.");
                            continue;
                        }

                        // Compare with a tolerance to account for floating‑point rounding
                        const double tolerance = 0.001; // inches
                        bool widthMatches = Math.Abs(actualWidth - expected.expectedWidth) <= tolerance;
                        bool heightMatches = Math.Abs(actualHeight - expected.expectedHeight) <= tolerance;

                        if (!widthMatches || !heightMatches)
                        {
                            anyMismatch = true;
                            Console.WriteLine($"Page \"{page.Name}\" (ID={page.ID}) size mismatch:");
                            Console.WriteLine($"  Expected: Width={expected.expectedWidth:F3}\" Height={expected.expectedHeight:F3}\"");
                            Console.WriteLine($"  Actual:   Width={actualWidth:F3}\" Height={actualHeight:F3}\"");
                        }
                    }

                    if (!anyMismatch)
                    {
                        Console.WriteLine("All pages match the predefined template.");
                    }
                    else
                    {
                        // Optionally, you could throw an exception to signal failure
                        // throw new Exception("One or more pages do not match the template.");
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }