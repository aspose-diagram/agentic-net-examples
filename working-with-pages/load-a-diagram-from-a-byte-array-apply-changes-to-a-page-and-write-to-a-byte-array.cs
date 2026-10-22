using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Read the entire file into a byte array.
        byte[] inputBytes;
        try
        {
            inputBytes = File.ReadAllBytes(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read input file: {ex.Message}");
            return;
        }

        // Write the byte array to a temporary file so Aspose.Diagram can load it.
        string tempFilePath = Path.GetTempFileName();
        try
        {
            File.WriteAllBytes(tempFilePath, inputBytes);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write temporary file: {ex.Message}");
            return;
        }

        // Load the diagram from the temporary file and apply modifications.
        byte[] outputBytes;
        try
        {
            // Load diagram from file.
            using (Diagram diagram = new Diagram(tempFilePath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Retrieve the first page.
                Page page = diagram.Pages[0];

                // Example modification: rename the page.
                page.Name = "ModifiedPage";

                // Add a rectangle shape to the page.
                // Parameters: pinX, pinY, width, height, masterName, pageIndex.
                long shapeId = diagram.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle", 0);

                // Retrieve the newly added shape.
                Shape shape = page.Shapes.GetShape(shapeId);

                // Update the shape's text.
                shape.Text.Value.Clear();                         // Remove any existing text runs.
                shape.Text.Value.Add(new Txt("Hello Aspose"));    // Add new text run.

                // Save the modified diagram to a memory stream (byte array).
                using (MemoryStream outputStream = new MemoryStream())
                {
                    diagram.Save(outputStream, SaveFileFormat.Vsdx);
                    outputBytes = outputStream.ToArray();
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram related exceptions.
            Console.Error.WriteLine($"Aspose operation failed: {ex.Message}");
            return;
        }
        finally
        {
            // Clean up the temporary file.
            try
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
            catch
            {
                // Suppress any cleanup errors.
            }
        }

        // For demonstration, write the output byte array to a file next to the input.
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            "output.vsdx");

        try
        {
            File.WriteAllBytes(outputPath, outputBytes);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write output file: {ex.Message}");
        }
    }
}