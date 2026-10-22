using System;
using System.IO;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file '{inputPath}' does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Check if the shape contains an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        // Retrieve the generic ActiveX control
                        var activeX = shape.ActiveXControl;

                        // Example: handle CommandButton controls specifically
                        if (activeX.Type == ControlType.CommandButton)
                        {
                            // Cast to the concrete control type
                            var button = (CommandButtonActiveXControl)activeX;

                            // Attempt to modify writable properties safely
                            SetPropertyIfWritable(button, "Caption", "Updated Caption");
                            SetPropertyIfWritable(button, "Width", 2.0);
                            SetPropertyIfWritable(button, "Height", 0.5);

                            // Attempt to modify a known read‑only property (Type) – should log a warning
                            SetPropertyIfWritable(button, "Type", ControlType.CheckBox);
                        }
                        else
                        {
                            // For other control types, demonstrate generic property handling
                            // Attempt to set a common writable property if it exists (e.g., Width)
                            SetPropertyIfWritable(activeX, "Width", 1.5);
                            SetPropertyIfWritable(activeX, "Height", 0.5);

                            // Attempt to modify a read‑only property (e.g., Type) for any control
                            SetPropertyIfWritable(activeX, "Type", ControlType.Image);
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Sets a property on the target object if the property exists and is writable.
    /// Logs a warning when the property is read‑only or missing.
    /// </summary>
    static void SetPropertyIfWritable(object target, string propertyName, object value)
    {
        if (target == null)
        {
            Console.WriteLine("Warning: Target object is null.");
            return;
        }

        PropertyInfo propInfo = target.GetType().GetProperty(propertyName);
        if (propInfo == null)
        {
            Console.WriteLine($"Warning: Property '{propertyName}' not found on type '{target.GetType().Name}'.");
            return;
        }

        if (propInfo.CanWrite)
        {
            try
            {
                // Convert the value to the property's type if necessary
                object convertedValue = Convert.ChangeType(value, propInfo.PropertyType);
                propInfo.SetValue(target, convertedValue);
                Console.WriteLine($"Info: Property '{propertyName}' set to '{value}' on '{target.GetType().Name}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Failed to set property '{propertyName}' on '{target.GetType().Name}'. {ex.Message}");
            }
        }
        else
        {
            // Property is read‑only; log a warning and skip modification
            Console.WriteLine($"Warning: Property '{propertyName}' is read‑only on '{target.GetType().Name}'. Modification skipped.");
        }
    }
}