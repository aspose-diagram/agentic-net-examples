using System;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

namespace ActiveXReadOnlyGuard
{
    // Helper class that safely sets properties on ActiveX controls
    public static class ActiveXPropertyGuard
    {
        // Attempts to set a property; logs a warning if the property is read‑only or does not exist
        public static void SetPropertyIfWritable(ActiveXControl control, string propertyName, object value)
        {
            if (control == null)
            {
                Console.WriteLine("Warning: ActiveX control instance is null.");
                return;
            }

            // Get the type of the concrete control (e.g., CommandButtonActiveXControl)
            Type ctrlType = control.GetType();

            // Find the property (public instance)
            PropertyInfo propInfo = ctrlType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propInfo == null)
            {
                Console.WriteLine($"Warning: Property '{propertyName}' does not exist on control type '{ctrlType.Name}'.");
                return;
            }

            // Check if the property can be written to
            if (!propInfo.CanWrite)
            {
                Console.WriteLine($"Warning: Property '{propertyName}' on control type '{ctrlType.Name}' is read‑only. Modification skipped.");
                return;
            }

            // Attempt to set the value, handling type conversion if necessary
            try
            {
                // Convert the value to the property's type if needed
                object convertedValue = Convert.ChangeType(value, propInfo.PropertyType);
                propInfo.SetValue(control, convertedValue);
                Console.WriteLine($"Info: Property '{propertyName}' set to '{value}' on control type '{ctrlType.Name}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Failed to set property '{propertyName}' on control type '{ctrlType.Name}'. Exception: {ex.Message}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the input Visio diagram (replace with actual path)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes to find ActiveX controls
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape hosts an ActiveX control
                        if (shape.ActiveXControl != null)
                        {
                            ActiveXControl activeX = shape.ActiveXControl;

                            // Example: attempt to modify Width (read‑write) and Type (read‑only)
                            ActiveXPropertyGuard.SetPropertyIfWritable(activeX, "Width", 150.0);   // Width in points
                            ActiveXPropertyGuard.SetPropertyIfWritable(activeX, "Height", 30.0);   // Height in points
                            ActiveXPropertyGuard.SetPropertyIfWritable(activeX, "Type", ControlType.CheckBox); // Read‑only, will log warning

                            // Additional property examples
                            ActiveXPropertyGuard.SetPropertyIfWritable(activeX, "IsEnabled", true); // Read‑write
                            ActiveXPropertyGuard.SetPropertyIfWritable(activeX, "IsLocked", false); // Read‑write
                        }
                    }
                }

                // Save the modified diagram (output path can be adjusted)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}