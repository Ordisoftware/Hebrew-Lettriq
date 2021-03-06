﻿/// <license>
/// This file is part of Ordisoftware Hebrew Calendar/Letters/Words.
/// Copyright 2012-2021 Olivier Rogier. 
/// See www.ordisoftware.com for more information.
/// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
/// If a copy of the MPL was not distributed with this file, You can obtain one at 
/// https://mozilla.org/MPL/2.0/.
/// If it is not possible or desirable to put the notice in a particular file, 
/// then You may include the notice in a location(such as a LICENSE file in a 
/// relevant directory) where a recipient would be likely to look for such a notice.
/// You may add additional accurate notices of copyright ownership.
/// </license>
/// <created> 2021-05 </created>
/// <edited> 2021-05 </edited>
using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace Ordisoftware.Hebrew
{

  [Serializable]
  [Table("TermLettriq")]
  public class TermLettriq
  {
    [PrimaryKey]
    public string ID { get; set; }
    public string TermID { get; set; }
    public string Sentence { get; set; }
    public List<TermAnalysis> Analyzes
      => HebrewDatabase.Instance.TermAnalyzes
                                .Where(item => item.LettriqID == ID)
                                .OrderBy(m => m.Position).ToList();
  }

}
