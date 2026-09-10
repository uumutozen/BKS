using BKS;
using System.Data;
using System.IO.Compression;
using System.Globalization;
var count=0;
void Check(bool condition,string title){if(!condition)throw new Exception(title);Console.WriteLine("PASS "+title);count++;}
string[] known={"students","staff"};
Check(!ModuleAccess.Resolve(known,null,"ADMIN").Any(),"Missing permission response denies access");
Check(!ModuleAccess.Resolve(known,Array.Empty<string>(),"USER").Any(),"Empty permissions deny ordinary user");
Check(!ModuleAccess.Resolve(known,new[]{"unknown"},"USER").Any(),"Unknown module cannot unlock all pages");
Check(ModuleAccess.Resolve(known,new[]{"STUDENTS"},"USER").SequenceEqual(new[]{"students"}),"Only granted module opens");
Check(ModuleAccess.Resolve(known,Array.Empty<string>(),"ADMİN").Count()==2,"Validated Turkish admin role supported");
Check(DataValues.Boolean(true)&&DataValues.Boolean("Evet")&&DataValues.Boolean(1)&&!DataValues.Boolean(DBNull.Value),"Pre-registration payment flags survive conversion");
Check(DataValues.Money("1.250,55")==1250.55M,"Payment cents preserved");
var line=new InvoiceLine("Örnek",3,10.55M,20);line.Validate();Check(line.Net==31.65M&&line.Vat==6.33M&&line.Total==37.98M,"Invoice totals and VAT reconcile");
var invalid=false;try{new InvoiceLine("Örnek",-1,10,20).Validate();}catch(InvalidOperationException){invalid=true;}Check(invalid,"Negative invoice quantity rejected");
Check(LayoutRules.FieldColumns(1000,1)==3&&LayoutRules.FieldColumns(1000,1.5F)==2&&LayoutRules.FieldColumns(230,2)==1,"Layout adapts to width and DPI");
var table=new DataTable();table.Columns.Add("Ad");table.Rows.Add("A[1] % * O'Neil");table.Rows.Add("Normal kayıt");
foreach(var term in new[]{"[1]","%","*","O'Neil"}){table.DefaultView.RowFilter=$"Ad LIKE '%{DataValues.EscapeLike(term)}%'";Check(table.DefaultView.Count==1,"Literal search works: "+term);}
var file=Path.Combine(Path.GetTempPath(),"bks-import-"+Guid.NewGuid().ToString("N")+".xlsx");
try
{
 using(var zip=ZipFile.Open(file,ZipArchiveMode.Create))
 {
  void Entry(string name,string content){using var w=new StreamWriter(zip.CreateEntry(name).Open());w.Write(content);}
  Entry("xl/workbook.xml","<workbook xmlns='http://schemas.openxmlformats.org/spreadsheetml/2006/main' xmlns:r='http://schemas.openxmlformats.org/officeDocument/2006/relationships'><sheets><sheet name='Data' sheetId='1' r:id='rId1'/></sheets></workbook>");
  Entry("xl/_rels/workbook.xml.rels","<Relationships xmlns='http://schemas.openxmlformats.org/package/2006/relationships'><Relationship Id='rId1' Target='worksheets/sheet1.xml'/></Relationships>");
  Entry("xl/styles.xml","<styleSheet xmlns='http://schemas.openxmlformats.org/spreadsheetml/2006/main'><cellXfs><xf numFmtId='0'/><xf numFmtId='14'/></cellXfs></styleSheet>");
  Entry("xl/sharedStrings.xml","<sst xmlns='http://schemas.openxmlformats.org/spreadsheetml/2006/main'><si><t>Çağrı Şen</t></si></sst>");
  Entry("xl/worksheets/sheet1.xml","<worksheet xmlns='http://schemas.openxmlformats.org/spreadsheetml/2006/main'><sheetData><row r='2'><c r='A2' t='s'><v>0</v></c><c r='B2'><v>1250.55</v></c><c r='C2' s='1'><v>45000</v></c><c r='D2' t='inlineStr'><is><t>İstanbul</t></is></c></row></sheetData></worksheet>");
 }
 using var book=new XlsxWorkbook(new FileInfo(file));
 Check(book.FirstSheet.Cells[2,1].Text=="Çağrı Şen"&&book.FirstSheet.Cells[2,4].Text=="İstanbul","XLSX shared/inline Turkish strings preserved");
 Check(book.FirstSheet.Cells[2,2].Text=="1250,55","XLSX numeric values use Turkish formatting");
 Check(book.FirstSheet.Cells[2,3].Value is DateTime date&&date==DateTime.FromOADate(45000),"Excel serial date interpreted via style");
 Check(book.FirstSheet.Cells[50,50].Text=="","Empty Excel cells safe");
}
finally{File.Delete(file);}
var layoutCases = 0;
foreach (var scale in new[] { 1F, 1.25F, 1.5F, 2F })
foreach (var size in new[] { (0, 0), (600, 400), (900, 600), (1366, 700), (1920, 1000) })
foreach (var expanded in new[] { true, false })
foreach (var title in new[] { true, false })
foreach (var footer in new[] { true, false })
{
 var r = LayoutRules.Workspace(size.Item1, size.Item2, scale, expanded, title, footer);
 if (r.Content.Top != r.TitleHeight + r.RibbonHeight || r.Content.Height < 0 || r.Content.Bottom + r.FooterHeight != size.Item2)
   throw new Exception("Workspace sections overlap or exceed client bounds");
 layoutCases++;
}
Check(layoutCases == 160, "160 workspace cases: 100/125/150/200% DPI, resize and ribbon states do not overlap");
Check(LayoutRules.EditorHeight(400, .15F, 1F) >= 96, "Short search panel preserves one readable input row");
Check(LayoutRules.EditorHeight(700, .15F, 2F) >= 192, "Search field minimum scales at 200% DPI");
Check(LayoutRules.EditorHeight(0, .15F, 2F) == 0, "Minimized editor cannot produce negative bounds");
var documents = new DocumentRegistry<object>();
var factoryCalls = 0;
var firstDocument = documents.GetOrCreate("student:42", () => { factoryCalls++; return new object(); });
var sameDocument = documents.GetOrCreate("STUDENT:42", () => { factoryCalls++; return new object(); });
Check(ReferenceEquals(firstDocument, sameDocument) && factoryCalls == 1, "Repeated navigation preserves the same editor and unsaved values");
documents.Remove("student:42");
var reopened = documents.GetOrCreate("student:42", () => new object());
Check(!ReferenceEquals(firstDocument, reopened), "Closing and reopening creates a fresh editor");
try { documents.GetOrCreate("failed", () => throw new InvalidOperationException("Factory failed")); }
catch (InvalidOperationException) { }
Check(!documents.TryGet("failed", out _), "A failed editor factory does not reserve its document key");
var compact = LayoutRules.Workspace(1366, 768, 1F, true, false, true);
Check(compact.RibbonHeight == 134 && compact.Content.Height == 606, "Compact ribbon preserves working space at 1366x768");
RegressionChecks.Run(Check);
WorkflowChecks.Run(Check);
Console.WriteLine($"{count} checks passed.");
