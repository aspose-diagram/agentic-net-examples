using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create a new custom property with ISO 8601 date value.
            CustomProp customProp = new CustomProp();
            customProp.Name = "CreatedDate";
            customProp.PropType = PropType.String;

            // Format current UTC time as ISO 8601.
            string isoDate = DateTime.UtcNow.ToString("o");
            customProp.CustomValue.ValueString = isoDate;

            // Add the custom property to the document's custom properties collection.
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
            return;
        }

        // Verification: reload the saved diagram and check the custom property.
        try
        {
            Diagram loadedDiagram = new Diagram(outputPath);
            bool found = false;
            foreach (CustomProp prop in loadedDiagram.DocumentProps.CustomProps)
            {
                if (prop.Name == "CreatedDate")
                {
                    found = true;
                    string storedValue = prop.CustomValue.ValueString;
                    // Compare stored value with the expected ISO 8601 format.
                    // Since the original value was generated just before saving, we allow a small time difference.
                    // For simplicity, we just check that the stored string can be parsed as a DateTime.
                    if (DateTime.TryParse(storedValue, out DateTime parsedDate))
                    {
                        Console.WriteLine("Custom property 'CreatedDate' verified successfully.");
                    }
                    else
                    {
                        throw new Exception("Stored custom property value is not a valid ISO 8601 date.");
                    }
                    break;
                }
            }

            if (!found)
            {
                throw new Exception("Custom property 'CreatedDate' was not found after saving.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Verification failed: {ex.Message}");
        }
    }
}