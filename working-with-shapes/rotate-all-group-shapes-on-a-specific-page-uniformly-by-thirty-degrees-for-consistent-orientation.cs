using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input Visio file, target page name, output Visio file
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: DiagramGroupRotation <inputFile> <pageName> <outputFile>");
                return;
            }

            string inputPath = args[0];
            string targetPageName = args[1];
            string outputPath = args[2];

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Retrieve the specified page; if not found, fall back to the first page
                Page page = diagram.Pages.GetPage(targetPageName);
                if (page == null)
                {
                    Console.WriteLine($"Page \"{targetPageName}\" not found. Using the first page instead.");
                    page = diagram.Pages[0];
                }

                // Rotate each group shape on the page by 30 degrees
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Group)
                    {
                        // Add 30 degrees to the existing rotation angle
                        double currentAngle = shape.XForm.Angle.Value;
                        double newAngle = (currentAngle + 30) % 360;
                        shape.XForm.Angle.Value = newAngle;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to \"{outputPath}\" with group shapes rotated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }