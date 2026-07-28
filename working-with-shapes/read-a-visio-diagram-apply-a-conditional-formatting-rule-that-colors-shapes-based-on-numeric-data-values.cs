using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments: input Visio file and output file paths
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: ConditionalFormattingExample <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Use Data1 as the numeric value source (adjust if needed)
                    string dataValue = shape.Data1;

                    if (double.TryParse(dataValue, out double numericValue))
                    {
                        // Example conditional logic:
                        // - Value > 100  => Red fill
                        // - Value between 50 and 100 => Yellow fill
                        // - Value < 50   => Green fill
                        if (numericValue > 100)
                        {
                            shape.Fill.FillForegnd.Value = "#FF0000"; // Red
                        }
                        else if (numericValue >= 50)
                        {
                            shape.Fill.FillForegnd.Value = "#FFFF00"; // Yellow
                        }
                        else
                        {
                            shape.Fill.FillForegnd.Value = "#00FF00"; // Green
                        }

                        // Ensure the fill pattern is solid (1 = solid)
                        shape.Fill.FillPattern.Value = 1;
                    }
                }
            }

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }