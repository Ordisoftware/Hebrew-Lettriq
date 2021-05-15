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
/// <created> 2021-02 </created>
/// <edited> 2021-05 </edited>
using System;
using SQLite;
using Ordisoftware.Core;

namespace Ordisoftware.Hebrew
{

  [Serializable]
  [Table("Parashot")]
  public class Parashah
  {

    [PrimaryKey]
    public string ID { get; set; }
    public TorahBooks Book { get; set; }
    public int Number { get; set; }
    public string VerseBegin { get; set; }
    public string VerseEnd { get; set; }
    public bool IsLinkedToNext { get; set; }
    public string Name { get; set; }
    public string Hebrew { get; set; }
    public string Unicode { get; set; }
    public string Translation { get; set; }
    public string Lettriq { get; set; }
    public string Memo { get; set; }

    [Ignore]
    public Parashah Linked { get; set; }

    public string FullReferenceBegin
      => $"{(int)Book}.{VerseBegin}";

    public string ToString(bool useHebrewFont)
      => $"Sefer {Book} " +
         $"{VerseBegin} - {VerseEnd} " +
         $"Parashah n°{Number} " +
         $"{Name}{( IsLinkedToNext ? "*" : string.Empty )} " +
         $"({( useHebrewFont ? Hebrew : Unicode )}) : " +
         $"{Translation.GetOrEmpty()} ; " +
         $"{Lettriq.GetOrEmpty()} ; " +
         $"{Memo.GetOrEmpty()} ";

    public override string ToString()
      => ToString(false);

    public string ToStringLinked()
      => Name + ( Linked != null ? " - " + Linked.Name : "" );

    public object Clone()
    {
      return new Parashah(Book, Number, Name, Unicode, VerseBegin, VerseEnd, IsLinkedToNext, Translation, Lettriq);
    }

    public Parashah()
    {
    }

    public Parashah(TorahBooks book,
                    int number,
                    string name,
                    string unicode,
                    string verseBegin,
                    string verseEnd,
                    bool isLinkedToNext = false,
                    string translation = "",
                    string lettriq = "")
    {
      Book = book;
      Number = number;
      Name = name;
      Unicode = unicode;
      Hebrew = HebrewAlphabet.ToHebrewFont(unicode);
      VerseBegin = verseBegin;
      VerseEnd = verseEnd;
      IsLinkedToNext = isLinkedToNext;
      Translation = translation;
      Lettriq = lettriq;
      Memo = string.Empty;
      ID = $"{(int)book}.{number}";
    }

  }

}
