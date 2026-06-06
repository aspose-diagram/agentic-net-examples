using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Iterate through each page, convert dimensions to millimeters and store them as custom properties
        foreach (Page page in diagram.Pages)
        {
            double widthInches = page.PageSheet.PageProps.PageWidth.Value;
            double heightInches = page.PageSheet.PageProps.PageHeight.Value;

            double widthMm = widthInches * 25.4;
            double heightMm = heightInches * 25.4;

            // Store width and height in custom properties
            AddOrUpdateCustomProp(diagram, $"Page{page.ID}_Width_mm", widthMm);
            AddOrUpdateCustomProp(diagram, $"Page{page.ID}_Height_mm", heightMm);
        }

        // Save the updated diagram
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }

    // Helper method to add a new custom property or update an existing one
    private static void AddOrUpdateCustomProp(Diagram diagram, string propName, double value)
    {
        // Search for an existing property with the same name
        foreach (CustomProp existingProp in diagram.DocumentProps.CustomProps)
        {
            if (existingProp.Name == propName)
            {
                existingProp.CustomValue.ValueString = value.ToString("F2");
                return;
            }
        }

        // Property not found – create a new one
        CustomProp newProp = new CustomProp
        {
            Name = propName,
            PropType = PropType.String,
            CustomValue = { ValueString = value.ToString("F2") }
        };

        diagram.DocumentProps.CustomProps.Add(newProp);
    }
}
