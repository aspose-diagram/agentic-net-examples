using System;
using System.Collections.Generic;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        // Simple logger that writes warnings to the console
        private static void LogWarning(string message)
        {
            Console.WriteLine($"[Warning] {message}");
        }

        // Attempts to set a property on an ActiveX control only if the property is writable
        private static void SetPropertyIfWritable<T>(ActiveXControl control, string propertyName, T value)
        {
            if (control == null)
                return;

            // Use reflection to find the property on the concrete control type
            PropertyInfo propInfo = control.GetType().GetProperty(propertyName,
                BindingFlags.Public | BindingFlags.Instance);

            if (propInfo == null)
            {
                LogWarning($"Property '{propertyName}' does not exist on control type '{control.GetType().Name}'.");
                return;
            }

            if (propInfo.CanWrite)
            {
                try
                {
                    propInfo.SetValue(control, value);
                    Console.WriteLine($"Property '{propertyName}' set to '{value}'.");
                }
                catch (Exception ex)
                {
                    LogWarning($"Failed to set property '{propertyName}': {ex.Message}");
                }
            }
            else
            {
                LogWarning($"Attempt to modify read‑only property '{propertyName}'. Operation skipped.");
            }
        }

        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (replace with actual path)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains an ActiveX control
                        ActiveXControl activeX = shape.ActiveXControl;
                        if (activeX == null)
                            continue;

                        Console.WriteLine($"Processing ActiveX control of type '{activeX.Type}' on shape ID {shape.ID}.");

                        // Example of safe modifications
                        SetPropertyIfWritable(activeX, "IsEnabled", false);          // writable
                        SetPropertyIfWritable(activeX, "Width", 150.0);             // writable
                        SetPropertyIfWritable(activeX, "Height", 30.0);             // writable

                        // Attempt to modify a read‑only property (should trigger warning)
                        SetPropertyIfWritable(activeX, "Type", ControlType.CommandButton);
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }