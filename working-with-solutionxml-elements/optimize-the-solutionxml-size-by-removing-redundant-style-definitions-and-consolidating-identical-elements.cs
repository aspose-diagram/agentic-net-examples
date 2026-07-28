using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class OptimizeSolutionXml
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (lifecycle rule: use provided load method)
            Diagram diagram = new Diagram("input.vsdx");

            // ------------------------------------------------------------
            // 1. Remove unused style definitions (feature rule: GetUnusedStyles)
            // ------------------------------------------------------------
            // Get the collection of styles that are not referenced anywhere.
            StyleSheetCollection unusedStyles = diagram.GetUnusedStyles();

            // Iterate over a copy of the collection because we will modify it.
            // The StyleSheetCollection inherits from Collection, which provides a Remove method.
            // Remove each unused style from the diagram.
            foreach (StyleSheet style in new List<StyleSheet>(unusedStyles))
            {
                // The StyleSheetCollection does not expose a direct Remove method in the docs,
                // but it inherits from Collection which provides RemoveAt. Find the index first.
                int index = unusedStyles.IndexOf(style);
                if (index >= 0)
                {
                    unusedStyles.RemoveAt(index);
                }
            }

            // ------------------------------------------------------------
            // 2. Consolidate identical SolutionXML elements
            // ------------------------------------------------------------
            // The diagram may contain multiple SolutionXML entries with the same XML content.
            // Keep only one instance per unique XmlValue and remove the duplicates.
            SolutionXMLCollection solXmls = diagram.SolutionXMLs;
            var seen = new Dictionary<string, SolutionXML>(StringComparer.Ordinal);
            var duplicates = new List<SolutionXML>();

            // Identify duplicates
            foreach (SolutionXML solXml in solXmls)
            {
                if (solXml == null) continue;

                string key = solXml.XmlValue ?? string.Empty;
                if (seen.ContainsKey(key))
                {
                    // This is a duplicate; mark for removal
                    duplicates.Add(solXml);
                }
                else
                {
                    seen[key] = solXml;
                }
            }

            // Remove duplicate SolutionXML entries (lifecycle rule: use provided Remove method)
            foreach (SolutionXML dup in duplicates)
            {
                solXmls.Remove(dup);
            }

            // ------------------------------------------------------------
            // Save the optimized diagram (lifecycle rule: use provided save method)
            // ------------------------------------------------------------
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
