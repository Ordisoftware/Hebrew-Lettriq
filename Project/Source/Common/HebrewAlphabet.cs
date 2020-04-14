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
/// <edited> 2020-03 </edited>
using System;
using System.Collections.Generic;

namespace Ordisoftware.HebrewCommon
{

  /// <summary>
  /// Provide hebrew letters class to manage hebrew font and unicode chars
  /// from text available at www.fourmilab.ch/etexts/www/hebrew/Bible.
  /// </summary>
  static public class HebrewAlphabet
  {

    /// <summary>
    /// Indicate letters keyboard codes.
    /// </summary>
    static public readonly string[] Codes = 
    {
      "a", "b", "g", "d", "h", "v", "z", "x", "u", "y", "k",
      "l", "m", "n", "c", "i", "p", "j", "q", "r", ">", "t"
    };

    /// <summary>
    /// Indicate final letters disabled keys values.
    /// </summary>
    static private readonly Dictionary<char, char> FinalDisable = new Dictionary<char, char>()
    {
      { '!', 'k' }, { ',', 'm' }, { ']', 'n' }, { '[', 'p' }, { '/', 'j' }
    };

    /// <summary>
    /// Indicate final letters enabled keys values.
    /// </summary>
    static private Dictionary<char, char> FinalEnable = new Dictionary<char, char>()
    {
      { 'k', '!' }, { 'm', ',' }, { 'n', ']' }, { 'p', '[' }, { 'j', '/' }
    };

    /// <summary>
    /// Set final letter.
    /// </summary>
    static public string SetFinal(string str, bool enable)
    {
      var array = enable ? FinalEnable : FinalDisable;
      str = str.Trim();
      if ( str.Length == 0 ) return str;
      char c = str[0];
      foreach ( var v in array )
        if ( c == v.Key )
        {
          c = v.Value;
          break;
        }
      str = c + str.Remove(0, 1);
      return str;
    }

    /// <summary>
    /// Convert all final letters to non final.
    /// </summary>
    static public string UnFinalAll(string str)
    {
      foreach ( var v in FinalDisable )
        str = str.Replace(v.Key, v.Value);
      return str;
    }

    /// <summary>
    /// Convert unicode hebrew chars to hebrew font chars.
    /// </summary>
    static public string ConvertToHebrewFont(string str)
    {
      string result = "";
      foreach ( char c in str.RemoveDiacritics() )
        result = ConvertToKey(c) + result;
      return result;
    }

    /// <summary>
    /// Convert hebrew font chars to unicode hebrew chars.
    /// </summary>
    static public string ConvertToUnicode(string str)
    {
      string result = "";
      foreach ( char c in str )
        result = ConvertToUnicode(c) + result;
      return result;
    }

    /// <summary>
    /// Convert unicode hebrew chars to hebrew font chars.
    /// </summary>
    static public char ConvertToKey(char c)
    {
      switch ( c )
      {
        case 'א': return 'a';
        case 'ב': return 'b';
        case 'ג': return 'g';
        case 'ד': return 'd';
        case 'ה': return 'h';
        case 'ו': return 'v';
        case 'ז': return 'z';
        case 'ח': return 'x';
        case 'ט': return 'u';
        case 'י': return 'y';
        case 'כ': return 'k';
        case 'ך': return '!';
        case 'ל': return 'l';
        case 'מ': return 'm';
        case 'ם': return ',';
        case 'נ': return 'n';
        case 'ן': return ']';
        case 'ס': return 'c';
        case 'ע': return 'i';
        case 'פ': return 'p';
        case 'ף': return '[';
        case 'צ': return 'j';
        case 'ץ': return '/';
        case 'ק': return 'q';
        case 'ר': return 'r';
        case 'ש': return '>';
        case 'ת': return 't';
        case ':': return '.';
        case '-': return ' ';
        default: return ' ';
      }
    }

    /// <summary>
    /// Convert hebrew font chars to unicode hebrew chars.
    /// </summary>
    static public char ConvertToUnicode(char c)
    {
      switch ( c )
      {
        case 'a': return 'א';
        case 'b': return 'ב';
        case 'g': return 'ג';
        case 'd': return 'ד';
        case 'h': return 'ה';
        case 'v': return 'ו';
        case 'z': return 'ז';
        case 'x': return 'ח';
        case 'u': return 'ט';
        case 'y': return 'י';
        case 'k': return 'כ';
        case '!': return 'ך';
        case 'l': return 'ל';
        case 'm': return 'מ';
        case ',': return 'ם';
        case 'n': return 'נ';
        case ']': return 'ן';
        case 'c': return 'ס';
        case 'i': return 'ע';
        case 'p': return 'פ';
        case '[': return 'ף';
        case 'j': return 'צ';
        case '/': return 'ץ';
        case 'q': return 'ק';
        case 'r': return 'ר';
        case '>': return 'ש';
        case 't': return 'ת';
        case '.': return ':';
        case ' ': return ' ';
        default: return ' ';
      }
    }

    /// <summary>
    /// Indicate letters simple values.
    /// </summary>
    static public readonly int[] ValuesSimple =
    {
      1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20,
      30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400
    };

    /// <summary>
    /// Indicate letters full values.
    /// </summary>
    static public readonly int[] ValuesFull =
    {
      111, 412, 83, 434, 6, 12, 67, 418, 419, 20, 100,
      74, 90, 106, 120, 130, 81, 104, 186, 510, 360, 406
    };

    /// <summary>
    /// Indicate letters names in phonetic.
    /// </summary>
    static public readonly Dictionary<string, string[]> Names
      = new Dictionary<string, string[]>()
      {
        {
          "en", new string[]
          { "Alef", "Bet", "Gimel", "Dalet", "He", "Vav", "Zayin", "'Het", "Tet", "Yod", "Kaf",
            "Lamed", "Mem", "Nun", "Samek", "'Ayin", "Pay", "Tsadi", "Qof", "Resh", "Shin", "Tav"
          }
        },
        {
          "fr", new string[]
          { "Alef", "Bet", "Guimel", "Dalet", "Hé", "Vav", "Zayin", "'Het", "Tet", "Youd", "Kaf",
            "Lamed", "Mem", "Noun", "Samek", "'Ayin", "Pé", "Tsadi", "Qouf", "Resh", "Shin", "Tav"
          }
        }
      };

    /// <summary>
    /// Indicate letters names in hebrew font chars.
    /// </summary>
    static public readonly string[] HebrewFontNames =
    {
      "pla", "tyb", "lmyg", "tld", "ah", "vv", "]yz", "tx", "tu", "dvy", "pk",
      "dml", ",m", "]vn", "!mc", "]yi", "hp", "ydj", "pvq", ">r", "]y>", "vt",
    };

  }

}