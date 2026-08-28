using System;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

namespace ActiveXReadOnlyGuardExample
{
    // Helper class that checks for read‑only properties before modifying an ActiveX control
    public static class ActiveXGuard
    {
        // Attempts to set a property value; logs a warning if the property is read‑only
        public static void TrySetProperty<T>(ActiveXControl control, string propertyName, T value)
        {
            if (control == null)
                return;

            // Get the property info using reflection
            PropertyInfo propInfo = control.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propInfo == null)
            {
                Console.WriteLine($"[Warning] Property '{propertyName}' does not exist on control type '{control.GetType().Name}'.");
                return;
            }

            // Check if the property can be written to
            if (!propInfo.CanWrite)
            {
                Console.WriteLine($"[Warning] Attempted to modify read‑only property '{propertyName}' on control type '{control.GetType().Name}'. Modification skipped.");
                return;
            }

            // Set the property value
            try
            {
                propInfo.SetValue(control, value);
                Console.WriteLine($"[Info] Property '{propertyName}' set to '{value}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to set property '{propertyName}': {ex.Message}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes to find ActiveX controls
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        ActiveXControl activeX = shape.ActiveXControl;
                        if (activeX == null)
                            continue;

                        Console.WriteLine($"[Info] Found ActiveX control of type '{activeX.Type}' on shape ID {shape.ID}.");

                        // Example: attempt to modify a writable property (Width)
                        ActiveXGuard.TrySetProperty(activeX, "Width", 2.0);

                        // Example: attempt to modify a read‑only property (Data)
                        // This should trigger a warning and skip the modification
                        byte[] dummyData = new byte[] { 0x01, 0x02, 0x03 };
                        ActiveXGuard.TrySetProperty(activeX, "Data", dummyData);
                    }
                }

                // Save the diagram after processing
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