using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify image shapes by checking for foreign (image) type
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // Extract raw image bytes from the shape's foreign data
                        byte[] imageBytes = shape.ForeignData.Value;

                        // Convert the image bytes to a Base64 string
                        string base64String = Convert.ToBase64String(imageBytes);

                        // Build a data URI assuming PNG format (adjust if needed)
                        string dataUri = $"data:image/png;base64,{base64String}";

                        // Attempt to retrieve an existing custom property named "Base64Image"
                        Prop existingProp = shape.Props.GetProp("Base64Image");

                        if (existingProp == null)
                        {
                            // Create a new custom property to store the data URI
                            Prop newProp = new Prop
                            {
                                Name = "Base64Image",
                                Label = { Value = "Base64 Image" },
                                Value = { Val = dataUri }
                            };
                            shape.Props.Add(newProp);
                        }
                        else
                        {
                            // Update the existing property with the new data URI
                            existingProp.Value.Val = dataUri;
                        }

                        // Output a short preview of the generated data URI for verification
                        Console.WriteLine($"Shape ID {shape.ID} embedded Base64 (preview): {dataUri.Substring(0, 30)}...");
                    }
                }
            }

            // Save the modified diagram to the output file using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with embedded Base64 images to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}