using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Define ISO 8601 date string
            string isoDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            // Create a custom property
            CustomProp customProp = new CustomProp
            {
                Name = "CreationDate",
                PropType = PropType.String,
                // Assign the ISO date value
                CustomValue = { ValueString = isoDate }
            };

            // Add the custom property to the document
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Verify that the property was stored correctly
            if (diagram.DocumentProps.CustomProps.Count == 0)
                throw new Exception("Custom property was not added.");

            CustomProp storedProp = diagram.DocumentProps.CustomProps[0];
            if (storedProp.Name != "CreationDate")
                throw new Exception("Custom property name mismatch.");

            if (storedProp.CustomValue.ValueString != isoDate)
                throw new Exception($"Custom property value mismatch. Expected: {isoDate}, Actual: {storedProp.CustomValue.ValueString}");

            Console.WriteLine("Custom property added and verified successfully.");

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
