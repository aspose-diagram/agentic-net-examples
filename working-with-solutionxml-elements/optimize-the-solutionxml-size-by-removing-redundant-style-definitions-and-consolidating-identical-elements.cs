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

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // ---------- Remove redundant SolutionXML elements ----------
            // Keep the first occurrence of each unique XmlValue, remove duplicates
            var seenXmlValues = new HashSet<string>();
            var solutionXmlsToRemove = new List<SolutionXML>();

            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                // Use XmlValue as the uniqueness key
                string xml = solXml.XmlValue ?? string.Empty;
                if (seenXmlValues.Contains(xml))
                {
                    solutionXmlsToRemove.Add(solXml);
                }
                else
                {
                    seenXmlValues.Add(xml);
                }
            }

            foreach (SolutionXML dup in solutionXmlsToRemove)
            {
                diagram.SolutionXMLs.Remove(dup);
            }

            // ---------- Remove redundant StyleSheet definitions ----------
            // Keep the first StyleSheet with a given Name, remove subsequent duplicates
            var seenStyleNames = new HashSet<string>();
            var styleSheetsToRemove = new List<StyleSheet>();

            foreach (StyleSheet style in diagram.StyleSheets)
            {
                string name = style.Name ?? string.Empty;
                if (seenStyleNames.Contains(name))
                {
                    styleSheetsToRemove.Add(style);
                }
                else
                {
                    seenStyleNames.Add(name);
                }
            }

            foreach (StyleSheet dupStyle in styleSheetsToRemove)
            {
                diagram.StyleSheets.Remove(dupStyle);
            }

            // Save the optimized diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Optimized diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}