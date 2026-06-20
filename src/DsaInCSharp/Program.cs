using DsaInCSharp.Core;

// =====================================================================
//  ENTRY POINT
// =====================================================================
// This is where the program starts. Its only job is to launch the menu.
// The menu finds every lesson automatically, so this file stays tiny and
// almost never needs to change — even as the course grows to 50+ lessons.
// =====================================================================

Console.OutputEncoding = System.Text.Encoding.UTF8; // lets us print emojis/symbols nicely

var menu = new LessonMenu();
menu.Show();
