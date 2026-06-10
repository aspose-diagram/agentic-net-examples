using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create an AutoSpaceOptions instance using the default constructor
        AutoSpaceOptions options = new AutoSpaceOptions();

        // Set the default spacing values (in inches) explicitly
        // Default horizontal spacing: 0.375 inch
        options.DistanceInHorizontal = 0.375;
        // Default vertical spacing: 0.375 inch
        options.DistanceInVertical = 0.375;

        // Example usage (commented out to avoid requiring a diagram file):
        // Diagram diagram = new Diagram("input.vsdx");
        // Page page = diagram.Pages[0];
        // page.AutoSpaceShapes(page.Shapes, options);
        // diagram.Save("output.vsdx");
    }
}
