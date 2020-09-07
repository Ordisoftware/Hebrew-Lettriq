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
/// <edited> 2020-08 </edited>
using System;
using System.IO;
using System.Windows.Forms;

namespace Ordisoftware.HebrewCommon
{

  /// <summary>
  /// Provide global variables.
  /// </summary>
  static public partial class Globals
  {

    /// <summary>
    /// Indicate Hebrew Common directory name.
    /// </summary>
    static public readonly string HebrewCommonDirectoryName
      = "Hebrew Common";

    /// <summary>
    /// Indicate generated executable bin directory name.
    /// </summary>
    static public string BinDirectoryName { get; set; }
      = "Bin";

    /// <summary>
    /// Indicate generated executable debug directory.
    /// </summary>
    static public string DebugDirectory { get; set; }
      = Path.Combine(BinDirectoryName, "Debug");

    /// <summary>
    /// Indicate generated executable release directory.
    /// </summary>
    static public string ReleaseDirectory { get; set; }
      = Path.Combine(BinDirectoryName, "Release");

    /// <summary>
    /// Indicate the root folder path of the application.
    /// </summary>
    static public string RootFolderPath
      => Directory.GetParent(Path.GetDirectoryName(Application.ExecutablePath
                                                              .Replace(DebugDirectory, BinDirectoryName)
                                                              .Replace(ReleaseDirectory, BinDirectoryName))).FullName;

    /// <summary>
    /// Indicate the application help folder.
    /// </summary>
    static public string HelpFolderPath
      => Path.Combine(RootFolderPath, "Help");

    /// <summary>
    /// Indicate the application documents folder.
    /// </summary>
    static public string DocumentsFolderPath
      => Path.Combine(RootFolderPath, "Documents");

    /// <summary>
    /// Indicate the application guides folder.
    /// </summary>
    static public string GuidesFolderPath
      => Path.Combine(DocumentsFolderPath, "Guides");

    /// <summary>
    /// Indicate the application web links folder.
    /// </summary>
    static public string WebLinksFolderPath
      => Path.Combine(DocumentsFolderPath, "WebLinks");

    /// <summary>
    /// Indicate the application web providers folder.
    /// </summary>
    static public string WebProvidersFolderPath
      => Path.Combine(DocumentsFolderPath, "WebProviders");

    /// <summary>
    /// Indicate the user applicationtrace folder.
    /// </summary>
    static public string TraceFolderPath
      => Path.Combine(UserDataFolderPath, TraceDirectoryName);

    /// <summary>
    /// Indicate the user applicationdatabase folder.
    /// </summary>
    static public string DatabaseFolderPath
      => UserDataFolderPath;

    /// <summary>
    /// Indicate the user documents folder path.
    /// </summary>
    static public string UserDocumentsFolderPath
      => CreateSpecialFolderPath(Environment.SpecialFolder.MyDocuments, AssemblyTitle);

    /// <summary>
    /// Indicate the user data folder in local.
    /// </summary>
    static public string UserLocalDataFolderPath
      => CreateSpecialFolderPath(Environment.SpecialFolder.LocalApplicationData, "");

    /// <summary>
    /// Indicate the user data folder in roaming.
    /// </summary>
    static public string UserDataFolderPath
      => CreateSpecialFolderPath(Environment.SpecialFolder.ApplicationData, AssemblyTitle);

    /// <summary>
    /// Indicate the hebrew common data folder in roaming.
    /// </summary>
    static public string UserDataCommonFolderPath
      => CreateSpecialFolderPath(Environment.SpecialFolder.ApplicationData, HebrewCommonDirectoryName);

    /// <summary>
    /// Indicate the hebrew common data folder in program data.
    /// </summary>
    static public string ProgramDataFolderPath
      => CreateSpecialFolderPath(Environment.SpecialFolder.CommonApplicationData, HebrewCommonDirectoryName);

    /// <summary>
    /// Indicate a path for in a special folder.
    /// </summary>
    static private string CreateSpecialFolderPath(Environment.SpecialFolder folder, string directory)
      => Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(folder),
                                                AssemblyCompany,
                                                directory)).FullName;

  }

}