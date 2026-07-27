using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Access custom properties collection
                CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

                // Try to find an existing custom property named "Status"
                CustomProp statusProp = null;
                foreach (CustomProp prop in customProps)
                {
                    if (prop.Name == "Status")
                    {
                        statusProp = prop;
                        break;
                    }
                }

                if (statusProp != null)
                {
                    // Update the existing property's value
                    statusProp.CustomValue.ValueString = "Completed";
                }
                else
                {
                    // Create a new custom property if it does not exist
                    CustomProp newProp = new CustomProp
                    {
                        Name = "Status",
                        PropType = PropType.String,
                        CustomValue = { ValueString = "Completed" }
                    };
                    customProps.Add(newProp);
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }