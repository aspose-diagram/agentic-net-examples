using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram.
        Diagram diagram = new Diagram(inputPath);

        // Define the conditional formatting XML.
        string conditionalXml = @"<ConditionalFormatting>
    <Rule DataValue="">100"" HighlightColor=""#FF0000""/>
</ConditionalFormatting>";

        // Try to find an existing SolutionXML with the same name.
        bool updated = false;
        foreach (SolutionXML existing in diagram.SolutionXMLs)
        {
            if (existing.Name == "ConditionalFormatting")
            {
                existing.XmlValue = conditionalXml;
                updated = true;
                break;
            }
        }

        // If not found, add a new SolutionXML element.
        if (!updated)
        {
            SolutionXML solXml = new SolutionXML();
            solXml.Name = "ConditionalFormatting";
            solXml.XmlValue = conditionalXml;
            diagram.SolutionXMLs.Add(solXml);
        }

        // Save the modified diagram in VSDX format.
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine($"Diagram saved to '{outputPath}' with conditional formatting rule.");
    }
}
