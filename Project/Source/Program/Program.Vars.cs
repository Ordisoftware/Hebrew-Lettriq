﻿/// <license>
/// This file is part of Ordisoftware Hebrew Letters.
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
/// <edited> 2020-04 </edited>
using System;
using System.Linq;
using Ordisoftware.HebrewCommon;

namespace Ordisoftware.HebrewLetters
{

  /// <summary>
  /// Provide Program class.
  /// </summary>
  static partial class Program
  {

    /// <summary>
    /// Indicate filename of the grammar guide.
    /// </summary>
    static public string GrammarGuideFilename
    {
      get
      {
        return Globals.HelpFolderPath + $"grammar-{Localizer.Language}.htm";
      }
    }

    /// <summary>
    /// Indicate filename of the letters meanings.
    /// </summary>
    static public string MeaningsFilename
    {
      get
      {
        return Globals.DocumentsFolderPath + "Alphabet-" + Localizer.Language + ".txt";
      }
    }

    /// <summary>
    /// Indicate the command line argument for word used at startup.
    /// </summary>
    static public string StartupWord
    {
      get
      {
        if ( _StartupWord == null )
        {
          string word = "";
          if ( CommandLineArguments != null && CommandLineArguments.Length == 1 )
          {
            string str = Localizer.RemoveDiacritics(CommandLineArguments[0]);
            foreach ( char c in str )
            {
              string @char = Convert.ToString(c);
              if ( HebrewAlphabet.Codes.Contains(@char) )
                word += HebrewAlphabet.SetFinal(@char, false);
            }
          }
          _StartupWord = word;
        }
        return _StartupWord;
      }
    }
    static public string _StartupWord = null;

  }

}
