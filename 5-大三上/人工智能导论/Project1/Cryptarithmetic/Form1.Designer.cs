using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace Cryptarithmetic
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SolveButton = new System.Windows.Forms.ToolStripButton();
            this.problemButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.AddOperatorButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.edit = new System.Windows.Forms.ToolStripLabel();
            this.EditTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.online_doc = new System.Windows.Forms.ToolStripMenuItem();
            this.guide = new System.Windows.Forms.ToolStripMenuItem();
            this.solveTimeLabel = new System.Windows.Forms.ToolStripLabel();
            this.OperatorTab = new System.Windows.Forms.TabControl();
            this.plusTabpage = new System.Windows.Forms.TabPage();
            this.minusTabpage = new System.Windows.Forms.TabPage();
            this.multiplyTabpage = new System.Windows.Forms.TabPage();
            this.divideTabpage = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.state_transfer_label = new System.Windows.Forms.ToolStripLabel();
            this.state_transfer_comboBox = new System.Windows.Forms.ToolStripComboBox();
            this.search_methods_label = new System.Windows.Forms.ToolStripLabel();
            this.search_method_comboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip1.SuspendLayout();
            this.OperatorTab.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SolveButton,
            this.problemButton,
            this.toolStripSeparator1,
            this.AddOperatorButton,
            this.toolStripSeparator2,
            this.edit,
            this.EditTextBox,
            this.toolStripDropDownButton1,
            this.solveTimeLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 36);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(1043, 34);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SolveButton
            // 
            this.SolveButton.ForeColor = System.Drawing.SystemColors.Control;
            this.SolveButton.Image = ((System.Drawing.Image)(resources.GetObject("SolveButton.Image")));
            this.SolveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SolveButton.Name = "SolveButton";
            this.SolveButton.Size = new System.Drawing.Size(110, 31);
            this.SolveButton.Text = "开始求解";
            this.SolveButton.Click += new System.EventHandler(this.SolveButton_Click);
            // 
            // problemButton
            // 
            this.problemButton.ForeColor = System.Drawing.SystemColors.Control;
            this.problemButton.Image = ((System.Drawing.Image)(resources.GetObject("problemButton.Image")));
            this.problemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.problemButton.Name = "problemButton";
            this.problemButton.Size = new System.Drawing.Size(110, 31);
            this.problemButton.Text = "查看问题";
            this.problemButton.Click += new System.EventHandler(this.problemButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // AddOperatorButton
            // 
            this.AddOperatorButton.ForeColor = System.Drawing.Color.White;
            this.AddOperatorButton.Image = ((System.Drawing.Image)(resources.GetObject("AddOperatorButton.Image")));
            this.AddOperatorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddOperatorButton.Name = "AddOperatorButton";
            this.AddOperatorButton.Size = new System.Drawing.Size(128, 31);
            this.AddOperatorButton.Text = "增加操作数";
            this.AddOperatorButton.Click += new System.EventHandler(this.AddOperatorButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // edit
            // 
            this.edit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.edit.Image = ((System.Drawing.Image)(resources.GetObject("edit.Image")));
            this.edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.edit.Name = "edit";
            this.edit.Size = new System.Drawing.Size(24, 31);
            this.edit.Text = "toolStripButton2";
            // 
            // EditTextBox
            // 
            this.EditTextBox.BackColor = System.Drawing.Color.LightBlue;
            this.EditTextBox.Enabled = false;
            this.EditTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.EditTextBox.Name = "EditTextBox";
            this.EditTextBox.Size = new System.Drawing.Size(201, 30);
            this.EditTextBox.ToolTipText = "修改";
            this.EditTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditTextBox_KeyDown);
            this.EditTextBox.EnabledChanged += new System.EventHandler(this.EditTextBox_EnabledChanged);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.online_doc,
            this.guide});
            this.toolStripDropDownButton1.ForeColor = System.Drawing.Color.White;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ShowDropDownArrow = false;
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(76, 31);
            this.toolStripDropDownButton1.Text = "帮助(&H)";
            this.toolStripDropDownButton1.DropDownClosed += new System.EventHandler(this.toolStripDropDownButton1_DropDownClosed);
            this.toolStripDropDownButton1.DropDownOpened += new System.EventHandler(this.toolStripDropDownButton1_DropDownOpened);
            // 
            // online_doc
            // 
            this.online_doc.BackColor = System.Drawing.Color.Transparent;
            this.online_doc.Image = ((System.Drawing.Image)(resources.GetObject("online_doc.Image")));
            this.online_doc.Name = "online_doc";
            this.online_doc.Size = new System.Drawing.Size(164, 30);
            this.online_doc.Text = "在线文档";
            this.online_doc.Click += new System.EventHandler(this.online_doc_Click);
            // 
            // guide
            // 
            this.guide.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guide.Name = "guide";
            this.guide.Size = new System.Drawing.Size(164, 30);
            this.guide.Text = "操作指南";
            this.guide.Click += new System.EventHandler(this.Guide_Click);
            // 
            // solveTimeLabel
            // 
            this.solveTimeLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.solveTimeLabel.Name = "solveTimeLabel";
            this.solveTimeLabel.Size = new System.Drawing.Size(0, 31);
            // 
            // OperatorTab
            // 
            this.OperatorTab.Controls.Add(this.plusTabpage);
            this.OperatorTab.Controls.Add(this.minusTabpage);
            this.OperatorTab.Controls.Add(this.multiplyTabpage);
            this.OperatorTab.Controls.Add(this.divideTabpage);
            this.OperatorTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OperatorTab.Location = new System.Drawing.Point(0, 70);
            this.OperatorTab.Margin = new System.Windows.Forms.Padding(0);
            this.OperatorTab.Name = "OperatorTab";
            this.OperatorTab.SelectedIndex = 0;
            this.OperatorTab.Size = new System.Drawing.Size(1043, 459);
            this.OperatorTab.TabIndex = 0;
            this.OperatorTab.SelectedIndexChanged += new System.EventHandler(this.OperatorTab_SelectedIndexChanged);
            // 
            // plusTabpage
            // 
            this.plusTabpage.BackColor = System.Drawing.Color.Azure;
            this.plusTabpage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.plusTabpage.Location = new System.Drawing.Point(4, 28);
            this.plusTabpage.Margin = new System.Windows.Forms.Padding(99, 101, 99, 101);
            this.plusTabpage.Name = "plusTabpage";
            this.plusTabpage.Padding = new System.Windows.Forms.Padding(99, 101, 99, 101);
            this.plusTabpage.Size = new System.Drawing.Size(1035, 427);
            this.plusTabpage.TabIndex = 0;
            this.plusTabpage.Text = "加法";
            // 
            // minusTabpage
            // 
            this.minusTabpage.Location = new System.Drawing.Point(4, 28);
            this.minusTabpage.Margin = new System.Windows.Forms.Padding(2);
            this.minusTabpage.Name = "minusTabpage";
            this.minusTabpage.Padding = new System.Windows.Forms.Padding(2);
            this.minusTabpage.Size = new System.Drawing.Size(1035, 427);
            this.minusTabpage.TabIndex = 1;
            this.minusTabpage.Text = "减法";
            this.minusTabpage.UseVisualStyleBackColor = true;
            // 
            // multiplyTabpage
            // 
            this.multiplyTabpage.Location = new System.Drawing.Point(4, 28);
            this.multiplyTabpage.Margin = new System.Windows.Forms.Padding(2);
            this.multiplyTabpage.Name = "multiplyTabpage";
            this.multiplyTabpage.Padding = new System.Windows.Forms.Padding(2);
            this.multiplyTabpage.Size = new System.Drawing.Size(1035, 427);
            this.multiplyTabpage.TabIndex = 2;
            this.multiplyTabpage.Text = "乘法";
            this.multiplyTabpage.UseVisualStyleBackColor = true;
            // 
            // divideTabpage
            // 
            this.divideTabpage.Location = new System.Drawing.Point(4, 28);
            this.divideTabpage.Margin = new System.Windows.Forms.Padding(2);
            this.divideTabpage.Name = "divideTabpage";
            this.divideTabpage.Padding = new System.Windows.Forms.Padding(2);
            this.divideTabpage.Size = new System.Drawing.Size(1035, 427);
            this.divideTabpage.TabIndex = 3;
            this.divideTabpage.Text = "除法";
            this.divideTabpage.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightBlue;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.state_transfer_label,
            this.state_transfer_comboBox,
            this.search_methods_label,
            this.search_method_comboBox});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1043, 36);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // state_transfer_label
            // 
            this.state_transfer_label.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.state_transfer_label.Name = "state_transfer_label";
            this.state_transfer_label.Size = new System.Drawing.Size(118, 29);
            this.state_transfer_label.Text = "状态转移方式";
            // 
            // state_transfer_comboBox
            // 
            this.state_transfer_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.state_transfer_comboBox.Name = "state_transfer_comboBox";
            this.state_transfer_comboBox.Size = new System.Drawing.Size(150, 32);
            this.state_transfer_comboBox.SelectedIndexChanged += new System.EventHandler(this.state_transfer_comboBox_SelectedIndexChanged);
            // 
            // search_methods_label
            // 
            this.search_methods_label.Margin = new System.Windows.Forms.Padding(100, 0, 0, 0);
            this.search_methods_label.Name = "search_methods_label";
            this.search_methods_label.Size = new System.Drawing.Size(82, 32);
            this.search_methods_label.Text = "搜索算法";
            // 
            // search_method_comboBox
            // 
            this.search_method_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.search_method_comboBox.Name = "search_method_comboBox";
            this.search_method_comboBox.Size = new System.Drawing.Size(150, 32);
            this.search_method_comboBox.SelectedIndexChanged += new System.EventHandler(this.search_method_comboBox_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1043, 529);
            this.Controls.Add(this.OperatorTab);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "算式谜";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.OperatorTab.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void SelfInitializeComponent()
        {
            BackColor = Color.FromArgb(255, 0, 155, 219);
            for (int i = 0; i < TableLayoutPanels.Count; i++)
            {
                TableLayoutPanels[i].AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                TableLayoutPanels[i].BackColor = System.Drawing.Color.Transparent;
                TableLayoutPanels[i].Margin = new System.Windows.Forms.Padding(0);
                TableLayoutPanels[i].Name = "TableLayoutPanel" + i.ToString();
                TableLayoutPanels[i].Size = new System.Drawing.Size(834, 249);
                TableLayoutPanels[i].TabIndex = 0;
                TableLayoutPanels[i].Paint += new PaintEventHandler(TableLayoutPanel_Paint);
                NumbersLabels[i] = new List<List<Label>>();

            }

            for (int i = 0; i < OperatorTab.TabCount; i++)
            {
                OperatorTab.TabPages[i].Controls.Add(TableLayoutPanels[i]);
                OperatorTab.TabPages[i].BackColor = Color.Azure;
            }
            state_transfer_comboBox.Items.AddRange(state_transfer_methods);
            state_transfer_comboBox.SelectedIndex = 0;
        }

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl OperatorTab;
        private System.Windows.Forms.TabPage plusTabpage;
        private System.Windows.Forms.TabPage minusTabpage;
        private System.Windows.Forms.TabPage multiplyTabpage;
        private System.Windows.Forms.TabPage divideTabpage;
        private List<List<Label>>[] NumbersLabels = new List<List<Label>>[4];
        private Label OpLabel;
        private ToolStripButton SolveButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton AddOperatorButton;
        private ToolStripTextBox EditTextBox;
        private ToolStripLabel edit;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton problemButton;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem online_doc;
        private ToolStripMenuItem guide;
        private ToolStripLabel solveTimeLabel;
        private MenuStrip menuStrip1;
        private ToolStripLabel state_transfer_label;
        private ToolStripLabel search_methods_label;
        private ToolStripComboBox state_transfer_comboBox;
        private ToolStripComboBox search_method_comboBox;
    }
}

