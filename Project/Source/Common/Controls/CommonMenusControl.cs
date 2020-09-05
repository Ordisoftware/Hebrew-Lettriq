﻿/// <license>
/// This file is part of Ordisoftware Hebrew Calendar/Letters/Words.
/// Copyright 2016-2020 Olivier Rogier.
/// See www.ordisoftware.com for more information.
/// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
/// If a copy of the MPL was not distributed with this file, You can obtain one at 
/// https://mozilla.org/MPL/2.0/.
/// If it is not possible or desirable to put the notice in a particular file, 
/// then You may include the notice in a location(such as a LICENSE file in a 
/// relevant directory) where a recipient would be likely to look for such a notice.
/// You may add additional accurate notices of copyright ownership.
/// </license>
/// <created> 2016-04 </created>
/// <edited> 2020-08 </edited>
using System;

using System.Windows.Forms;

namespace Ordisoftware.HebrewCommon
{

  public partial class CommonMenusControl : UserControl
  {

    public EventHandler AboutBoxHandler;
    public EventHandler WebCheckUpdateHandler;

    public CommonMenusControl()
    {
      InitializeComponent();
      check(ActionDownloadHebrewCalendar);
      check(ActionDownloadHebrewLetters);
      check(ActionDownloadHebrewWords);
      void check(ToolStripMenuItem item)
      {
        if ( item.Text.Contains(Globals.AssemblyTitle) ) MenuInformation.DropDownItems.Remove(item);
      }
    }

    private void ActionAbout_Click(object sender, EventArgs e)
    {
      AboutBoxHandler?.Invoke(this, EventArgs.Empty);
    }

    private void ActionWebCheckUpdate_Click(object sender, EventArgs e)
    {
      WebCheckUpdateHandler?.Invoke(this, EventArgs.Empty);
    }

    private void ActionWebReleaseNotes_Click(object sender, EventArgs e)
    {
      SystemManager.OpenApplicationReleaseNotes();
    }

    private void ActionWebHome_Click(object sender, EventArgs e)
    {
      SystemManager.OpenApplicationHome();
    }

    private void ActionWebContact_Click(object sender, EventArgs e)
    {
      SystemManager.OpenContactPage();
    }

    private void ActionCreateGitHubIssue_Click(object sender, EventArgs e)
    {
      SystemManager.CreateGitHubIssue();
    }

    private void ActionOpenWebsiteURL_Click(object sender, EventArgs e)
    {
      SystemManager.OpenWebLink((string)( (ToolStripItem)sender ).Tag);
    }

  }

}
