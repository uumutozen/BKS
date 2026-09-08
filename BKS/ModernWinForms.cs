using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BKS
{
    internal static class ModernWinForms
    {
<<<<<<< HEAD
        public static readonly Color PageBack = RibbonPalette.Workspace;
        public static readonly Color CardBack = Color.White;
        public static readonly Color Border = RibbonPalette.Border;
        public static readonly Color Primary = RibbonPalette.Accent;
        public static readonly Color PrimaryDark = RibbonPalette.ActiveBorder;
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);
        public static readonly Color Success = Color.FromArgb(22, 163, 74);
        public static readonly Color Text = RibbonPalette.Text;
        public static readonly Color Muted = RibbonPalette.CaptionText;
=======
        public static readonly Color PageBack = Color.FromArgb(246, 248, 252);
        public static readonly Color CardBack = Color.White;
        public static readonly Color Border = Color.FromArgb(226, 232, 240);
        public static readonly Color Primary = Color.FromArgb(37, 99, 235);
        public static readonly Color PrimaryDark = Color.FromArgb(30, 64, 175);
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);
        public static readonly Color Success = Color.FromArgb(22, 163, 74);
        public static readonly Color Text = Color.FromArgb(15, 23, 42);
        public static readonly Color Muted = Color.FromArgb(100, 116, 139);
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31

        public static void StyleForm(Form form, string title, Size minSize)
        {
            form.Text = title;
            form.BackColor = PageBack;
            form.MinimumSize = minSize;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.AutoScaleMode = AutoScaleMode.Dpi;
            form.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 162);
        }

        public static Panel CreateCard(string name, int padding = 16)
        {
            return new Panel
            {
                Name = name,
                BackColor = CardBack,
                Padding = new Padding(padding),
                Margin = new Padding(0, 0, 0, 14),
                Dock = DockStyle.Fill
            };
        }

        public static TableLayoutPanel CreatePageLayout(int rows)
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = rows,
                BackColor = PageBack,
                Padding = new Padding(16)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            return layout;
        }

        public static FlowLayoutPanel CreateFlow(string name, bool wrap = true)
        {
            return new FlowLayoutPanel
            {
                Name = name,
                Dock = DockStyle.Fill,
                WrapContents = wrap,
                AutoScroll = false,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
        }

        public static Label CreateTitle(string text)
        {
            return new Label
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 30,
                Text = text,
                Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, 162),
                ForeColor = Text,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        public static Label CreateSubtitle(string text)
        {
            return new Label
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 24,
                Text = text,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 162),
                ForeColor = Muted,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        public static Label CreateBadge(string text)
        {
            return new Label
            {
                AutoSize = false,
                Width = 220,
                Height = 40,
                Margin = new Padding(0, 0, 10, 10),
                BackColor = Color.FromArgb(239, 246, 255),
                ForeColor = PrimaryDark,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 162),
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        public static ToolStrip CreateCommandStrip(string name)
        {
            var strip = new ToolStrip
            {
                Name = name,
                Dock = DockStyle.Fill,
                BackColor = CardBack,
                GripStyle = ToolStripGripStyle.Hidden,
                RenderMode = ToolStripRenderMode.System,
                Padding = new Padding(8, 6, 8, 6),
                ImageScalingSize = new Size(18, 18)
            };
            return strip;
        }

        public static ToolStripButton CreateCommand(string text, EventHandler click)
        {
            var item = new ToolStripButton(text)
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 162),
                ForeColor = Text,
                Margin = new Padding(0, 0, 8, 0),
                Padding = new Padding(10, 5, 10, 5),
                AutoSize = true
            };
            item.Click += click;
            return item;
        }

        public static void StyleInput(Control control, int width = 220, int height = 34)
        {
            control.Width = width;
            control.Height = height;
            control.Margin = new Padding(0, 0, 12, 12);
            control.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 162);
<<<<<<< HEAD
=======

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            if (control is TextBox tb)
            {
                tb.BorderStyle = BorderStyle.FixedSingle;
                tb.ForeColor = Text;
                tb.BackColor = Color.White;
            }
            else if (control is RichTextBox rtb)
            {
                rtb.BorderStyle = BorderStyle.FixedSingle;
                rtb.ForeColor = Text;
                rtb.BackColor = Color.White;
            }
            else if (control is MaskedTextBox mtb)
            {
                mtb.BorderStyle = BorderStyle.FixedSingle;
                mtb.ForeColor = Text;
                mtb.BackColor = Color.White;
            }
            else if (control is ComboBox cb)
            {
                cb.ForeColor = Text;
                cb.BackColor = Color.White;
            }
        }

        public static void StyleCheck(Control control)
        {
            control.AutoSize = true;
            control.Margin = new Padding(0, 0, 14, 8);
            control.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 162);
            control.ForeColor = Text;
        }

        public static void HideLegacyButton(Button? button)
        {
            if (button == null) return;
            button.Visible = false;
            button.TabStop = false;
            button.Width = 1;
            button.Height = 1;
        }

        public static void StyleGroupBox(GroupBox groupBox, string? title = null)
        {
            if (!string.IsNullOrWhiteSpace(title))
<<<<<<< HEAD
            groupBox.Text = title;
=======
                groupBox.Text = title;

>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            groupBox.Dock = DockStyle.Fill;
            groupBox.Padding = new Padding(14, 28, 14, 14);
            groupBox.Margin = new Padding(0, 0, 14, 14);
            groupBox.BackColor = CardBack;
            groupBox.ForeColor = PrimaryDark;
            groupBox.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 162);
        }

        public static void StyleGrid(DataGridView grid)
        {
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = CardBack;
            grid.GridColor = Border;
            grid.EnableHeadersVisualStyles = false;
<<<<<<< HEAD
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            grid.AllowUserToOrderColumns = true;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
=======
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
            grid.AllowUserToResizeRows = false;
<<<<<<< HEAD
            grid.ColumnHeadersHeight = (int)(44 * grid.DeviceDpi / 96F);
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(222, 232, 245);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = RibbonPalette.Text;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point,
            162);
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 162);
=======
            grid.ColumnHeadersHeight = 40;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersDefaultCellStyle.BackColor = PrimaryDark;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 162);
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 162);
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            grid.DefaultCellStyle.ForeColor = Text;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            grid.DefaultCellStyle.SelectionForeColor = Text;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
<<<<<<< HEAD
            grid.RowTemplate.Height = (int)(40 * grid.DeviceDpi / 96F);
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.DefaultCellStyle.Padding = new Padding(9, 3, 9, 3);
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            grid.Paint -= PaintEmptyGrid;
            grid.Paint += PaintEmptyGrid;
        }

        private static void PaintEmptyGrid(object? sender, PaintEventArgs e)
        {
            if (sender is not DataGridView grid || grid.Rows.Count>(grid.AllowUserToAddRows ? 1: 0)) return;
            var rect = new Rectangle(12, grid.ColumnHeadersHeight + 16, Math.Max(0, grid.Width - 24), Math.Max(0, grid.Height - grid.ColumnHeadersHeight - 32));
            TextRenderer.DrawText(e.Graphics, "Gösterilecek kayıt bulunamadı.", grid.Font, rect, Muted,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
=======
            grid.RowTemplate.Height = 34;
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
        }

        public static void ApplySearchFilter(DataGridView grid, string searchText)
        {
            if (grid.DataSource == null) return;
<<<<<<< HEAD
            var term = DataValues.EscapeLike((searchText ?? string.Empty).Trim());
            if (grid.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = string.IsNullOrWhiteSpace(term)
                ? string.Empty
                : string.Join(" OR ", dt.Columns.Cast<DataColumn>()
                .Where(c => c.DataType == typeof(string))
                .Select(c => $"CONVERT([{c.ColumnName.Replace("\\", "\\\\").Replace("]", "\\]")}], 'System.String') LIKE '%{term}%'"));
=======
            var term = (searchText ?? string.Empty).Trim().Replace("'", "''");
            if (grid.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = string.IsNullOrWhiteSpace(term)
                    ? string.Empty
                    : string.Join(" OR ", dt.Columns.Cast<DataColumn>()
                        .Where(c => c.DataType == typeof(string))
                        .Select(c => $"CONVERT([{c.ColumnName}], 'System.String') LIKE '%{term}%'"));
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            }
            else if (grid.DataSource is DataView dv)
            {
                dv.RowFilter = string.IsNullOrWhiteSpace(term)
<<<<<<< HEAD
                ? string.Empty
                : string.Join(" OR ", dv.Table.Columns.Cast<DataColumn>()
                .Where(c => c.DataType == typeof(string))
                .Select(c => $"CONVERT([{c.ColumnName.Replace("\\", "\\\\").Replace("]", "\\]")}], 'System.String') LIKE '%{term}%'"));
=======
                    ? string.Empty
                    : string.Join(" OR ", dv.Table.Columns.Cast<DataColumn>()
                        .Where(c => c.DataType == typeof(string))
                        .Select(c => $"CONVERT([{c.ColumnName}], 'System.String') LIKE '%{term}%'"));
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
            }
        }

        public static void UseSegoeRecursive(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child.Font != null)
<<<<<<< HEAD
                child.Font = new Font("Segoe UI", child.Font.Size <= 0 ? 9.5F: child.Font.Size, child.Font.Style, GraphicsUnit.Point,
                162);
=======
                    child.Font = new Font("Segoe UI", child.Font.Size <= 0 ? 9.5F : child.Font.Size, child.Font.Style, GraphicsUnit.Point, 162);
>>>>>>> 645a1b309fb5801e896c1ed802514678b2a0ed31
                UseSegoeRecursive(child);
            }
        }
    }
}
