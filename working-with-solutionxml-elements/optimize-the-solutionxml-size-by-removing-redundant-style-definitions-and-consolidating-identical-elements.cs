using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // ------------------------------------------------------------
            // Remove redundant SolutionXML elements (identical XmlValue)
            // ------------------------------------------------------------
            var seenXmlValues = new HashSet<string>();
            var solutionXmlsToRemove = new List<SolutionXML>();

            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                // If the same XML content has been seen, mark for removal
                if (seenXmlValues.Contains(solXml.XmlValue))
                {
                    solutionXmlsToRemove.Add(solXml);
                }
                else
                {
                    seenXmlValues.Add(solXml.XmlValue);
                }
            }

            // Remove duplicates from the collection
            foreach (SolutionXML dup in solutionXmlsToRemove)
            {
                diagram.SolutionXMLs.Remove(dup);
            }

            // ------------------------------------------------------------
            // Remove redundant StyleSheet definitions (identical Name)
            // ------------------------------------------------------------
            var seenStyleNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var styleSheetsToRemove = new List<StyleSheet>();

            foreach (StyleSheet styleSheet in diagram.StyleSheets)
            {
                // If a style sheet with the same name already exists, mark for removal
                if (seenStyleNames.Contains(styleSheet.Name))
                {
                    styleSheetsToRemove.Add(styleSheet);
                }
                else
                {
                    seenStyleNames.Add(styleSheet.Name);
                }
            }

            // Remove duplicate style sheets
            foreach (StyleSheet dupStyle in styleSheetsToRemove)
            {
                diagram.StyleSheets.Remove(dupStyle);
            }

            // ------------------------------------------------------------
            // Save the optimized diagram
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Optimized diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}