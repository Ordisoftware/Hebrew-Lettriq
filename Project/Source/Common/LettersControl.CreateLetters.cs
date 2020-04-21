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
/// <created> 2012-10 </created>
/// <edited> 2020-04 </edited>
using System;
using System.Drawing;
using System.Windows.Forms;
using Ordisoftware.Core;

namespace Ordisoftware.HebrewCommon
{

  /// <summary>
  /// Provide Letters input panel Control class.
  /// </summary>
  public partial class LettersControl
  {

    /// <summary>
    /// Create letters buttons.
    /// </summary>
    private void CreateLetters()
    {
      try
      {
        Panel.Controls.Clear();
        int dy = 45;
        int dx = -dy;
        int x = 500 + dx;
        int y = 5;
        int n = 1;
        var colorLabel = Color.DimGray;
        var sizeLabelValue = new Size(45, 8);
        var sizeLabelKey = new Size(45, 13);
        var fontLetter = new Font("Hebrew", 20.25F, FontStyle.Bold);
        var fontValue = new Font("Microsoft Sans Serif", 6.25F);
        for ( int index = 0; index < HebrewAlphabet.Codes.Length; index++ )
        {
          // Label value
          var labelValue = new Label();
          if ( _ShowValues )
          {
            labelValue.Location = new Point(x, y + dy);
            labelValue.Size = sizeLabelKey;
            labelValue.Font = fontValue;
            labelValue.ForeColor = colorLabel;
            labelValue.BackColor = Color.Transparent;
            labelValue.Text = HebrewAlphabet.ValuesSimple[index].ToString();
            labelValue.TextAlign = ContentAlignment.MiddleCenter;
            Panel.Controls.Add(labelValue);
          }
          // Label key
          var labelKey = new Label();
          labelKey.Location = new Point(x, y + dy + ( _ShowValues ? labelValue.Height : -2 ) + 2);
          labelKey.Size = sizeLabelKey;
          labelKey.Text = HebrewAlphabet.Codes[index];
          labelKey.ForeColor = colorLabel;
          labelKey.BackColor = Color.Transparent;
          labelKey.TextAlign = ContentAlignment.MiddleCenter;
          Panel.Controls.Add(labelKey);
          // Button letter
          var buttonLetter = new Button();
          buttonLetter.Location = new Point(x, y);
          buttonLetter.Size = new Size(Math.Abs(dx), dy);
          buttonLetter.FlatStyle = FlatStyle.Flat;
          buttonLetter.FlatAppearance.BorderSize = 0;
          buttonLetter.FlatAppearance.BorderColor = SystemColors.Control;
          buttonLetter.Font = fontLetter;
          buttonLetter.Text = HebrewAlphabet.Codes[index];
          buttonLetter.BackColor = Color.Transparent;
          buttonLetter.TabStop = false;
          buttonLetter.Click += delegate (object sender, EventArgs e)
          {
            if ( Input.Text.Length < MaxLength )
            {
              Previous.Set(Input.Text, Input.SelectionStart);
              int pos = Input.SelectionStart;
              Input.Text = Input.Text.Insert(Input.SelectionStart, ( (Button)sender ).Text);
              Input.Focus();
              Input.SelectionLength = 0;
              Input.SelectionStart = pos;
            }
            OnClick(new LetterEventArgs(( (Button)sender ).Text));
          };
          Panel.Controls.Add(buttonLetter);
          // Loop
          n += 1;
          if ( n != 12 )
            x += dx;
          else
          {
            x = 500 + dx;
            y += dy + ( _ShowValues ? labelValue.Height : -2 ) + labelKey.Height + 15;
          }
        }
      }
      catch ( Exception ex )
      {
        ex.Manage(this);
      }
    }

  }

}