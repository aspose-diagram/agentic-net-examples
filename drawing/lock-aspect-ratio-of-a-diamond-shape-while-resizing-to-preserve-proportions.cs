using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find the diamond shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a master and check its name
                    if (shape.Master != null && shape.Master.Name == "Diamond")
                    {
                        // Lock the aspect ratio so width and height stay proportional
                        shape.Protection.LockAspect.Value = BOOL.True;

                        // Example resize: set a new width; height will follow the locked ratio
                        double newWidth = 2.0; // inches
                        shape.SetWidth(newWidth);
                        // Height is automatically adjusted because the aspect is locked
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
