using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio diagram file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No diagram file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Define the grid spacing (e.g., 0.5 inches). Adjust as needed.
            const double gridSpacing = 0.5;

            // Helper local function to determine if a coordinate aligns with the grid.
            bool IsOnGrid(double value)
            {
                // Checks if value is a multiple of gridSpacing within a small tolerance.
                double remainder = Math.Abs(value % gridSpacing);
                return remainder < 1e-6 || Math.Abs(remainder - gridSpacing) < 1e-6;
            }

            bool allPentagonsValid = true;

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify pentagon shapes by checking the master name.
                    if (shape.Master != null && shape.Master.Name == "Pentagon")
                    {
                        // Retrieve geometric properties (values are stored in DoubleValue objects).
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Validate that the main transformation values align with the grid.
                        bool pinXOnGrid = IsOnGrid(pinX);
                        bool pinYOnGrid = IsOnGrid(pinY);
                        bool widthOnGrid = IsOnGrid(width);
                        bool heightOnGrid = IsOnGrid(height);

                        if (!pinXOnGrid || !pinYOnGrid || !widthOnGrid || !heightOnGrid)
                        {
                            allPentagonsValid = false;
                            Console.WriteLine($"Pentagon shape ID {shape.ID} on page {page.ID} has misaligned vertices:");
                            Console.WriteLine($"  PinX = {pinX} (OnGrid: {pinXOnGrid})");
                            Console.WriteLine($"  PinY = {pinY} (OnGrid: {pinYOnGrid})");
                            Console.WriteLine($"  Width = {width} (OnGrid: {widthOnGrid})");
                            Console.WriteLine($"  Height = {height} (OnGrid: {heightOnGrid})");
                        }
                        else
                        {
                            Console.WriteLine($"Pentagon shape ID {shape.ID} on page {page.ID} aligns with the grid.");
                        }
                    }
                }
            }

            if (allPentagonsValid)
            {
                Console.WriteLine("All pentagon vertices align with the expected coordinate grid positions.");
            }
            else
            {
                Console.WriteLine("Some pentagon vertices do not align with the expected coordinate grid positions.");
                // Optionally, you could throw an exception to signal failure.
                // throw new Exception("Pentagon grid validation failed.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or general exceptions and report them.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}