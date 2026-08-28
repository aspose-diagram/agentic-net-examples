using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the universal name of the shape to which the hyperlink will be added
                string targetShapeNameU = "MyShape";

                // Locate the shape across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU == targetShapeNameU)
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null)
                        break;
                }

                if (targetShape == null)
                {
                    Console.WriteLine($"Shape with NameU '{targetShapeNameU}' not found.");
                    return;
                }

                // Create a new hyperlink instance
                Hyperlink hyperlink = new Hyperlink
                {
                    Name = "ExternalWebsiteLink",               // Optional internal identifier
                    Address = { Value = "https://www.example.com" }, // External URL
                    Description = { Value = "Visit Example.com" }   // Tooltip / descriptive name
                };

                // Ensure the Hyperlinks collection is initialized (it always is) and add the hyperlink
                targetShape.Hyperlinks.Add(hyperlink);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Hyperlink added to shape '{targetShapeNameU}' and diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }