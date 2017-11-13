using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WorldRpgCommon
{
	[Description("提供一个Vista样式的选择文件对话框"), Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
	public class FolderBrowserDialog : Component
	{
		[Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")]
		[ComImport]
		private class FileOpenDialog
		{
			//[MethodImpl(MethodImplOptions.InternalCall)]
			//public extern FileOpenDialog();
		}

		[Guid("42f85136-db7e-439c-85f1-e4075d135fc8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		private interface IFileOpenDialog
		{
			[PreserveSig]
			uint Show([In] IntPtr parent);

			void SetFileTypes();

			void SetFileTypeIndex([In] uint iFileType);

			void GetFileTypeIndex(out uint piFileType);

			void Advise();

			void Unadvise();

			void SetOptions([In] FolderBrowserDialog.FOS fos);

			void GetOptions(out FolderBrowserDialog.FOS pfos);

			void SetDefaultFolder(FolderBrowserDialog.IShellItem psi);

			void SetFolder(FolderBrowserDialog.IShellItem psi);

			void GetFolder(out FolderBrowserDialog.IShellItem ppsi);

			void GetCurrentSelection(out FolderBrowserDialog.IShellItem ppsi);

			void SetFileName([MarshalAs(UnmanagedType.LPWStr)] [In] string pszName);

			void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

			void SetTitle([MarshalAs(UnmanagedType.LPWStr)] [In] string pszTitle);

			void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] [In] string pszText);

			void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] [In] string pszLabel);

			void GetResult(out FolderBrowserDialog.IShellItem ppsi);

			void AddPlace(FolderBrowserDialog.IShellItem psi, int alignment);

			void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] [In] string pszDefaultExtension);

			void Close(int hr);

			void SetClientGuid();

			void ClearClientData();

			void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);

			void GetResults([MarshalAs(UnmanagedType.Interface)] out IntPtr ppenum);

			void GetSelectedItems([MarshalAs(UnmanagedType.Interface)] out IntPtr ppsai);
		}

		[Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		private interface IShellItem
		{
			void BindToHandler();

			void GetParent();

			void GetDisplayName([In] FolderBrowserDialog.SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

			void GetAttributes();

			void Compare();
		}

		private enum SIGDN : uint
		{
			SIGDN_DESKTOPABSOLUTEEDITING = 2147794944u,
			SIGDN_DESKTOPABSOLUTEPARSING = 2147647488u,
			SIGDN_FILESYSPATH = 2147844096u,
			SIGDN_NORMALDISPLAY = 0u,
			SIGDN_PARENTRELATIVE = 2148007937u,
			SIGDN_PARENTRELATIVEEDITING = 2147684353u,
			SIGDN_PARENTRELATIVEFORADDRESSBAR = 2147991553u,
			SIGDN_PARENTRELATIVEPARSING = 2147581953u,
			SIGDN_URL = 2147909632u
		}

		[Flags]
		private enum FOS
		{
			FOS_ALLNONSTORAGEITEMS = 128,
			FOS_ALLOWMULTISELECT = 512,
			FOS_CREATEPROMPT = 8192,
			FOS_DEFAULTNOMINIMODE = 536870912,
			FOS_DONTADDTORECENT = 33554432,
			FOS_FILEMUSTEXIST = 4096,
			FOS_FORCEFILESYSTEM = 64,
			FOS_FORCESHOWHIDDEN = 268435456,
			FOS_HIDEMRUPLACES = 131072,
			FOS_HIDEPINNEDPLACES = 262144,
			FOS_NOCHANGEDIR = 8,
			FOS_NODEREFERENCELINKS = 1048576,
			FOS_NOREADONLYRETURN = 32768,
			FOS_NOTESTFILECREATE = 65536,
			FOS_NOVALIDATE = 256,
			FOS_OVERWRITEPROMPT = 2,
			FOS_PATHMUSTEXIST = 2048,
			FOS_PICKFOLDERS = 32,
			FOS_SHAREAWARE = 16384,
			FOS_STRICTFILETYPES = 4
		}

		public string DirectoryPath
		{
			
			get
			{
				return this.DirectoryPath;
			}
			
			set
			{
				this.DirectoryPath = value;
			}
		}

		public FolderBrowserDialog()
		{
			
			
		}

		public DialogResult ShowDialog(IWin32Window owner)
		{
			IntPtr parent;
			FolderBrowserDialog.IFileOpenDialog fileOpenDialog;
			uint num;
			IntPtr pidl;
			FolderBrowserDialog.IShellItem shellItem;
			uint num2;
			DialogResult result;
			string directoryPath;
			parent = ((owner != null) ? owner.Handle : FolderBrowserDialog.GetActiveWindow());
			fileOpenDialog = (FolderBrowserDialog.IFileOpenDialog)new FolderBrowserDialog.FileOpenDialog();
			try
			{
				if (!string.IsNullOrEmpty(this.DirectoryPath))
				{
					num = 0u;
					if (FolderBrowserDialog.SHILCreateFromPath(this.DirectoryPath, out pidl, ref num) == 0 && FolderBrowserDialog.SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, pidl, out shellItem) == 0)
					{
						fileOpenDialog.SetFolder(shellItem);
					}
				}
				fileOpenDialog.SetOptions(FolderBrowserDialog.FOS.FOS_FORCEFILESYSTEM | FolderBrowserDialog.FOS.FOS_PICKFOLDERS);
				num2 = fileOpenDialog.Show(parent);
				if (num2 == 2147943623u)
				{
					result = DialogResult.Cancel;
				}
				else
				{
					if (num2 > 0u)
					{
						result = DialogResult.Abort;
					}
					else
					{
						fileOpenDialog.GetResult(out shellItem);
						shellItem.GetDisplayName((FolderBrowserDialog.SIGDN)2147844096u, out directoryPath);
						this.DirectoryPath = directoryPath;
						result = DialogResult.OK;
					}
				}
			}
			finally
			{
				Marshal.ReleaseComObject(fileOpenDialog);
			}
			return result;
		}

		[DllImport("shell32.dll")]
		private static extern int SHILCreateFromPath([MarshalAs(UnmanagedType.LPWStr)] string pszPath, out IntPtr ppIdl, ref uint rgflnOut);

		[DllImport("shell32.dll")]
		private static extern int SHCreateShellItem(IntPtr pidlParent, IntPtr psfParent, IntPtr pidl, out FolderBrowserDialog.IShellItem ppsi);

		[DllImport("user32.dll")]
		private static extern IntPtr GetActiveWindow();
	}
}
