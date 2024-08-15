<<<<<<< HEAD
﻿namespace uWidgets.Core.Models.Settings;

/// <summary>
/// Application theme settings.
/// </summary>
/// <param name="DarkMode">
/// Should the application use the dark mode
/// <para><c>null</c> to use system settings</para>
/// </param>
/// <param name="AccentColor">
/// Accent color in HEX format
/// <para><c>null</c> to use system accent color</para>
/// </param>
/// <param name="Transparency">
/// Should the application use transparency effects
/// </param>
/// <param name="Monochrome">
/// Should the application use monochrome theme
/// </param>
=======
﻿namespace uWidgets.Core.Models.Settings;

/// <summary>
/// Application theme settings.
/// </summary>
/// <param name="DarkMode">
/// Should the application use the dark mode
/// <para><c>null</c> to use system settings</para>
/// </param>
/// <param name="AccentColor">
/// Accent color in HEX format
/// <para><c>null</c> to use system accent color</para>
/// </param>
/// <param name="Transparency">
/// Should the application use transparency effects
/// </param>
/// <param name="Monochrome">
/// Should the application use monochrome theme
/// </param>
>>>>>>> parent of 15524c5 (Delete src directory)
public record Theme(bool? DarkMode, string? AccentColor, bool Transparency, bool Monochrome);