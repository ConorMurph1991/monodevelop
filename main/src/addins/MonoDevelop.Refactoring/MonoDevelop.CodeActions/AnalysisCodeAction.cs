// 
// AnalysisQuickFix.cs
//  
// Author:
//       Mike Krüger <mkrueger@novell.com>
// 
// Copyright (c) 2011 Novell, Inc (http://www.novell.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using MonoDevelop.AnalysisCore;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory;
using System.Threading;

namespace MonoDevelop.CodeActions
{
	public class AnalysisCodeAction : CodeAction
	{
		public Result Result {
			get;
			private set;
		}
		
		Action<MonoDevelop.Ide.Gui.Document, TextLocation> act;
		
		public AnalysisCodeAction (string title, Result result, Action<MonoDevelop.Ide.Gui.Document, TextLocation> act)
		{
			this.Title = title;
			this.act = act;
			this.Result = result;
		}

		public override void Run (MonoDevelop.Ide.Gui.Document document, TextLocation loc)
		{
			act (document, loc);
		}
	}


	public class AnalysisContextActionProvider : CodeActionProvider
	{
		public Result Result {
			get;
			private set;
		}
		
		public IAnalysisFixAction Action {
			get;
			private set;
		}
		
		public AnalysisContextActionProvider (Result result, IAnalysisFixAction action)
		{
			this.Result = result;
			this.Action = action;
			this.Description = result.Message;
		}
		
		public override System.Collections.Generic.IEnumerable<CodeAction> GetActions (MonoDevelop.Ide.Gui.Document document, TextLocation loc, CancellationToken cancellationToken)
		{
			yield return new AnalysisCodeAction (Action.Label, Result, (d, l) => Action.Fix ());
		}
	}
}
