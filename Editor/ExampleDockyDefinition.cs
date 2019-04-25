/*
MIT License

Copyright (c) 2018 Jeff Campbell

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using UnityEditor;
using UnityEngine;

namespace JCMG.Docky.Editor
{
	/// <summary>
	/// This is a simple example of how to write a DockyDefinition for automatically generating a markdown file from code.
	/// What is important to note is that since you have full use of the C# language to generate the contents of the markdown file
	/// you can use your own game/tool's code and reflection to generate information that would otherwise be hand-written and maintained.
	/// </summary>
	public class ExampleDockyDefinition : DockyDefinition
	{
		protected override string RelativeWritePath
		{
			get { return "JCMG/Docky/README.MD"; }
		}

		protected override DockyLicenseType GetLicenseType()
		{
			return DockyLicenseType.MIT;
		}

		public override void Write()
		{
			var path = GetAbsoluteWritePath();
			using (var db = new DockyBuilder(path))
			{
				// Main Header
				db.AppendHeader(DockyHeader.One, "Docky");
				db.AppendLine("Docky is a custom Unity editor tool to help automate documentation creation and updating.");
				db.AppendBlankLine();
				db.AppendLine(string.Format("**Last Updated:** {0:g}", DateTime.Now));
				db.AppendBlankLine();
				db.AppendLine(string.Format("**Updated with Unity Editor Version:** {0:g}", Application.unityVersion));
				db.AppendBlankLine();

				// Overview
				db.AppendHeader(DockyHeader.Two, "Overview");
				db.AppendLine(@"Docky can help automate the creation of markdown files documenting aspects of your game through two ways.");
				db.AppendHeader(DockyHeader.Three, DockyFormattingUtility.Bold("DockyDefinition"));
				db.Append("Any class derived from DockyDefinition is equivalent to a markdown file in the project. It's Write method is " +
"resonsible for creating the documentation content. There are several utilities such as DockyBuilder, DockyFormattingUtility, and ReflectionUtility " +
"that can support getting any of the markdown formatting and code meta info necessary for creating documentation content.");
				db.AppendBlankLines(2);

				db.AppendHeader(DockyHeader.Three, DockyFormattingUtility.Bold("Menu Item"));
				db.Append("Underneath the Tools menu item, there is a menu option " + DockyFormattingUtility.Bold("Create or Update All Readmes") + " ");
				db.Append("that will search out all derived classes of DockyDefinition and iterate through each one, writing each one's markdown content " +
				          "through the appropriate folder.");
				db.AppendBlankLines(2);

				// COC Integration
				db.AppendHeader(DockyHeader.Two, "Optional Unity-CoC Integration");
				db.AppendLine("If the Unity-CoC (convention over configuration) library found " +
DockyFormattingUtility.InlineLink("here", "https://github.com/jeffcampbellmakesgames/unity-coc", "Unity-CoC Repo") +
@" is present, additional menu items will be present that allow for
generating code for individual menu items per DockyDefinition derived clas. Each one of these menu items will allow for updating just that DockyDefinition derived class's
markdown file. This integration is entirely optional and could be slightly rewritten in a forked/duplicated version to write to hardcoded paths.");
				db.AppendBlankLine();

				// License
				db.AppendHeader(DockyHeader.Two, "License");
				db.AppendLine(GetLicense("Jeff Campbell"));
				db.AppendBlankLine();

				db.Write();
			}

			AssetDatabase.Refresh();
		}
	}
}
