using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the updated Visio file
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Access the collection of custom properties
            var customProps = diagram.DocumentProps.CustomProps;

            // Flag to indicate whether the property was found and updated
            bool updated = false;

            // Iterate through custom properties to find "Priority"
            for (int i = 0; i < customProps.Count; i++)
            {
                var prop = customProps[i];
                if (prop.Name == "Priority")
                {
                    // Check current value
                    string currentValue = prop.CustomValue.ValueString;
                    if (currentValue == "Low")
                    {
                        // Update to "High"
                        prop.CustomValue.ValueString = "High";
                        updated = true;
                    }
                    break; // Property found; exit loop
                }
            }

            // If the property was not found, optionally add it (not required by task)
            // Uncomment the following block if you want to ensure the property exists:
            /*
            if (!updated)
            {
                var newProp = new CustomProp
                {
                    Name = "Priority",
                    PropType = PropType.String,
                    CustomValue = { ValueString = "High" }
                };
                customProps.Add(newProp);
                updated = true;
            }
            */

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Simple feedback
            if (updated)
                Console.WriteLine("Custom property 'Priority' was updated to 'High'.");
            else
                Console.WriteLine("Custom property 'Priority' was not found or did not require updating.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
