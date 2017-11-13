using DSkin.Controls;
using DSkin.DirectUI;
using DSkin.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WorldRpgCommon;
using WorldRpgEquip.Services;
using WorldRpgModel;
using WorldRpgService;

namespace PrickeInMagicWorldRPG
{
	public class MargicEquip : DSkinForm
	{

		private IContainer components;

		private DSkinTabControl tabEquip;

		private DSkinTabPage pageWq;

		private DSkinTabPage pageTk;

		private DSkinTabPage pageYf;

		private DSkinTabPage pageSp;

		private DSkinTabPage pageCb;

		private DSkinTabPage pageQt;

		private DSkinListBox equipDetail;
        private DSkinListBox dataViewHty;

        private DSkinPanel dSkinPanel1;

		private DSkinButton btnXx;

		private DSkinButton btnJj;

		private DSkinButton btnSearch;

		private DSkinTextBox txtName;



        private DSkinTabPage pageBoss;

		private DSkinTabPage pageZs;

		private DSkinTabPage pageCl;
        private List<string> htyList;

		public MargicEquip()
		{
            htyList = new List<string>();

            this.components = null;
			
			this.InitializeComponent();
		}

		private void LoadData(int mode)
		{
			this.LoadEquipData("武器", mode);
			this.LoadEquipData("头盔", mode);
			this.LoadEquipData("盔甲", mode);
			this.LoadEquipData("饰品", mode);
			this.LoadEquipData("翅膀", mode);
			this.LoadBossData();
			this.LoadHeroData();
			this.LoadMaterialData();
		}

		private void ImportRpgData()
		{
			OpenFileDialog openFileDialog;
			DataSet ds;
			DataSet ds2;
			DataSet ds3;
			DataSet ds4;
			DataSet ds5;
			openFileDialog = new OpenFileDialog();
			if (DialogResult.OK == openFileDialog.ShowDialog())
			{
				ds = this.LoadExcelData(openFileDialog.FileName, "武器");
				this.Create(ds, 1);
				ds2 = this.LoadExcelData(openFileDialog.FileName, "头盔");
				this.Create(ds2, 2);
				ds3 = this.LoadExcelData(openFileDialog.FileName, "衣服");
				this.Create(ds3, 3);
				ds4 = this.LoadExcelData(openFileDialog.FileName, "饰品");
				this.Create(ds4, 4);
				ds5 = this.LoadExcelData(openFileDialog.FileName, "翅膀");
				this.Create(ds5, 5);
				MessageBox.Show("Success");

            }
		}

		private void Create(DataSet ds, int type)
		{
			List<string> list;
			List<SQLiteParameter[]> list2;
			IEnumerator enumerator;
			DataTable dataTable;
			IEnumerator enumerator2;
			DataRow dataRow;
			string item;
			SQLiteParameter[] item2;
			IDisposable disposable;
			if (ds != null && ds.Tables.Count != 0)
			{
				list = new List<string>();
				list2 = new List<SQLiteParameter[]>();
				enumerator = ds.Tables.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						dataTable = (DataTable)enumerator.Current;
						enumerator2 = dataTable.Rows.GetEnumerator();
						try
						{
							while (enumerator2.MoveNext())
							{
								dataRow = (DataRow)enumerator2.Current;
								if (dataRow["名称"] != null && !string.Equals(dataRow["名称"].ToString().Trim(), ""))
								{
									item = "insert into Equip values(@key,@mc,@pz,@sx,@dj,@hqtj,@zsqh,@type)";
									item2 = new SQLiteParameter[]
									{
										new SQLiteParameter("@key", Guid.NewGuid().ToString()),
										new SQLiteParameter("@mc", dataRow["名称"]),
										new SQLiteParameter("@pz", dataRow["品质"]),
										new SQLiteParameter("@sx", dataRow["属性"]),
										new SQLiteParameter("@dj", dataRow["等级"]),
										new SQLiteParameter("@hqtj", dataRow["获得途径"]),
										new SQLiteParameter("@zsqh", dataRow["专属强化"]),
										new SQLiteParameter("@type", type)
									};
									list.Add(item);
									list2.Add(item2);
								}
							}
						}
						finally
						{
							disposable = (enumerator2 as IDisposable);
							if (disposable != null)
							{
								disposable.Dispose();
							}
						}
					}
				}
				finally
				{
					disposable = (enumerator as IDisposable);
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				SqliteHelper.ExecuteTrans(list, list2);
			}
		}

		private DataSet LoadExcelData(string file, string sheel)
		{
			string text;
			OleDbConnection oleDbConnection;
			string selectCommandText;
			OleDbDataAdapter oleDbDataAdapter;
			DataSet dataSet;
			text = string.Concat("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", file, ";Extended Properties=Excel 8.0;");
			oleDbConnection = new OleDbConnection(text);
			oleDbConnection.Open();
			selectCommandText = string.Concat("select * from [", sheel, "$]");
			oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, text);
			dataSet = new DataSet();
			oleDbDataAdapter.Fill(dataSet, "table1");
			return dataSet;
		}

		private void MargicEquip_Load(object sender, EventArgs e)
		{
			this.LoadData(1);
		}

		private void LoadLayout()
		{
		}

		private void LoadEquipData(string type, int mode)
		{
			//MargicEquip.tempName tempName__DisplayClass7_;
			IList<Equip> source;
			List<Equip> list;
			DSkinListBox dSkinListBox;
			Point location;
			int i;
			DuiBaseControl duiBaseControl;
			DuiLabel duiLabel;
			string text;
			DuiLabel duiLabel2;
			Color foreColor;
			DuiLabel duiLabel3;
			DuiLabel duiLabel4;
			//tempName__DisplayClass7_ = new MargicEquip.tempName();
			//tempName__DisplayClass7_.type = type;
			source = EquipService.LoadData();
            //list = Enumerable.ToList<Equip>(Enumerable.Where<Equip>(source, new Func<Equip, bool>(tempName__DisplayClass7_, (LoadEquipData))));
            list = source.ToList<Equip>().Where(it=>it.Type==type).ToList();
            dSkinListBox = new DSkinListBox();
			dSkinListBox.Dock = DockStyle.Fill;
			dSkinListBox.BackColor = SystemColors.Control;
			dSkinListBox.Ulmul = true;
			dSkinListBox.InnerScrollBar.Fillet = true;
			dSkinListBox.InnerScrollBar.BackColor = Color.Transparent;
			dSkinListBox.ItemSize = new Size(192, 218);
			if (mode == 1)
			{
				dSkinListBox.ItemSize = new Size(dSkinListBox.ItemSize.Width, 43);
			}
			location = new Point(5, 5);
			i = 0;
			while (i < list.Count)
			{
				duiBaseControl = new DuiBaseControl();
				duiBaseControl.Margin = new Padding(5);
				duiBaseControl.BackColor = Color.White;
				duiBaseControl.Borders.AllWidth = 1;
				duiBaseControl.Borders.AllColor = Color.FromArgb(226, 226, 226);
				duiBaseControl.AutoSize = false;
				duiBaseControl.Font = new Font("微软雅黑", 10f);
				duiBaseControl.ForeColor = Color.Black;
				duiBaseControl.Size = new Size(189, 215);
				if (mode == 1)
				{
					duiBaseControl.Size = new Size(duiBaseControl.Size.Width, 40);
				}
				if (location.X + duiBaseControl.Width >= base.Width)
				{
					location = new Point(5, location.Y + 220);
				}
				duiBaseControl.Tag = list[i];
				duiBaseControl.Location = location;
				duiBaseControl.MouseClick += new EventHandler<DuiMouseEventArgs>(item_MouseClick);
				duiBaseControl.MouseEnter += new EventHandler<MouseEventArgs>((tmpLbl_MouseEnter));
				duiBaseControl.MouseLeave += new EventHandler((tmpLbl_MouseLeave));
				duiLabel = new DuiLabel();
				duiLabel.AutoSize = false;
				duiLabel.ForeColor = Color.Black;
				if (mode == 1)
				{
					duiLabel.TextAlign = ContentAlignment.MiddleCenter;
				}
				duiLabel.ToolTip = (duiLabel.Text = list[i].Name);
				duiLabel.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
				duiLabel.Size = new Size(183, 34);
				duiLabel.Location = new Point(3, 3);
				duiLabel.AutoEllipsis = true;
				text = list[i].Quality.Replace("\n", "").Trim();
				duiLabel2 = new DuiLabel();
				duiLabel2.AutoSize = false;
				duiLabel2.ForeColor = Color.Black;
				duiLabel2.ToolTip = (duiLabel2.Text = text);
				duiLabel2.Font = new Font("微软雅黑", 9f);
				duiLabel2.Size = new Size(183, 17);
				duiLabel2.Location = new Point(3, 40);
				duiLabel2.AutoEllipsis = true;
				foreColor = default(Color);
				if (string.Equals(text, "[极其罕见]"))
				{
					foreColor = Color.FromArgb(0, 191, 255);
				}
				else
				{
					if (string.Equals(text, "[史诗]天绝史诗"))
					{
						foreColor = Color.FromArgb(147, 112, 219);
					}
					else
					{
						if (string.Equals(text, "[史诗]神话传说"))
						{
							foreColor = Color.FromArgb(199, 21, 133);
						}
						else
						{
							if (string.Equals(text, "[史诗]禁断圣物"))
							{
								foreColor = Color.FromArgb(220, 20, 60);
							}
							else
							{
								if (string.Equals(text, "[史诗]传奇至宝"))
								{
									foreColor = Color.FromArgb(0, 112, 192);
								}
							}
						}
					}
				}
				duiLabel2.ForeColor = (duiLabel.ForeColor = foreColor);
				duiBaseControl.Controls.Add(duiLabel);
				duiBaseControl.Controls.Add(duiLabel2);
				if (mode != 1)
				{
					duiLabel3 = new DuiLabel();
					duiLabel3.AutoSize = false;
					duiLabel3.ForeColor = Color.Black;
					duiLabel3.ToolTip = (duiLabel3.Text = list[i].Attribute);
					duiLabel3.Font = new Font("微软雅黑", 9f);
					duiLabel3.Size = new Size(183, 130);
					duiLabel3.Location = new Point(3, 60);
					duiLabel3.AutoEllipsis = true;
					duiLabel4 = new DuiLabel();
					duiLabel4.AutoSize = false;
					duiLabel4.ToolTip = (duiLabel4.Text = list[i].Origin.Replace("\n", ""));
					duiLabel4.Font = new Font("微软雅黑", 9f);
					duiLabel4.ForeColor = Color.Orange;
					duiLabel4.Size = new Size(183, 20);
					duiLabel4.Location = new Point(3, 195);
					duiLabel4.AutoEllipsis = true;
					duiBaseControl.Controls.Add(duiLabel3);
					duiBaseControl.Controls.Add(duiLabel4);
				}
				dSkinListBox.Items.Add(duiBaseControl);
				location = new Point(location.X + duiBaseControl.Width + 5, duiBaseControl.Top);
				i = i + 1;
			}
			if (string.Equals(type, "武器"))
			{
				this.pageWq.Controls.Clear();
				this.pageWq.Controls.Add(dSkinListBox);
			}
			else
			{
				if (string.Equals(type, "头盔"))
				{
					this.pageTk.Controls.Clear();
					this.pageTk.Controls.Add(dSkinListBox);
				}
				else
				{
					if (string.Equals(type, "盔甲"))
					{
						this.pageYf.Controls.Clear();
						this.pageYf.Controls.Add(dSkinListBox);
					}
					else
					{
						if (string.Equals(type, "饰品"))
						{
							this.pageSp.Controls.Clear();
							this.pageSp.Controls.Add(dSkinListBox);
						}
						else
						{
							if (string.Equals(type, "翅膀"))
							{
								this.pageCb.Controls.Clear();
								this.pageCb.Controls.Add(dSkinListBox);
							}
						}
					}
				}
			}
		}

		private void LoadBossData()
		{
			IList<Boss> list;
			DSkinListBox dSkinListBox;
			Point location;
			int i;
			DuiBaseControl duiBaseControl;
			DuiLabel duiLabel;
			list = BossService.LoadData();
			dSkinListBox = new DSkinListBox();
			dSkinListBox.Dock = DockStyle.Fill;
			dSkinListBox.BackColor = SystemColors.Control;
			dSkinListBox.Ulmul = true;
			dSkinListBox.InnerScrollBar.Fillet = true;
			dSkinListBox.InnerScrollBar.BackColor = Color.Transparent;
			dSkinListBox.ItemSize = new Size(192, 43);
			location = new Point(5, 5);
			i = 0;
			while (i < list.Count)
			{
				duiBaseControl = new DuiBaseControl();
				duiBaseControl.Margin = new Padding(5);
				duiBaseControl.BackColor = Color.White;
				duiBaseControl.Borders.AllWidth = 1;
				duiBaseControl.Borders.AllColor = Color.FromArgb(226, 226, 226);
				duiBaseControl.AutoSize = false;
				duiBaseControl.Font = new Font("微软雅黑", 10f);
				duiBaseControl.ForeColor = Color.Black;
				duiBaseControl.Size = new Size(189, 40);
				if (location.X + duiBaseControl.Width >= base.Width)
				{
					location = new Point(5, location.Y + 220);
				}
				duiBaseControl.Tag = list[i];
				duiBaseControl.Location = location;
				duiBaseControl.MouseClick += new EventHandler<DuiMouseEventArgs>((Boss_MouseClick));
				duiBaseControl.MouseEnter += new EventHandler<MouseEventArgs>((tmpLbl_MouseEnter));
				duiBaseControl.MouseLeave += new EventHandler((tmpLbl_MouseLeave));
				duiLabel = new DuiLabel();
				duiLabel.AutoSize = false;
				duiLabel.ForeColor = Color.Black;
				duiLabel.TextAlign = ContentAlignment.MiddleCenter;
				duiLabel.ToolTip = (duiLabel.Text = list[i].Name);
				duiLabel.Font = new Font("微软雅黑", 10f);
				duiLabel.Size = new Size(183, 34);
				duiLabel.Location = new Point(3, 3);
				duiLabel.AutoEllipsis = true;
				duiBaseControl.Controls.Add(duiLabel);
				dSkinListBox.Items.Add(duiBaseControl);
				location = new Point(location.X + duiBaseControl.Width + 5, duiBaseControl.Top);
				i = i + 1;
			}
			this.pageBoss.Controls.Clear();
			this.pageBoss.Controls.Add(dSkinListBox);
		}

		private void LoadHeroData()
		{
			IList<Hero> list;
			DSkinListBox dSkinListBox;
			Point location;
			int i;
			DuiBaseControl duiBaseControl;
			DuiLabel duiLabel;
			list = HeroService.LoadData();
			dSkinListBox = new DSkinListBox();
			dSkinListBox.Dock = DockStyle.Fill;
			dSkinListBox.BackColor = SystemColors.Control;
			dSkinListBox.Ulmul = true;
			dSkinListBox.InnerScrollBar.Fillet = true;
			dSkinListBox.InnerScrollBar.BackColor = Color.Transparent;
			dSkinListBox.ItemSize = new Size(192, 43);
			location = new Point(5, 5);
			i = 0;
			while (i < list.Count)
			{
				duiBaseControl = new DuiBaseControl();
				duiBaseControl.Margin = new Padding(5);
				duiBaseControl.BackColor = Color.White;
				duiBaseControl.Borders.AllWidth = 1;
				duiBaseControl.Borders.AllColor = Color.FromArgb(226, 226, 226);
				duiBaseControl.AutoSize = false;
				duiBaseControl.Font = new Font("微软雅黑", 10f);
				duiBaseControl.ForeColor = Color.Black;
				duiBaseControl.Size = new Size(189, 40);
				if (location.X + duiBaseControl.Width >= base.Width)
				{
					location = new Point(5, location.Y + 220);
				}
				duiBaseControl.Tag = list[i];
				duiBaseControl.Location = location;
				duiBaseControl.MouseClick += new EventHandler<DuiMouseEventArgs>( Hero_MouseClick);
				duiBaseControl.MouseEnter += new EventHandler<MouseEventArgs>( tmpLbl_MouseEnter);
				duiBaseControl.MouseLeave += new EventHandler( tmpLbl_MouseLeave);
				duiLabel = new DuiLabel();
				duiLabel.AutoSize = false;
				duiLabel.ForeColor = Color.Black;
				duiLabel.TextAlign = ContentAlignment.MiddleCenter;
				duiLabel.ToolTip = (duiLabel.Text = list[i].Name);
				duiLabel.Font = new Font("微软雅黑", 10f);
				duiLabel.Size = new Size(183, 34);
				duiLabel.Location = new Point(3, 3);
				duiLabel.AutoEllipsis = true;
				duiBaseControl.Controls.Add(duiLabel);
				dSkinListBox.Items.Add(duiBaseControl);
				location = new Point(location.X + duiBaseControl.Width + 5, duiBaseControl.Top);
				i = i + 1;
			}
			this.pageZs.Controls.Clear();
			this.pageZs.Controls.Add(dSkinListBox);
		}

		private void LoadMaterialData()
		{
			IList<Material> list;
			DSkinListBox dSkinListBox;
			Point location;
			int i;
			DuiBaseControl duiBaseControl;
			DuiLabel duiLabel;
			list = MaterialService.LoadData();
			dSkinListBox = new DSkinListBox();
			dSkinListBox.Dock = DockStyle.Fill;
			dSkinListBox.BackColor = SystemColors.Control;
			dSkinListBox.Ulmul = true;
			dSkinListBox.InnerScrollBar.Fillet = true;
			dSkinListBox.InnerScrollBar.BackColor = Color.Transparent;
			dSkinListBox.ItemSize = new Size(192, 43);
			location = new Point(5, 5);
			i = 0;
			while (i < list.Count)
			{
				duiBaseControl = new DuiBaseControl();
				duiBaseControl.Margin = new Padding(5);
				duiBaseControl.BackColor = Color.White;
				duiBaseControl.Borders.AllWidth = 1;
				duiBaseControl.Borders.AllColor = Color.FromArgb(226, 226, 226);
				duiBaseControl.AutoSize = false;
				duiBaseControl.Font = new Font("微软雅黑", 10f);
				duiBaseControl.ForeColor = Color.Black;
				duiBaseControl.Size = new Size(189, 40);
				if (location.X + duiBaseControl.Width >= base.Width)
				{
					location = new Point(5, location.Y + 220);
				}
				duiBaseControl.Tag = list[i];
				duiBaseControl.Location = location;
				duiBaseControl.MouseClick += new EventHandler<DuiMouseEventArgs>( (Material_MouseClick));
				duiBaseControl.MouseEnter += new EventHandler<MouseEventArgs>( (tmpLbl_MouseEnter));
				duiBaseControl.MouseLeave += new EventHandler((tmpLbl_MouseLeave));
				duiLabel = new DuiLabel();
				duiLabel.AutoSize = false;
				duiLabel.ForeColor = Color.Black;
				duiLabel.TextAlign = ContentAlignment.MiddleCenter;
				duiLabel.ToolTip = (duiLabel.Text = list[i].Name);
				duiLabel.Font = new Font("微软雅黑", 10f);
				duiLabel.Size = new Size(183, 34);
				duiLabel.Location = new Point(3, 3);
				duiLabel.AutoEllipsis = true;
				duiBaseControl.Controls.Add(duiLabel);
				dSkinListBox.Items.Add(duiBaseControl);
				location = new Point(location.X + duiBaseControl.Width + 5, duiBaseControl.Top);
				i = i + 1;
			}
			this.pageCl.Controls.Clear();
			this.pageCl.Controls.Add(dSkinListBox);
		}

		private void Hero_MouseClick(object sender, DuiMouseEventArgs e)
		{
			DuiBaseControl duiBaseControl;
			Hero hero;
			duiBaseControl = (sender as DuiBaseControl);
			if (duiBaseControl != null)
			{
				hero = (duiBaseControl.Tag as Hero);
				if (hero != null)
				{
					this.ShowHeroDetail(hero);
				}
			}
		}

		private void Boss_MouseClick(object sender, DuiMouseEventArgs e)
		{
			DuiBaseControl duiBaseControl;
			Boss boss;
			duiBaseControl = (sender as DuiBaseControl);
			if (duiBaseControl != null)
			{
				boss = (duiBaseControl.Tag as Boss);
				if (boss != null)
				{
					this.ShowBossDetail(boss);
				}
			}
		}

		private void Material_MouseClick(object sender, DuiMouseEventArgs e)
		{
			DuiBaseControl duiBaseControl;
			Material material;
			duiBaseControl = (sender as DuiBaseControl);
			if (duiBaseControl != null)
			{
				material = (duiBaseControl.Tag as Material);
				if (material != null)
				{
					this.ShowMaterialDetail(material);
				}
			}
		}

		private void item_MouseClick(object sender, DuiMouseEventArgs e)
		{
			DuiBaseControl duiBaseControl;
			Equip equip;
			duiBaseControl = (sender as DuiBaseControl);
			if (duiBaseControl != null)
			{
				equip = (duiBaseControl.Tag as Equip);
				if (equip != null)
				{
					this.ShowEquipDetail(equip);
				}
			}
		}

		private void tmpLbl_MouseLeave(object sender, EventArgs e)
		{
			DuiBaseControl duiBaseControl;
			duiBaseControl = (sender as DuiBaseControl);
			if (duiBaseControl != null)
			{
				duiBaseControl.BackColor = Color.White;
			}
		}

		private void tmpLbl_MouseEnter(object sender, EventArgs e)
		{
			DuiBaseControl duiBaseControl;
			duiBaseControl = (sender as DuiBaseControl);
			if (duiBaseControl != null)
			{
				duiBaseControl.BackColor = Color.FromArgb(240, 240, 240);
			}
		}
        private void DrawHtyLab()
        {
            //htyList
            this.dataViewHty.Items.Clear();
            DuiBaseControl viewHty = new DuiBaseControl();
            viewHty.Margin = new Padding(1);
            viewHty.BackColor = Color.White;
            viewHty.Borders.AllWidth = 1;
            viewHty.Borders.AllColor = Color.FromArgb(226, 226, 226);
            viewHty.AutoSize = true;
            viewHty.Font = new Font("微软雅黑", 12f);
            viewHty.ForeColor = Color.Black;
            viewHty.Size = new Size(600, 26);
            viewHty.Location = new Point(3, 3);

            DuiLabel duiLabel = new DuiLabel();
            duiLabel.AutoSize = true;
            duiLabel.ForeColor = Color.Green;
            duiLabel.ToolTip = (duiLabel.Text = "最近查看：");
            duiLabel.Font = new Font("微软雅黑", 8f, FontStyle.Bold);
            duiLabel.Size = new Size(60, 18);
            duiLabel.Location = new Point(2, 2);
            duiLabel.TextAlign = ContentAlignment.MiddleLeft;
            duiLabel.AutoEllipsis = true;
            viewHty.Controls.Add(duiLabel);
            for (int i= htyList.Count-1, j=64;i>=0&&j< viewHty.Size.Width; i--)
            {
                duiLabel = new DuiLabel();
                duiLabel.AutoSize = true;
                duiLabel.ForeColor = Color.Green;
                duiLabel.ToolTip = (duiLabel.Text = htyList[i]);
                duiLabel.Font = new Font("微软雅黑", 8f, FontStyle.Bold);
                duiLabel.Size = new Size(htyList[i].Length*12, 18);
                duiLabel.Location = new Point(j, 2);
                duiLabel.TextAlign = ContentAlignment.MiddleLeft;
                duiLabel.AutoEllipsis = true;
                duiLabel.MouseClick += new EventHandler<DuiMouseEventArgs>((HtyItem_MouseClick));
                duiLabel.MouseEnter += new EventHandler<MouseEventArgs>(new mouseFun((object sender, EventArgs e)=>{ ((DuiLabel)sender).BackColor = Color.FromArgb(240, 240, 240); }));
                duiLabel.MouseLeave +=new EventHandler(new mouseFun((object sender, EventArgs e) => { ((DuiLabel)sender).BackColor =Color.White; }));
                //duiLabel.BackColor = Color.FromArgb(240, 240, 240);
                //duiLabel.BackColor = Color.White;
                j += htyList[i].Length*15 + 5;
                viewHty.Controls.Add(duiLabel);
            }
            this.dataViewHty.Items.Add(viewHty);
            dSkinPanel1.Borders.AllColor = Color.Blue;
        }
        public delegate void mouseFun(object sender, EventArgs e);
        private void HtyItem_MouseClick(object sender, DuiMouseEventArgs e)
        {
            DuiLabel duiLabel;
            string text;
            Equip equip;
            Boss boss;
            Material material;
            duiLabel = (sender as DuiLabel);
            if (duiLabel != null)
            {
                text = duiLabel.Text;
                equip = EquipService.LoadDataByName(text);
                if (equip != null)
                {
                    this.ShowEquipDetail(equip);
                }
                else
                {
                    boss = BossService.LoadDataByName(text);
                    if (boss != null)
                    {
                        this.ShowBossDetail(boss);
                    }
                    else
                    {
                        material = MaterialService.LoadDataByNameEx(text);
                        if (material != null)
                        {
                            this.ShowMaterialDetail(material);
                        }
                    }
                }
            }
        }

        private void ShowEquipDetail(Equip equip)
		{
			DuiBaseControl duiBaseControl;
			DuiLabel duiLabel;
			string text;
			DuiLabel duiLabel2;
			Color foreColor;
			DuiLabel duiLabel3;
			Size size;
			DuiBaseControl duiBaseControl2;
			DuiLabel duiLabel4;
			Equip equip2;
			string[] array;
			Point location;
			int i;
			string[] array2;
			int j;
			DuiBaseControl duiBaseControl3;
			DuiLabel duiLabel5;
			DuiBaseControl duiBaseControl4;
			DuiLabel duiLabel6;
			IList<Equip> list;
			DuiBaseControl duiBaseControl5;
			DuiLabel duiLabel7;
			Point location2;
			int k;
			DuiBaseControl duiBaseControl6;
			DuiLabel duiLabel8;
			this.equipDetail.Items.Clear();
			duiBaseControl = new DuiBaseControl();
			duiBaseControl.Margin = new Padding(5);
			duiBaseControl.BackColor = Color.White;
			duiBaseControl.Borders.AllWidth = 1;
			duiBaseControl.Borders.AllColor = Color.FromArgb(226, 226, 226);
			duiBaseControl.AutoSize = false;
			duiBaseControl.Font = new Font("微软雅黑", 12f);
			duiBaseControl.ForeColor = Color.Black;
			duiBaseControl.Location = new Point(10, 10);
			duiLabel = new DuiLabel();
			duiLabel.AutoSize = false;
			duiLabel.ForeColor = Color.Black;
			duiLabel.ToolTip = (duiLabel.Text ="[Lv."+equip.Level+"]"+ equip.Name.Trim());
            htyList.RemoveAll(it=>it.Equals(equip.Name.Trim()));
            htyList.Add(equip.Name.Trim());
            DrawHtyLab();
            duiLabel.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
			duiLabel.Size = new Size(this.equipDetail.Width, 30);
			duiLabel.Location = new Point(3, 3);
			duiLabel.TextAlign = ContentAlignment.MiddleLeft;
			duiLabel.AutoEllipsis = true;
			text = equip.Quality.Replace("\n", "").Trim();
			duiLabel2 = new DuiLabel();
			duiLabel2.AutoSize = false;
			duiLabel2.ForeColor = Color.Black;
			duiLabel2.ToolTip = (duiLabel2.Text = text);
			duiLabel2.Font = new Font("微软雅黑", 9f);
			duiLabel2.Size = new Size(this.equipDetail.Width, 17);
			duiLabel2.Location = new Point(3, 40);
			duiLabel2.AutoEllipsis = true;
			foreColor = default(Color);
			if (string.Equals(text, "[极其罕见]"))
			{
				foreColor = Color.FromArgb(0, 191, 255);
			}
			else
			{
				if (string.Equals(text, "[史诗]天绝史诗"))
				{
					foreColor = Color.FromArgb(147, 112, 219);
				}
				else
				{
					if (string.Equals(text, "[史诗]神话传说"))
					{
						foreColor = Color.FromArgb(199, 21, 133);
					}
					else
					{
						if (string.Equals(text, "[史诗]禁断圣物"))
						{
							foreColor = Color.FromArgb(220, 20, 60);
						}
						else
						{
							if (string.Equals(text, "[史诗]传奇至宝"))
							{
								foreColor = Color.FromArgb(0, 112, 192);
							}
						}
					}
				}
			}
			duiLabel2.ForeColor = (duiLabel.ForeColor = foreColor);
			duiLabel3 = new DuiLabel();
			duiLabel3.AutoSize = false;
			duiLabel3.ForeColor = Color.Black;
			duiLabel3.ToolTip = (duiLabel3.Text = equip.Attribute);
			duiLabel3.Font = new Font("微软雅黑", 9f);
			size = TextRenderer.MeasureText(equip.Attribute, duiLabel3.Font, new Size(this.equipDetail.Width, 130), TextFormatFlags.TextBoxControl);
			duiLabel3.Size = new Size(size.Width, size.Height + 20);
			duiLabel3.Location = new Point(3, 60);
			duiLabel3.AutoEllipsis = true;
			duiBaseControl.Controls.Add(duiLabel);
			duiBaseControl.Controls.Add(duiLabel2);
			duiBaseControl.Controls.Add(duiLabel3);
			duiBaseControl.Size = new Size(this.equipDetail.Width, duiLabel3.Top + duiLabel3.Height + 10);
			this.equipDetail.Items.Add(duiBaseControl);
			duiBaseControl2 = new DuiBaseControl();
			duiBaseControl2.Margin = new Padding(5);
			duiBaseControl2.BackColor = Color.White;
			duiBaseControl2.Borders.AllWidth = 1;
			duiBaseControl2.Borders.AllColor = Color.FromArgb(226, 226, 226);
			duiBaseControl2.AutoSize = false;
			duiBaseControl2.Font = new Font("微软雅黑", 12f);
			duiBaseControl2.ForeColor = Color.Black;
			duiBaseControl2.Size = new Size(this.equipDetail.Width, 32);
			duiBaseControl2.Location = new Point(10, duiBaseControl.Top + duiBaseControl.Height + 10);
			duiLabel4 = new DuiLabel();
			duiLabel4.AutoSize = false;
			duiLabel4.ForeColor = Color.Green;
			duiLabel4.ToolTip = (duiLabel4.Text = "合成该物品所需材料：");
			duiLabel4.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
			duiLabel4.Size = new Size(this.equipDetail.Width, 26);
			duiLabel4.Location = new Point(3, 3);
			duiLabel4.TextAlign = ContentAlignment.MiddleLeft;
			duiLabel4.AutoEllipsis = true;
			duiBaseControl2.Controls.Add(duiLabel4);
			this.equipDetail.Items.Add(duiBaseControl2);
			equip2 = EquipService.LoadDataByName(equip.Name);
			array = equip2.Origin.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			location = new Point(10, duiBaseControl2.Top + duiBaseControl2.Height + 10);
			i = 0;
			while (i < array.Length)
			{
				array2 = array[i].Split(new string[]
				{
					"或者"
				}, StringSplitOptions.RemoveEmptyEntries);
				j = 0;
				while (j < array2.Length)
				{
					if (j == 0 && array2.Length > 1)
					{
						duiBaseControl3 = new DuiBaseControl();
						duiBaseControl3.Location = new Point(location.X, location.Y - 13);
						duiBaseControl3.Size = new Size(this.equipDetail.Width, 13);
						duiLabel5 = new DuiLabel();
						duiLabel5.ForeColor = Color.Green;
						duiLabel5.Size = duiBaseControl3.Size;
						duiLabel5.Location = new Point(5, 0);
						duiLabel5.Text = string.Concat("下面", array2.Length, "个材料任意一个");
						duiLabel5.Font = new Font("微软雅黑", 8f, FontStyle.Italic);
						duiBaseControl3.Controls.Add(duiLabel5);
						this.equipDetail.Items.Add(duiBaseControl3);
					}
					duiBaseControl4 = new DuiBaseControl();
					duiBaseControl4.Margin = new Padding(5);
					duiBaseControl4.BackColor = Color.White;
					duiBaseControl4.Borders.AllWidth = 1;
					duiBaseControl4.Borders.AllColor = Color.FromArgb(226, 226, 226);
					duiBaseControl4.AutoSize = false;
					duiBaseControl4.Font = new Font("微软雅黑", 12f);
					duiBaseControl4.ForeColor = Color.Black;
					duiBaseControl4.Size = new Size(this.equipDetail.Width, 32);
					duiBaseControl4.Location = location;
					duiBaseControl4.MouseClick += new EventHandler<DuiMouseEventArgs>(TmpItem_MouseClick);
					duiBaseControl4.MouseEnter += new EventHandler<MouseEventArgs>((tmpLbl_MouseEnter));
					duiBaseControl4.MouseLeave += new EventHandler((tmpLbl_MouseLeave));
					duiLabel6 = new DuiLabel();
					duiLabel6.AutoSize = false;
					duiLabel6.ForeColor = Color.Black;
					duiLabel6.ToolTip = (duiLabel6.Text = array2[j]);
					duiLabel6.Font = new Font("微软雅黑", 10f);
					duiLabel6.Size = new Size(this.equipDetail.Width, 26);
					duiLabel6.Location = new Point(3, 3);
					duiLabel6.TextAlign = ContentAlignment.MiddleLeft;
					duiLabel6.AutoEllipsis = true;
					duiLabel6.Cursor = Cursors.Hand;
                    duiLabel6.Tag = TagLabelTag.Name;
					duiBaseControl4.Controls.Add(duiLabel6);
					this.equipDetail.Items.Add(duiBaseControl4);
					location = new Point(10, duiBaseControl4.Top + duiBaseControl4.Height + 10);
					j = j + 1;
				}
				i = i + 1;
			}
			list = EquipService.LoadDataByOrigin(equip.Name);
			duiBaseControl5 = new DuiBaseControl();
			duiBaseControl5.Margin = new Padding(5);
			duiBaseControl5.BackColor = Color.White;
			duiBaseControl5.Borders.AllWidth = 1;
			duiBaseControl5.Borders.AllColor = Color.FromArgb(226, 226, 226);
			duiBaseControl5.AutoSize = false;
			duiBaseControl5.Font = new Font("微软雅黑", 12f);
			duiBaseControl5.ForeColor = Color.Black;
			duiBaseControl5.Size = new Size(this.equipDetail.Width, 32);
			duiBaseControl5.Location = location;
			duiLabel7 = new DuiLabel();
			duiLabel7.AutoSize = false;
			duiLabel7.ForeColor = Color.Orange;
			duiLabel7.ToolTip = (duiLabel7.Text = "该物品可以合成：");
			duiLabel7.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
			duiLabel7.Size = new Size(this.equipDetail.Width, 26);
			duiLabel7.Location = new Point(3, 3);
			duiLabel7.TextAlign = ContentAlignment.MiddleLeft;
			duiLabel7.AutoEllipsis = true;
			duiBaseControl5.Controls.Add(duiLabel7);
			this.equipDetail.Items.Add(duiBaseControl5);
			location2 = new Point(10, duiBaseControl5.Top + duiBaseControl5.Height + 10);
			k = 0;
			while (k < list.Count)
			{
				duiBaseControl6 = new DuiBaseControl();
				duiBaseControl6.Margin = new Padding(5);
				duiBaseControl6.BackColor = Color.White;
				duiBaseControl6.Borders.AllWidth = 1;
				duiBaseControl6.Borders.AllColor = Color.FromArgb(226, 226, 226);
				duiBaseControl6.AutoSize = false;
				duiBaseControl6.Font = new Font("微软雅黑", 12f);
				duiBaseControl6.ForeColor = Color.Black;
				duiBaseControl6.Size = new Size(this.equipDetail.Width, 32);
				duiBaseControl6.Location = location2;
				duiBaseControl6.MouseClick += new EventHandler<DuiMouseEventArgs>( (TmpItem_MouseClick));
				duiBaseControl6.MouseEnter += new EventHandler<MouseEventArgs>((tmpLbl_MouseEnter));
				duiBaseControl6.MouseLeave += new EventHandler(tmpLbl_MouseLeave);
				duiLabel8 = new DuiLabel();
				duiLabel8.AutoSize = false;
				duiLabel8.ForeColor = Color.Black;
				duiLabel8.ToolTip = (duiLabel8.Text = list[k].Name);
				duiLabel8.Font = new Font("微软雅黑", 10f);
				duiLabel8.Size = new Size(this.equipDetail.Width, 26);
				duiLabel8.Location = new Point(3, 3);
				duiLabel8.TextAlign = ContentAlignment.MiddleLeft;
				duiLabel8.AutoEllipsis = true;
				duiLabel8.Cursor = Cursors.Hand;
                duiLabel8.Tag = TagLabelTag.Name;
                duiBaseControl6.Controls.Add(duiLabel8);
				this.equipDetail.Items.Add(duiBaseControl6);
				location2 = new Point(10, duiBaseControl6.Top + duiBaseControl6.Height + 10);
				k = k + 1;
			}
		}

		private void ShowBossDetail(Boss boss)
		{
			DuiBaseControl duiBaseControl;
			DuiLabel duiLabel;
			DuiLabel duiLabel2;
			Size size;
			DuiBaseControl duiBaseControl2;
			DuiLabel duiLabel3;
			//string[] array;
			Point location;
			int i;
			DuiBaseControl duiBaseControl3;
			//string[] array2;
			DuiLabel duiLabel4;
			DuiLabel duiLabel5;
			this.equipDetail.Items.Clear();
			duiBaseControl = new DuiBaseControl();
			duiBaseControl.Margin = new Padding(5);
			duiBaseControl.BackColor = Color.White;
			duiBaseControl.Borders.AllWidth = 1;
			duiBaseControl.Borders.AllColor = Color.FromArgb(226, 226, 226);
			duiBaseControl.AutoSize = false;
			duiBaseControl.Font = new Font("微软雅黑", 12f);
			duiBaseControl.ForeColor = Color.Black;
			duiBaseControl.Location = new Point(10, 10);
			duiLabel = new DuiLabel();
			duiLabel.AutoSize = false;
			duiLabel.ForeColor = Color.Black;
			duiLabel.ToolTip = (duiLabel.Text = boss.Name.Trim());
            htyList.RemoveAll(it => it.Equals(boss.Name.Trim()));
            htyList.Add(boss.Name.Trim());
            DrawHtyLab();
            duiLabel.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
			duiLabel.Size = new Size(this.equipDetail.Width, 30);
			duiLabel.Location = new Point(3, 3);
			duiLabel.TextAlign = ContentAlignment.MiddleLeft;
			duiLabel.AutoEllipsis = true;
			duiLabel2 = new DuiLabel();
			duiLabel2.AutoSize = false;
			duiLabel2.ForeColor = Color.Black;
			duiLabel2.ToolTip = (duiLabel2.Text = boss.Beckon);
			duiLabel2.Font = new Font("微软雅黑", 9f);
			size = TextRenderer.MeasureText(boss.Beckon, duiLabel2.Font, new Size(this.equipDetail.Width, 200));
			duiLabel2.Size = new Size(this.equipDetail.Width, size.Height + 20);
			duiLabel2.Location = new Point(3, 40);
			duiLabel2.AutoEllipsis = true;
			duiBaseControl.Controls.Add(duiLabel);
			duiBaseControl.Controls.Add(duiLabel2);
			duiBaseControl.Size = new Size(this.equipDetail.Width, duiLabel2.Top + duiLabel2.Height + 10);
			this.equipDetail.Items.Add(duiBaseControl);
			duiBaseControl2 = new DuiBaseControl();
			duiBaseControl2.Margin = new Padding(5);
			duiBaseControl2.BackColor = Color.White;
			duiBaseControl2.Borders.AllWidth = 1;
			duiBaseControl2.Borders.AllColor = Color.FromArgb(226, 226, 226);
			duiBaseControl2.AutoSize = false;
			duiBaseControl2.Font = new Font("微软雅黑", 12f);
			duiBaseControl2.ForeColor = Color.Black;
			duiBaseControl2.Size = new Size(this.equipDetail.Width, 32);
			duiBaseControl2.Location = new Point(10, duiBaseControl.Top + duiBaseControl.Height + 10);
			duiLabel3 = new DuiLabel();
			duiLabel3.AutoSize = false;
			duiLabel3.ForeColor = Color.Green;
			duiLabel3.ToolTip = (duiLabel3.Text = "Boss掉落装备/材料：");
			duiLabel3.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
			duiLabel3.Size = new Size(this.equipDetail.Width, 26);
			duiLabel3.Location = new Point(3, 3);
			duiLabel3.TextAlign = ContentAlignment.MiddleLeft;
			duiLabel3.AutoEllipsis = true;
			duiBaseControl2.Controls.Add(duiLabel3);
			this.equipDetail.Items.Add(duiBaseControl2);
			//array = boss.DropOut.Split(new char[]
			//{
			//	'\n'
			//}, StringSplitOptions.RemoveEmptyEntries);
            IList<BossDropOutShow> bdShow=BossDropOutService.LoadDataByBossKey(boss.Key);
            location = new Point(10, duiBaseControl2.Top + duiBaseControl2.Height + 10);
			i = 0;
			while (i < bdShow.Count)
			{
				duiBaseControl3 = new DuiBaseControl();
				duiBaseControl3.Margin = new Padding(5);
				duiBaseControl3.BackColor = Color.White;
				duiBaseControl3.Borders.AllWidth = 1;
				duiBaseControl3.Borders.AllColor = Color.FromArgb(226, 226, 226);
				duiBaseControl3.AutoSize = false;
				duiBaseControl3.Font = new Font("微软雅黑", 12f);
				duiBaseControl3.ForeColor = Color.Black;
				duiBaseControl3.Size = new Size(this.equipDetail.Width, 32);
				duiBaseControl3.Location = location;
				duiBaseControl3.MouseClick += new EventHandler<DuiMouseEventArgs>( (TmpItem_MouseClick));
				duiBaseControl3.MouseEnter += new EventHandler<MouseEventArgs>((tmpLbl_MouseEnter));
				duiBaseControl3.MouseLeave += new EventHandler( (tmpLbl_MouseLeave));
                //array2 = array[i].Split(new string[]
                //{
                //	"——"
                //}, StringSplitOptions.RemoveEmptyEntries);
                duiLabel4 = new DuiLabel();
                duiLabel4.AutoSize = false;
                duiLabel4.ForeColor = Color.Black;
                duiLabel4.ToolTip = duiLabel4.Text=DropType.GetDropTypeName(bdShow[i].DropType);
                duiLabel4.Font = new Font("微软雅黑", 10f);
                duiLabel4.Size = new Size(50, 26);
                duiLabel4.Location = new Point(3, 3);
                duiLabel4.TextAlign = ContentAlignment.MiddleLeft;
                duiLabel4.AutoEllipsis = true;
                duiLabel4.Tag = TagLabelTag.DropType;
                duiBaseControl3.Controls.Add(duiLabel4);

                duiLabel4 = new DuiLabel();
				duiLabel4.AutoSize = false;
				duiLabel4.ForeColor = Color.Black;
				duiLabel4.ToolTip = (duiLabel4.Text = (bdShow[i].Chance*100).ToString()+"%");
				duiLabel4.Font = new Font("微软雅黑", 10f);
				duiLabel4.Size = new Size(50, 26);
				duiLabel4.Location = new Point(56, 3);
				duiLabel4.TextAlign = ContentAlignment.MiddleLeft;
				duiLabel4.AutoEllipsis = true;
                duiLabel4.Tag = TagLabelTag.Chance;
                duiBaseControl3.Controls.Add(duiLabel4);

                duiLabel5 = new DuiLabel();
				duiLabel5.AutoSize = false;
				duiLabel5.ForeColor = Color.Black;
				duiLabel5.ToolTip = (duiLabel5.Text = bdShow[i].EquipName);
				duiLabel5.Font = new Font("微软雅黑", 10f);
				duiLabel5.Size = new Size(this.equipDetail.Width - 109, 26);
				duiLabel5.Location = new Point(109, 3);
				duiLabel5.TextAlign = ContentAlignment.MiddleLeft;
				duiLabel5.AutoEllipsis = true;
				duiLabel5.Cursor = Cursors.Hand;
                duiLabel5.Tag = TagLabelTag.Name;
				duiBaseControl3.Controls.Add(duiLabel5);

				this.equipDetail.Items.Add(duiBaseControl3);
				location = new Point(10, duiBaseControl3.Top + duiBaseControl3.Height + 10);
				i = i + 1;
			}
		}

		private void ShowMaterialDetail(Material material)
		{
			DuiBaseControl duiBaseControl;
			DuiLabel duiLabel;
			DuiBaseControl duiBaseControl2;
			DuiLabel duiLabel2;
			IList<Material> list;
			Point location;
			int i;
			Boss boss;
			DuiBaseControl duiBaseControl3;
			DuiLabel duiLabel3;
			DuiBaseControl duiBaseControl4;
			DuiLabel duiLabel4;
			IList<Equip> list2;
			Point location2;
			int j;
			DuiBaseControl duiBaseControl5;
			DuiLabel duiLabel5;
			DuiLabel duiLabel6;
			this.equipDetail.Items.Clear();
			duiBaseControl = new DuiBaseControl();
			duiBaseControl.Margin = new Padding(5);
			duiBaseControl.BackColor = Color.White;
			duiBaseControl.Borders.AllWidth = 1;
			duiBaseControl.Borders.AllColor = Color.FromArgb(226, 226, 226);
			duiBaseControl.AutoSize = false;
			duiBaseControl.Font = new Font("微软雅黑", 12f);
			duiBaseControl.ForeColor = Color.Black;
			duiBaseControl.Location = new Point(10, 10);
			duiLabel = new DuiLabel();
			duiLabel.AutoSize = false;
			duiLabel.ForeColor = Color.Black;
			duiLabel.ToolTip = (duiLabel.Text = material.Name.Trim());
            htyList.RemoveAll(it => it.Equals(material.Name.Trim()));
            htyList.Add(material.Name.Trim());
            DrawHtyLab();
            duiLabel.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
			duiLabel.Size = new Size(this.equipDetail.Width, 30);
			duiLabel.Location = new Point(3, 3);
			duiLabel.TextAlign = ContentAlignment.MiddleLeft;
			duiLabel.AutoEllipsis = true;
			duiBaseControl.Controls.Add(duiLabel);
			duiBaseControl.Size = new Size(this.equipDetail.Width, duiLabel.Top + duiLabel.Height + 10);
			this.equipDetail.Items.Add(duiBaseControl);
			duiBaseControl2 = new DuiBaseControl();
			duiBaseControl2.Margin = new Padding(5);
			duiBaseControl2.BackColor = Color.White;
			duiBaseControl2.Borders.AllWidth = 1;
			duiBaseControl2.Borders.AllColor = Color.FromArgb(226, 226, 226);
			duiBaseControl2.AutoSize = false;
			duiBaseControl2.Font = new Font("微软雅黑", 12f);
			duiBaseControl2.ForeColor = Color.Black;
			duiBaseControl2.Size = new Size(this.equipDetail.Width, 32);
			duiBaseControl2.Location = new Point(10, duiBaseControl.Top + duiBaseControl.Height + 10);
			duiLabel2 = new DuiLabel();
			duiLabel2.AutoSize = false;
			duiLabel2.ForeColor = Color.Green;
			duiLabel2.ToolTip = (duiLabel2.Text = "材料掉落：");
			duiLabel2.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
			duiLabel2.Size = new Size(this.equipDetail.Width, 26);
			duiLabel2.Location = new Point(3, 3);
			duiLabel2.TextAlign = ContentAlignment.MiddleLeft;
			duiLabel2.AutoEllipsis = true;
			duiBaseControl2.Controls.Add(duiLabel2);
			this.equipDetail.Items.Add(duiBaseControl2);
			list = MaterialService.LoadDataByName(material.Name);
			location = new Point(10, duiBaseControl2.Top + duiBaseControl2.Height + 10);
			i = 0;
			while (i < list.Count)
			{
				boss = BossService.LoadDataByKey(list[i].Boss);
				if (boss != null)
				{
					duiBaseControl3 = new DuiBaseControl();
					duiBaseControl3.Margin = new Padding(5);
					duiBaseControl3.BackColor = Color.White;
					duiBaseControl3.Borders.AllWidth = 1;
					duiBaseControl3.Borders.AllColor = Color.FromArgb(226, 226, 226);
					duiBaseControl3.AutoSize = false;
					duiBaseControl3.Font = new Font("微软雅黑", 12f);
					duiBaseControl3.ForeColor = Color.Black;
					duiBaseControl3.Size = new Size(this.equipDetail.Width, 32);
					duiBaseControl3.Location = location;
					duiBaseControl3.MouseClick += new EventHandler<DuiMouseEventArgs>((TmpItem_MouseClick));
					duiBaseControl3.MouseEnter += new EventHandler<MouseEventArgs>( (tmpLbl_MouseEnter));
					duiBaseControl3.MouseLeave += new EventHandler( (tmpLbl_MouseLeave));
					duiLabel3 = new DuiLabel();
					duiLabel3.AutoSize = false;
					duiLabel3.ForeColor = Color.Black;
					duiLabel3.ToolTip = (duiLabel3.Text = ((boss != null) ? boss.Name : ""));
					duiLabel3.Font = new Font("微软雅黑", 10f);
					duiLabel3.Size = new Size(this.equipDetail.Width - 56, 26);
					duiLabel3.Location = new Point(3, 3);
					duiLabel3.TextAlign = ContentAlignment.MiddleLeft;
					duiLabel3.AutoEllipsis = true;
					duiLabel3.Cursor = Cursors.Hand;
                    duiLabel3.Tag = TagLabelTag.Name;
                    duiBaseControl3.Controls.Add(duiLabel3);
					this.equipDetail.Items.Add(duiBaseControl3);
					location = new Point(10, duiBaseControl3.Top + duiBaseControl3.Height + 10);
				}
				i = i + 1;
			}
			duiBaseControl4 = new DuiBaseControl();
			duiBaseControl4.Margin = new Padding(5);
			duiBaseControl4.BackColor = Color.White;
			duiBaseControl4.Borders.AllWidth = 1;
			duiBaseControl4.Borders.AllColor = Color.FromArgb(226, 226, 226);
			duiBaseControl4.AutoSize = false;
			duiBaseControl4.Font = new Font("微软雅黑", 12f);
			duiBaseControl4.ForeColor = Color.Black;
			duiBaseControl4.Size = new Size(this.equipDetail.Width, 32);
			duiBaseControl4.Location = new Point(10, duiBaseControl.Top + duiBaseControl.Height + 10);
			duiLabel4 = new DuiLabel();
			duiLabel4.AutoSize = false;
			duiLabel4.ForeColor = Color.Green;
			duiLabel4.ToolTip = (duiLabel4.Text = "该材料可合成：");
			duiLabel4.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
			duiLabel4.Size = new Size(this.equipDetail.Width, 26);
			duiLabel4.Location = new Point(3, 3);
			duiLabel4.TextAlign = ContentAlignment.MiddleLeft;
			duiLabel4.AutoEllipsis = true;
			duiBaseControl4.Controls.Add(duiLabel4);
			this.equipDetail.Items.Add(duiBaseControl4);
			list2 = EquipService.LoadDataByOrigin(material.Name);
			location2 = new Point(10, duiBaseControl4.Top + duiBaseControl4.Height + 10);
			j = 0;
			while (j < list2.Count)
			{
				duiBaseControl5 = new DuiBaseControl();
				duiBaseControl5.Margin = new Padding(5);
				duiBaseControl5.BackColor = Color.White;
				duiBaseControl5.Borders.AllWidth = 1;
				duiBaseControl5.Borders.AllColor = Color.FromArgb(226, 226, 226);
				duiBaseControl5.AutoSize = false;
				duiBaseControl5.Font = new Font("微软雅黑", 12f);
				duiBaseControl5.ForeColor = Color.Black;
				duiBaseControl5.Size = new Size(this.equipDetail.Width, 49);
				duiBaseControl5.Location = location2;
				duiBaseControl5.MouseClick += new EventHandler<DuiMouseEventArgs>( (TmpItem_MouseClick));
				duiBaseControl5.MouseEnter += new EventHandler<MouseEventArgs>( (tmpLbl_MouseEnter));
				duiBaseControl5.MouseLeave += new EventHandler( (tmpLbl_MouseLeave));
				duiLabel5 = new DuiLabel();
				duiLabel5.AutoSize = false;
				duiLabel5.ForeColor = Color.Orange;
				duiLabel5.ToolTip = (duiLabel5.Text = ((list2[j] != null) ? list2[j].Name : ""));
				duiLabel5.Font = new Font("微软雅黑", 10f);
				duiLabel5.Size = new Size(this.equipDetail.Width - 56, 26);
				duiLabel5.Location = new Point(3, 3);
				duiLabel5.TextAlign = ContentAlignment.MiddleLeft;
				duiLabel5.AutoEllipsis = true;
				duiLabel5.Cursor = Cursors.Hand;
				duiLabel6 = new DuiLabel();
				duiLabel6.AutoSize = false;
				duiLabel6.ForeColor = Color.Gray;
				duiLabel6.ToolTip = (duiLabel6.Text = ((list2[j] != null) ? list2[j].Origin.Trim(new char[]
				{
					','
				}).Replace(",", " + ") : ""));
				duiLabel6.Font = new Font("微软雅黑", 9f);
				duiLabel6.Size = new Size(this.equipDetail.Width - 56, 14);
				duiLabel6.Location = new Point(3, 32);
				duiLabel6.TextAlign = ContentAlignment.MiddleLeft;
				duiLabel6.AutoEllipsis = true;
				duiLabel6.Cursor = Cursors.Hand;
                duiLabel5.Tag = TagLabelTag.Name;
                duiBaseControl5.Controls.Add(duiLabel5);
				duiBaseControl5.Controls.Add(duiLabel6);
				this.equipDetail.Items.Add(duiBaseControl5);
				location2 = new Point(10, duiBaseControl5.Top + duiBaseControl5.Height + 10);
				j = j + 1;
			}
		}

		private void ShowHeroDetail(Hero hero)
		{
			DuiBaseControl duiBaseControl;
			DuiLabel duiLabel;
			DuiBaseControl duiBaseControl2;
			DuiLabel duiLabel2;
			IList<Exclusive> list;
			Point location;
			int i;
			DuiBaseControl duiBaseControl3;
			Equip equip;
			DuiLabel duiLabel3;
			DuiLabel duiLabel4;
			DuiLabel duiLabel5;
			this.equipDetail.Items.Clear();
			duiBaseControl = new DuiBaseControl();
			duiBaseControl.Margin = new Padding(5);
			duiBaseControl.BackColor = Color.White;
			duiBaseControl.Borders.AllWidth = 1;
			duiBaseControl.Borders.AllColor = Color.FromArgb(226, 226, 226);
			duiBaseControl.AutoSize = false;
			duiBaseControl.Font = new Font("微软雅黑", 12f);
			duiBaseControl.ForeColor = Color.Black;
			duiBaseControl.Location = new Point(10, 10);
			duiLabel = new DuiLabel();
			duiLabel.AutoSize = false;
			duiLabel.ForeColor = Color.Black;
			duiLabel.ToolTip = (duiLabel.Text = hero.Name.Trim());
            //htyList.RemoveAll(it => it.Equals(hero.Name.Trim()));
            //htyList.Add(hero.Name.Trim());
            //DrawHtyLab();
            duiLabel.Font = new Font("微软雅黑", 12f, FontStyle.Bold);
			duiLabel.Size = new Size(this.equipDetail.Width, 30);
			duiLabel.Location = new Point(3, 3);
			duiLabel.TextAlign = ContentAlignment.MiddleLeft;
			duiLabel.AutoEllipsis = true;
			duiBaseControl.Controls.Add(duiLabel);
			duiBaseControl.Size = new Size(this.equipDetail.Width, duiLabel.Top + duiLabel.Height + 10);
			this.equipDetail.Items.Add(duiBaseControl);
			duiBaseControl2 = new DuiBaseControl();
			duiBaseControl2.Margin = new Padding(5);
			duiBaseControl2.BackColor = Color.White;
			duiBaseControl2.Borders.AllWidth = 1;
			duiBaseControl2.Borders.AllColor = Color.FromArgb(226, 226, 226);
			duiBaseControl2.AutoSize = false;
			duiBaseControl2.Font = new Font("微软雅黑", 12f);
			duiBaseControl2.ForeColor = Color.Black;
			duiBaseControl2.Size = new Size(this.equipDetail.Width, 32);
			duiBaseControl2.Location = new Point(10, duiBaseControl.Top + duiBaseControl.Height + 10);
			duiLabel2 = new DuiLabel();
			duiLabel2.AutoSize = false;
			duiLabel2.ForeColor = Color.Green;
			duiLabel2.ToolTip = (duiLabel2.Text = "英雄专属：");
			duiLabel2.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
			duiLabel2.Size = new Size(this.equipDetail.Width, 26);
			duiLabel2.Location = new Point(3, 3);
			duiLabel2.TextAlign = ContentAlignment.MiddleLeft;
			duiLabel2.AutoEllipsis = true;
			duiBaseControl2.Controls.Add(duiLabel2);
			this.equipDetail.Items.Add(duiBaseControl2);
			list = ExclusiveServices.LoadDataByHero(hero.Key);
			location = new Point(10, duiBaseControl2.Top + duiBaseControl2.Height + 10);
			i = 0;
			while (i < list.Count)
			{
				duiBaseControl3 = new DuiBaseControl();
				duiBaseControl3.Margin = new Padding(5);
				duiBaseControl3.BackColor = Color.White;
				duiBaseControl3.Borders.AllWidth = 1;
				duiBaseControl3.Borders.AllColor = Color.FromArgb(226, 226, 226);
				duiBaseControl3.AutoSize = false;
				duiBaseControl3.Font = new Font("微软雅黑", 12f);
				duiBaseControl3.ForeColor = Color.Black;
				duiBaseControl3.Size = new Size(this.equipDetail.Width, 143);
				duiBaseControl3.Location = location;
				duiBaseControl3.MouseClick += new EventHandler<DuiMouseEventArgs>( TmpItem_MouseClick);
				duiBaseControl3.MouseEnter += new EventHandler<MouseEventArgs>( (tmpLbl_MouseEnter));
				duiBaseControl3.MouseLeave += new EventHandler((tmpLbl_MouseLeave));
				equip = EquipService.LoadDataByKey(list[i].EquipKey);
				duiLabel3 = new DuiLabel();
				duiLabel3.AutoSize = false;
				duiLabel3.ForeColor = Color.Green;
				duiLabel3.ToolTip = (duiLabel3.Text = equip.Name);
				duiLabel3.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
				duiLabel3.Size = new Size(this.equipDetail.Width, 22);
				duiLabel3.Location = new Point(3, 3);
				duiLabel3.TextAlign = ContentAlignment.MiddleLeft;
				duiLabel3.AutoEllipsis = true;
				duiLabel4 = new DuiLabel();
				duiLabel4.AutoSize = false;
				duiLabel4.ForeColor = Color.Gray;
				duiLabel4.ToolTip = (duiLabel4.Text = list[i].Name);
				duiLabel4.Font = new Font("微软雅黑", 10f);
				duiLabel4.Size = new Size(this.equipDetail.Width, 22);
				duiLabel4.Location = new Point(3, 28);
				duiLabel4.TextAlign = ContentAlignment.MiddleLeft;
				duiLabel4.AutoEllipsis = true;
				duiLabel4.Cursor = Cursors.Hand;
				duiLabel5 = new DuiLabel();
				duiLabel5.AutoSize = false;
				duiLabel5.ForeColor = Color.Black;
				duiLabel5.ToolTip = (duiLabel5.Text = list[i].Effect);
				duiLabel5.Font = new Font("微软雅黑", 10f);
				duiLabel5.Size = new Size(this.equipDetail.Width, 90);
				duiLabel5.Location = new Point(3, 53);
				duiLabel5.TextAlign = ContentAlignment.TopLeft;
				duiLabel5.AutoEllipsis = true;
				duiLabel5.Cursor = Cursors.Hand;
				duiBaseControl3.Controls.Add(duiLabel3);
				duiBaseControl3.Controls.Add(duiLabel4);
				duiBaseControl3.Controls.Add(duiLabel5);
				this.equipDetail.Items.Add(duiBaseControl3);
				location = new Point(10, duiBaseControl3.Top + duiBaseControl3.Height + 10);
				i = i + 1;
			}
		}

		private void TmpItem_MouseClick(object sender, DuiMouseEventArgs e)
		{
			DuiBaseControl duiBaseControl;
			string text;
			Equip equip;
			Boss boss;
			Material material;
			duiBaseControl = (sender as DuiBaseControl);
			if (duiBaseControl != null&& duiBaseControl.Controls.Exists(it=>it.Tag!=null&&it.Tag.Equals(TagLabelTag.Name)))
			{

                text = duiBaseControl.Controls.First(it=>it.Tag.Equals(TagLabelTag.Name)).Text;
				equip = EquipService.LoadDataByName(text);
				if (equip != null)
				{
					this.ShowEquipDetail(equip);
				}
				else
				{
					boss = BossService.LoadDataByName(text);
					if (boss != null)
					{
						this.ShowBossDetail(boss);
					}
					else
					{
						material = MaterialService.LoadDataByNameEx(text);
						if (material != null)
						{
							this.ShowMaterialDetail(material);
						}
					}
				}
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			IList<Equip> list;
			IList<Boss> list2;
			IList<Material> list3;
			List<string> nameList=new List<string> ();
			Point location;
			int i;
			DuiBaseControl duiBaseControl;
			DuiLabel duiLabel;
			this.equipDetail.Items.Clear();
			list = EquipService.LoadDataByNameEx(this.txtName.Text.Trim());
			list2 = BossService.SearchDataByName(this.txtName.Text.Trim());
			list3 = MaterialService.SearchDataByName(this.txtName.Text.Trim());
			if (list != null)
                nameList.AddRange(list.Select(it=>it.Name));
            if (list2 != null)
                nameList.AddRange(list2.Select(it => it.Name));
            if (list3 != null)
                nameList.AddRange(list3.Select(it => it.Name));
            nameList = Enumerable.ToList<string>(Enumerable.Distinct<string>(nameList));
			if (nameList.Count > 0)
			{
				location = new Point(10, 10);
				i = 0;
				while (i < nameList.Count)
				{
					duiBaseControl = new DuiBaseControl();
					duiBaseControl.Margin = new Padding(5);
					duiBaseControl.BackColor = Color.White;
					duiBaseControl.Borders.AllWidth = 1;
					duiBaseControl.Borders.AllColor = Color.FromArgb(226, 226, 226);
					duiBaseControl.AutoSize = false;
					duiBaseControl.Font = new Font("微软雅黑", 12f);
					duiBaseControl.ForeColor = Color.Black;
					duiBaseControl.Size = new Size(this.equipDetail.Width, 32);
					duiBaseControl.Location = location;
					duiBaseControl.MouseClick += new EventHandler<DuiMouseEventArgs>( (TmpItem_MouseClick));
					duiBaseControl.MouseEnter += new EventHandler<MouseEventArgs>( (tmpLbl_MouseEnter));
					duiBaseControl.MouseLeave += new EventHandler((tmpLbl_MouseLeave));
					duiLabel = new DuiLabel();
					duiLabel.AutoSize = false;
					duiLabel.ForeColor = Color.Black;
					duiLabel.ToolTip = (duiLabel.Text = nameList[i]);
					duiLabel.Font = new Font("微软雅黑", 10f);
					duiLabel.Size = new Size(this.equipDetail.Width, 26);
					duiLabel.Location = new Point(3, 3);
					duiLabel.TextAlign = ContentAlignment.MiddleLeft;
					duiLabel.AutoEllipsis = true;
					duiLabel.Cursor = Cursors.Hand;
                    duiLabel.Tag = TagLabelTag.Name;
					duiBaseControl.Controls.Add(duiLabel);
					this.equipDetail.Items.Add(duiBaseControl);
					location = new Point(10, duiBaseControl.Top + duiBaseControl.Height + 10);
					i = i + 1;
				}
			}
		}

		private void btnJj_Click(object sender, EventArgs e)
		{
			this.LoadData(1);
		}

		private void btnXx_Click(object sender, EventArgs e)
		{
			this.LoadData(2);
		}

		private void txtName_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.btnSearch_Click(null, null);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager;
			componentResourceManager = new ComponentResourceManager(Type.GetTypeFromHandle(typeof(MargicEquip).TypeHandle));
			this.tabEquip = new DSkinTabControl();
			this.pageWq = new DSkinTabPage();
			this.pageBoss = new DSkinTabPage();
			this.pageTk = new DSkinTabPage();
			this.pageYf = new DSkinTabPage();
			this.pageSp = new DSkinTabPage();
			this.pageCb = new DSkinTabPage();
			this.pageQt = new DSkinTabPage();
			this.pageZs = new DSkinTabPage();
			this.pageCl = new DSkinTabPage();
			this.equipDetail = new DSkinListBox();
			this.dSkinPanel1 = new DSkinPanel();
			this.btnSearch = new DSkinButton();
			this.txtName = new DSkinTextBox();
            this.dataViewHty = new DSkinListBox();

            this.btnXx = new DSkinButton();
			this.btnJj = new DSkinButton();
			this.tabEquip.SuspendLayout();
			((ISupportInitialize)this.equipDetail).BeginInit();
			this.dSkinPanel1.SuspendLayout();
			base.SuspendLayout();
			this.tabEquip.Alignment = TabAlignment.Left;
			this.tabEquip.BitmapCache = false;
			this.tabEquip.Controls.Add(this.pageWq);
			this.tabEquip.Controls.Add(this.pageBoss);
			this.tabEquip.Controls.Add(this.pageTk);
			this.tabEquip.Controls.Add(this.pageYf);
			this.tabEquip.Controls.Add(this.pageSp);
			this.tabEquip.Controls.Add(this.pageCb);
			this.tabEquip.Controls.Add(this.pageQt);
			this.tabEquip.Controls.Add(this.pageZs);
			this.tabEquip.Controls.Add(this.pageCl);
			this.tabEquip.Dock = DockStyle.Left;
			this.tabEquip.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
			this.tabEquip.HoverBackColors = new Color[]
			{
				Color.FromArgb(236, 242, 230)
			};
			this.tabEquip.ItemBackgroundImage = null;
			this.tabEquip.ItemBackgroundImageHover = null;
			this.tabEquip.ItemBackgroundImageSelected = null;
			this.tabEquip.ItemSize = new Size(85, 45);
			this.tabEquip.Location = new Point(4, 100);
			this.tabEquip.Multiline = true;
			this.tabEquip.Name = "tabEquip";
			this.tabEquip.NormalBackColors = new Color[]
			{
				Color.FromArgb(240, 240, 240)
			};
			this.tabEquip.PageImagePosition = ePageImagePosition.Left;
			this.tabEquip.SelectedBackColors = new Color[]
			{
				Color.FromArgb(226, 237, 218)
			};
			this.tabEquip.Size = new Size(636, 799);
			this.tabEquip.SizeMode = TabSizeMode.Fixed;
			this.tabEquip.TabIndex = 1;
			this.tabEquip.UpdownBtnArrowNormalColor = Color.Black;
			this.tabEquip.UpdownBtnArrowPressColor = Color.Gray;
			this.tabEquip.UpdownBtnBackColor = Color.White;
			this.tabEquip.UpdownBtnBorderColor = Color.Black;
			this.pageWq.BackColor = Color.Transparent;
			this.pageWq.Dock = DockStyle.Fill;
			this.pageWq.Location = new Point(45, 0);
			this.pageWq.Name = "pageWq";
			this.pageWq.Size = new Size(591, 799);
			this.pageWq.TabIndex = 0;
			this.pageWq.TabItemImage = null;
			this.pageWq.Text = "武器";
			this.pageBoss.BackColor = Color.Transparent;
			this.pageBoss.Dock = DockStyle.Fill;
			this.pageBoss.Location = new Point(45, 0);
			this.pageBoss.Name = "pageBoss";
			this.pageBoss.Size = new Size(591, 799);
			this.pageBoss.TabIndex = 6;
			this.pageBoss.TabItemImage = null;
			this.pageBoss.Text = "Boss";
			this.pageTk.BackColor = Color.Transparent;
			this.pageTk.Dock = DockStyle.Fill;
			this.pageTk.Location = new Point(45, 0);
			this.pageTk.Name = "pageTk";
			this.pageTk.Size = new Size(591, 799);
			this.pageTk.TabIndex = 1;
			this.pageTk.TabItemImage = null;
			this.pageTk.Text = "头盔";
			this.pageYf.BackColor = Color.Transparent;
			this.pageYf.Dock = DockStyle.Fill;
			this.pageYf.Location = new Point(45, 0);
			this.pageYf.Name = "pageYf";
			this.pageYf.Size = new Size(591, 799);
			this.pageYf.TabIndex = 2;
			this.pageYf.TabItemImage = null;
			this.pageYf.Text = "衣服";
			this.pageSp.BackColor = Color.Transparent;
			this.pageSp.Dock = DockStyle.Fill;
			this.pageSp.Location = new Point(45, 0);
			this.pageSp.Name = "pageSp";
			this.pageSp.Size = new Size(591, 799);
			this.pageSp.TabIndex = 3;
			this.pageSp.TabItemImage = null;
			this.pageSp.Text = "饰品";
			this.pageCb.BackColor = Color.Transparent;
			this.pageCb.Dock = DockStyle.Fill;
			this.pageCb.Location = new Point(45, 0);
			this.pageCb.Name = "pageCb";
			this.pageCb.Size = new Size(591, 799);
			this.pageCb.TabIndex = 4;
			this.pageCb.TabItemImage = null;
			this.pageCb.Text = "翅膀";
			this.pageQt.BackColor = Color.Transparent;
			this.pageQt.Dock = DockStyle.Fill;
			this.pageQt.Location = new Point(45, 0);
			this.pageQt.Name = "pageQt";
			this.pageQt.Size = new Size(591, 799);
			this.pageQt.TabIndex = 5;
			this.pageQt.TabItemImage = null;
			this.pageQt.Text = "其他";
			this.pageZs.BackColor = Color.Transparent;
			this.pageZs.Dock = DockStyle.Fill;
			this.pageZs.Location = new Point(45, 0);
			this.pageZs.Name = "pageZs";
			this.pageZs.Size = new Size(591, 799);
			this.pageZs.TabIndex = 7;
			this.pageZs.TabItemImage = null;
			this.pageZs.Text = "专属";
			this.pageCl.BackColor = Color.Transparent;
			this.pageCl.Dock = DockStyle.Fill;
			this.pageCl.Location = new Point(45, 0);
			this.pageCl.Name = "pageCl";
			this.pageCl.Size = new Size(591, 799);
			this.pageCl.TabIndex = 8;
			this.pageCl.TabItemImage = null;
			this.pageCl.Text = "材料";
			this.equipDetail.BackColor = Color.Transparent;
			//this.equipDetail.Dock = DockStyle.Fill;
            this.equipDetail.Location = new Point(640,100);
			this.equipDetail.Name = "equipDetail";
			this.equipDetail.ScrollBarWidth = 12;
            this.equipDetail.Size = new Size(664, 769);//799);
			this.equipDetail.TabIndex = 2;
			this.equipDetail.Text = "dSkinListBox7";
			this.equipDetail.Value = 0.0;
            this.equipDetail.Borders.AllColor = Color.Red;
			this.dSkinPanel1.BackColor = Color.Transparent;
			this.dSkinPanel1.Controls.Add(this.btnSearch);
			this.dSkinPanel1.Controls.Add(this.txtName);


            this.dSkinPanel1.Controls.Add(this.btnXx);
			this.dSkinPanel1.Controls.Add(this.btnJj);
			this.dSkinPanel1.Dock = DockStyle.Top;
			this.dSkinPanel1.Location = new Point(4, 32);
			this.dSkinPanel1.Name = "dSkinPanel1";
			this.dSkinPanel1.RightBottom = (Image)componentResourceManager.GetObject("dSkinPanel1.RightBottom");
            this.dSkinPanel1.Size = new Size(1300,33);
			this.dSkinPanel1.TabIndex = 3;
			this.dSkinPanel1.Text = "dSkinPanel1";

            this.btnSearch.AdaptImage = true;
			this.btnSearch.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.btnSearch.BaseColor = Color.FromArgb(255, 128, 0);
			this.btnSearch.ButtonBorderColor = Color.Gray;
			this.btnSearch.ButtonBorderWidth = 1;
			this.btnSearch.ButtonStyle = ButtonStyles.Style2;
			this.btnSearch.DialogResult = DialogResult.None;
			this.btnSearch.ForeColor = Color.White;
			this.btnSearch.HoverColor = Color.Empty;
			this.btnSearch.HoverImage = null;
			this.btnSearch.IsPureColor = false;
			this.btnSearch.Location = new Point(1219, 3);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.NormalImage = null;
			this.btnSearch.PressColor = Color.Empty;
			this.btnSearch.PressedImage = null;
			this.btnSearch.Radius = 2;
			this.btnSearch.ShowButtonBorder = false;
			this.btnSearch.Size = new Size(78, 27);
			this.btnSearch.TabIndex = 3;
			this.btnSearch.Tag = "E:\\Game\\Warcraft III Frozen Throne";
			this.btnSearch.Text = "搜索";
			this.btnSearch.TextAlign = ContentAlignment.MiddleCenter;
			this.btnSearch.TextPadding = 0;
			this.btnSearch.Click += new EventHandler( (btnSearch_Click));
			this.txtName.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.txtName.BitmapCache = false;
			this.txtName.BorderColor = Color.FromArgb(226, 226, 226);
			this.txtName.Font = new Font("微软雅黑", 10f, FontStyle.Regular, GraphicsUnit.Point, 134);
			this.txtName.Location = new Point(636, 4);
			this.txtName.Name = "txtName";
			this.txtName.Size = new Size(577, 25);
			this.txtName.TabIndex = 2;
			this.txtName.TransparencyKey = Color.Empty;
			this.txtName.WaterFont = new Font("微软雅黑", 10f, FontStyle.Regular, GraphicsUnit.Point, 134);
			this.txtName.WaterText = "输入装备/材料/Boss名称搜索";
			this.txtName.WaterTextOffset = new Point(0, 3);
			this.txtName.KeyPress += new KeyPressEventHandler( (txtName_KeyPress));

            this.dataViewHty.BackColor = Color.Transparent;
            this.dataViewHty.Dock = DockStyle.Fill;
            this.dataViewHty.Location = new Point(636, 67);
            this.dataViewHty.Name = "dataViewHty";
            this.dataViewHty.ScrollBarWidth = 3;
            this.dataViewHty.Size = new Size(600, 30);
            this.dataViewHty.TabIndex = 9;
            this.dataViewHty.Text = "dSkinListBox8";
            this.dataViewHty.Value = 0.0;

            DuiBaseControl viewHty = new DuiBaseControl();
            viewHty.Margin = new Padding(1);
            viewHty.BackColor = Color.White;
            viewHty.Borders.AllWidth = 1;
            viewHty.Borders.AllColor = Color.FromArgb(226, 226, 226);
            viewHty.AutoSize = true;
            viewHty.Font = new Font("微软雅黑", 12f);
            viewHty.ForeColor = Color.Black;
            viewHty.Size = new Size(600, 26);
            viewHty.Location = new Point(3, 3);


            DuiLabel duiLabel = new DuiLabel();
            duiLabel.AutoSize = true;
            duiLabel.ForeColor = Color.Green;
            duiLabel.ToolTip = (duiLabel.Text = "最近查看:");
            duiLabel.Font = new Font("微软雅黑", 8f, FontStyle.Bold);
            duiLabel.Size = new Size(60, 18);
            duiLabel.Location = new Point(2, 2);
            duiLabel.TextAlign = ContentAlignment.MiddleLeft;
            duiLabel.AutoEllipsis = true;
            viewHty.Controls.Add(duiLabel);
            this.dataViewHty.Items.Add(viewHty);
            //dataViewHty.Borders.AllColor = Color.Red;
            dSkinPanel1.Borders.AllColor = Color.Blue;



            this.btnXx.AdaptImage = true;
			this.btnXx.BaseColor = Color.Green;
			this.btnXx.ButtonBorderColor = Color.Gray;
			this.btnXx.ButtonBorderWidth = 1;
			this.btnXx.ButtonStyle = ButtonStyles.Style2;
			this.btnXx.DialogResult = DialogResult.None;
			this.btnXx.ForeColor = Color.White;
			this.btnXx.HoverColor = Color.Empty;
			this.btnXx.HoverImage = null;
			this.btnXx.IsPureColor = false;
			this.btnXx.Location = new Point(129, 3);
			this.btnXx.Name = "btnXx";
			this.btnXx.NormalImage = null;
			this.btnXx.PressColor = Color.Empty;
			this.btnXx.PressedImage = null;
			this.btnXx.Radius = 2;
			this.btnXx.ShowButtonBorder = false;
			this.btnXx.Size = new Size(78, 27);
			this.btnXx.TabIndex = 1;
			this.btnXx.Tag = "E:\\Game\\Warcraft III Frozen Throne";
			this.btnXx.Text = "详细模式";
			this.btnXx.TextAlign = ContentAlignment.MiddleCenter;
			this.btnXx.TextPadding = 0;
			this.btnXx.Click += new EventHandler( (btnXx_Click));
			this.btnJj.AdaptImage = true;
			this.btnJj.BaseColor = Color.Green;
			this.btnJj.ButtonBorderColor = Color.Gray;
			this.btnJj.ButtonBorderWidth = 1;
			this.btnJj.ButtonStyle = ButtonStyles.Style2;
			this.btnJj.DialogResult = DialogResult.None;
			this.btnJj.ForeColor = Color.White;
			this.btnJj.HoverColor = Color.Empty;
			this.btnJj.HoverImage = null;
			this.btnJj.IsPureColor = false;
			this.btnJj.Location = new Point(45, 3);
			this.btnJj.Name = "btnJj";
			this.btnJj.NormalImage = null;
			this.btnJj.PressColor = Color.Empty;
			this.btnJj.PressedImage = null;
			this.btnJj.Radius = 2;
			this.btnJj.ShowButtonBorder = false;
			this.btnJj.Size = new Size(78, 27);
			this.btnJj.TabIndex = 1;
			this.btnJj.Tag = "E:\\Game\\Warcraft III Frozen Throne";
			this.btnJj.Text = "精简模式";
			this.btnJj.TextAlign = ContentAlignment.MiddleCenter;
			this.btnJj.TextPadding = 0;
			this.btnJj.Click += new EventHandler( (btnJj_Click));
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CaptionOffset = new Point(0, 2);
			base.ClientSize = new Size(1308, 870);
            base.Controls.Add(this.equipDetail);
            base.Controls.Add(this.dataViewHty);
			base.Controls.Add(this.tabEquip);
			base.Controls.Add(this.dSkinPanel1);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "MargicEquip";
			base.ShowShadow = true;
			base.Tag = "";
			this.Text = "PrickeIn世界RPG装备大全";
			base.Load += new EventHandler( (MargicEquip_Load));
			this.tabEquip.ResumeLayout(false);
			((ISupportInitialize)this.equipDetail).EndInit();
			this.dSkinPanel1.ResumeLayout(false);
			this.dSkinPanel1.PerformLayout();
			base.ResumeLayout(false);

        }
	}
}
