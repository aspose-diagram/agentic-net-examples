using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output_optimized.vsdx";

        try
        {
            Diagram diagram = new Diagram(inputPath);

            // ---------- Remove duplicate StyleSheets ----------
            var seenSignatures = new System.Collections.Generic.Dictionary<string, int>();
            for (int i = diagram.StyleSheets.Count - 1; i >= 0; i--)
            {
                var style = diagram.StyleSheets[i];
                string signature = $"{style.Name}|{style.Fill?.FillForegnd?.Value}|{style.Fill?.FillPattern?.Value}|{style.Line?.LineColor?.Value}|{style.Line?.LineWeight?.Value}";
                if (seenSignatures.ContainsKey(signature))
                {
                    diagram.StyleSheets.Remove(style);
                    Console.WriteLine($"Removed duplicate StyleSheet at index {i} (Name: {style.Name}).");
                }
                else
                {
                    seenSignatures[signature] = i;
                }
            }

            // ---------- Consolidate identical SolutionXML entries ----------
            var seenSolutionXml = new System.Collections.Generic.HashSet<string>();
            for (int i = diagram.SolutionXMLs.Count - 1; i >= 0; i--)
            {
                var solXml = diagram.SolutionXMLs[i];
                string key = $"{solXml.Name}|{solXml.XmlValue}";
                if (seenSolutionXml.Contains(key))
                {
                    diagram.SolutionXMLs.Remove(solXml);
                    Console.WriteLine($"Removed duplicate SolutionXML (Name: {solXml.Name}).");
                }
                else
                {
                    seenSolutionXml.Add(key);
                }
            }

            // Save the optimized diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Optimized diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}