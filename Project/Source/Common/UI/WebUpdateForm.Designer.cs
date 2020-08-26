﻿namespace Ordisoftware.HebrewCommon
{
  partial class WebUpdateForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if ( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebUpdateForm));
      this.LabelNewVersion = new System.Windows.Forms.Label();
      this.PanelBottom = new System.Windows.Forms.Panel();
      this.ActionCancel = new System.Windows.Forms.Button();
      this.ActionOk = new System.Windows.Forms.Button();
      this.SelectInstall = new System.Windows.Forms.RadioButton();
      this.SelectDownload = new System.Windows.Forms.RadioButton();
      this.SelectOpenWebPage = new System.Windows.Forms.RadioButton();
      this.PanelBottom.SuspendLayout();
      this.SuspendLayout();
      // 
      // LabelNewVersion
      // 
      resources.ApplyResources(this.LabelNewVersion, "LabelNewVersion");
      this.LabelNewVersion.Name = "LabelNewVersion";
      // 
      // PanelBottom
      // 
      this.PanelBottom.Controls.Add(this.ActionCancel);
      this.PanelBottom.Controls.Add(this.ActionOk);
      resources.ApplyResources(this.PanelBottom, "PanelBottom");
      this.PanelBottom.Name = "PanelBottom";
      // 
      // ActionCancel
      // 
      resources.ApplyResources(this.ActionCancel, "ActionCancel");
      this.ActionCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.ActionCancel.Name = "ActionCancel";
      // 
      // ActionOk
      // 
      resources.ApplyResources(this.ActionOk, "ActionOk");
      this.ActionOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.ActionOk.Name = "ActionOk";
      this.ActionOk.UseVisualStyleBackColor = true;
      // 
      // SelectInstall
      // 
      resources.ApplyResources(this.SelectInstall, "SelectInstall");
      this.SelectInstall.Checked = true;
      this.SelectInstall.Name = "SelectInstall";
      this.SelectInstall.TabStop = true;
      this.SelectInstall.UseVisualStyleBackColor = true;
      // 
      // SelectDownload
      // 
      resources.ApplyResources(this.SelectDownload, "SelectDownload");
      this.SelectDownload.Name = "SelectDownload";
      this.SelectDownload.UseVisualStyleBackColor = true;
      // 
      // SelectOpenWebPage
      // 
      resources.ApplyResources(this.SelectOpenWebPage, "SelectOpenWebPage");
      this.SelectOpenWebPage.Name = "SelectOpenWebPage";
      this.SelectOpenWebPage.UseVisualStyleBackColor = true;
      // 
      // WebUpdateForm
      // 
      this.AcceptButton = this.ActionOk;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.ActionCancel;
      this.Controls.Add(this.SelectOpenWebPage);
      this.Controls.Add(this.SelectDownload);
      this.Controls.Add(this.SelectInstall);
      this.Controls.Add(this.PanelBottom);
      this.Controls.Add(this.LabelNewVersion);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "WebUpdateForm";
      this.TopMost = true;
      this.PanelBottom.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Panel PanelBottom;
    private System.Windows.Forms.Button ActionCancel;
    private System.Windows.Forms.Button ActionOk;
    internal System.Windows.Forms.RadioButton SelectInstall;
    internal System.Windows.Forms.RadioButton SelectDownload;
    internal System.Windows.Forms.RadioButton SelectOpenWebPage;
    internal System.Windows.Forms.Label LabelNewVersion;
  }
}