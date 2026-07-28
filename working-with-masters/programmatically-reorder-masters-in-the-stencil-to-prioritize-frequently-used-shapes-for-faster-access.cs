using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the stencil (VST) file
            string stencilPath = "input.vst";
            Diagram diagram = new Diagram(stencilPath); // load existing stencil

            // Define the order of frequently used masters (most used first)
            List<string> priorityNames = new List<string>
            {
                "Rectangle",
                "Ellipse",
                "Connector"
            };

            // Build a lookup of masters by their universal name
            var masterLookup = new Dictionary<string, Master>(StringComparer.OrdinalIgnoreCase);
            foreach (Master m in diagram.Masters)
            {
                masterLookup[m.NameU] = m;
            }

            // Create a new ordered list of masters
            var reorderedMasters = new List<Master>();

            // Add prioritized masters first (if they exist in the stencil)
            foreach (string name in priorityNames)
            {
                if (masterLookup.TryGetValue(name, out Master m))
                {
                    reorderedMasters.Add(m);
                    masterLookup.Remove(name);
                }
            }

            // Append the remaining masters preserving their original order
            foreach (Master m in diagram.Masters)
            {
                if (masterLookup.ContainsKey(m.NameU))
                    reorderedMasters.Add(m);
            }

            // Clear the existing masters collection
            for (int i = diagram.Masters.Count - 1; i >= 0; i--)
            {
                diagram.Masters.RemoveAt(i);
            }

            // Re‑add masters in the new order
            foreach (Master m in reorderedMasters)
            {
                diagram.Masters.Add(m);
            }

            // Save the reordered stencil
            string outputPath = "reordered.vst";
            diagram.Save(outputPath, SaveFileFormat.Vst);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
