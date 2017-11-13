using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace WorldRpgCommon
{
	public class FolderNameEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			FolderBrowserDialog folderBrowserDialog;
			object result;
			folderBrowserDialog = new FolderBrowserDialog();
			if (value != null)
			{
				folderBrowserDialog.DirectoryPath = string.Format("{0}", value);
			}
			if (folderBrowserDialog.ShowDialog(null) == DialogResult.OK)
			{
				result = folderBrowserDialog.DirectoryPath;
			}
			else
			{
				result = value;
			}
			return result;
		}

		public FolderNameEditor()
		{
			
			
		}
	}
}
