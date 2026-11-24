using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find ActiveX controls
            foreach (Page page in diagram.Pages)
            {
                // Use zero‑based indexer for shapes collection
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Skip shapes without an ActiveX control
                    if (shape.ActiveXControl == null)
                        continue;

                    // Retrieve the generic ActiveX control
                    ActiveXControl activeX = shape.ActiveXControl;
                    string controlTypeName = activeX.Type.ToString();

                    // Use the shape's NameU (string) for logging
                    string shapeName = shape.NameU ?? "UnnamedShape";

                    // Process based on specific control type
                    switch (activeX.Type)
                    {
                        case ControlType.TextBox:
                            // Cast to TextBoxActiveXControl to access Text property
                            TextBoxActiveXControl txtCtrl = (TextBoxActiveXControl)activeX;
                            string oldText = txtCtrl.Text;
                            string newText = "Updated Text";
                            txtCtrl.Text = newText;
                            LogChange(shapeName, controlTypeName, "Text", oldText, newText);
                            break;

                        case ControlType.CheckBox:
                            // Cast to CheckBoxActiveXControl to access Value property
                            CheckBoxActiveXControl chkCtrl = (CheckBoxActiveXControl)activeX;
                            CheckValueType oldValue = chkCtrl.Value;
                            // Toggle the checkbox state (Checked <-> Unchecked)
                            CheckValueType newValue = (oldValue == CheckValueType.Checked) ? (CheckValueType)0 : CheckValueType.Checked;
                            chkCtrl.Value = newValue;
                            LogChange(shapeName, controlTypeName, "Value", oldValue.ToString(), newValue.ToString());
                            break;

                        case ControlType.CommandButton:
                            // Cast to CommandButtonActiveXControl to access Caption property
                            CommandButtonActiveXControl btnCtrl = (CommandButtonActiveXControl)activeX;
                            string oldCaption = btnCtrl.Caption;
                            string newCaption = "Clicked!";
                            btnCtrl.Caption = newCaption;
                            LogChange(shapeName, controlTypeName, "Caption", oldCaption, newCaption);
                            break;

                        // Add handling for other control types as needed
                        default:
                            // For unsupported types, just log that they were found
                            Console.WriteLine($"Shape '{shapeName}' contains ActiveX control of type '{controlTypeName}' which is not processed.");
                            break;
                    }
                }
            }

            // Save the modified diagram with explicit format argument
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Logs a property change with previous and new values.
    /// </summary>
    /// <param name="shapeName">Name of the shape containing the control.</param>
    /// <param name="controlType">ActiveX control type.</param>
    /// <param name="propertyName">Name of the property that changed.</param>
    /// <param name="oldValue">Previous value.</param>
    /// <param name="newValue">New value.</param>
    private static void LogChange(string shapeName, string controlType, string propertyName, string oldValue, string newValue)
    {
        Console.WriteLine($"[Audit] Shape: '{shapeName}', Control: '{controlType}', Property: '{propertyName}' changed from '{oldValue}' to '{newValue}'.");
    }
}