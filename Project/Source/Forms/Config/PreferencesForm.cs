﻿/// <license>
/// This file is part of Ordisoftware Hebrew letters.
/// Copyright 2016-2021 Olivier Rogier.
/// See www.ordisoftware.com for more information.
/// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
/// If a copy of the MPL was not distributed with this file, You can obtain one at 
/// https://mozilla.org/MPL/2.0/.
/// If it is not possible or desirable to put the notice in a particular file, 
/// then You may include the notice in a location(such as a LICENSE file in a 
/// relevant directory) where a recipient would be likely to look for such a notice.
/// You may add additional accurate notices of copyright ownership.
/// </license>
/// <created> 2019-09 </created>
/// <edited> 2021-02 </edited>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Ordisoftware.Core;
using KVPImageExportTarget = System.Collections.Generic.KeyValuePair<Ordisoftware.Core.ImageExportTarget, string>;

namespace Ordisoftware.Hebrew.Letters
{

  partial class PreferencesForm : Form
  {

    static private readonly Properties.Settings Settings = Program.Settings;

    static private bool LanguageChanged;
    static private bool DoReset;

    static public void Run()
    {
      var form = new PreferencesForm();
      form.ShowDialog();
      while ( LanguageChanged || DoReset )
      {
        LanguageChanged = false;
        DoReset = false;
        form = new PreferencesForm();
        form.ShowDialog();
      }
    }

    private NullSafeOfStringDictionary<DataExportTarget> ExportTarget
      = ExportHelper.CreateExportTargets(DataExportTarget.XML);

    private bool IsReady;

    private PreferencesForm()
    {
      InitializeComponent();
      Icon = MainForm.Instance.Icon;
      SaveSettingsDialog.InitialDirectory = Program.Settings.GetExportDirectory();
      OpenSettingsDialog.InitialDirectory = SaveSettingsDialog.InitialDirectory;
      SaveSettingsDialog.Filter = ExportTarget.CreateFilters();
      OpenSettingsDialog.Filter = SaveSettingsDialog.Filter;
    }

    private void PreferencesForm_Load(object sender, EventArgs e)
    {
      this.CenterToMainFormElseScreen();
    }

    private void PreferencesForm_Shown(object sender, EventArgs e)
    {
      TopMost = MainForm.Instance.TopMost;
      ActiveControl = ActionClose;
      BringToFront();
      UpdateLanguagesButtons();
      EditVacuumAtStartup.Checked = Settings.VacuumAtStartup;
      EditDebuggerEnabled.Checked = Settings.DebuggerEnabled;
      EditLogEnabled.Checked = Settings.TraceEnabled;
      EditCheckUpdateAtStartup.Checked = Settings.CheckUpdateAtStartup;
      EditAutoSortAnalysisMeanings.Checked = Settings.AutoSortAnalysisMeanings;
      EditFontSize.Value = Settings.FontSizeSentence;
      EditMaxLength.Value = Settings.HebrewTextBoxMaxLength;
      EditWebLinksMenuEnabled.Checked = Settings.WebLinksMenuEnabled;
      EditCheckUpdateAtStartupInterval.Value = Settings.CheckUpdateAtStartupDaysInterval;
      EditUsageStatisticsEnabled.Checked = Settings.UsageStatisticsEnabled;
      EditVolume.Value = Settings.ApplicationVolume;
      EditExportFolder.Text = Settings.ExportFolder;
      EditAutoOpenExportFolder.Checked = Settings.AutoOpenExportFolder;
      EditAutoOpenExportedFile.Checked = Settings.AutoOpenExportedFile;
      EditImageExportFileFormat.Fill(Program.ImageExportTargets, Settings.ExportImagePreferredTarget);
      LoadColors();
      IsReady = true;
    }

    private void PreferencesForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if ( DoReset )
      {
        EditFontSize_ValueChanged(null, null);
        MainForm.Instance.InitializeTheme();
      }
      else
        SaveSettings();
    }

    private void LoadColors()
    {
      EditLettersBack.BackColor = Settings.ColorLettersPanel;
      EditWordBack.BackColor = Settings.ColorHebrewWordTextBox;
      EditAnalyseBack.BackColor = Settings.ColorMeaningsPanel;
      EditEditableBack.BackColor = Settings.ColorSentenceTextBox;
      EditReadonlyBack.BackColor = Settings.ColorGematriaTextBox;
    }

    private void SaveColors()
    {
      Settings.ColorLettersPanel = EditLettersBack.BackColor;
      Settings.ColorHebrewWordTextBox = EditWordBack.BackColor;
      Settings.ColorMeaningsPanel = EditAnalyseBack.BackColor;
      Settings.ColorSentenceTextBox = EditEditableBack.BackColor;
      Settings.ColorGematriaTextBox = EditReadonlyBack.BackColor;
    }

    private void SaveSettings()
    {
      Settings.VacuumAtStartup = EditVacuumAtStartup.Checked;
      Settings.DebuggerEnabled = EditDebuggerEnabled.Checked;
      Settings.TraceEnabled = EditLogEnabled.Checked;
      Settings.CheckUpdateAtStartup = EditCheckUpdateAtStartup.Checked;
      Settings.AutoSortAnalysisMeanings = EditAutoSortAnalysisMeanings.Checked;
      Settings.FontSizeSentence = EditFontSize.Value;
      Settings.HebrewTextBoxMaxLength = EditMaxLength.Value;
      Settings.WebLinksMenuEnabled = EditWebLinksMenuEnabled.Checked;
      Settings.CheckUpdateAtStartupDaysInterval = (int)EditCheckUpdateAtStartupInterval.Value;
      Settings.ApplicationVolume = EditVolume.Value;
      Settings.UsageStatisticsEnabled = EditUsageStatisticsEnabled.Checked;
      Settings.ExportFolder = EditExportFolder.Text;
      Settings.AutoOpenExportFolder = EditAutoOpenExportFolder.Checked;
      Settings.AutoOpenExportedFile = EditAutoOpenExportedFile.Checked;
      SaveColors();
      Settings.Save();
    }

    private void EditDebuggerEnabled_CheckedChanged(object sender, EventArgs e)
    {
      if ( !EditDebuggerEnabled.Checked )
        EditLogEnabled.Checked = false;
      DebugManager.Enabled = EditDebuggerEnabled.Checked;
      EditLogEnabled.Enabled = DebugManager.Enabled;
    }

    private void EditLogEnabled_CheckedChanged(object sender, EventArgs e)
    {
      DebugManager.TraceEnabled = EditLogEnabled.Checked;
      CommonMenusControl.Instance.ActionViewLog.Enabled = DebugManager.TraceEnabled;
      StatisticsForm.Instance.ActionViewLog.Enabled = DebugManager.TraceEnabled;
    }

    private void EditUsageStatisticsEnabled_CheckedChanged(object sender, EventArgs e)
    {
      CommonMenusControl.Instance.ActionViewStats.Enabled = EditUsageStatisticsEnabled.Checked;
      AboutBox.Instance.ActionViewStats.Enabled = EditUsageStatisticsEnabled.Checked;
      StatisticsForm.Instance.Timer.Enabled = EditUsageStatisticsEnabled.Checked;
      if ( !EditUsageStatisticsEnabled.Checked )
        StatisticsForm.Instance.Close();
    }

    private void UpdateLanguagesButtons()
    {
      if ( Settings.LanguageSelected == Language.EN )
      {
        ActionSelectLangEN.BackColor = SystemColors.ControlLightLight;
        ActionSelectLangFR.BackColor = SystemColors.Control;
      }
      if ( Settings.LanguageSelected == Language.FR )
      {
        ActionSelectLangFR.BackColor = SystemColors.ControlLightLight;
        ActionSelectLangEN.BackColor = SystemColors.Control;
      }
    }

    private void ActionSelectLangEN_Click(object sender, EventArgs e)
    {
      if ( Settings.LanguageSelected == Language.EN ) return;
      string temp = MainForm.Instance.EditLetters.Input.Text;
      MainForm.Instance.ActionClear.PerformClick();
      Settings.LanguageSelected = Language.EN;
      Program.UpdateLocalization();
      UpdateLanguagesButtons();
      LanguageChanged = true;
      MainForm.Instance.EditLetters.Input.Text = temp;
      MainForm.Instance.EditLetters.Focus(LettersControlFocusSelect.Keep);
      Close();
    }

    private void ActionSelectLangFR_Click(object sender, EventArgs e)
    {
      if ( Settings.LanguageSelected == Language.FR ) return;
      string temp = MainForm.Instance.EditLetters.Input.Text;
      MainForm.Instance.ActionClear.PerformClick();
      Settings.LanguageSelected = Language.FR;
      Program.UpdateLocalization();
      UpdateLanguagesButtons();
      LanguageChanged = true;
      MainForm.Instance.EditLetters.Input.Text = temp;
      MainForm.Instance.EditLetters.Focus(LettersControlFocusSelect.Keep);
      Close();
    }

    private void EditFontSize_ValueChanged(object sender, EventArgs e)
    {
      MainForm.Instance.EditSentence.Font = new Font("Microsoft Sans Serif", (float)EditFontSize.Value);
    }

    private void ActionResetSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if ( !DisplayManager.QueryYesNo(SysTranslations.AskToResetPreferences.GetLang()) ) return;
      IsReady = false;
      long starttime = Settings.BenchmarkStartingApp;
      long loadtime = Settings.BenchmarkLoadData;
      var lastupdate = Settings.CheckUpdateLastDone;
      var lastvacuum = Settings.VacuumLastDone;
      Settings.Reset();
      Settings.SetFirstAndUpgradeFlagsOff();
      Settings.CheckUpdateLastDone = lastupdate;
      Settings.VacuumLastDone = lastvacuum;
      Settings.LanguageSelected = Languages.Current;
      Settings.BenchmarkStartingApp = starttime;
      Settings.BenchmarkLoadData = loadtime;
      Settings.RestoreMainForm();
      Settings.Save();
      MainForm.Instance.EditSentence.Font = new Font("Microsoft Sans Serif", (float)Settings.FontSizeSentence);
      Program.GrammarGuideForm.CenterToMainFormElseScreen();
      Program.MethodNoticeForm.CenterToMainFormElseScreen();
      DoReset = true;
      IsReady = true;
      Close();
    }

    private void EditVolume_ValueChanged(object sender, EventArgs e)
    {
      if ( !IsReady ) return;
      MediaMixer.SetApplicationVolume(System.Diagnostics.Process.GetCurrentProcess().Id, EditVolume.Value);
      LabelVolumeValue.Text = EditVolume.Value + "%";
      Settings.ApplicationVolume = EditVolume.Value;
      Settings.Save();
      DisplayManager.DoSound(Globals.ClipboardSoundFilePath);
    }

    private void EditAutoOpenExportedFile_CheckedChanged(object sender, EventArgs e)
    {
      if ( EditAutoOpenExportedFile.Checked && EditAutoOpenExportFolder.Checked )
        EditAutoOpenExportFolder.Checked = false;
    }

    private void EditAutoOpenExportFolder_CheckedChanged(object sender, EventArgs e)
    {
      if ( EditAutoOpenExportedFile.Checked && EditAutoOpenExportFolder.Checked )
        EditAutoOpenExportedFile.Checked = false;
    }
    private void EditImageExportFileFormat_Format(object sender, ListControlConvertEventArgs e)
    {
      e.Value = ( (KVPImageExportTarget)e.ListItem ).Key.ToString();
    }

    private void EditImageExportFileFormat_SelectedIndexChanged(object sender, EventArgs e)
    {
      if ( !IsReady ) return;
      Settings.ExportImagePreferredTarget = ( (KVPImageExportTarget)EditImageExportFileFormat.SelectedItem ).Key;
    }

    private void ActionSelectExportFolder_Click(object sender, EventArgs e)
    {
      SystemManager.TryCatch(() =>
      {
        FolderBrowserDialog.SelectedPath = Settings.GetExportDirectory();
      });
      if ( FolderBrowserDialog.ShowDialog() == DialogResult.OK )
        EditExportFolder.Text = FolderBrowserDialog.SelectedPath;
    }

    private void ActionResetExportFolder_Click(object sender, EventArgs e)
    {
      if ( DisplayManager.QueryYesNo(SysTranslations.AskToResetParameter.GetLang()) )
        EditExportFolder.Text = (string)Settings.Properties[nameof(Settings.ExportFolder)].DefaultValue;
    }

    private void EditColor_Click(object sender, EventArgs e)
    {
      var panel = (Panel)sender;
      DialogColor.Color = panel.BackColor;
      if ( DialogColor.ShowDialog() == DialogResult.Cancel ) return;
      panel.BackColor = DialogColor.Color;
      SaveColors();
      MainForm.Instance.InitializeTheme();
    }

    private void ActionUseColorsPastel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      EditLettersBack.BackColor  = Color.LightYellow;
      EditWordBack.BackColor     = Color.AliceBlue;
      EditAnalyseBack.BackColor  = Color.MintCream;
      EditEditableBack.BackColor = SystemColors.Window;
      EditReadonlyBack.BackColor = Color.LavenderBlush;
      SaveColors();
      MainForm.Instance.InitializeTheme();
    }

    private void ActionUseColorsSystem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      EditLettersBack.BackColor = SystemColors.Control;
      EditWordBack.BackColor    = SystemColors.Window;
      EditAnalyseBack.BackColor = SystemColors.Window;
      EditEditableBack.BackColor= SystemColors.Window;
      EditReadonlyBack.BackColor= SystemColors.Control;
      SaveColors();
      MainForm.Instance.InitializeTheme();
    }

    private void ActionImportSettings_Click(object sender, EventArgs e)
    {
      OpenSettingsDialog.FileName = string.Empty;
      if ( OpenSettingsDialog.ShowDialog() != DialogResult.OK ) return;
      StatisticsForm.Instance.Hide();
      long starttime = Settings.BenchmarkStartingApp;
      long loadtime = Settings.BenchmarkLoadData;
      try
      {
        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
        string context = Properties.Settings.Default.Context["GroupName"].ToString();
        var xmldata = XDocument.Load(OpenSettingsDialog.FileName);
        var settings = xmldata.XPathSelectElements("//" + context);
        var section = config.GetSectionGroup("userSettings").Sections[context].SectionInformation;
        section.SetRawXml(settings.Single().ToString());
        config.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("userSettings");
        Settings.Reload();
        Settings.BenchmarkStartingApp = starttime;
        Settings.BenchmarkLoadData = loadtime;
        Settings.Save();
        Settings.Retrieve();
        Program.UpdateLocalization();
        Settings.SetFirstAndUpgradeFlagsOff();
        LanguageChanged = true;
        DoReset = true;
        Close();
      }
      catch ( Exception ex )
      {
        DisplayManager.ShowError(ex.Message);
        Settings.Reload();
      }
    }

    private void ActionExportSettings_Click(object sender, EventArgs e)
    {
      SaveSettingsDialog.FileName = "Settings";
      if ( SaveSettingsDialog.ShowDialog() != DialogResult.OK ) return;
      TabControl.SelectedIndex = 0;
      SaveSettings();
      Settings.Store();
      var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
      config.SaveAs(SaveSettingsDialog.FileName);
    }

    private void ActionOpenTheme_Click(object sender, EventArgs e)
    {
      SystemManager.TryCatch(() =>
      {
        OpenThemeDialog.InitialDirectory = Settings.GetExportDirectory();
      });
      if ( OpenThemeDialog.ShowDialog() != DialogResult.OK ) return;
      var items = new NullSafeOfStringDictionary<string>();
      if ( !items.LoadKeyValuePairs(OpenThemeDialog.FileName, "=") ) return;
      TabPageTheme.Controls.OfType<Panel>().ToList().ForEach(panel =>
      {
        string name = panel.Name.Substring(4);
        if ( items.ContainsKey(name) )
          panel.BackColor = ColorTranslator.FromHtml(items[name]);
      });
      SaveColors();
      MainForm.Instance.InitializeTheme();
    }

    private void ActionSaveTheme_Click(object sender, EventArgs e)
    {
      SystemManager.TryCatch(() =>
      {
        SaveThemeDialog.InitialDirectory = Settings.GetExportDirectory();
        SaveThemeDialog.FileName = "Theme.ini";
      });
      if ( SaveThemeDialog.ShowDialog() != DialogResult.OK ) return;
      var items = new List<string>();
      TabPageTheme.Controls.OfType<Panel>().ToList().ForEach(panel =>
      {
        items.Add(panel.Name.Substring(4) + "=" + ColorTranslator.ToHtml(panel.BackColor));
      });
      File.WriteAllLines(SaveThemeDialog.FileName, items);
    }

  }

}
