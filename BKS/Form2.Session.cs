using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Json;
namespace BKS;
public partial class Form2
{
    private readonly HashSet<string> _allowedModules = new(StringComparer.OrdinalIgnoreCase);
    private readonly HashSet<string> _loadedModules = new(StringComparer.OrdinalIgnoreCase);
    private readonly HashSet<string> _loadingModules = new(StringComparer.OrdinalIgnoreCase);
    private readonly CancellationTokenSource _sessionCancellation = new();
    private bool _sessionLoading;
    private const string FinanceQuery = ModuleReader.FinanceQuery;
    private readonly Dictionary<string, string> _titles = new()
    {
        ["tabPageOgrenciOnKayit"] = "Ön kayıt",
        ["tabPageStok"] = "Öğrenciler",
        ["tabPageSatis"] = "Ödemeler",
        ["tabPagePersonelYonetimi"] = "Personel",
        ["tabPageGelirGider"] = "Gelir / gider",
        ["tabPageOzelRaporlar"] = "Raporlar"
    };
    private void SetRibbonStatus(string text)
    {
        if (!IsDisposed) _ribbonStatus.Text = $"{Role}  ·  {UserId.ToString()[..8]}  |  {text}";
    }
    private void ApplyAccess(IEnumerable<string> modules, string role)
    {
        _allowedModules.Clear();
        _allowedModules.UnionWith(ModuleAccess.Resolve(_allModulePages.Select(p => p.Name), modules, role));
        _documents.ApplyAccess(_allowedModules);
        var ribbonKeys = new List<string>
        {
            "home",
            "system"
        };
        if (_allowedModules.Contains(tabPageStok.Name)) ribbonKeys.AddRange(new[]
        {
            tabPageStok.Name,
            "classes"
        });
        if (_allowedModules.Contains(tabPageOgrenciOnKayit.Name)) ribbonKeys.Add(tabPageStok.Name);
        if (_allowedModules.Contains(tabPageSatis.Name) || _allowedModules.Contains(tabPageGelirGider.Name)) ribbonKeys.Add("finance");
        if (_allowedModules.Contains(tabPagePersonelYonetimi.Name)) ribbonKeys.Add(tabPagePersonelYonetimi.Name);
        if (_allowedModules.Contains(tabPageOzelRaporlar.Name)) ribbonKeys.Add(tabPageOzelRaporlar.Name);
        _ribbon.SetAllowed(ribbonKeys);
        RefreshHome();
    }
    private async Task InitializeSessionAsync()
    {
        if (_sessionLoading || AppConfiguration.DesignPreview) return;
        _sessionLoading = true;
        _loadedModules.Clear();
        ApplyAccess(Array.Empty<string>(), "");
        SetRibbonStatus("Modül yetkileri doğrulanıyor…");
        try
        {
            if (UserId == Guid.Empty) throw new InvalidOperationException("Geçerli kullanıcı oturumu yok.");
            using var client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(25)
            };
            var result = await client.GetFromJsonAsync<ModuleResponse>(AppConfiguration.Api($"api/modules/{UserId}?role={Uri.EscapeDataString(Role ?? "")}"),
            _sessionCancellation.Token);
            if (result?.modules is null || string.IsNullOrWhiteSpace(result.role)) throw new InvalidOperationException("Modül yetkileri eksik döndü. Ayarlar sekmesinden yeniden deneyebilirsiniz.");
            Role = result.role;
            SessionContext.Role = Role;
            ApplyAccess(result.modules, result.role);
            SetRibbonStatus(_allowedModules.Count == 0 ? "Bu kullanıcıya atanmış modül bulunamadı.": $"{_allowedModules.Count} modül erişime açık.");
        }
        catch (OperationCanceledException)
        {
            if (!IsDisposed) SetRibbonStatus("Yetki isteği iptal edildi veya zaman aşımına uğradı.");
        }
        catch (Exception ex)
        {
            ApplyAccess(Array.Empty<string>(), "");
            SetRibbonStatus("Yetkiler yüklenemedi. Ayarlar → Yetkileri yenile.");
            UiActions.ShowError(ex);
        }
        finally
        {
            _sessionLoading = false;
            if (!IsDisposed) _ribbon.RefreshCommands();
        }
        if (!IsDisposed)
        {
            _documents.Activate("home");
            _ribbon.SelectPage("home", false);
        }
    }
    private void SelectModuleByKey(string key)
    {
        if (!_allowedModules.Contains(key)) return;
        var page = _allModulePages.First(p => p.Name == key);
        _documents.OpenPage(key, _titles[key], page, key);
        SetRibbonStatus(_titles[key]);
        if (!_sessionLoading && !_loadedModules.Contains(key)) _ = LoadModuleAsync(key);
    }
    private async Task LoadModuleAsync(string key)
    {
        if (AppConfiguration.DesignPreview || !_allowedModules.Contains(key) || !_loadingModules.Add(key)) return;
        var user = UserId;
        var loadingPage = _allModulePages.First(page => page.Name == key);
        loadingPage.Enabled = false;
        if (key == tabPageStok.Name) _classesPage.Enabled = false;
        SetRibbonStatus(_titles[key] + " yükleniyor…");
        try
        {
            // Only database reads run in the worker. All bindings and controls stay on the UI thread.
            var result = await Task.Run(() => new ModuleReader(Database).Read(key, user), _sessionCancellation.Token);
            if (IsDisposed || !_allowedModules.Contains(key)) return;
            switch (key)
            {
                case "tabPageOgrenciOnKayit":
                dgvOnKayitlar.DataSource = result[0];
                break;
                case "tabPageStok":
                dataGridViewStok.DataSource = result[0];
                DgvOgrenciYonetimiSiniflar.DataSource = result[1];
                BindCombo(cbxOgrenciYonetimiOgretmen, result[2], "isim", "PersonelId");
                YasGrubuLoad();
                ModernWinForms.ApplySearchFilter(dataGridViewStok, txtOgrenciYonetimiAra.Text);
                break;
                case "tabPageSatis":
                dataOgrVw.DataSource = result[0];
                BindCombo(comboBoxStok, result[1], "Name", "Id");
                break;
                case "tabPagePersonelYonetimi":
                dgvPersonelYonetimi.DataSource = result[0];
                ModernWinForms.ApplySearchFilter(dgvPersonelYonetimi, _personnelSearch.Text);
                break;
                case "tabPageGelirGider":
                dataGridOdeme.DataSource = result[0];
                UpdateFinanceSummary(result[0]);
                break;
                case "tabPageOzelRaporlar":
                salesGrid.DataSource = result[0];
                break;
            }
            foreach (var grid in new[]
            {
                dataGridViewStok,
                dgvPersonelYonetimi,
                DgvOgrenciYonetimiSiniflar,
                dgvOnKayitlar,
                salesGrid
            }) foreach (var name in new[]
            {
                "Id",
                "PersonelId",
                "Photo",
                "photobinary",
                "Sorgu",
                "FotoId"
            }) if (grid.Columns.Contains(name)) grid.Columns[name].Visible = false;
            if (!_loadedModules.Contains(key))
            {
                if (_pageEdits.TryGetValue(key, out var edits)) edits.AcceptChanges();
                if (key == tabPageStok.Name) _pageEdits["classes"].AcceptChanges();
            }
            _loadedModules.Add(key);
            RefreshHome();
            SetRibbonStatus(_titles[key] + " hazır.");
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception ex)
        {
            if (!IsDisposed)
            {
                SetRibbonStatus("Yükleme tamamlanamadı. F5 ile yeniden deneyin.");
                UiActions.ShowError(ex);
            }
        }
        finally
        {
            _loadingModules.Remove(key);
            if (!IsDisposed)
            {
                loadingPage.Enabled = _allowedModules.Contains(key);
                if (key == tabPageStok.Name) _classesPage.Enabled = _allowedModules.Contains(key);
                _ribbon.RefreshCommands();
            }
        }
    }
    private static void BindCombo(ComboBox combo, DataTable table, string text, string value)
    {
        var selectedValue = (combo.SelectedItem as ComboBoxItem)?.Value;
        var enteredText = combo.Text;
        combo.BeginUpdate();
        try
        {
            combo.Items.Clear();
            foreach (DataRow row in table.Rows) combo.Items.Add(new ComboBoxItem
            {
                Text = Convert.ToString(row[text]),
                Value = Convert.ToString(row[value])
            });
        }
        finally
        {
            combo.SelectedItem = combo.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Value == selectedValue);
            if (combo.SelectedItem == null && combo.DropDownStyle != ComboBoxStyle.DropDownList) combo.Text = enteredText;
            combo.EndUpdate();
        }
    }
    private void ReloadCurrent()
    {
        var key = tabControl.SelectedTab?.Name;
        if (key == "classes") key = tabPageStok.Name;
        if (key != null && _allowedModules.Contains(key)) _ = LoadModuleAsync(key);
    }
}
