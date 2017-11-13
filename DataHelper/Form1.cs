using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldRpgEquip.Services;
using WorldRpgModel;
using System.IO;
using DataHelper.Common;
using DSkin.Forms;
using DSkin.DirectUI;
using WorldRpgService;

namespace DataHelper
{
    public partial class Form1 : DSkinForm
    {
        BindingList<Boss> bossList;
        BindingList<Equip> equipList;
        BindingList<MaterialShow> materList;
        BindingList<BossDropOutShow> dropOutList;
        BindingList<Hero> heroList;
        BindingList<Exclusive> exclusiveList;
        ComboBox cb_boss;
        ComboBox cb_boss_mater;
        ComboBox cb_equipAndMater;

        ComboBox cb_hero;
        public static class ControlsName
        {
            public static class DataGridView
            {
                public const string BOSS = "dgv_boss";
                public const string EQUIP = "dgv_equip";
                public const string MATER = "dgv_mater";
                public const string DROPOUT = "dgv_dropOut";
                public const string HEROS = "dgv_heros";
                public const string EXCLUSIVE = "dgv_exclusive";
            }
            public static class TabPage
            {
                public const string BOSS = "tp_boss";
                public const string EQUIP = "tp_equip";
                public const string MATER = "tp_mater";
                public const string DROPOUT = "tp_dropOut";
                public const string HEROS = "tp_heros";
                public const string EXCLUSIVE = "tp_exclusive";
            }

        }
        public Form1()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception e1) { MessageBox.Show(e1.Message); }
            //TranBossDropOutData();
            InitalDGV();
        }
        private void TranBossDropOutData()
        {
            Logger.WriteHeader();
            List<BossDropOut> bossDropOut = new List<BossDropOut>();
            IList<Boss> boss = BossService.LoadData();
            BossDropOut bdo;
            Equip equip;
            Material material;
            foreach (var item in boss)
            {
                var array = item.DropOut.Split(new char[]
                {
                    '\n'
                }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var it in array)
                {
                    bdo = new BossDropOut();
                    bdo.DropType = 0;
                    bdo.Key = Guid.NewGuid().ToString();
                    bdo.BossKey = item.Key;
                    var array2 = it.Split(new string[]//0 name 1 chance
                    {
                        "——"
                    }, StringSplitOptions.RemoveEmptyEntries);
                    if (array2.Length > 0)
                    {
                        equip = EquipService.LoadDataByName(array2[0].Trim().Replace(',',' ').Replace("，"," ").Replace('.',' ').Replace("誓约之戒,勇气", "誓约之戒-勇气"));
                        material = MaterialService.LoadDataByNameEx(array2[0].Replace(',',' ').Replace("，", " "));
                        if (equip != null)
                            bdo.EquipKey = equip.Key;
                        else if (material != null)
                            bdo.EquipKey = material.Key;
                        else
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("BOSS:").Append(item.Name).Append("\t");
                            sb.Append("Item info:").Append(it).Append("\r\n");
                            Logger.WriteLog(sb.ToString());
                            continue;
                        }//throw new Exception("No such equipment:" + array2[0]);
                        double chance = 0;
                        string c = array2[1].Substring(0, array2[1].Length - 1);
                        if (double.TryParse(array2[1].Substring(0, array2[1].Length - 1), out chance)) bdo.Chance = chance / 100;
                    }
                    bossDropOut.Add(bdo);
                }
            }
            Logger.WriteTail();
            bossDropOut = bossDropOut.Where(it => !String.IsNullOrWhiteSpace(it.EquipKey)).ToList();

            BossDropOutService.insertBatch(bossDropOut);
            return;
        }
        private void tb_MouseClick(object sender, TabControlEventArgs e)
        {
            //MessageBox.Show("OK");
            return;
        }
        private void dgvDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "错误");
            return;
        }
        private void dgvCellEditEnd(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Rows[e.RowIndex].Cells["key"].Value == null)
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Chocolate;
            else
            if ((dgv.Rows[e.RowIndex].Cells["Changed"].Value.Equals(true)))
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Green;
        }

        void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = ((ComboBox)sender);
            float size = cb.ItemHeight;
            //MessageBox.Show(e.Bounds.Height.ToString());
            // Draw the background of the item.
            e.DrawBackground();
            // Draw each string in the items
            if(e.Index>=0)
            e.Graphics.DrawString(((Model.Unit)cb.Items[e.Index]).Name,
                cb.Font,
                System.Drawing.Brushes.Black,
                new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();

        }
        private void dgvCell_clicked(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            switch (dgv.Name)
            {
                case ControlsName.DataGridView.DROPOUT:
                    if (e.ColumnIndex == dgv.Columns["BossName"].Index)
                    {
                        Rectangle rect = dgv_dropOut.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                        //MessageBox.Show(rect.ToString());
                        cb_boss.Location = rect.Location;
                        cb_boss.Size = rect.Size;
                        //cb_boss.SetBounds(rect.X, rect.Y, rect.Width, rect.Height);
                        if (dgv["BossKey", e.RowIndex].Value != null)
                            cb_boss.SelectedValue = dgv["BossKey", e.RowIndex].Value.ToString();
                        cb_boss.Visible = true;
                        //MessageBox.Show(cb_boss.Size.ToString());
                    }
                    if (e.ColumnIndex == dgv.Columns["EquipName"].Index)
                    {
                        Rectangle rect = dgv_dropOut.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                        //MessageBox.Show(rect.ToString());
                        cb_equipAndMater.Location = rect.Location;
                        cb_equipAndMater.Size = rect.Size;
                        //cb_boss.SetBounds(rect.X, rect.Y, rect.Width, rect.Height);
                        if (dgv["EquipKey", e.RowIndex].Value != null)
                            cb_equipAndMater.SelectedValue = dgv["EquipKey", e.RowIndex].Value.ToString();
                        cb_equipAndMater.Visible = true;
                        //MessageBox.Show(cb_boss.Size.ToString());
                    }
                    break;
                case ControlsName.DataGridView.MATER:
                    if (e.ColumnIndex == dgv.Columns["BossName"].Index)
                    {
                        Rectangle rect = dgv_mater.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                        cb_boss_mater.Location = rect.Location;
                        cb_boss_mater.Size = rect.Size;
                        if (dgv["BossName", e.RowIndex].Value != null)
                            cb_boss_mater.SelectedValue = dgv["BossKey", e.RowIndex].Value.ToString();
                        cb_boss_mater.Visible = true;
                    }
                    break;
                default:break;
            }

        }
        private void dgvCell_leave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            switch (dgv.Name)
            {
                case ControlsName.DataGridView.DROPOUT:
                    if (e.ColumnIndex == dgv.Columns["BossName"].Index)
                    {
                        if (cb_boss.Visible&&cb_boss.SelectedValue!=null &&(dgv.Rows[e.RowIndex].IsNewRow || dgv.Rows[e.RowIndex].Cells["BossKey"].Value!=((cb_boss.SelectedValue))))
                        {
                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cb_boss.Text;
                            dgv.Rows[e.RowIndex].Cells["BossKey"].Value = cb_boss.SelectedValue;
                        }
                        cb_boss.Visible = false;
                        dgvCellEditEnd(sender, e);
                    }
                    if (e.ColumnIndex == dgv.Columns["EquipName"].Index)
                    {
                        if (cb_equipAndMater.Visible && cb_equipAndMater.SelectedValue != null && (dgv.Rows[e.RowIndex].IsNewRow||dgv.Rows[e.RowIndex].Cells["EquipKey"].Value!=((cb_equipAndMater.SelectedValue))))
                        {
                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cb_equipAndMater.Text;
                            dgv.Rows[e.RowIndex].Cells["EquipKey"].Value = cb_equipAndMater.SelectedValue;
                        }
                        cb_equipAndMater.Visible = false;
                        dgvCellEditEnd(sender, e);
                    }
                    break;
                case ControlsName.DataGridView.MATER:
                    if (e.ColumnIndex == dgv.Columns["BossName"].Index)
                    {
                        if (cb_boss_mater.Visible && cb_boss_mater.SelectedValue != null && (dgv.Rows[e.RowIndex].IsNewRow || dgv.Rows[e.RowIndex].Cells["BossKey"].Value!=((cb_boss_mater.SelectedValue))))
                        {
                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cb_boss_mater.Text;
                            dgv.Rows[e.RowIndex].Cells["BossKey"].Value = cb_boss_mater.SelectedValue;
                        }
                        cb_boss_mater.Visible = false;
                        dgvCellEditEnd(sender, e);
                    }
                    break;
                default: break;
            }

        }
        private void tb_searConditionEnter(object sender,KeyEventArgs e) {
            if (e.KeyCode.Equals(Keys.Enter))
                btn_Search_Clicked(btn_search, null);
        }
        private void btn_Search_Clicked(object sender,EventArgs e)
        {
            //tc_all.SelectedTab.Name
            string strCondition = tb_searCondition.Text.Trim();
            switch (tc_all.SelectedTab.Name)
            {
                case ControlsName.TabPage.BOSS:
                    dgv_boss.DataSource = bossList.Where(it=>it.Beckon.Contains(strCondition)||it.Name.Contains(strCondition)||it.DropOut.Contains(strCondition)).ToList();
                    dgv_boss.AllowUserToAddRows = true;
                    break;
                case ControlsName.TabPage.EQUIP:
                    dgv_equip.DataSource = equipList.Where(it => it.Attribute.Contains(strCondition) || it.Name.Contains(strCondition) || it.Origin.Contains(strCondition)).ToList();
                    dgv_equip.AllowUserToAddRows = true;
                    break;
                case ControlsName.TabPage.MATER:
                    dgv_mater.DataSource = materList.Where(it => it.Name.Contains(strCondition)).ToList();
                    dgv_mater.AllowUserToAddRows = true;
                    break;
                case ControlsName.TabPage.HEROS:
                    dgv_heros.DataSource = heroList.Where(it => it.Vocation.Contains(strCondition) || it.Name.Contains(strCondition) ).ToList();
                    dgv_heros.AllowUserToAddRows = true;
                    break;
                case ControlsName.TabPage.DROPOUT:
                    dgv_dropOut.DataSource = dropOutList.Where(it => it.BossName.Contains(strCondition) || it.EquipName.Contains(strCondition)).ToList();
                    dgv_dropOut.AllowUserToAddRows = true;
                    break;
                case ControlsName.TabPage.EXCLUSIVE:
                    dgv_exclusive.DataSource = exclusiveList.Where(it => it.Effect.Contains(strCondition) || it.Name.Contains(strCondition)).ToList();
                    dgv_exclusive.AllowUserToAddRows = true;
                    break;
                default: break;
            }
        }
        private void btn_Save_Clicked(object sender, EventArgs e) {
            switch (tc_all.SelectedTab.Name)
            {
                case ControlsName.TabPage.BOSS:
                    foreach (var it in bossList.Where(item => item.Changed))
                    {
                        if (!string.IsNullOrWhiteSpace(it.Key))
                            BossService.Update(it);
                        else
                        {
                            it.Key = Guid.NewGuid().ToString();
                            BossService.Add(it); 
                        }
                    }
                    ReloadData(ControlsName.DataGridView.BOSS);
                    break;
                case ControlsName.TabPage.EQUIP:
                    foreach (var it in equipList.Where(item => item.Changed))
                    {
                        if (!string.IsNullOrWhiteSpace(it.Key))
                            EquipService.Update(it);
                        else
                        {
                            it.Key = Guid.NewGuid().ToString();
                            EquipService.Add(it);
                        }
                    }
                    ReloadData(ControlsName.DataGridView.EQUIP);
                    break;
                case ControlsName.TabPage.MATER:
                    foreach (var it in materList.Where(item => item.Changed))
                    {
                        if (!string.IsNullOrWhiteSpace(it.Key))
                            MaterialService.Update(it);
                        else
                        {
                            it.Key = Guid.NewGuid().ToString();
                            MaterialService.Add(it);
                        }
                    }
                    ReloadData(ControlsName.DataGridView.MATER);
                    break;
                case ControlsName.TabPage.HEROS:
                    foreach (var it in heroList.Where(item => item.Changed))
                    {
                        if (!string.IsNullOrWhiteSpace(it.Key))
                            HeroService.Update(it);
                        else
                        {
                            it.Key = Guid.NewGuid().ToString();
                            HeroService.Add(it);
                        }
                    }
                    ReloadData(ControlsName.DataGridView.HEROS);
                    break;
                case ControlsName.TabPage.DROPOUT:
                    foreach (var it in dropOutList.Where(item => item.Changed))
                    {
                        if (!string.IsNullOrWhiteSpace(it.Key))
                            BossDropOutService.Update(it);
                        else
                        {
                            it.Key = Guid.NewGuid().ToString();
                            BossDropOutService.Add(it);
                        }
                    }
                    ReloadData(ControlsName.DataGridView.DROPOUT);
                    break;
                case ControlsName.TabPage.EXCLUSIVE:
                    foreach (var it in exclusiveList.Where(item => item.Changed))
                    {
                        if (!string.IsNullOrWhiteSpace(it.Key))
                            ExclusiveServices.Update(it);
                        else
                        {
                            it.Key = Guid.NewGuid().ToString();
                            ExclusiveServices.Add(it);
                        }
                    }
                    ReloadData(ControlsName.DataGridView.EXCLUSIVE);
                    break;
                default: break;
            }
            //foreach (DataGridViewRow it in dgv_boss.Rows)
            //{
            //    MessageBox.Show(bossList.First(item => item.Key.Equals(it.Cells["key"].Value)).Changed.ToString());
            //}
        }
        private void Save(string dgvName)
        {
            switch (dgvName)
            {
                case ControlsName.DataGridView.BOSS:break;
                case ControlsName.DataGridView.EQUIP: break;
                case ControlsName.DataGridView.MATER: break;
                case ControlsName.DataGridView.HEROS: break;
                case ControlsName.DataGridView.DROPOUT: break;
                case ControlsName.DataGridView.EXCLUSIVE: break;
                default: break;
            }
        }
        private void ReloadData(string dgvName)
        {
            Model.Common.MarkChange = false;
            switch (dgvName)
            {
                case ControlsName.DataGridView.BOSS:
                    bossList = new BindingList<Boss>(BossService.LoadData());
                    dgv_boss.DataSource = bossList;
                    dgv_boss.Columns["key"].Visible = false;
                    dgv_boss.Columns["Changed"].Visible = false;
                    break;
                case ControlsName.DataGridView.EQUIP:
                    equipList = new BindingList<Equip>(EquipService.LoadData());
                    dgv_equip.DataSource = equipList;
                    dgv_equip.Columns["key"].Visible = false;
                    dgv_equip.Columns["Changed"].Visible = false;
                    break;
                case ControlsName.DataGridView.MATER:
                    materList = new BindingList<MaterialShow>(MaterialService.LoadData());
                    dgv_mater.DataSource = materList;
                    dgv_mater.Columns["key"].Visible = false;
                    dgv_mater.Columns["Changed"].Visible = false;
                    dgv_mater.Columns["Boss"].Visible = false;
                    break;
                case ControlsName.DataGridView.HEROS:
                    heroList = new BindingList<WorldRpgModel.Hero>(HeroService.LoadData());
                    dgv_heros.DataSource = heroList;
                    dgv_heros.Columns["key"].Visible = false;
                    dgv_heros.Columns["Changed"].Visible = false;
                    break;
                case ControlsName.DataGridView.DROPOUT:
                    dropOutList = new BindingList<BossDropOutShow>(BossDropOutService.LoadData());
                    dgv_dropOut.DataSource = dropOutList;
                    dgv_dropOut.Columns["key"].Visible = false;
                    dgv_dropOut.Columns["Changed"].Visible = false;
                    dgv_dropOut.Columns["BossKey"].Visible = false;
                    dgv_dropOut.Columns["EquipKey"].Visible = false;
                    break;
                case ControlsName.DataGridView.EXCLUSIVE:
                    exclusiveList = new BindingList<WorldRpgModel.Exclusive>(ExclusiveServices.LoadAllData());
                    dgv_exclusive.DataSource = exclusiveList;
                    dgv_exclusive.Columns["key"].Visible = false;
                    dgv_exclusive.Columns["Changed"].Visible = false;
                    break;
                default: break;
            }
            Model.Common.MarkChange = true;
        }
        private void InitalDGV()
        {
            ReloadData(ControlsName.DataGridView.BOSS);
            ReloadData(ControlsName.DataGridView.EQUIP);
            ReloadData(ControlsName.DataGridView.MATER);
            ReloadData(ControlsName.DataGridView.HEROS);
            ReloadData(ControlsName.DataGridView.DROPOUT);
            ReloadData(ControlsName.DataGridView.EXCLUSIVE);
            this.dgv_dropOut.CellClick += new DataGridViewCellEventHandler(dgvCell_clicked);
            this.dgv_dropOut.CellLeave += new DataGridViewCellEventHandler(dgvCell_leave);
            cb_boss = new ComboBox();
            cb_boss.ValueMember = "Key";
            cb_boss.DisplayMember = "Name";
            cb_boss.DataSource = bossList;// BossService.LoadData();
            //cb_boss.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_boss.DrawMode = DrawMode.OwnerDrawVariable;
            cb_boss.ItemHeight += 1;
            cb_boss.DrawItem += new DrawItemEventHandler(comboBox_DrawItem);
            cb_boss.Visible = false;
            dgv_dropOut.Controls.Add(cb_boss);
            dgv_dropOut.Columns["BossName"].Width = 166;
            this.dgv_mater.CellClick += new DataGridViewCellEventHandler(dgvCell_clicked);
            this.dgv_mater.CellLeave += new DataGridViewCellEventHandler(dgvCell_leave);

            cb_boss_mater = new ComboBox();
            cb_boss_mater.ValueMember = "Key";
            cb_boss_mater.DisplayMember = "Name";
            cb_boss_mater.DataSource = bossList;// BossService.LoadData();
            //cb_boss.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_boss_mater.DrawMode = DrawMode.OwnerDrawVariable;
            cb_boss_mater.ItemHeight += 1;
            cb_boss_mater.DrawItem += new DrawItemEventHandler(comboBox_DrawItem);
            cb_boss_mater.Visible = false;
            dgv_mater.Controls.Add(cb_boss_mater);
            dgv_mater.Columns["BossName"].Width = 166;

            cb_equipAndMater = new ComboBox();
            cb_equipAndMater.ValueMember = "Key";
            cb_equipAndMater.DisplayMember = "Name";
            try
            {
                cb_equipAndMater.DataSource = (((equipList.Cast<Model.Unit>().ToList())).Union(materList.Distinct(new MaterComparer()).Cast<Model.Unit>().ToList())).ToList();
                
            }
            catch (Exception e1) {
                MessageBox.Show(e1.Message+EquipService.LoadData().Count());
            }
           // cb_equipAndMater.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_equipAndMater.DrawMode = DrawMode.OwnerDrawVariable;
            cb_equipAndMater.DrawItem += new DrawItemEventHandler(comboBox_DrawItem);
            cb_equipAndMater.Visible = false;
            dgv_dropOut.Controls.Add(cb_equipAndMater);
        }
        public class MaterComparer : EqualityComparer<Material>
        {
            public override bool Equals(Material x, Material y)
            {
                return x.Name == y.Name;
            }
            public override int GetHashCode(Material obj)
            {
                return obj.Key.GetHashCode();
            }
        }
    }

}
