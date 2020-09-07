﻿/// <license>
/// This file is part of Ordisoftware Hebrew Calendar/Letters/Words.
/// Copyright 2012-2020 Olivier Rogier.
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
/// <edited> 2020-09 </edited>
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Ordisoftware.HebrewCommon
{

  /// <summary>
  /// Provide display manager.
  /// </summary>
  static public partial class DisplayManager
  {

    private const int WM_SYSCOMMAND = 0x0112;
    private const int SC_SCREENSAVE = 0xF140;
    private const int SPI_GETSCREENSAVERRUNNING = 0x0072;

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
      public int left;
      public int top;
      public int right;
      public int bottom;
    }

    [DllImport("user32.dll")]
    static private extern bool SystemParametersInfo(int action, int param, ref int retval, int updini);

    [DllImport("user32.dll")]
    static private extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);

    [DllImport("user32.dll")]
    static private extern IntPtr GetForegroundWindow();

    static public bool IsForegroundFullScreen(Screen screen = null)
    {
      if ( screen == null ) screen = Screen.PrimaryScreen;
      RECT rect = new RECT();
      GetWindowRect(new HandleRef(null, GetForegroundWindow()), ref rect);
      var rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
      return rectangle.Contains(screen.Bounds);
    }

    static public bool IsScreensaverActive()
    {
      int active = 1;
      SystemParametersInfo(SPI_GETSCREENSAVERRUNNING, 0, ref active, 0);
      return active != 0;
    }

    static public bool IsForegroundFullScreenOrScreensaver()
    {
      return IsForegroundFullScreen() || IsScreensaverActive();
    }

  }

}