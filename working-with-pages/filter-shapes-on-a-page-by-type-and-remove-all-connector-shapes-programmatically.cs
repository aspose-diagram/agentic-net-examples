using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Prompt user for input Visio file path
            Console.Write("Enter the path of the Visio file to process: ");
            string inputPath = Console.ReadLine();

            // Prompt user for output Visio file path
            Console.Write("Enter the desired output file path (e.g., output.vsdx): ");
            string outputPath = Console.ReadLine();

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes: they are 1‑D shapes (OneD == true)
                    if (shape.OneD)
                    {
                        // Mark the connector shape for deletion
                        shape.Del = BOOL.True;
                    }
                }
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("All connector shapes have been removed and the diagram saved.");
        }
    }