using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output_customized.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Process shapes on the first page (index 0)
                if (diagram.Pages.Count > 0)
                {
                    var page = diagram.Pages[0];

                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Read inherited fill properties
                        string inheritedFore = shape.InheritFill.FillForegnd.Value;
                        string inheritedBack = shape.InheritFill.FillBkgnd.Value;
                        int inheritedPattern = shape.InheritFill.FillPattern.Value;

                        // Example: Log inherited values (could be replaced with any logic)
                        Console.WriteLine($"Shape ID {shape.ID}: Inherited Foreground = {inheritedFore}, Background = {inheritedBack}, Pattern = {inheritedPattern}");

                        // Customize the fill based on inherited values
                        // Here we simply set a solid red foreground and keep the background unchanged
                        shape.Fill.FillPattern.Value = 1; // Solid fill pattern
                        shape.Fill.FillForegnd.Value = "#FF0000"; // Red foreground
                        // Optionally modify background if needed
                        // shape.Fill.FillBkgnd.Value = "#FFFFFF"; // White background
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }