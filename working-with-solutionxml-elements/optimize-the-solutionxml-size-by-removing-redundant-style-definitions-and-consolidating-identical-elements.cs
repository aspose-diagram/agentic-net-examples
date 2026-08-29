using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "optimized.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // ---------- Remove redundant StyleSheets ----------
                    // Build a dictionary to keep the first occurrence of each unique style definition.
                    // For simplicity, styles are considered identical if their Name is the same.
                    // In a real scenario, you would compare all relevant style properties.
                    var uniqueStyles = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    var styleIndicesToRemove = new List<int>();

                    for (int i = 0; i < diagram.StyleSheets.Count; i++)
                    {
                        var style = diagram.StyleSheets[i];
                        if (uniqueStyles.ContainsKey(style.Name))
                        {
                            // Duplicate found – mark for removal
                            styleIndicesToRemove.Add(i);
                        }
                        else
                        {
                            uniqueStyles[style.Name] = i;
                        }
                    }

                    // Remove duplicates in reverse order to keep indices valid
                    for (int i = styleIndicesToRemove.Count - 1; i >= 0; i--)
                    {
                        diagram.StyleSheets.RemoveAt(styleIndicesToRemove[i]);
                    }

                    // ---------- Consolidate identical SolutionXML elements ----------
                    // Identify SolutionXML entries with the same Name and XmlValue.
                    var seenSolutionXml = new HashSet<string>();
                    var solutionXmlIndicesToRemove = new List<int>();

                    for (int i = 0; i < diagram.SolutionXMLs.Count; i++)
                    {
                        var solXml = diagram.SolutionXMLs[i];
                        string key = $"{solXml.Name}|{solXml.XmlValue}";
                        if (seenSolutionXml.Contains(key))
                        {
                            // Duplicate entry – mark for removal
                            solutionXmlIndicesToRemove.Add(i);
                        }
                        else
                        {
                            seenSolutionXml.Add(key);
                        }
                    }

                    // Remove duplicate SolutionXML entries (reverse order)
                    for (int i = solutionXmlIndicesToRemove.Count - 1; i >= 0; i--)
                    {
                        diagram.SolutionXMLs.RemoveAt(solutionXmlIndicesToRemove[i]);
                    }

                    // ---------- Save the optimized diagram ----------
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Optimization complete. Saved to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }