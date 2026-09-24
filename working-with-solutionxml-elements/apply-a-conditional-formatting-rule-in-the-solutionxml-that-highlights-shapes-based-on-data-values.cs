using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape has a Data1 value that can be parsed as a number
                        if (!string.IsNullOrWhiteSpace(shape.Data1) && double.TryParse(shape.Data1, out double dataValue))
                        {
                            // Example condition: highlight shapes where Data1 > 100
                            if (dataValue > 100)
                            {
                                // Apply highlight color (red) to the shape's fill foreground
                                shape.Fill.FillForegnd.Value = "#FF0000";
                            }
                        }
                    }
                }

                // Create a SolutionXML element that describes the conditional formatting rule
                SolutionXML conditionalXml = new SolutionXML();
                conditionalXml.Name = "ConditionalFormatting";
                conditionalXml.XmlValue =
                    "<ConditionalFormatting>" +
                    "  <Rule>" +
                    "    <Condition>Data1 > 100</Condition>" +
                    "    <HighlightColor>#FF0000</HighlightColor>" +
                    "  </Rule>" +
                    "</ConditionalFormatting>";

                // Add the SolutionXML to the diagram
                diagram.SolutionXMLs.Add(conditionalXml);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }