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
using UnityEngine;

namespace JCMG.Docky.Editor.Core
{
    public class DockyDefinition
    {
	    protected virtual DockyLicenseType GetLicenseType()
	    {
		    return DockyLicenseType.None;
	    }

		/// <summary>
		/// The relative path including name to write the markdown file to. The default value shown here will
		/// be written to the root of the Assets folder.
		/// </summary>
		protected virtual string RelativeWritePath
	    {
		    get { return "readme.md"; }
	    }

	    /// <summary>
	    /// The absolute file path to write the markdown file to
	    /// </summary>
	    /// <returns></returns>
	    protected string GetAbsoluteWritePath()
	    {
		    return string.Format("{0}/{1}", ROOT_PATH, RelativeWritePath);
	    }

	    /// <summary>
	    /// The license this markdown should display.
	    /// </summary>
	    protected virtual string GetLicense(string owner = "")
	    {
		    switch (GetLicenseType())
		    {
			    case DockyLicenseType.None:
				    return string.Empty;
			    case DockyLicenseType.MIT:
				    return LicenseConstants.GetMITLICENSE(owner);
			    default:
					return string.Empty;
			}
	    }

		/// <summary>
		/// The path on your local filesystem to the Assets folder in this Unity project.
		/// </summary>
		private static string ROOT_PATH
        {
            get { return Application.dataPath; }
        }

		/// <summary>
		/// Intended to handle any logic for creating and writing the markdown file.
		/// In any derived class of DockyDefinition this must be overriden.
		/// </summary>
        public virtual void Write()
        {
			throw new NotImplementedException();
        }
    }
}