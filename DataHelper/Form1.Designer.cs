using DSkin.Controls;
using DSkin.DirectUI;
using System.Drawing;
using System.Windows.Forms;

namespace DataHelper
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ch_boss = new ControlHost();
            this.ch_equip = new ControlHost();
            this.ch_material = new ControlHost();
            this.ch_dropOut = new ControlHost();
            this.ch_heros = new ControlHost();
            this.ch_exclusive = new ControlHost();
            this.tc_all = new DSkinTabControl();
            this.tb_boss = new DSkinTabPage();
            this.tb_equip = new DSkinTabPage();
            this.tb_mater = new DSkinTabPage();
            this.tb_dropOut = new DSkinTabPage();
            this.tb_heros = new DSkinTabPage();
            this.tb_exclusive = new DSkinTabPage();
            this.dgv_boss = new DataGridView  ();
            this.dgv_equip = new DataGridView  ();
            this.dgv_mater = new DataGridView  ();
            this.dgv_dropOut = new DataGridView  ();
            this.dgv_heros = new DataGridView  ();
            this.dgv_exclusive = new DataGridView  ();
            this.btn_save = new DSkinButton();
            this.btn_saveAll = new DSkinButton();
            this.tb_searCondition = new DSkinTextBox();
            this.btn_search = new DSkinButton();
            this.tc_all.SuspendLayout();
            this.tb_boss.SuspendLayout();
            this.tb_equip.SuspendLayout();
            this.tb_mater.SuspendLayout();
            this.tb_dropOut.SuspendLayout();
            this.tb_heros.SuspendLayout();
            this.tb_exclusive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_boss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_equip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_mater)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dropOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_heros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_exclusive)).BeginInit();
            this.SuspendLayout();
            // 
            // tc_all
            // 
            this.tc_all.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.tc_all.HoverBackColors = new Color[]
            {
                Color.FromArgb(236, 242, 230)
            };
            this.tc_all.ItemBackgroundImage = null;
            this.tc_all.ItemBackgroundImageHover = null;
            this.tc_all.ItemBackgroundImageSelected = null;
            //this.tabEquip.ItemSize = new Size(85, 45);
            this.tc_all.Multiline = true;
            this.tc_all.NormalBackColors = new Color[]
            {
                Color.FromArgb(240, 240, 240)
            };
            this.tc_all.PageImagePosition = ePageImagePosition.Left;
            this.tc_all.SelectedBackColors = new Color[]
            {
                Color.FromArgb(226, 237, 218)
            };
            this.tc_all.SizeMode = TabSizeMode.Fixed;
            this.tc_all.TabIndex = 1;
            this.tc_all.UpdownBtnArrowNormalColor = Color.Black;
            this.tc_all.UpdownBtnArrowPressColor = Color.Gray;
            this.tc_all.UpdownBtnBackColor = Color.White;
            this.tc_all.UpdownBtnBorderColor = Color.Black;

            this.tc_all.Controls.Add(this.tb_boss);
            this.tc_all.Controls.Add(this.tb_equip);
            this.tc_all.Controls.Add(this.tb_mater);
            this.tc_all.Controls.Add(this.tb_dropOut);
            this.tc_all.Controls.Add(this.tb_heros);
            this.tc_all.Controls.Add(this.tb_exclusive);
            this.tc_all.Location = new System.Drawing.Point(13, 60);
            this.tc_all.Name = "tc_all";
            this.tc_all.SelectedIndex = 9;
            this.tc_all.Size = new System.Drawing.Size(1163, 821);
            this.tc_all.TabIndex = 9;
            // 
            // tb_boss
            // 
            this.tb_boss.BackColor = Color.Transparent;
            this.ch_boss.Controls.Add(this.dgv_boss);
            this.tb_boss.Controls.Add(this.ch_boss);
            this.tb_boss.Location = new System.Drawing.Point(4, 22);
            this.tb_boss.Name = ControlsName.TabPage.BOSS;
            this.tb_boss.Padding = new System.Windows.Forms.Padding(3);
            this.tb_boss.Size = new System.Drawing.Size(855, 295);
            this.tb_boss.TabIndex = 0;
            this.tb_boss.Text = "BOSS";
            this.tb_boss.UseVisualStyleBackColor = true;
        
            // 
            // tb_equip
            // 
            this.tb_equip.BackColor = Color.Transparent;
            this.ch_equip.Dock = DockStyle.Fill;
            this.ch_equip.Controls.Add(this.dgv_equip);
            this.tb_equip.Controls.Add(this.ch_equip);
            this.tb_equip.Location = new System.Drawing.Point(4, 22);
            this.tb_equip.Name = ControlsName.TabPage.EQUIP;
            this.tb_equip.Padding = new System.Windows.Forms.Padding(3);
            this.tb_equip.Size = new System.Drawing.Size(855, 295);
            this.tb_equip.TabIndex = 1;
            this.tb_equip.Text = "装备";
            this.tb_equip.UseVisualStyleBackColor = true;
          
            // 
            // tb_mater
            // 
            this.tb_mater.BackColor = Color.Transparent;
            this.ch_material.Dock = DockStyle.Fill;
            this.ch_material.Controls.Add(this.dgv_mater);
            this.tb_mater.Controls.Add(this.ch_material);
            this.tb_mater.Location = new System.Drawing.Point(4, 22);
            this.tb_mater.Name = ControlsName.TabPage.MATER;
            this.tb_mater.Padding = new System.Windows.Forms.Padding(3);
            this.tb_mater.Size = new System.Drawing.Size(855, 295);
            this.tb_mater.TabIndex = 2;
            this.tb_mater.Text = "材料";
            this.tb_mater.UseVisualStyleBackColor = true;
            // 
            // tb_dropOut
            // 
            this.tb_dropOut.BackColor = Color.Transparent;
            this.ch_dropOut.Dock = DockStyle.Fill;
            this.ch_dropOut.Controls.Add(this.dgv_dropOut);
            this.tb_dropOut.Controls.Add(this.ch_dropOut);
            this.tb_dropOut.Location = new System.Drawing.Point(4, 22);
            this.tb_dropOut.Name = ControlsName.TabPage.DROPOUT;
            this.tb_dropOut.Padding = new System.Windows.Forms.Padding(3);
            this.tb_dropOut.Size = new System.Drawing.Size(855, 295);
            this.tb_dropOut.TabIndex = 3;
            this.tb_dropOut.Text = "掉落";
            this.tb_dropOut.UseVisualStyleBackColor = true;
            // 
            // tb_heros
            // 
            this.tb_heros.BackColor = Color.Transparent;
            this.ch_heros.Dock = DockStyle.Fill;
            this.ch_heros.Controls.Add(this.dgv_heros);
            this.tb_heros.Controls.Add(this.ch_heros);
            this.tb_heros.Location = new System.Drawing.Point(4, 22);
            this.tb_heros.Name = ControlsName.TabPage.HEROS;
            this.tb_heros.Padding = new System.Windows.Forms.Padding(3);
            this.tb_heros.Size = new System.Drawing.Size(855, 295);
            this.tb_heros.TabIndex = 4;
            this.tb_heros.Text = "英雄";
            this.tb_heros.UseVisualStyleBackColor = true;
            // 
            // tb_exclusive
            // 
           
            this.tb_exclusive.BackColor = Color.Transparent;
            this.ch_exclusive.Dock = DockStyle.Fill;
            this.ch_exclusive.Controls.Add(this.dgv_exclusive);
            this.tb_exclusive.Controls.Add(this.ch_exclusive);
            this.tb_exclusive.Location = new System.Drawing.Point(4, 22);
            this.tb_exclusive.Name = ControlsName.TabPage.EXCLUSIVE;
            this.tb_exclusive.Padding = new System.Windows.Forms.Padding(3);
            this.tb_exclusive.Size = new System.Drawing.Size(855, 295);
            this.tb_exclusive.TabIndex = 5;
            this.tb_exclusive.Text = "专属";
            this.tb_exclusive.UseVisualStyleBackColor = true;
            // 
            // dgv_boss
            // 
            this.dgv_boss.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_boss.Location = new System.Drawing.Point(6, 6);
            this.dgv_boss.Name = ControlsName.DataGridView.BOSS;//"dgv_boss";
            this.dgv_boss.RowTemplate.Height = 25;
            this.dgv_boss.Size = new System.Drawing.Size(1142, 783);
            this.dgv_boss.TabIndex = 0;
            this.ch_boss.Location= new System.Drawing.Point(6, 6);
            this.ch_boss.Size= new System.Drawing.Size(1142, 783);
            this.dgv_boss.DataError += new DataGridViewDataErrorEventHandler(dgvDataError);
            this.dgv_boss.CellEndEdit += new DataGridViewCellEventHandler(dgvCellEditEnd);
            // 
            // dgv_equip
            // 
            this.dgv_equip.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_equip.Location = new System.Drawing.Point(6, 6);
            this.dgv_equip.Name = ControlsName.DataGridView.EQUIP;//"dgv_equip";
            this.dgv_equip.RowTemplate.Height = 25;
            this.dgv_equip.Size = new System.Drawing.Size(1142, 783);
            this.dgv_equip.TabIndex = 1;
            this.dgv_equip.DataError += new DataGridViewDataErrorEventHandler(dgvDataError);
            this.dgv_equip.CellEndEdit += new DataGridViewCellEventHandler(dgvCellEditEnd);
            // 
            // dgv_mater
            // 
            this.dgv_mater.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_mater.Location = new System.Drawing.Point(6, 6);
            this.dgv_mater.Name = "dgv_mater";
            this.dgv_mater.RowTemplate.Height = 25;
            this.dgv_mater.Size = new System.Drawing.Size(1142, 783);
            this.dgv_mater.TabIndex = 2;
            this.dgv_mater.DataError += new DataGridViewDataErrorEventHandler(dgvDataError);
            this.dgv_mater.CellEndEdit += new DataGridViewCellEventHandler(dgvCellEditEnd);
            // 
            // dgv_dropOut
            // 
            //this.dgv_dropOut.Dock = DockStyle.Fill;
            this.dgv_dropOut.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_dropOut.Location = new System.Drawing.Point(6, 6);
            this.dgv_dropOut.Name = ControlsName.DataGridView.DROPOUT;// "dgv_dropOut";
            this.dgv_dropOut.RowTemplate.Height = 25;
            this.dgv_dropOut.Size = new System.Drawing.Size(1142, 783);
            this.dgv_dropOut.TabIndex = 3;
            this.dgv_dropOut.DataError += new DataGridViewDataErrorEventHandler(dgvDataError);
            this.dgv_dropOut.CellEndEdit += new DataGridViewCellEventHandler(dgvCellEditEnd);
            // 
            // dgv_heros
            // 
            this.dgv_heros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_heros.Location = new System.Drawing.Point(6, 6);
            this.dgv_heros.Name = ControlsName.DataGridView.HEROS;//"dgv_heros";
            this.dgv_heros.RowTemplate.Height = 25;
            this.dgv_heros.Size = new System.Drawing.Size(1142, 783);
            this.dgv_heros.TabIndex = 4;
            this.dgv_heros.DataError += new DataGridViewDataErrorEventHandler(dgvDataError);
            this.dgv_heros.CellEndEdit += new DataGridViewCellEventHandler(dgvCellEditEnd);
            // 
            // dgv_exclusive
            // 
            this.dgv_exclusive.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_exclusive.Location = new System.Drawing.Point(6, 6);
            this.dgv_exclusive.Name = ControlsName.DataGridView.EXCLUSIVE;//"dgv_exclusive";
            this.dgv_exclusive.RowTemplate.Height = 25;
            this.dgv_exclusive.Size = new System.Drawing.Size(1142, 783);
            this.dgv_exclusive.TabIndex = 5;
            this.dgv_exclusive.DataError += new DataGridViewDataErrorEventHandler(dgvDataError);
            this.dgv_exclusive.CellEndEdit += new DataGridViewCellEventHandler(dgvCellEditEnd);
            // 
            // btn_save
            // 
            this.btn_save.AdaptImage = true;
            this.btn_save.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.btn_save.BaseColor = Color.FromArgb(255, 128, 0);
            this.btn_save.ButtonBorderColor = Color.Gray;
            this.btn_save.ButtonBorderWidth = 1;
            this.btn_save.ButtonStyle = ButtonStyles.Style2;
            this.btn_save.DialogResult = DialogResult.None;
            this.btn_save.ForeColor = Color.White;
            this.btn_save.HoverColor = Color.Empty;
            this.btn_save.HoverImage = null;
            this.btn_save.IsPureColor = false;
            this.btn_save.NormalImage = null;
            this.btn_save.PressColor = Color.Empty;
            this.btn_save.PressedImage = null;
            this.btn_save.Radius = 2;
            this.btn_save.ShowButtonBorder = false;
            this.btn_save.Click += new System.EventHandler(btn_Save_Clicked);
            //--
            this.btn_save.Location = new System.Drawing.Point(630, 31);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "保存";
            //this.btn_save.UseVisualStyleBackColor = true;
            // 
            // btn_saveAll
            // 
            this.btn_saveAll.AdaptImage = true;
            this.btn_saveAll.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.btn_saveAll.BaseColor = Color.FromArgb(255, 128, 0);
            this.btn_saveAll.ButtonBorderColor = Color.Gray;
            this.btn_saveAll.ButtonBorderWidth = 1;
            this.btn_saveAll.ButtonStyle = ButtonStyles.Style2;
            this.btn_saveAll.DialogResult = DialogResult.None;
            this.btn_saveAll.ForeColor = Color.White;
            this.btn_saveAll.HoverColor = Color.Empty;
            this.btn_saveAll.HoverImage = null;
            this.btn_saveAll.IsPureColor = false;
            this.btn_saveAll.NormalImage = null;
            this.btn_saveAll.PressColor = Color.Empty;
            this.btn_saveAll.PressedImage = null;
            this.btn_saveAll.Radius = 2;
            this.btn_saveAll.ShowButtonBorder = false;
            //--
            this.btn_saveAll.Location = new System.Drawing.Point(711, 31);
            this.btn_saveAll.Name = "btn_saveAll";
            this.btn_saveAll.Size = new System.Drawing.Size(75, 23);
            this.btn_saveAll.TabIndex = 2;
            this.btn_saveAll.Text = "保存全部";
            //this.btn_saveAll.UseVisualStyleBackColor = true;
            // 
            // tb_searCondition
            // 
            this.tb_searCondition.Location = new System.Drawing.Point(17, 31);
            this.tb_searCondition.Name = "tb_searCondition";
            this.tb_searCondition.Size = new System.Drawing.Size(301, 21);
            this.tb_searCondition.TabIndex = 3;
            this.tb_searCondition.KeyUp += new KeyEventHandler(tb_searConditionEnter);
            // 
            // btn_search
            // 
            this.btn_search.Click += new System.EventHandler(btn_Search_Clicked);
            this.btn_search.AdaptImage = true;
            this.btn_search.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.btn_search.BaseColor = Color.FromArgb(255, 128, 0);
            this.btn_search.ButtonBorderColor = Color.Gray;
            this.btn_search.ButtonBorderWidth = 1;
            this.btn_search.ButtonStyle = ButtonStyles.Style2;
            this.btn_search.DialogResult = DialogResult.None;
            this.btn_search.ForeColor = Color.White;
            this.btn_search.HoverColor = Color.Empty;
            this.btn_search.HoverImage = null;
            this.btn_search.IsPureColor = false;
            this.btn_search.NormalImage = null;
            this.btn_search.PressColor = Color.Empty;
            this.btn_search.PressedImage = null;
            this.btn_search.Radius = 2;
            this.btn_search.ShowButtonBorder = false;
            //--
            this.btn_search.Location = new System.Drawing.Point(325, 31);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 4;
            this.btn_search.Text = "搜索";
            //this.btn_search.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 900);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.tb_searCondition);
            this.Controls.Add(this.btn_saveAll);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.tc_all);
            this.Name = "WorldRpgDataManager";
            this.Text = "WorldRpgDataManager";
            this.tc_all.ResumeLayout(false);
            this.tb_boss.ResumeLayout(false);
            this.tb_equip.ResumeLayout(false);
            this.tb_mater.ResumeLayout(false);
            this.tb_dropOut.ResumeLayout(false);
            this.tb_heros.ResumeLayout(false);
            this.tb_exclusive.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_boss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_equip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_mater)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dropOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_heros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_exclusive)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DSkinTabControl tc_all;
        private DSkinTabPage tb_boss;
        private DataGridView   dgv_boss;
        private DSkinTabPage tb_equip;
        private DataGridView   dgv_equip;
        private DSkinTabPage tb_mater;
        private DataGridView   dgv_mater;
        private DSkinTabPage tb_dropOut;
        private DataGridView   dgv_dropOut;
        private DSkinTabPage tb_heros;
        private DataGridView   dgv_heros;
        private DSkinTabPage tb_exclusive;
        private DataGridView   dgv_exclusive;
        private DSkinButton btn_save;
        private DSkinButton btn_saveAll;
        private DSkinTextBox tb_searCondition;
        private DSkinButton btn_search;
        private ControlHost ch_boss;
        private ControlHost ch_equip;
        private ControlHost ch_material;
        private ControlHost ch_dropOut;
        private ControlHost ch_heros;
        private ControlHost ch_exclusive;
    }
}

