using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SolvingMethods;
using MathUtils;

namespace Cryptarithmetic
{
    public partial class Form1 : Form
    {
        enum State { PROBLEM, SOLUTION };
        private State state;
        private State Currentstate
        {
            set
            {
                state = value;
                if (value == State.PROBLEM)
                {
                    problemButton.Enabled = false;
                    SolveButton.Enabled = true;
                    AddOperatorButton.Enabled = true;
                }
                else
                {
                    problemButton.Enabled = true;
                    SolveButton.Enabled = false;
                    AddOperatorButton.Enabled = false;
                }
            }
            get
            {
                return state;
            }
        }

        private int CurrentIndex { get { return OperatorTab.SelectedIndex; } }
        private List<List<string>> defaultInput = new List<List<string>> {
            new List<string> { "FIVE", "FOUR", "NINE" },
            new List<string> { "FOUR", "TWO", "TWO"},
            new List<string> { "SIX", "TWO", "TWELVE"},
            new List<string> { "TWELVE", "SIX", "TWO"},
        };
        private List<List<string>> Numbers = new List<List<string>> {
            new List<string>(),
            new List<string>(),
            new List<string>(),
            new List<string>(),
        };
        private const int MAX_LABEL_SIZE = 100;
        private const int MIN_PADDING = 20;
        private const string fontName = "Serif";
        private char[] operatorSymbols = { '+', '-', '×', '÷' };
        private bool changed = true;
        private string[] solution;

        private List<TableLayoutPanel> TableLayoutPanels = new List<TableLayoutPanel>{
            new TableLayoutPanel(),//plus
            new TableLayoutPanel(),//minus
            new TableLayoutPanel(),//multiply
            new TableLayoutPanel(),//divide
        };

        private string[] state_transfer_methods = new string[] { "附加法", "交换法" };
        private string[][]  search_methods = new string[][] {
            new string[] { "贪婪最佳优先", "深度优先"},
            new string[]{ "贪婪最佳优先", "宽度优先", "深度优先"},
        };
        private Solver[][] solvers = new Solver[][]{
            new AppendSolver[]
            {
                new GreedyAppendSolver(),
                new DFSAppendSolver(),
            },
            new SwapSolver[]
            {
                new GreedySwapSolver(),
                new BFSSwapSolver(),
                new DFSSwapSolver(),
            },
        };

        public Form1()
        {
            InitializeComponent();
            SelfInitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Currentstate = State.PROBLEM;
            MinimumSize = new Size(1000, 400);
            for (int i = 0; i < OperatorTab.TabPages.Count; i++)
            {
                InitLabels(defaultInput[i], (Op)i);
                Numbers[i] = defaultInput[i];
            }
        }

        private void InitLabels(List<string> numbers, Op op)
        {
            int Index = (int)op;
            NumbersLabels[Index].ForEach(labels => labels.ForEach(label => {
                label.Dispose();
            }));
            TableLayoutPanels[(int)op].Controls.Clear();
            NumbersLabels[Index] = new List<List<Label>>();
            foreach (string number in numbers)
            {
                List<Label> numberLabels = new List<Label>();
                foreach (char c in number)
                {
                    Label label = new Label
                    {
                        Text = c.ToString(),
                        Tag = new Tuple<int, int>(NumbersLabels[Index].Count, numberLabels.Count),                        
                        //BackColor = Color.Blue,
                    };
                    label.DoubleClick += new EventHandler(Label_Click);
                    label.MouseEnter += new EventHandler((s, e) => {
                        if (Currentstate == State.SOLUTION)
                            return;
                        (s as Label).BackColor = Color.LightSkyBlue;
                        (s as Label).ForeColor = Color.White;
                    });
                    label.MouseLeave += new EventHandler((s, e) => {
                        if (Currentstate == State.SOLUTION)
                            return;
                        (s as Label).BackColor = Color.Transparent;
                        (s as Label).ForeColor = Color.Black;
                    });
                    InitLabel(ref label);
                    numberLabels.Add(label);
                }
                NumbersLabels[Index].Add(numberLabels);
            }

            int maxLength = numbers.OrderByDescending(s => s.Length).First().Length;
            TableLayoutPanels[(int)op].RowCount = NumbersLabels[Index].Count;
            TableLayoutPanels[(int)op].ColumnCount = maxLength + 1;
            OpLabel = new Label
            {
                Text = operatorSymbols[(int)op].ToString()
            };
            InitLabel(ref OpLabel);

            int delta;
            for (int i = 0; i < TableLayoutPanels[(int)op].RowCount; i++)
            {
                delta = TableLayoutPanels[(int)op].ColumnCount - NumbersLabels[Index][i].Count;
                for (int j = 0; j < NumbersLabels[Index][i].Count; j++)
                {
                    TableLayoutPanels[(int)op].Controls.Add(NumbersLabels[Index][i][j], j + delta, i);
                }
            }
            TableLayoutPanels[(int)op].Controls.Add(OpLabel, 0, TableLayoutPanels[(int)op].RowCount - 2);
            ResizeTableLayoutPanel((int)op);
        }

        private void UpdateLabels(string[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers[i].Length; j++)
                {
                    NumbersLabels[CurrentIndex][i][j].Text = numbers[i][j].ToString();
                }
            }
        }

        private void InitLabel(ref Label label)
        {
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Margin = new Padding(2);
        }

        private void ResizeTableLayoutPanel(int index)
        {
            try
            {
                TableLayoutPanel TLP = TableLayoutPanels[index];
                TLP.RowStyles.Clear();
                TLP.ColumnStyles.Clear();

                int max_width = TLP.Parent.Width - 2 * MIN_PADDING;
                int max_height = TLP.Parent.Height - 2 * MIN_PADDING;
                int max_cell_size = Math.Min(max_width / TLP.ColumnCount, max_height / TLP.RowCount);
                int cell_size = (max_cell_size > MAX_LABEL_SIZE) ? MAX_LABEL_SIZE : max_cell_size;

                TLP.Width = TLP.ColumnCount * cell_size;
                TLP.Height = TLP.RowCount * cell_size;

                for (int i = 0; i < TLP.ColumnCount; i++)
                {
                    TLP.ColumnStyles.Add(new ColumnStyle { SizeType = SizeType.Absolute, Width = cell_size });
                }

                for (int i = 0; i < TLP.RowCount; i++)
                {
                    TLP.RowStyles.Add(new RowStyle { SizeType = SizeType.Absolute, Height = cell_size });
                }

                TLP.Left = (TLP.Parent.Width - TLP.Width) >> 1;
                TLP.Top = (TLP.Parent.Height - TLP.Height) >> 1;

                NumbersLabels[CurrentIndex].ForEach(labels => labels.ForEach(label => label.Font = label.Font = new Font(fontName, cell_size / 3, FontStyle.Bold)));
                OpLabel.Font = new Font(fontName, cell_size / 3, FontStyle.Bold);
            }
            catch (Exception)
            {

            }

        }

        private void ExtractNumbers()
        {
            Numbers[OperatorTab.SelectedIndex] = new List<string> { Capacity = NumbersLabels[CurrentIndex].Count };
            for (int i = 0; i < NumbersLabels[CurrentIndex].Count; i++)
            {
                string s = "";
                for (int j = 0; j < NumbersLabels[CurrentIndex][i].Count; j++)
                {
                    s = s + NumbersLabels[CurrentIndex][i][j].Text;
                }
                Numbers[OperatorTab.SelectedIndex].Add(s);
            }
        }

        private void EditNumber(int index, string str)
        {
            if (!str.Any())
            {
                if (index == Numbers[CurrentIndex].Count - 1)
                {
                    throw new Exception("结果不能为空");
                }
                if (Numbers[CurrentIndex].Count == 2)
                {
                    throw new Exception("操作数不能为空");
                }
                if (Numbers[CurrentIndex].Count == 3 && (CurrentIndex == 1 || CurrentIndex == 3))
                {
                    throw new Exception("该操作符不能删除操作数");
                }

                Numbers[CurrentIndex].RemoveAt(index);
            }
            else
                Numbers[CurrentIndex][index] = str;
            InitLabels(Numbers[CurrentIndex], (Op)CurrentIndex);
            changed = true;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeTableLayoutPanel((int)OperatorTab.SelectedIndex);
        }

        private void TableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {
            TableLayoutPanel TLP = sender as TableLayoutPanel;
            var O = new Point(0, (int)((TLP.RowCount - 1) * TLP.RowStyles[0].Height));
            e.Graphics.DrawLine(new Pen(Color.Black, 2), O, new Point(O.X + TLP.Width, O.Y));
        }

        private void Label_Click(object sender, EventArgs e)
        {
            if (Currentstate == State.SOLUTION)
                return;
            MouseEventArgs me = e as MouseEventArgs;
            if (me.Button == MouseButtons.Left)
            {
                Label label = (Label)sender;
                Tuple<int, int> t = (Tuple<int, int>)label.Tag;
                EditTextBox.Enabled = true;
                EditTextBox.Text = Numbers[CurrentIndex][t.Item1];
                EditTextBox.SelectionStart = t.Item2;
                EditTextBox.SelectionLength = 1;
                EditTextBox.Focus();
                EditTextBox.Tag = t.Item1;
            }
            else
            {
                try
                {
                    EditNumber(((sender as Label).Tag as Tuple<int, int>).Item1, "");
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            if (Currentstate == State.SOLUTION)
                return;
            if (!changed)
            {
                UpdateLabels(solution);
                Currentstate = State.SOLUTION;
                return;
            }
            ExtractNumbers();

            Thread Solve_Thread;
            switch (state_transfer_comboBox.SelectedIndex)
            {
                case 0:
                    Solve_Thread = new Thread(new ParameterizedThreadStart(AppendSolve_Run));
                    break;
                case 1:
                    Solve_Thread = new Thread(new ParameterizedThreadStart(SwapSolve_Run));
                    break;
                default:
                    Solve_Thread = new Thread(new ParameterizedThreadStart(AppendSolve_Run));
                    break;
            }
            Solve_Thread.Start(new Tuple<int, int>(CurrentIndex, search_method_comboBox.SelectedIndex));
            GC.Collect(3);
        }

        private void AppendSolve_Run(object tuple)
        {
            Solve_Start();
            Tuple<int, int> tp = tuple as Tuple<int, int>;
            int currentIndex = tp.Item1;
            int searching_method = tp.Item2;
            try
            {
                setSolveTime(null);
                Stopwatch stopwatch;
                solvers[0][searching_method].Init(Numbers[(int)currentIndex].ToArray(), (Op)(int)currentIndex);
                try
                {
                    stopwatch = Stopwatch.StartNew();
                    solvers[0][searching_method].Solve();
                    solution = solvers[0][searching_method].GetResult();
                    stopwatch.Stop();
                    setSolveTime(stopwatch);
                    UpdateLabels(solution);
                    Currentstate = State.SOLUTION;
                    changed = false;
                }
                catch (Exception exc)
                {
                    solveTimeLabel.Text = "";
                    MessageBox.Show(exc.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exc)
            {

                solveTimeLabel.Text = "";
                MessageBox.Show(exc.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Solve_End();
        }

        private void SwapSolve_Run(object tuple)
        {
            Solve_Start();
            Tuple<int, int> tp = tuple as Tuple<int, int>;
            int currentIndex = tp.Item1;
            int searching_method = tp.Item2;
            Stopwatch[] stopwatch = new Stopwatch[2];
            try
            {   //in case of no solution or solving too slow
                setSolveTime(null);
                solvers[0][0].Init(Numbers[currentIndex].ToArray(), (Op)currentIndex);
                solvers[1][searching_method].Init(Numbers[currentIndex].ToArray(), (Op)currentIndex);
                try
                {
                    var tokenSource = new CancellationTokenSource();
                    var token = tokenSource.Token;
                    Task t;
                    var tasks = new ConcurrentBag<Task>();
                    var tasksIds = new List<int>();
                    t = Task.Factory.StartNew(() => solvers[0][0].Solve(token), token);
                    tasks.Add(t);
                    tasksIds.Add(t.Id);
                    t = Task.Factory.StartNew(() => solvers[1][searching_method].Solve(token), token);
                    tasks.Add(t);
                    tasksIds.Add(t.Id);

                    Console.Write("tasksIds: \n");
                    foreach (int id in tasksIds)
                    {
                        Console.Write(id.ToString());
                    }

                    int w = -1;
                    stopwatch[0] = Stopwatch.StartNew();
                    stopwatch[1] = Stopwatch.StartNew();
                    w = 1 - Task.WaitAny(tasks.ToArray());
                    
                    if (w == 0)// append solver finished first
                    {
                        stopwatch[0].Stop();
                        try
                        {
                            solvers[0][0].GetResult();
                            Console.WriteLine("Has solution");
                            // Has solution
                        }
                        catch (Exception e)
                        {
                            // No solution
                            Console.WriteLine("No solution");
                            tokenSource.Cancel();
                            try
                            {
                                Task.WaitAll(tasks.ToArray());
                            }
                            catch (AggregateException)
                            {
                            }
                            finally
                            {
                                tokenSource.Dispose();
                            }
                            foreach (var task in tasks)
                                Console.WriteLine("Task {0} status is now {1}", task.Id, task.Status);
                            throw new Exception("无解");
                        }
                        Task.WaitAll(tasks.ToArray());
                        stopwatch[1].Stop();
                    }
                    else
                    {
                        stopwatch[1].Stop();
                        Task.WaitAll(tasks.ToArray());
                        stopwatch[0].Stop();
                    }

                    try
                    {
                        solution = solvers[1][searching_method].GetResult();
                    }
                    catch (Exception)
                    {
                        throw new Exception("Timeout");
                    }
                    setSolveTime(stopwatch[1]);
                    UpdateLabels(solution);
                    Currentstate = State.SOLUTION;
                    changed = false;
                }
                catch (AggregateException exc)// Handle no solution exception
                {

                    solveTimeLabel.Text = "";
                    MessageBox.Show(exc.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exc)
            {
                solveTimeLabel.Text = "";
                if (exc.Message.Equals("Timeout"))
                {
                    var show_append = MessageBox.Show("求解超时，是否显示附加法的结果？", "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (show_append == DialogResult.OK)
                    {
                        solution = solvers[0][0].GetResult();
                        setSolveTime(stopwatch[0]);
                        UpdateLabels(solution);
                        Currentstate = State.SOLUTION;
                        changed = false;
                    }
                }
                else
                {
                    MessageBox.Show(exc.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            Solve_End();
        }

        private void AddOperatorButton_Click(object sender, EventArgs e)
        {
            if (Currentstate == State.SOLUTION)
                return;
            if (CurrentIndex == 1 || CurrentIndex == 3)
            {
                MessageBox.Show("该操作符不能添加操作数", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Numbers[CurrentIndex][0].Any())
                Numbers[CurrentIndex].Insert(0, "");
            EditTextBox.Enabled = true;
            EditTextBox.Tag = 0;
            EditTextBox.Focus();
        }

        private void EditTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ToolStripTextBox tb = (ToolStripTextBox)sender;
                try
                {
                    EditNumber((int)tb.Tag, tb.Text);
                    tb.Clear();
                    EditTextBox.Enabled = false;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void problemButton_Click(object sender, EventArgs e)
        {
            view_problem();
        }

        private void OperatorTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            Currentstate = State.PROBLEM;
            InitLabels(Numbers[CurrentIndex], (Op)CurrentIndex);
            changed = true;
        }

        private void toolStripDropDownButton1_DropDownOpened(object sender, EventArgs e)
        {
            (sender as ToolStripDropDownButton).ForeColor = Color.Black;
        }

        private void toolStripDropDownButton1_DropDownClosed(object sender, EventArgs e)
        {
            (sender as ToolStripDropDownButton).ForeColor = Color.White;
        }

        private void Label_MouseHover(object sender, EventArgs e)
        {
            (sender as Label).BackColor = Color.Crimson;
        }

        private void EditTextBox_EnabledChanged(object sender, EventArgs e)
        {
            var s = sender as ToolStripTextBox;
            if (s.Enabled)
            {
                s.BackColor = Color.LightYellow;
            }
            else
            {
                s.BackColor = Color.LightBlue;
            }
        }

        private void Guide_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. 点击标签页切换算式类型（加、减、乘、除）\n" +
                            "2. 在菜单栏可以选择状态转移方式和搜索算法（详见在线文档或README）\n" +
                            "3. 在【问题模式】下可以修改算式：\n" +
                            "  - 增加操作数：点击工具栏中【增加操作数】按钮，即可在修改框中填写操作数，填写完毕后按下回车。\n" +
                            "  - 修改操作数：左键双击任意一个字符，在工具栏中的修改框中会显示该字符所在操作数，修改完毕后按下回车。\n" +
                            "  - 删除操作数：右键双击该操作数的任意一个字符，或修改操作数时将其置为空。\n" +
                            "4. 点击【开始求解】按钮启动求解过程。如果算式无解或输入不正确，会显示响应的信息提示框。如果算式有解，则切换到【解答模式】并显示求解结果，在菜单栏显示求解时间\n" +
                            "5. 【问题模式】下【查看问题】按钮被禁用，【解答模式】下【开始求解】按钮以及和修改操作数相关的功能被禁用。\n" +
                            "6. 点击工具栏中【帮助】菜单，随时查看操作指南或在线文档。\n" +
                            "7. 若输入字符为数字，则认为该位置的值为该数字。\n" +
                            "8. 每个算式中不同字母代表的数字不同、至多有10个不同字母（对应0~9）。\n",
                            "操作指南", MessageBoxButtons.OK);
        }

        private void online_doc_Click(object sender, EventArgs e)
        {
            Process.Start("http://ca.johnwilliams.online");
        }

        private void setSolveTime(Stopwatch stopwatch)
        {
            if (stopwatch == null)
            {
                solveTimeLabel.Text = "求解中...";
            }
            else
            {
                solveTimeLabel.Text = "用时：" + stopwatch.ElapsedMilliseconds + "ms";
            }
        }

        private void state_transfer_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = sender as ToolStripComboBox;
            search_method_comboBox.Items.Clear();
            search_method_comboBox.Items.AddRange(search_methods[s.SelectedIndex]);
            search_method_comboBox.SelectedIndex = 0;
            changed = true;
            view_problem();
        }

        private void search_method_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            changed = true;
            view_problem();
        }

        private void view_problem()
        {
            UpdateLabels(Numbers[CurrentIndex].ToArray());
            Currentstate = State.PROBLEM;
        }

        private void Solve_Start()
        {
            SolveButton.Enabled = false;
            OperatorTab.Enabled = false;
        }

        private void Solve_End()
        {
            OperatorTab.Enabled = true;
        }
    }
}
