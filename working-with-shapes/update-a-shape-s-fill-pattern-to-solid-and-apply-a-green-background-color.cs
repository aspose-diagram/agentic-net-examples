using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page (adjust selection logic as needed)
                Shape shape = null;
                foreach (Shape s in page.Shapes)
                {
                    shape = s;
                    break;
                }

                if (shape != null)
                {
                    // Set fill pattern to solid (value 1)
                    shape.Fill.FillPattern.Value = 1;

                    // Apply a green background color (hex format)
                    shape.Fill.FillForegnd.Value = "#00FF00";
                }
                else
                {
                    // If no shape was found, inform the user
                    Console.WriteLine("No shape found on the first page.");
                }

                // Save the modified diagram to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }