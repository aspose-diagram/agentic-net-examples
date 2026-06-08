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

        string outputPath = "output.vsdx";

        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        double threshold = 100.0;

        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.True)
                    continue;

                if (shape.Fields.Count == 0)
                    continue;

                for (int i = 0; i < shape.Fields.Count; i++)
                {
                    Field field = shape.Fields[i];
                    double newValue = 150.0;
                    field.Value.Val = newValue.ToString();

                    if (double.TryParse(field.Value.Val, out double parsedValue))
                    {
                        if (parsedValue < threshold)
                        {
                            string message = $"Validation failed for shape ID {shape.ID}, field index {i}. Value {parsedValue} is below the threshold {threshold}.";
                            Console.WriteLine(message);
                            throw new Exception(message);
                        }
                        else
                        {
                            Console.WriteLine($"Shape ID {shape.ID}, field index {i} passed validation. Value: {parsedValue}");
                        }
                    }
                    else
                    {
                        string message = $"Field value '{field.Value.Val}' on shape ID {shape.ID} is not a valid number.";
                        Console.WriteLine(message);
                        throw new Exception(message);
                    }
                }
            }
        }

        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
            return;
        }

        Console.WriteLine("Diagram processing completed successfully.");
    }
}