using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists before proceeding
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has an Event section
                    if (shape.Event == null)
                        continue;

                    // Attempt to access a valid event cell (EventXFMod) and apply conditional formatting
                    try
                    {
                        // Retrieve the formula string from the EventXFMod cell
                        string eventFormula = shape.Event.EventXFMod.Ufe.F;

                        // Simple condition: if the formula text contains "TRUE"
                        if (!string.IsNullOrEmpty(eventFormula) && eventFormula.Contains("TRUE", StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply green fill for true condition
                            shape.Fill.FillForegnd.Value = "#00FF00"; // Green fill
                            shape.Line.LineColor.Value = "#006600";   // Dark green line
                        }
                        else
                        {
                            // Apply red fill for false/other condition
                            shape.Fill.FillForegnd.Value = "#FF0000"; // Red fill
                            shape.Line.LineColor.Value = "#660000";   // Dark red line
                        }
                    }
                    catch (Exception)
                    {
                        // If the specific event cell does not exist, ignore and continue
                        continue;
                    }
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading, processing, or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}