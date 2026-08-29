using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (default names if not provided)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                // Load the diagram from the specified file
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate over every page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate over every shape on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Process only connector shapes (1‑D shapes)
                            if (shape.OneD)
                            {
                                // Reset the connector's line jump style to the library default
                                shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.PageDefault;
                            }
                        }
                    }

                    // Save the updated diagram back to a file
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }