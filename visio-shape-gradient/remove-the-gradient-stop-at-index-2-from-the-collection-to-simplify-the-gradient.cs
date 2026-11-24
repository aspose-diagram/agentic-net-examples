using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths.
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            bool gradientModified = false;

            // Iterate through pages and shapes to find a shape with a gradient fill.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a gradient fill enabled.
                    if (shape.Fill != null &&
                        shape.Fill.GradientFill != null &&
                        shape.Fill.GradientFill.GradientEnabled != null &&
                        shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                    {
                        // Access the gradient stops collection.
                        GradientStopCollection stops = shape.Fill.GradientFill.GradientStops;

                        // Collect stops except the one at index 2.
                        List<GradientStop> keptStops = new List<GradientStop>();
                        int currentIndex = 0;
                        foreach (GradientStop stop in stops)
                        {
                            if (currentIndex != 2)
                            {
                                keptStops.Add(stop);
                            }
                            currentIndex++;
                        }

                        // Clear existing stops and re-add the kept ones.
                        stops.Clear();
                        foreach (GradientStop stop in keptStops)
                        {
                            // Re-add using the original position and color.
                            stops.Add(stop.Position, stop.Color);
                        }

                        gradientModified = true;
                        // Assuming only one shape needs modification; break out of loops.
                        break;
                    }
                }
                if (gradientModified)
                    break;
            }

            if (!gradientModified)
            {
                Console.WriteLine("No shape with a gradient fill was found or gradient had fewer than 3 stops.");
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}