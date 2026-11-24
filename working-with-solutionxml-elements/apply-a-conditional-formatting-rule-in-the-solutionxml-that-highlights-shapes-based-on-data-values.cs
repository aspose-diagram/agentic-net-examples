using System;
using System.IO;
using Aspose.Diagram;

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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define a conditional formatting rule as custom XML.
                // This XML can be interpreted by a consuming application to highlight shapes
                // where the custom data field "Data1" exceeds 100.
                string conditionalXml = @"
<ConditionalFormatting>
    <Rule>
        <Condition>Data1 &gt; 100</Condition>
        <HighlightColor>#FF0000</HighlightColor>
        <Description>Highlight shapes with Data1 greater than 100</Description>
    </Rule>
</ConditionalFormatting>";

                // Create a new SolutionXML element and assign its properties.
                SolutionXML solutionXml = new SolutionXML
                {
                    // Identifier for the SolutionXML element.
                    Name = "ConditionalFormatting",

                    // The actual XML content stored in the element.
                    XmlValue = conditionalXml
                };

                // Add the SolutionXML element to the diagram's collection.
                diagram.SolutionXMLs.Add(solutionXml);

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}