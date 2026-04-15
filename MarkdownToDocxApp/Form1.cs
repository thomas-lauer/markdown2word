using System.Diagnostics;
using System.Drawing.Text;
using System.Text.Json;

namespace MarkdownToDocxApp;

public partial class Form1 : Form
{
    private readonly string _configPath = Path.Combine(AppContext.BaseDirectory, "config.json");
    private AppConfig _config = new();

    public Form1()
    {
        InitializeComponent();
        InitializeForm();
    }

    private void InitializeForm()
    {
        AllowDrop = true;
        DragEnter += SharedDragEnter;
        DragDrop += SharedDragDrop;

        dropPanel.AllowDrop = true;
        dropPanel.DragEnter += SharedDragEnter;
        dropPanel.DragDrop += SharedDragDrop;
        dropPanel.Click += (_, _) => ChooseMarkdownFile();

        LoadFonts();
        LoadConfiguration();
        UpdateUiState();
    }

    private void LoadFonts()
    {
        using var installedFonts = new InstalledFontCollection();
        var fontNames = installedFonts.Families
            .Select(f => f.Name)
            .OrderBy(name => name, StringComparer.CurrentCultureIgnoreCase)
            .ToArray();

        textFontComboBox.Items.AddRange(fontNames);
        sourceFontComboBox.Items.AddRange(fontNames);
    }

    private void LoadConfiguration()
    {
        if (File.Exists(_configPath))
        {
            try
            {
                var json = File.ReadAllText(_configPath);
                _config = JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
            }
            catch
            {
                _config = new AppConfig();
            }
        }

        textFontComboBox.SelectedItem = FindFontOrDefault(_config.TextFont, "Calibri");
        sourceFontComboBox.SelectedItem = FindFontOrDefault(_config.SourceFont, "Consolas");
    }

    private string FindFontOrDefault(string? preferredFont, string fallback)
    {
        var availableFonts = textFontComboBox.Items.Cast<string>().ToArray();

        return availableFonts.FirstOrDefault(name => string.Equals(name, preferredFont, StringComparison.OrdinalIgnoreCase))
            ?? availableFonts.FirstOrDefault(name => string.Equals(name, fallback, StringComparison.OrdinalIgnoreCase))
            ?? availableFonts.FirstOrDefault()
            ?? fallback;
    }

    private void SaveConfiguration()
    {
        _config.TextFont = textFontComboBox.SelectedItem?.ToString() ?? "Calibri";
        _config.SourceFont = sourceFontComboBox.SelectedItem?.ToString() ?? "Consolas";

        var json = JsonSerializer.Serialize(_config, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_configPath, json);
    }

    private void ChooseMarkdownFile()
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "Markdown files (*.md;*.markdown)|*.md;*.markdown|All files (*.*)|*.*",
            Title = "Markdown-Datei auswählen"
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            SetSelectedFile(dialog.FileName);
        }
    }

    private void SetSelectedFile(string filePath)
    {
        selectedFileTextBox.Text = filePath;
        statusLabel.Text = "Bereit zur Konvertierung.";
        UpdateUiState();
    }

    private void UpdateUiState()
    {
        var hasValidFile = File.Exists(selectedFileTextBox.Text) &&
            (selectedFileTextBox.Text.EndsWith(".md", StringComparison.OrdinalIgnoreCase) ||
             selectedFileTextBox.Text.EndsWith(".markdown", StringComparison.OrdinalIgnoreCase));

        convertButton.Enabled = hasValidFile;
    }

    private void SharedDragEnter(object? sender, DragEventArgs e)
    {
        if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
        {
            var paths = (string[]?)e.Data.GetData(DataFormats.FileDrop);
            if (paths?.Any(IsMarkdownFile) == true)
            {
                e.Effect = DragDropEffects.Copy;
                return;
            }
        }

        e.Effect = DragDropEffects.None;
    }

    private void SharedDragDrop(object? sender, DragEventArgs e)
    {
        if (e.Data?.GetData(DataFormats.FileDrop) is string[] paths)
        {
            var markdownFile = paths.FirstOrDefault(IsMarkdownFile);
            if (markdownFile is not null)
            {
                SetSelectedFile(markdownFile);
            }
        }
    }

    private static bool IsMarkdownFile(string filePath)
    {
        return File.Exists(filePath) &&
               (filePath.EndsWith(".md", StringComparison.OrdinalIgnoreCase) ||
                filePath.EndsWith(".markdown", StringComparison.OrdinalIgnoreCase));
    }

    private void browseButton_Click(object sender, EventArgs e)
    {
        ChooseMarkdownFile();
    }

    private void fontComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        SaveConfiguration();
    }

    private async void convertButton_Click(object sender, EventArgs e)
    {
        try
        {
            SaveConfiguration();
            ToggleBusyState(true);
            statusLabel.Text = "Konvertierung läuft...";

            var markdownPath = selectedFileTextBox.Text;
            var outputPath = Path.ChangeExtension(markdownPath, ".docx");

            await Task.Run(() => MarkdownDocxConverter.Convert(markdownPath, outputPath, _config));

            statusLabel.Text = $"Konvertiert: {outputPath}";
            OpenInWord(outputPath);
        }
        catch (Exception ex)
        {
            statusLabel.Text = "Fehler bei der Konvertierung.";
            MessageBox.Show(this, ex.Message, "Konvertierung fehlgeschlagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            ToggleBusyState(false);
        }
    }

    private void ToggleBusyState(bool isBusy)
    {
        browseButton.Enabled = !isBusy;
        convertButton.Enabled = !isBusy && File.Exists(selectedFileTextBox.Text);
        textFontComboBox.Enabled = !isBusy;
        sourceFontComboBox.Enabled = !isBusy;
        UseWaitCursor = isBusy;
    }

    private static void OpenInWord(string outputPath)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "winword.exe",
                Arguments = $"\"{outputPath}\"",
                UseShellExecute = true
            });
        }
        catch
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = outputPath,
                UseShellExecute = true
            });
        }
    }

    private void selectedFileTextBox_TextChanged(object sender, EventArgs e)
    {
        UpdateUiState();
    }
}
