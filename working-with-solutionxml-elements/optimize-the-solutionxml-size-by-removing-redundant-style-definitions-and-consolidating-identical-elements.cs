using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the optimized output file
                string outputPath = "output_optimized.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // ---------- Remove duplicate StyleSheets ----------
                    // Track the first occurrence of each style name
                    var seenStyleNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    // Collect styles to remove to avoid modifying the collection while iterating
                    var stylesToRemove = new List<StyleSheet>();

                    foreach (StyleSheet style in diagram.StyleSheets)
                    {
                        if (seenStyleNames.Contains(style.Name))
                        {
                            // Duplicate found
                            stylesToRemove.Add(style);
                        }
                        else
                        {
                            seenStyleNames.Add(style.Name);
                        }
                    }

                    // Remove the duplicates
                    foreach (StyleSheet dupStyle in stylesToRemove)
                    {
                        diagram.StyleSheets.Remove(dupStyle);
                    }

                    // ---------- Remove duplicate SolutionXML entries ----------
                    var seenSolutionNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    var solutionsToRemove = new List<SolutionXML>();

                    foreach (SolutionXML solXml in diagram.SolutionXMLs)
                    {
                        // Consider both Name and XmlValue for uniqueness
                        string key = $"{solXml.Name}|{solXml.XmlValue}";
                        if (seenSolutionNames.Contains(key))
                        {
                            solutionsToRemove.Add(solXml);
                        }
                        else
                        {
                            seenSolutionNames.Add(key);
                        }
                    }

                    foreach (SolutionXML dupSol in solutionsToRemove)
                    {
                        diagram.SolutionXMLs.Remove(dupSol);
                    }

                    // Save the optimized diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Optimization completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }