namespace BKS;
/// <summary>Tracks inputs in visible and hidden sections. A successful save explicitly accepts the new state.</summary>
internal sealed class EditSession
{
    private readonly Control root;
    private readonly Action save;
    private readonly Func<object?>? extraValue;
    private Dictionary<Control, object?> baseline = new();
    private object? originalExtra;
    private bool ready;
    private bool confirming;
    public EditSession(Control root, Action save, Func<object?>? extraValue = null)
    {
        this.root = root;
        this.save = save;
        this.extraValue = extraValue;
        if (root is Form form)
        {
            form.Shown += (_, _) => AcceptChanges();
            form.FormClosing += (_, e) => e.Cancel = !ConfirmClose();
        }
        else AcceptChanges();
    }
    public void AcceptChanges()
    {
        baseline = Snapshot();
        originalExtra = extraValue?.Invoke();
        ready = true;
    }
    private Dictionary<Control, object?> Snapshot() => Walk(root).Distinct()
    .Where(c => c is TextBoxBase or ComboBox or DateTimePicker or NumericUpDown or CheckBox or RadioButton)
    .ToDictionary(c => c, ReadValue);
    private static object? ReadValue(Control control) => control switch
    {
        DateTimePicker date => (date.Value, date.Checked),
        NumericUpDown number => number.Value,
        CheckBox check => check.Checked,
        RadioButton radio => radio.Checked,
        _ => control.Text
    };
    private static IEnumerable<Control> Walk(Control root)
    {
        yield return root;
        var children = root is SectionedForm sections
        ? root.Controls.Cast<Control>().Concat(sections.Contents): root.Controls.Cast<Control>();
        foreach (var child in children)
        foreach (var nested in Walk(child)) yield return nested;
    }
    private bool HasChanges()
    {
        if (!ready) return false;
        var current = Snapshot();
        return current.Count != baseline.Count || current.Any(p => !baseline.TryGetValue(p.Key, out var old) || !Equals(old,
        p.Value))
        || !Equals(originalExtra, extraValue?.Invoke());
    }
    public bool ConfirmClose()
    {
        if (confirming || !HasChanges()) return true;
        var owner = root.FindForm();
        var result = System.Windows.Forms.MessageBox.Show(owner,
        "Değişiklikler kaydedilsin mi?\nEvet: Kaydet · Hayır: Vazgeç · İptal: Düzenlemeye dön",
        root.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (result == DialogResult.Cancel) return false;
        if (result == DialogResult.No)
        {
            if (root is not Form) RestoreValues();
            return true;
        }
        confirming = true;
        try
        {
            UiActions.Run(save);
            return root.IsDisposed || !HasChanges();
        }
        finally
        {
            confirming = false;
        }
    }
    private void RestoreValues()
    {
        foreach (var(control, value) in baseline)
        {
            if (control.IsDisposed) continue;
            switch (control)
            {
                case DateTimePicker date when value is ValueTuple<DateTime, bool> state:
                date.Value = state.Item1;
                date.Checked = state.Item2;
                break;
                case NumericUpDown number when value is decimal amount:
                number.Value = amount;
                break;
                case CheckBox check when value is bool isChecked:
                check.Checked = isChecked;
                break;
                case RadioButton radio when value is bool isSelected:
                radio.Checked = isSelected;
                break;
                default:
                control.Text = Convert.ToString(value) ?? "";
                break;
            }
        }
        AcceptChanges();
    }
}
