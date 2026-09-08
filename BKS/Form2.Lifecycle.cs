using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.Collections;
namespace BKS;
public partial class Form2
{
    private async void Form2_Load(object sender, EventArgs e)
    {
        if (AppConfiguration.DesignPreview)
        {
            PrepareDesignPreview();
            return;
        }
        _ribbon.SetExpanded(UiPreferences.ReadRibbonExpanded(UserId));
        await InitializeSessionAsync();
    }
    private void Form2_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!_documents.CloseAll())
        {
            e.Cancel = true;
            return;
        }
        _sessionCancellation.Cancel();
    }
}
