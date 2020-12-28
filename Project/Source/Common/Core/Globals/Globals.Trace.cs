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
/// <created> 2016-04 </created>
/// <edited> 2020-08 </edited>
using System;

namespace Ordisoftware.Core
{

  /// <summary>
  /// Provide global variables.
  /// </summary>
  static public partial class Globals
  {

    /// <summary>
    /// Indicate the trace file mode.
    /// </summary>
    static public TraceFileRollOverMode TraceFileMode { get; set; }
     = TraceFileRollOverMode.Daily;

    /// <summary>
    /// Indicate the trace files keep count.
    /// </summary>
    static public int TraceFilesKeepCount { get; set; }
     = 7;

    /// <summary>
    /// Indicate the log/trace directory name.
    /// </summary>
    static public string TraceDirectoryName { get; set; }
      = "Logs";

    /// <summary>
    /// Indicate the trace file code.
    /// </summary>
    static public string TraceFileCode { get; set; }
      = "Trace";

    /// <summary>
    /// Indicate the trace file extension.
    /// </summary>
    static public string TraceFileExtension { get; set; }
      = ".log";

  }

}
