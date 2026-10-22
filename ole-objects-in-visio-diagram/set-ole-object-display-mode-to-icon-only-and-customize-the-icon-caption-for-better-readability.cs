using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate over all shapes on the page.
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Identify OLE objects via ForeignData presence and embedded object data.
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null)
                    {
                        // Set the OLE object to display as an icon.
                        shape.ForeignData.ShowAsIcon = BOOL.True;

                        // Customize the icon caption for better readability.
                        if (shape.Text != null)
                        {
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt("OLE Icon"));
                        }
                    }
                }
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}