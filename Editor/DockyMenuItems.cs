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

using System.IO;
using JCMG.Docky.Editor.Core;
using JCMG.Docky.Editor.Utility;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

#pragma warning disable 1587

namespace JCMG.Docky.Editor
{
	public static class DockyMenuItems
    {
#if JCMG_COC
		[MenuItem("Tools/Docky/Refresh Readme Generation Options", priority = 0)]

        public static void RefreshReadmeMenuItems()
        {
            // the generated filepath
	        var filename = "DockyGeneratedMenuItems.cs";
			var relativeScriptFilePath = string.Format("{0}/{1}", DockyInitializer.GetGeneratedFolder(), filename);
	        var scriptFilePath = string.Format("{0}/{1}", Application.dataPath, relativeScriptFilePath.Remove(0, 7));

			// The class string
			var sb = new StringBuilder();
            sb.AppendLine("// This class is Auto-Generated");
            sb.AppendLine("using UnityEditor;");
            sb.AppendLine("");
            sb.AppendLine("public static class DockyGeneratedMenuItems");
            sb.AppendLine("{");
            sb.AppendLine("");

            var counter = 50;
            var dockyDefs = ReflectionUtility.GetAllDerivedInstancesOfType<DockyDefinition>().ToList();
            dockyDefs.Sort(CompareDockyDefinitions);

            foreach (var dockyDefinition in dockyDefs)
            {
                sb.AppendLine(string.Format("    [MenuItem(\"Tools/Docky/Create or Update {0}\", priority = {1})]", dockyDefinition.GetType().Name, counter++));
                sb.AppendLine(string.Format("    private static void CreateOrUpdate{0}()", dockyDefinition.GetType().Name));
                sb.AppendLine("    {");
                sb.AppendLine(string.Format(@"
        var dockyDef = new {0}();
        dockyDef.Write();
", dockyDefinition.GetType().FullName));
                

                sb.AppendLine("    }");
                sb.AppendLine("");
            }

            sb.AppendLine("");
            sb.AppendLine("}");

            // writes the class and imports it so it is visible in the Project window
			if(File.Exists(scriptFilePath))
				File.Delete(scriptFilePath);

            File.WriteAllText(scriptFilePath, sb.ToString(), Encoding.UTF8);
            AssetDatabase.ImportAsset(relativeScriptFilePath);

        }

        private static int CompareDockyDefinitions(DockyDefinition definition, DockyDefinition dockyDefinition1)
        {
            return definition.GetType().Name.CompareTo(dockyDefinition1.GetType().Name);
        }
#endif

		[MenuItem("Tools/Docky/Create or Update All Readmes", priority = 10000)]
        public static void CreateOrUpdateReadme()
        {
            var dockyDefs = ReflectionUtility.GetAllDerivedInstancesOfType<DockyDefinition>();
            foreach (var dockyDefinition in dockyDefs)
            {
                dockyDefinition.Write();
            }
        }
    }
}