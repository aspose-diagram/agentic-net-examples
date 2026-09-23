using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Path for the diagram file
            string filePath = "customPropsDemo.vsdx";

            // -------------------------------------------------
            // 1. Create a new diagram (empty)
            // -------------------------------------------------
            Diagram diagram = new Diagram();

            // -------------------------------------------------
            // 2. Add a custom document property
            // -------------------------------------------------
            CustomProp customProp = new CustomProp
            {
                Name = "MyCustomProperty",
                PropType = PropType.String,
                // Assign the value to the CustomValue cell
                CustomValue = { ValueString = "MyValue" }
            };
            diagram.DocumentProps.CustomProps.Add(customProp);

            // -------------------------------------------------
            // 3. Save the diagram to a file
            // -------------------------------------------------
            diagram.Save(filePath, SaveFileFormat.Vsdx);

            // -------------------------------------------------
            // 4. Load the diagram back from the file
            // -------------------------------------------------
            Diagram loadedDiagram = new Diagram(filePath);

            // -------------------------------------------------
            // 5. Retrieve the custom property and verify its value
            // -------------------------------------------------
            bool propertyFound = false;
            foreach (CustomProp prop in loadedDiagram.DocumentProps.CustomProps)
            {
                if (prop.Name == "MyCustomProperty")
                {
                    propertyFound = true;
                    if (prop.CustomValue.ValueString != "MyValue")
                    {
                        throw new Exception($"Custom property value mismatch. Expected 'MyValue', got '{prop.CustomValue.ValueString}'.");
                    }
                    break;
                }
            }

            if (!propertyFound)
            {
                throw new Exception("Custom property 'MyCustomProperty' was not found after loading the diagram.");
            }

            Console.WriteLine("Custom property persisted correctly after save and load.");
        }
    }