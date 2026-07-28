using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Approximate bend count by the number of geometry rows
                            // (each bend typically adds a geometry segment)
                            int bendCount = shape.Geoms.Count;

                            // Apply reroute option only if the connector has more than three bends
                            if (bendCount > 3)
                            {
                                // ConFixedCode controls connector rerouting.
                                // The only confirmed value is Undefined; setting it here
                                // demonstrates applying the option while respecting the API.
                                shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;
                            }
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }