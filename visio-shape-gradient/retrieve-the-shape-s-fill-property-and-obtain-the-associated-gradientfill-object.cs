using System.IO;
using System;
using Aspose.Diagram;

class RetrieveGradientFill
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access a specific shape; here we take the first shape on the first page
            // Adjust the indices as needed for your scenario
            Shape shape = diagram.Pages[0].Shapes[1];

            // Retrieve the Fill object of the shape
            Fill fill = shape.Fill;

            // Obtain the GradientFill object from the Fill
            GradientFill gradientFill = fill.GradientFill;

            // Example: output whether the gradient is enabled
            if (gradientFill != null && gradientFill.GradientEnabled != null)
            {
                Console.WriteLine("Gradient Enabled: " + gradientFill.GradientEnabled.Value);
            }
            else
            {
                Console.WriteLine("No gradient fill information available.");
            }

            // (Optional) Save the diagram if any modifications were made
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
