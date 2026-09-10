using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BKS
{
    internal static class ModernWinForms
    {
        public static readonly Color PageBack = RibbonPalette.Workspace;
        public static readonly Color CardBack = RibbonPalette.Group;
        public static readonly Color Border = RibbonPalette.Border;
        public static readonly Color Primary = RibbonPalette.Accent;
        public static readonly Color PrimaryDark = RibbonPalette.ActiveBorder;
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);
        public static readonly Color Success = Color.FromArgb(22, 163, 74);
        public static readonly Color Text = RibbonPalette.Text;
        public static readonly Color Muted = RibbonPalette.CaptionText;

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
            groupBox.Text = title;
            groupBox.Dock = DockStyle.Fill;
            groupBox.Padding = new Padding(14, 28, 14, 14);
            groupBox.Margin = new Padding(0, 0, 14, 14);
            groupBox.BackColor = CardBack;
            groupBox.ForeColor = PrimaryDark;
            groupBox.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 162);
        }

        public static void StyleGrid(DataGridView grid) => GridAppearance.Apply(grid);

        public static void ApplySearchFilter(DataGridView grid, string searchText) =>
            GridFilterController.For(grid).SetSearch(searchText);

        public static void UseSegoeRecursive(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child.Font != null)
                child.Font = new Font("Segoe UI", child.Font.Size <= 0 ? 9.5F: child.Font.Size, child.Font.Style, GraphicsUnit.Point,
                162);
                UseSegoeRecursive(child);
            }
        }
    }
}
