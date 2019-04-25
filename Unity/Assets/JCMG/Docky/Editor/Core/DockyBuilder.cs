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
using System.IO;
using System.Text;
using UnityEngine;

namespace JCMG.Docky.Editor
{
	public class DockyBuilder : IDisposable
    {
        private readonly StringBuilder _sb;
        private readonly string _filename;

        public DockyBuilder(string filename)
        {
            _filename = filename;

            _sb = new StringBuilder();
        }

        public void AppendHeader(DockyHeader header, string content)
        {
            _sb.AppendLine(string.Format("{0} {1}", ReflectionUtility.ToEnumString(header), content));
        }

        public void AppendBullet(DockyBullet bullet, string content)
        {
            _sb.AppendLine(string.Format("{0} {1}", ReflectionUtility.ToEnumString(bullet), content));
        }

        public void Append(string content)
        {
            _sb.Append(content);
        }

        public void AppendLine(string content)
        {
            _sb.AppendLine(content);
        }

        public void AppendBlankLine()
        {
	        AppendBlankLines(1);
        }

	    public void AppendBlankLines(int lines)
	    {
		    lines = Mathf.Clamp(lines, 0, int.MaxValue);

			for(var i = 0; i < lines; i++)
				_sb.AppendLine();
		}

		public void Write()
        {
            File.WriteAllText(_filename, _sb.ToString());
        }

        #region IDisposable

        public void Dispose()
        {

        }

        #endregion
    }
}
