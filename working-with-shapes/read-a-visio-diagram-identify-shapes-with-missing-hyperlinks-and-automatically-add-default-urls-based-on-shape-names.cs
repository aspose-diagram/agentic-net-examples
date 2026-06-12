using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output Visio file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // If the shape has no hyperlinks, add a default one
                        if (shape.Hyperlinks == null || shape.Hyperlinks.Count == 0)
                        {
                            // Create a new hyperlink instance
                            Hyperlink link = new Hyperlink();

                            // Optional: give the hyperlink a name
                            link.Name = "DefaultLink";

                            // Build a default URL based on the shape's universal name
                            // Example: https://example.com/ShapeNameU
                            string defaultUrl = "https://example.com/" + shape.NameU;

                            // Assign the URL to the hyperlink (cell-based property requires .Value)
                            link.Address.Value = defaultUrl;

                            // Optionally set a description (tooltip) for the hyperlink
                            link.Description.Value = "Default link for " + shape.NameU;

                            // Add the hyperlink to the shape's collection
                            shape.Hyperlinks.Add(link);
                        }
                    }
                }

                // Save the modified diagram back to a file (using VSDX format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }