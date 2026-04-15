namespace MarkdownToDocxApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private TableLayoutPanel mainLayoutPanel = null!;
    private Label titleLabel = null!;
    private Label descriptionLabel = null!;
    private Panel dropPanel = null!;
    private Label dropPanelLabel = null!;
    private TextBox selectedFileTextBox = null!;
    private Button browseButton = null!;
    private Label textFontLabel = null!;
    private ComboBox textFontComboBox = null!;
    private Label sourceFontLabel = null!;
    private ComboBox sourceFontComboBox = null!;
    private Button convertButton = null!;
    private Label statusLabel = null!;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        mainLayoutPanel = new TableLayoutPanel();
        titleLabel = new Label();
        descriptionLabel = new Label();
        dropPanel = new Panel();
        dropPanelLabel = new Label();
        selectedFileTextBox = new TextBox();
        browseButton = new Button();
        textFontLabel = new Label();
        textFontComboBox = new ComboBox();
        sourceFontLabel = new Label();
        sourceFontComboBox = new ComboBox();
        convertButton = new Button();
        statusLabel = new Label();
        mainLayoutPanel.SuspendLayout();
        dropPanel.SuspendLayout();
        SuspendLayout();
        // 
        // mainLayoutPanel
        // 
        mainLayoutPanel.ColumnCount = 3;
        mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        mainLayoutPanel.Controls.Add(titleLabel, 0, 0);
        mainLayoutPanel.Controls.Add(descriptionLabel, 0, 1);
        mainLayoutPanel.Controls.Add(dropPanel, 0, 2);
        mainLayoutPanel.Controls.Add(selectedFileTextBox, 0, 3);
        mainLayoutPanel.Controls.Add(browseButton, 1, 3);
        mainLayoutPanel.Controls.Add(textFontLabel, 0, 4);
        mainLayoutPanel.Controls.Add(textFontComboBox, 0, 5);
        mainLayoutPanel.Controls.Add(sourceFontLabel, 2, 4);
        mainLayoutPanel.Controls.Add(sourceFontComboBox, 2, 5);
        mainLayoutPanel.Controls.Add(convertButton, 0, 6);
        mainLayoutPanel.Controls.Add(statusLabel, 0, 7);
        mainLayoutPanel.Dock = DockStyle.Fill;
        mainLayoutPanel.Padding = new Padding(24);
        mainLayoutPanel.RowCount = 8;
        mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 170F));
        mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
        mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
        mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        mainLayoutPanel.Name = "mainLayoutPanel";
        mainLayoutPanel.TabIndex = 0;
        // 
        // titleLabel
        // 
        mainLayoutPanel.SetColumnSpan(titleLabel, 3);
        titleLabel.AutoSize = true;
        titleLabel.Dock = DockStyle.Fill;
        titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        titleLabel.Location = new Point(27, 24);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(746, 42);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Markdown nach DOCX";
        titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // descriptionLabel
        // 
        mainLayoutPanel.SetColumnSpan(descriptionLabel, 3);
        descriptionLabel.AutoSize = true;
        descriptionLabel.Dock = DockStyle.Fill;
        descriptionLabel.ForeColor = SystemColors.GrayText;
        descriptionLabel.Location = new Point(27, 66);
        descriptionLabel.Name = "descriptionLabel";
        descriptionLabel.Size = new Size(746, 36);
        descriptionLabel.TabIndex = 1;
        descriptionLabel.Text = "Wählen Sie eine Markdown-Datei per Dialog oder ziehen Sie sie in den Bereich unt" +
    "en.";
        descriptionLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dropPanel
        // 
        mainLayoutPanel.SetColumnSpan(dropPanel, 3);
        dropPanel.BackColor = Color.FromArgb(245, 247, 250);
        dropPanel.BorderStyle = BorderStyle.FixedSingle;
        dropPanel.Controls.Add(dropPanelLabel);
        dropPanel.Cursor = Cursors.Hand;
        dropPanel.Dock = DockStyle.Fill;
        dropPanel.Location = new Point(27, 105);
        dropPanel.Name = "dropPanel";
        dropPanel.Padding = new Padding(18);
        dropPanel.Size = new Size(746, 164);
        dropPanel.TabIndex = 2;
        // 
        // dropPanelLabel
        // 
        dropPanelLabel.Dock = DockStyle.Fill;
        dropPanelLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
        dropPanelLabel.Location = new Point(18, 18);
        dropPanelLabel.Name = "dropPanelLabel";
        dropPanelLabel.Size = new Size(708, 126);
        dropPanelLabel.TabIndex = 0;
        dropPanelLabel.Text = "Markdown-Datei hier ablegen oder klicken, um eine Datei auszuwählen";
        dropPanelLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // selectedFileTextBox
        // 
        selectedFileTextBox.Dock = DockStyle.Fill;
        selectedFileTextBox.Location = new Point(27, 275);
        selectedFileTextBox.Name = "selectedFileTextBox";
        selectedFileTextBox.PlaceholderText = "Keine Datei ausgewählt";
        selectedFileTextBox.ReadOnly = true;
        selectedFileTextBox.Size = new Size(295, 23);
        selectedFileTextBox.TabIndex = 3;
        selectedFileTextBox.TextChanged += selectedFileTextBox_TextChanged;
        // 
        // browseButton
        // 
        browseButton.Dock = DockStyle.Fill;
        browseButton.Location = new Point(328, 275);
        browseButton.Name = "browseButton";
        browseButton.Size = new Size(144, 36);
        browseButton.TabIndex = 4;
        browseButton.Text = "Datei auswählen";
        browseButton.UseVisualStyleBackColor = true;
        browseButton.Click += browseButton_Click;
        // 
        // textFontLabel
        // 
        textFontLabel.AutoSize = true;
        textFontLabel.Dock = DockStyle.Fill;
        textFontLabel.Location = new Point(27, 314);
        textFontLabel.Name = "textFontLabel";
        textFontLabel.Size = new Size(295, 24);
        textFontLabel.TabIndex = 5;
        textFontLabel.Text = "Schriftart für Fließtext";
        textFontLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // textFontComboBox
        // 
        textFontComboBox.Dock = DockStyle.Fill;
        textFontComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        textFontComboBox.FormattingEnabled = true;
        textFontComboBox.Location = new Point(27, 341);
        textFontComboBox.Name = "textFontComboBox";
        textFontComboBox.Size = new Size(295, 23);
        textFontComboBox.TabIndex = 6;
        textFontComboBox.SelectedIndexChanged += fontComboBox_SelectedIndexChanged;
        // 
        // sourceFontLabel
        // 
        sourceFontLabel.AutoSize = true;
        sourceFontLabel.Dock = DockStyle.Fill;
        sourceFontLabel.Location = new Point(478, 314);
        sourceFontLabel.Name = "sourceFontLabel";
        sourceFontLabel.Size = new Size(295, 24);
        sourceFontLabel.TabIndex = 7;
        sourceFontLabel.Text = "Schriftart für Code / Source";
        sourceFontLabel.TextAlign = ContentAlignment.BottomLeft;
        // 
        // sourceFontComboBox
        // 
        sourceFontComboBox.Dock = DockStyle.Fill;
        sourceFontComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        sourceFontComboBox.FormattingEnabled = true;
        sourceFontComboBox.Location = new Point(478, 341);
        sourceFontComboBox.Name = "sourceFontComboBox";
        sourceFontComboBox.Size = new Size(295, 23);
        sourceFontComboBox.TabIndex = 8;
        sourceFontComboBox.SelectedIndexChanged += fontComboBox_SelectedIndexChanged;
        // 
        // convertButton
        // 
        mainLayoutPanel.SetColumnSpan(convertButton, 3);
        convertButton.Dock = DockStyle.Fill;
        convertButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        convertButton.Location = new Point(27, 383);
        convertButton.Name = "convertButton";
        convertButton.Size = new Size(746, 48);
        convertButton.TabIndex = 9;
        convertButton.Text = "Convert";
        convertButton.UseVisualStyleBackColor = true;
        convertButton.Click += convertButton_Click;
        // 
        // statusLabel
        // 
        mainLayoutPanel.SetColumnSpan(statusLabel, 3);
        statusLabel.AutoSize = true;
        statusLabel.Dock = DockStyle.Fill;
        statusLabel.ForeColor = SystemColors.GrayText;
        statusLabel.Location = new Point(27, 434);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(746, 42);
        statusLabel.TabIndex = 10;
        statusLabel.Text = "Keine Datei ausgewählt.";
        statusLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 500);
        Controls.Add(mainLayoutPanel);
        MinimumSize = new Size(816, 539);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Markdown to DOCX Converter";
        mainLayoutPanel.ResumeLayout(false);
        mainLayoutPanel.PerformLayout();
        dropPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
}
