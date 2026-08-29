using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Threshold for conditional coloring
                double threshold = 100.0;

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve the numeric data from Data1 (string property)
                            string dataValue = shape.Data1;

                            if (!string.IsNullOrWhiteSpace(dataValue) && double.TryParse(dataValue, out double numericValue))
                            {
                                // Apply fill color based on the numeric value
                                if (numericValue > threshold)
                                {
                                    // Red fill for values above the threshold
                                    shape.Fill.FillForegnd.Value = "#FF0000";
                                }
                                else
                                {
                                    // Green fill for values at or below the threshold
                                    shape.Fill.FillForegnd.Value = "#00FF00";
                                }
                            }
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine("Diagram saved successfully to: " + outputPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }