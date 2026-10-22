using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output paths for visual verification (optional)
        string originalOutputPath = "original_output.vsdx";
        string cloneOutputPath = "clone_output.vsdx";

        try
        {
            // Load the original diagram
            Diagram originalDiagram = new Diagram(inputPath);

            // Ensure a custom property named "TestProp" exists
            bool testPropExists = false;
            foreach (CustomProp prop in originalDiagram.DocumentProps.CustomProps)
            {
                if (prop.Name == "TestProp")
                {
                    testPropExists = true;
                    break;
                }
            }

            if (!testPropExists)
            {
                // Create and add the custom property
                CustomProp newProp = new CustomProp();
                newProp.Name = "TestProp";
                newProp.PropType = PropType.String;
                newProp.CustomValue.ValueString = "OriginalValue";
                originalDiagram.DocumentProps.CustomProps.Add(newProp);
            }

            // Capture the original value of "TestProp"
            string originalValue = null;
            foreach (CustomProp prop in originalDiagram.DocumentProps.CustomProps)
            {
                if (prop.Name == "TestProp")
                {
                    originalValue = prop.CustomValue.ValueString;
                    break;
                }
            }

            // Create a clone diagram by combining the original into a new empty diagram
            Diagram cloneDiagram = new Diagram(); // empty diagram
            cloneDiagram.Combine(originalDiagram); // deep copy of pages, masters, properties, etc.

            // Update the custom property in the cloned diagram
            bool clonePropFound = false;
            foreach (CustomProp prop in cloneDiagram.DocumentProps.CustomProps)
            {
                if (prop.Name == "TestProp")
                {
                    prop.CustomValue.ValueString = "ModifiedValue";
                    clonePropFound = true;
                    break;
                }
            }

            // If for some reason the property is missing in the clone, add it
            if (!clonePropFound)
            {
                CustomProp cloneProp = new CustomProp();
                cloneProp.Name = "TestProp";
                cloneProp.PropType = PropType.String;
                cloneProp.CustomValue.ValueString = "ModifiedValue";
                cloneDiagram.DocumentProps.CustomProps.Add(cloneProp);
            }

            // Validate that the original diagram's custom property remains unchanged
            string postCloneOriginalValue = null;
            foreach (CustomProp prop in originalDiagram.DocumentProps.CustomProps)
            {
                if (prop.Name == "TestProp")
                {
                    postCloneOriginalValue = prop.CustomValue.ValueString;
                    break;
                }
            }

            if (originalValue != postCloneOriginalValue)
            {
                throw new Exception("Original diagram's custom property was altered after cloning.");
            }

            // Validate that the cloned diagram reflects the updated value
            string cloneValue = null;
            foreach (CustomProp prop in cloneDiagram.DocumentProps.CustomProps)
            {
                if (prop.Name == "TestProp")
                {
                    cloneValue = prop.CustomValue.ValueString;
                    break;
                }
            }

            if (cloneValue != "ModifiedValue")
            {
                throw new Exception("Cloned diagram's custom property does not contain the expected updated value.");
            }

            // Save both diagrams for optional visual inspection
            originalDiagram.Save(originalOutputPath, SaveFileFormat.Vsdx);
            cloneDiagram.Save(cloneOutputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Validation successful: original diagram unchanged, clone updated.");
            Console.WriteLine($"Original custom property value: {postCloneOriginalValue}");
            Console.WriteLine($"Cloned custom property value: {cloneValue}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}