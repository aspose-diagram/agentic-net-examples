using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Attempt to delete a built‑in property (Title) via the custom properties collection.
            // Built‑in properties are not stored in CustomProps, so this operation should fail.
            try
            {
                var customProps = diagram.DocumentProps.CustomProps;

                // Create a dummy custom property with the same name as a built‑in property.
                CustomProp dummyProp = new CustomProp
                {
                    Name = "Title",                     // Built‑in property name
                    PropType = PropType.String,
                    CustomValue = { ValueString = "Dummy" }
                };

                // Attempt to remove the dummy property (which does not actually exist in the collection).
                // This should throw an exception because the property cannot be found/removed.
                customProps.Remove(dummyProp);

                // If no exception is thrown, the test has failed.
                throw new Exception("Built‑in property removal did not throw an exception as expected.");
            }
            catch (Exception ex)
            {
                // Expected path: an exception is thrown and caught here.
                Console.WriteLine($"Expected exception caught: {ex.GetType().Name} - {ex.Message}");
            }

            // Save the diagram to verify that the diagram is still functional after the failed deletion.
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
    }