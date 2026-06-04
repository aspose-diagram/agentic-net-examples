using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Prepare ISO 8601 formatted date string
            string isoDate = DateTime.UtcNow.ToString("o"); // e.g., 2023-08-15T12:34:56.789Z

            // Create a custom property
            CustomProp customProp = new CustomProp
            {
                Name = "CreationDate",
                PropType = PropType.String,
                // Assign the date string to the custom value
                CustomValue = new CustomValue { ValueString = isoDate }
            };

            // Add the custom property to the document's custom properties collection
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Verify that the property was added correctly
            if (diagram.DocumentProps.CustomProps.Count == 0)
                throw new Exception("Custom property was not added.");

            // Retrieve the property (by index, as we just added one)
            CustomProp retrievedProp = diagram.DocumentProps.CustomProps[0];

            // Check name
            if (retrievedProp.Name != "CreationDate")
                throw new Exception("Custom property name mismatch.");

            // Check value
            string storedValue = retrievedProp.CustomValue.ValueString;
            if (storedValue != isoDate)
                throw new Exception($"Custom property value mismatch. Expected: {isoDate}, Got: {storedValue}");

            Console.WriteLine("Custom property added and verified successfully.");
            Console.WriteLine($"Name: {retrievedProp.Name}, Value: {storedValue}");

            // Save the diagram to a VSDX file
            diagram.Save("CustomPropertyDemo.vsdx", SaveFileFormat.Vsdx);
        }
    }