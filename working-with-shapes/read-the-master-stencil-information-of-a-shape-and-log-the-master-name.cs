using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            // (Replace "input.vsdx" with the path to your diagram file)
            Diagram diagram = new Diagram("input.vsdx");

            // Ensure the diagram has at least one page and one shape
            if (diagram.Pages.Count > 0 && diagram.Pages[0].Shapes.Count > 0)
            {
                // Get the first shape on the first page
                Shape shape = diagram.Pages[0].Shapes[0];

                // Retrieve the master from which this shape inherits its data
                Master master = shape.Master;

                if (master != null)
                {
                    // Log the master name (both Name and NameU are available)
                    Console.WriteLine("Master Name: " + master.Name);
                    Console.WriteLine("Master Universal Name (NameU): " + master.NameU);
                }
                else
                {
                    Console.WriteLine("The shape does not have an associated master.");
                }
            }
            else
            {
                Console.WriteLine("The diagram does not contain any pages or shapes.");
            }

            // Optionally, save the diagram if modifications were made
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
