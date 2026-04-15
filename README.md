# Markdown to Word

Eine Windows-Anwendung auf Basis von C# und WinForms, die Markdown-Dateien in `DOCX` konvertiert.

Die Anwendung wurde für Visual Studio 2022 erstellt und bietet eine einfache Desktop-Oberfläche für den täglichen Einsatz unter Windows. Markdown-Dateien koennen per Dateidialog oder per Drag-and-Drop ausgewaehlt werden. Vor der Konvertierung kann die Schriftart fuer normalen Text und fuer Code / Source getrennt festgelegt werden. Die gewaehlten Schriften werden in einer `config.json` gespeichert und bei jedem Start der Anwendung automatisch wieder geladen.

Nach der Konvertierung wird Microsoft Word direkt mit der erzeugten `DOCX`-Datei gestartet.

## Funktionen

- Auswahl einer Markdown-Datei per Dateidialog
- Auswahl einer Markdown-Datei per Drag-and-Drop
- Getrennte Auswahl der Schriftart fuer:
  - normalen Text
  - Code / Source
- Persistente Speicherung der ausgewaehlten Schriftarten in `config.json`
- Konvertierung von `.md` und `.markdown` nach `.docx`
- Automatisches Oeffnen der erzeugten Datei in Microsoft Word
- Einfache Windows-Desktop-Oberflaeche fuer Visual Studio 2022

## Verwendete Technologien

- C#
- .NET 8
- Windows Forms
- Visual Studio 2022
- [DocSharp.Markdown](https://www.nuget.org/packages/DocSharp.Markdown/)
- [DocSharp.Docx](https://www.nuget.org/packages/DocSharp.Docx/)
- [DocumentFormat.OpenXml](https://www.nuget.org/packages/DocumentFormat.OpenXml/)

## Projektziel

Das Projekt soll eine leicht bedienbare Windows-Anwendung bereitstellen, mit der Markdown-Inhalte ohne manuelle Nacharbeit in ein Word-Dokument ueberfuehrt werden koennen. Dabei ist besonders wichtig, dass:

- die Datei schnell ausgewaehlt werden kann
- Code-Bloecke und Fliesstext unterschiedliche Schriftarten verwenden koennen
- die zuletzt gewaehlen Einstellungen dauerhaft gespeichert werden
- das Ergebnis nach der Konvertierung sofort in Word geoeffnet wird

## Bedienung

1. Anwendung starten.
2. Markdown-Datei auf eine der folgenden Arten auswaehlen:
   - per Klick auf den Dateiauswahl-Button
   - per Klick auf den Drag-and-Drop-Bereich
   - per Drag-and-Drop einer `.md`- oder `.markdown`-Datei in das Fenster
3. Schriftart fuer normalen Text im ersten Dropdown waehlen.
4. Schriftart fuer Code / Source im zweiten Dropdown waehlen.
5. Auf `Convert` klicken.
6. Die Anwendung erzeugt im gleichen Verzeichnis wie die Eingabedatei eine `DOCX`-Datei mit gleichem Dateinamen.
7. Anschliessend wird Microsoft Word gestartet und die erzeugte Datei geoeffnet.

## Beispiel

Eingabedatei:

- `C:\Dokumente\Beispiel\notizen.md`

Ausgabedatei:

- `C:\Dokumente\Beispiel\notizen.docx`

## Speicherung der Einstellungen

Die Anwendung legt neben der ausfuehrbaren Datei eine `config.json` an. Darin werden die zuletzt gewaehlten Schriftarten gespeichert.

Beispiel:

```json
{
  "TextFont": "Calibri",
  "SourceFont": "Consolas"
}
```

Wenn die Datei fehlt oder nicht gelesen werden kann, verwendet die Anwendung Standardwerte.

Standardwerte:

- Text: `Calibri`
- Source: `Consolas`

## Technische Umsetzung

### 1. Benutzeroberflaeche

Die Oberflaeche basiert auf WinForms und enthaelt:

- einen Drag-and-Drop-Bereich fuer Markdown-Dateien
- ein Textfeld fuer den ausgewaehlten Dateipfad
- einen Button fuer die Dateiauswahl
- zwei Dropdowns fuer die Schriftauswahl
- einen `Convert`-Button
- eine Statusanzeige

Die installierten Windows-Schriften werden beim Start der Anwendung ueber `InstalledFontCollection` geladen und alphabetisch sortiert in die Dropdowns eingetragen.

### 2. Konvertierung mit DocSharp

Die eigentliche Markdown-zu-DOCX-Konvertierung erfolgt ueber `DocSharp.Markdown.MarkdownConverter`.

Ablauf:

1. Die Markdown-Datei wird ueber `MarkdownSource.FromFile(...)` eingelesen.
2. `DocSharp` erstellt daraus eine `DOCX`-Datei.
3. Anschliessend wird das erzeugte Dokument erneut ueber `DocumentFormat.OpenXml` geoeffnet.
4. Die Styles fuer Fliesstext und Code werden nachbearbeitet, damit die in der Anwendung ausgewaehlten Fonts sicher im Dokument gesetzt sind.

### 3. Nachbearbeitung der Schriftarten

Da die Anwendung zwei unterschiedliche Schriftarten unterstuetzt, werden die von `DocSharp` erzeugten Styles gezielt angepasst.

Dabei werden unter anderem folgende Styles beruecksichtigt:

- `MDParagraph`
- `MDQuote`
- `MDHeading1` bis `MDHeading6`
- `MDBulletedListItem`
- `MDOrderedListItem`
- `MDDefinitionItem`
- `MDDefinitionTerm`
- `MDCodeBlock`
- `MDCodeInline`

Zusaetzlich werden die Fonts direkt auf Runs in Code-Bloecken und Inline-Code gesetzt, damit die Monospace-Schrift auch dort konsistent angewendet wird.

### 4. Start von Microsoft Word

Nach erfolgreicher Konvertierung versucht die Anwendung zuerst `winword.exe` direkt zu starten. Falls das nicht moeglich ist, wird die erzeugte Datei alternativ ueber den Standard-Dateityp-Mechanismus von Windows geoeffnet.

## Projektstruktur

```text
MarkdownToDocx.sln
MarkdownToDocxApp/
  AppConfig.cs
  Form1.cs
  Form1.Designer.cs
  MarkdownDocxConverter.cs
  MarkdownToDocxApp.csproj
  Program.cs
```

### Wichtige Dateien

- `MarkdownToDocx.sln`
  - Solution-Datei fuer Visual Studio 2022
- `MarkdownToDocxApp/Program.cs`
  - Einstiegspunkt der Anwendung
- `MarkdownToDocxApp/Form1.cs`
  - Hauptlogik der Benutzeroberflaeche
- `MarkdownToDocxApp/Form1.Designer.cs`
  - WinForms-Layout und Steuerelemente
- `MarkdownToDocxApp/AppConfig.cs`
  - Konfigurationsmodell fuer die gespeicherten Schriftarten
- `MarkdownToDocxApp/MarkdownDocxConverter.cs`
  - Markdown-zu-DOCX-Konvertierung und Font-Anpassung
- `.gitignore`
  - Ausschluss von Build- und benutzerspezifischen Dateien

## Voraussetzungen

- Windows
- .NET 8 SDK
- Visual Studio 2022
- Microsoft Word, wenn das Dokument nach der Konvertierung automatisch geoeffnet werden soll

## Projekt in Visual Studio 2022 oeffnen

1. Repository klonen oder lokal oeffnen.
2. `MarkdownToDocx.sln` in Visual Studio 2022 oeffnen.
3. NuGet-Pakete wiederherstellen lassen.
4. Projekt starten.

## Build ueber die Kommandozeile

```powershell
dotnet build .\MarkdownToDocxApp\MarkdownToDocxApp.csproj
```

## NuGet-Abhaengigkeiten

Die Anwendung verwendet folgende Pakete:

- `DocSharp.Docx` `0.18.1`
- `DocSharp.Markdown` `0.18.1`
- `DocumentFormat.OpenXml` `3.5.1`

## Verhalten bei Fehlern

Die Anwendung zeigt eine Fehlermeldung an, wenn:

- keine gueltige Markdown-Datei ausgewaehlt wurde
- die Eingabedatei nicht existiert
- die Konvertierung fehlschlaegt
- das erzeugte `DOCX` nicht korrekt geoeffnet oder nachbearbeitet werden kann

## Bekannte Einschraenkungen

- Die Anwendung ist fuer Windows ausgelegt.
- Das automatische Oeffnen in Word setzt eine funktionierende Word-Installation oder eine passende Dateizuordnung voraus.
- Die Qualitaet der Konvertierung haengt von den von `DocSharp` unterstuetzten Markdown-Elementen ab.
- Sehr spezielle Markdown-Erweiterungen oder exotische Layout-Anforderungen koennen Nacharbeit im Word-Dokument erfordern.

## Moegliche Erweiterungen

- Auswahl eines separaten Ausgabeordners
- Batch-Konvertierung mehrerer Markdown-Dateien
- Vorschau des Markdown-Inhalts in der Anwendung
- Erweiterte Optionen fuer Seitenlayout, Abstaende und Formatvorlagen
- Export nach weiteren Formaten
- Sprachumschaltung der Benutzeroberflaeche

## Lizenz

Fuer dieses Repository ist aktuell keine eigene Lizenzdatei hinterlegt. Falls das Projekt veroeffentlicht oder weitergegeben werden soll, sollte eine passende Lizenzdatei ergaenzt werden.
