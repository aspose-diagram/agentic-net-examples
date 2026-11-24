using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the Visio file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all SolutionXML elements.
            foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
            {
                Console.WriteLine($"Processing SolutionXML: Name = \"{solutionXml.Name}\"");

                // Attempt to parse the stored XML value.
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(solutionXml.XmlValue);
                    // If parsing succeeds, optionally output root element name.
                    Console.WriteLine($"  XML is well‑formed. Root element: {xmlDoc.DocumentElement?.Name}");
                }
                catch (XmlException xmlEx)
                {
                    // Log detailed diagnostics for malformed XML.
                    Console.Error.WriteLine($"  *** Malformed XML detected in SolutionXML \"{solutionXml.Name}\" ***");
                    Console.Error.WriteLine($"  Message   : {xmlEx.Message}");
                    Console.Error.WriteLine($"  Line      : {xmlEx.LineNumber}, Position: {xmlEx.LinePosition}");
                    // Optionally, you could decide to remove or fix the entry here.
                }
                catch (Exception ex)
                {
                    // Catch any other unexpected errors during XML processing.
                    Console.Error.WriteLine($"  Unexpected error while processing SolutionXML \"{solutionXml.Name}\": {ex.Message}");
                }
            }

            // Optionally, save the diagram back (no changes made in this example).
            // Use a different file name to avoid overwriting the original.
            string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", 
                                            Path.GetFileNameWithoutExtension(diagramPath) + "_checked.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception e)
        {
            // Log any errors that occur during loading or saving the diagram.
            Console.Error.WriteLine($"Error processing diagram: {e.Message}");
        }
    }
}