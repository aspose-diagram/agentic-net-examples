using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new SolutionXML element that contains a conditional formatting rule.
                // The XML defines a rule that highlights shapes where the custom property "Status" equals "Critical".
                SolutionXML solXml = new SolutionXML
                {
                    Name = "ConditionalFormatting",
                    XmlValue = @"
<ConditionalFormatting>
    <Rule>
        <Condition Property=""Status"" Operator=""Equals"" Value=""Critical"" />
        <Highlight FillColor=""#FFCCCC"" LineColor=""#FF0000"" />
    </Rule>
</ConditionalFormatting>"
                };

                // Add the SolutionXML element to the document's collection.
                diagram.SolutionXMLs.Add(solXml);

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}