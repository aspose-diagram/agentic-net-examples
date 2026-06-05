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

        string outputPath = "validated_output.vsdx";

        try
        {
            Diagram diagram = new Diagram(inputPath);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Validate that the EventDblClick cell (used as a stand‑in for EventValidate) has a non‑empty formula
                    if (shape.Event != null && shape.Event.EventDblClick != null && shape.Event.EventDblClick.Ufe != null)
                    {
                        string formula = shape.Event.EventDblClick.Ufe.F;
                        if (string.IsNullOrWhiteSpace(formula))
                        {
                            throw new Exception($"Shape ID {shape.ID} on page \"{page.Name}\" has an empty EventDblClick formula.");
                        }
                    }
                    else
                    {
                        throw new Exception($"Shape ID {shape.ID} on page \"{page.Name}\" does not contain an EventDblClick cell.");
                    }
                }
            }

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully after validation.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}