using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "sample.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page
                Page page = diagram.Pages[0];

                // Get the first shape on the page
                Shape shape = page.Shapes[0];

                // Check if fill inheritance is disabled.
                // Inheritance is considered disabled when the local fill pattern differs from the inherited one.
                bool inheritanceDisabled = shape.Fill.FillPattern.Value != shape.InheritFill.FillPattern.Value;

                if (!inheritanceDisabled)
                {
                    // Disable inheritance by assigning explicit fill values.
                    // Example: solid fill (pattern 1) with red foreground and green background.
                    shape.Fill.FillPattern.Value = 1;               // Solid fill
                    shape.Fill.FillForegnd.Value = "#FF0000";       // Red foreground
                    shape.Fill.FillBkgnd.Value = "#00FF00";         // Green background
                }

                // Retrieve fill details
                string foregroundColor = shape.Fill.FillForegnd.Value;
                string backgroundColor = shape.Fill.FillBkgnd.Value;
                int fillPattern = shape.Fill.FillPattern.Value;

                // Output the fill properties
                Console.WriteLine("Fill Inheritance Disabled: " + inheritanceDisabled);
                Console.WriteLine("Foreground Color: " + foregroundColor);
                Console.WriteLine("Background Color: " + backgroundColor);
                Console.WriteLine("Fill Pattern (numeric code): " + fillPattern);

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }