namespace PrintGenerater.Services.Setup;
public class TexTemplateSelector
{
    public void Select(IPrintMasterEntity print)
    {
        SelectTemplate(print);
    }
    private void SelectTemplate(IPrintMasterEntity print)
    {
        var templateFolder = print.GetDirectory(print.PrintId, "tex-base");
        var templates = Directory.GetFiles(templateFolder, "*.svg");

        if (templates.Length == 0)
        {
            var bases = Directory.GetFiles(templateFolder.Replace(print.PrintId.ToString(), "000000"), "*.svg");
            foreach (var file in bases)
            {
                File.Copy(file, file.Replace("000000", print.PrintId.ToString()));
            }
            templates = Directory.GetFiles(templateFolder, "*.svg");
        }
        if (templates.Length > 1)
        {
            Console.WriteLine("Multiple templates found. Please choose one:");
            for (int i = 0; i < templates.Length; i++)
            {
                Console.WriteLine($"{i + 1}: {Path.GetFileName(templates[i])}");
            }

            while (true)
            {
                Console.Write("Enter the number of the template to use: ");
                if (!int.TryParse(Console.ReadLine(), out int choice) ||
                    choice < 1 || choice > templates.Length)
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                    continue; // re-prompt
                }

                var selectedPath = templates[choice - 1];
                var destPath = Path.Combine(Path.GetDirectoryName(selectedPath)!, "template.svg");

                try
                {
                    // Copy with overwrite
                    File.Copy(selectedPath, destPath, overwrite: true);
                    Console.WriteLine($"Copied {Path.GetFileName(selectedPath)} to template.svg");

                    // Delete other files
                    for (int i = 0; i < templates.Length; i++)
                    {
                        if (destPath != templates[i])
                        {
                            File.Delete(templates[i]);
                            Console.WriteLine($"Deleted: {Path.GetFileName(templates[i])}");
                        }
                    }

                    break; // exit loop when done
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while copying or deleting files: {ex.Message}");
                    break;
                }
            }

        }
    }
}
