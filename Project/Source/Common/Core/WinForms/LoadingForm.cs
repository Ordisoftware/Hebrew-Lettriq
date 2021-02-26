﻿/// <license>
/// This file is part of Ordisoftware Core Library.
/// Copyright 2004-2021 Olivier Rogier.
/// See www.ordisoftware.com for more information.
/// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
/// If a copy of the MPL was not distributed with this file, You can obtain one at 
/// https://mozilla.org/MPL/2.0/.
/// If it is not possible or desirable to put the notice in a particular file, 
/// then You may include the notice in a location(such as a LICENSE file in a 
/// relevant directory) where a recipient would be likely to look for such a notice.
/// You may add additional accurate notices of copyright ownership.
/// </license>
/// <created> 2019-01 </created>
/// <edited> 2020-08 </edited>
using System;
using System.Windows.Forms;

namespace Ordisoftware.Core
{

  partial class LoadingForm : Form
  {

    public const int QuantaTotal = 10;

    static public LoadingForm Instance { get; private set; }

    static LoadingForm()
    {
      Instance = new LoadingForm();
    }

    private bool UseQuanta;
    private int QuantaLevel;
    private int CurrentQuanta;

    public event Action Progressing;

    private LoadingForm()
    {
      InitializeComponent();
      Icon = Globals.MainForm?.Icon;
    }

    private void LoadingForm_Load(object sender, EventArgs e)
    {
      Relocalize();
    }

    public void Relocalize()
    {
      LabelTitle.Text = Globals.AssemblyTitle;
    }

    public void Initialize(string text, int count, int minimum, bool quantify = true)
    {
      this.CenterToMainFormElseScreen();
      if ( minimum == 0 || ( minimum > 0 && count > minimum ) )
      {
        Show();
        BringToFront();
      }
      if ( count <= 0 ) count = 1;
      UseQuanta = quantify && count > QuantaTotal;
      QuantaLevel = UseQuanta ? count / QuantaTotal : 1;
      CurrentQuanta = 0;
      ProgressBar.Value = 0;
      ProgressBar.Maximum = UseQuanta ? QuantaTotal - 1 : count - 1;
      ProgressBar.Refresh();
      LabelOperation.Text = text;
      Refresh();
      Application.DoEvents();
    }

    public void DoProgress()
    {
      bool process = true;
      if ( UseQuanta )
      {
        CurrentQuanta++;
        if ( CurrentQuanta >= QuantaLevel )
          CurrentQuanta = 0;
        else
          process = false;
      }
      if ( process )
      {
        ProgressBar.PerformStep();
        ProgressBar.Refresh();
        Refresh();
      }
      SystemManager.TryCatchManage(() => Progressing?.Invoke());
    }

    public void SetProgress(int index)
    {
      ProgressBar.Value = index > ProgressBar.Maximum ? ProgressBar.Maximum : index;
      ProgressBar.Refresh();
    }

  }

}
