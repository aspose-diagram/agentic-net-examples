using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be validated
                string diagramPath = "input.vsdx";

                // Define expected page dimensions (in inches) for each page name
                // Example: A4 size for "Page-1" and Letter size for "Page-2"
                var expectedDimensions = new System.Collections.Generic.Dictionary<string, (double Width, double Height)>
                {
                    { "Page-1", (8.27, 11.69) },   // A4
                    { "Page-2", (8.5, 11.0) }      // Letter
                };

                bool anyMismatch = false;

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve actual page width and height (values are in inches)
                        double actualWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double actualHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Use the page name as the key for expected dimensions
                        string pageName = page.Name;

                        if (expectedDimensions.TryGetValue(pageName, out var expected))
                        {
                            // Compare expected and actual dimensions with a small tolerance
                            const double tolerance = 0.01; // inches
                            bool widthMatches = Math.Abs(actualWidth - expected.Width) <= tolerance;
                            bool heightMatches = Math.Abs(actualHeight - expected.Height) <= tolerance;

                            if (!widthMatches || !heightMatches)
                            {
                                anyMismatch = true;
                                Console.WriteLine($"[Mismatch] Page '{pageName}': Expected {expected.Width}x{expected.Height} inches, " +
                                                  $"Actual {actualWidth:F2}x{actualHeight:F2} inches.");
                            }
                            else
                            {
                                Console.WriteLine($"[OK] Page '{pageName}' dimensions match the template.");
                            }
                        }
                        else
                        {
                            // No template defined for this page; log as informational
                            Console.WriteLine($"[Info] No template defined for page '{pageName}'. " +
                                              $"Actual size: {actualWidth:F2}x{actualHeight:F2} inches.");
                        }
                    }
                }

                // If any mismatches were found, throw an exception to indicate validation failure
                if (anyMismatch)
                {
                    throw new Exception("One or more pages do not match the predefined dimensions. See log for details.");
                }
                else
                {
                    Console.WriteLine("All pages validated successfully against the template.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }