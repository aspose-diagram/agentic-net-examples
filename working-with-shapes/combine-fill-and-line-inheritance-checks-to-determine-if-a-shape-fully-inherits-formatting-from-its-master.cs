using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (rule‑provided load method)
            Diagram diagram = LoadDiagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape inherits fill formatting from its master
                    bool inheritsFill = shape.InheritFill != null;

                    // Check if the shape inherits line formatting from its master
                    bool inheritsLine = shape.InheritLine != null;

                    // Shape fully inherits formatting when both fill and line are inherited
                    bool fullyInherits = inheritsFill && inheritsLine;

                    Console.WriteLine($"Shape ID {shape.ID} fully inherits formatting: {fullyInherits}");
                }
            }

            // Save the diagram (rule‑provided save method)
            SaveDiagram(diagram, "output.vsdx");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Placeholder for the rule‑provided diagram loading method
    static Diagram LoadDiagram(string path)
    {
        // The actual implementation will be supplied by the rule set
        return new Diagram(path);
    }

    // Placeholder for the rule‑provided diagram saving method
    static void SaveDiagram(Diagram diagram, string path)
    {
        // The actual implementation will be supplied by the rule set
        diagram.Save(path, SaveFileFormat.Vsdx);
    }
}
