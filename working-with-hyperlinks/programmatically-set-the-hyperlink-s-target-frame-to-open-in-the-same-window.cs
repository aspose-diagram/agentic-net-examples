using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("The diagram contains no pages.");
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Find the first non-deleted shape on the page
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    throw new Exception("No suitable shape found on the page.");
                }

                // Create a new hyperlink (or use an existing one)
                Hyperlink link = new Hyperlink();
                link.Name = "SampleLink";
                link.Address.Value = "https://example.com";
                link.Description.Value = "Open Example.com";

                // Set the hyperlink to open in the same window (NewWindow = false)
                link.NewWindow.Value = BOOL.False;

                // Add the hyperlink to the shape's collection
                targetShape.Hyperlinks.Add(link);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Hyperlink target set to open in the same window and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }