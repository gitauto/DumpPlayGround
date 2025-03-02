using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace DumpLibrary;

public static class DumpExtensions
{
    private static string CssDarkMode { get; } = @"html,body,div,span,iframe,p,pre,a,abbr,acronym,code,del,em,img,ins,q,var,i,fieldset,form,label,legend,table,caption,tbody,tfoot,thead,tr,th,td,article,aside,canvas,details,figure,figcaption,footer,header,hgroup,nav,output,section,summary,time,mark,audio,video{margin:0;padding:0;border:0;vertical-align:baseline;font:inherit;font-size:100%}h1,h2,h3,h4,h5,h6{margin:.2em 0 .05em 0;padding:0;border:0;vertical-align:baseline}i,em{font-style:italic}body{margin:0.5em;font-family:Segoe UI,Verdana,sans-serif;font-size:82%;background:rgb(30,30,30);color:rgb(220,220,220)}pre,code,.fixedfont{font-family:Consolas,monospace;font-size:10pt}a,a:visited{text-decoration:none;font-family:Segoe UI Semibold,sans-serif;font-weight:bold;cursor:pointer}a:hover,a:visited:hover{text-decoration:underline}span.hex{color:rgb(200,130,240);font-family:Consolas,monospace;margin-top:1px}span.hex::before{content:"" 0x"";color:rgb(100,100,100)}table{border-collapse:collapse;border-spacing:0;border:2px solid #3887B5;margin:0.3em 0.1em 0.2em 0.1em}table.limit{border-bottom-color:#f83}table.expandable{border-bottom-style:dashed}td,th{vertical-align:top;border:1px solid #3887B5;margin:0}th{position:-webkit-sticky;position:sticky;top:0;z-index:2}th[scope=row]{position:-webkit-sticky;position:sticky;left:0;z-index:2}th{padding:0.05em 0.3em 0.15em 0.3em;text-align:left;background-color:rgb(80,80,80);border:1px solid rgb(50,50,50);color:rgb(220,220,220);padding:0.3em 0.5em;font-size:.95em;font-family:Segoe UI Semibold,sans-serif;font-weight:bold}th.private{font-family:Segoe UI;font-weight:normal;font-style:italic}td.private{background:rgb(50,50,50)}td.private table{background:rgb(30,30,30)}td,th.member{padding:0.1em 0.3em 0.2em 0.3em;position:initial}tr.repeat>th{font-size:90%;font-family:Segoe UI Semibold,sans-serif;border:none;background-color:rgb(80,80,80);color:#999;padding:0.0em 0.2em 0.15em 0.3em}td.typeheader{font-size:.95em;background-color:#3887B5;color:#eee;border:1px solid #3887B5;padding:0 0.3em 0.25em 0.2em}td.n{text-align:right}a,a:link{color:rgb(140,200,255)}a:visited{color:rgb(220,120,235)}a.typeheader,a:link.typeheader,a:visited.typeheader,a:link.extenser,a:visited.extenser{font-family:Segoe UI Semibold,sans-serif;font-size:.95em;font-weight:bold;text-decoration:none;color:#eee;margin-bottom:-0.1em;float:left}a.difheader,a:link.difheader,a:visited.difheader{color:#ff8}a.extenser,a:link.extenser,a:visited.extenser{margin:0 0 0 0.3em;padding-left:0.5em;padding-right:0.3em;color:white}a:hover.extenser{text-decoration:none}span.extenser{font-size:1.1em;line-height:0.8}span.cyclic{padding:0 0.2em 0 0;margin:0;font-family:Arial,sans-serif;font-weight:bold;margin:2px;font-size:1.5em;line-height:0;vertical-align:middle}.arrow-up,.arrow-down{display:inline-block;margin:0 0.3em 0.15em 0.1em;width:0;height:0;cursor:pointer}.arrow-up{border-left:0.35em solid transparent;border-right:0.35em solid transparent;border-bottom:0.35em solid white}.arrow-down{border-left:0.35em solid transparent;border-right:0.35em solid transparent;border-top:0.35em solid white}table.group{border:none;margin:0}td.group{border:none;padding:0 0.1em}div.spacer{margin:0.6em 0}div.headingpresenter{border:none;border-left:0.17em dashed #E5B679;margin:.8em 0 1em 0.1em;padding-left:.5em}div.headingcontinuation{border:none;border-left:0.2em dotted #E5B679;margin:-0.4em 0 1em 0.1em;padding-left:.5em}h1.headingpresenter{border:none;padding:0 0 0.3em 0;margin:0;font-family:Segoe UI Semibold,Arial;font-weight:bold;background-color:rgb(30,30,30);color:#E5B679;font-size:1.1em;line-height:1}td.summary{background-color:#204D66;color:rgb(220,220,220);font-size:.95em;padding:0.05em 0.3em 0.2em 0.3em}tr.columntotal>td{background-color:rgb(80,80,80);font-family:Segoe UI Semibold,Verdana,sans-serif;font-weight:bold;font-size:.95em;color:rgb(220,220,220);;text-align:right}.error > table{border-color:#B56172}.error > table > thead > tr > td.summary{background-color:#F4DEE3;color:black}.error > table > thead > tr > td.typeheader{background-color:#B56172}span.graphbar{background:#3887B5;color:#3887B5;padding-bottom:1px;margin-left:-0.2em;margin-right:0.2em}a.graphcolumn,a:link.graphcolumn,a:visited.graphcolumn{color:rgb(140,200,255);text-decoration:none;font-weight:bold;font-family:Arial;font-size:1em;line-height:1;letter-spacing:-0.2em;margin-left:0.15em;margin-right:0.2em;cursor:pointer}a.collection,a:link.collection,a:visited.collection{color:#80d080}a.reference,a:link.reference,a:visited.reference{color:rgb(130,190,235)}span.meta,span.null{color:#90ee90}span.warning{color:rgb(210,90,90)}span.false{color:#888}span.true{font-weight:bold}.highlight{background:rgb(255,250,13);color:rgb(30,30,30);;padding:2px}code.xml b{color:rgb(140,200,255);font-weight:normal}code.xml i{color:rgb(220,120,255);font-weight:normal;font-style:normal}code.xml em{color:rgb(210,90,90);font-weight:normal;font-style:normal}span.cc{background:#eee;color:black;margin:0 1.5px;padding:0 1px;font-family:Consolas,monospace;border-radius:3px}ol,ul{margin:0.7em 0.3em;padding-left:2.5em}li{margin:0.3em 0}.difadd{background:#070;border:1px solid #090}.difremove{background:#a23;border:1px solid #d32}.rendering{font-style:italic;color:rgb(220,120,255)}p.scriptLog{color:#caa;background:#433;font-family:Consolas,monospace;font-size:9pt;padding:.1em .3em}::-ms-clear{display:none}input,textarea,button,select{font-family:Segoe UI;font-size:1em;padding:.2em}button{padding:.2em .4em}input,textarea,select{margin:.15em 0}input[type=""checkbox""],input[type=""radio""]{margin:0 0.4em 0 0;height:0.9em;width:0.9em}input[type=""radio""]:focus,input[type=""checkbox""]:focus{outline:thin dotted red}.checkbox-label{vertical-align:middle;position:relative;bottom:.07em;margin-right:.5em}fieldset{margin:0 .2em .4em .1em;border:1pt solid #aaa;padding:.1em .6em .4em .6em}legend{padding:.2em .1em}input,textarea,select,legend{background:black;color:rgb(220,220,220)}input,textarea,select{border:1pt solid rgb(220,220,220)}input[type=""range""]{border:none}";
    private static string CssLightMode { get; } = @"html,body,div,span,iframe,p,pre,a,abbr,acronym,code,del,em,img,ins,q,var,i,fieldset,form,label,legend,table,caption,tbody,tfoot,thead,tr,th,td,article,aside,canvas,details,figure,figcaption,footer,header,hgroup,nav,output,section,summary,time,mark,audio,video{margin:0;padding:0;border:0;vertical-align:baseline;font:inherit;font-size:100%}h1,h2,h3,h4,h5,h6{margin:.2em 0 .05em 0;padding:0;border:0;vertical-align:baseline}i,em{font-style:italic}body{margin:0.5em;font-family:Segoe UI,Verdana,sans-serif;font-size:82%;background:white}pre,code,.fixedfont{font-family:Consolas,monospace;font-size:10pt}a,a:visited{text-decoration:none;font-family:Segoe UI Semibold,sans-serif;font-weight:bold;cursor:pointer}a:hover,a:visited:hover{text-decoration:underline}span.hex{color:rgb(200,30,250);font-family:Consolas,monospace;margin-top:1px}span.hex::before{content:"" 0x"";color:rgb(200,200,200)}table{border-collapse:collapse;border-spacing:0;border:2px solid #4C74B2;margin:0.3em 0.1em 0.2em 0.1em}table.limit{border-bottom-color:#B56172}table.expandable{border-bottom-style:dashed}td,th{vertical-align:top;border:1px solid #bbb;margin:0}th{position:-webkit-sticky;position:sticky;top:0;z-index:2}th[scope=row]{position:-webkit-sticky;position:sticky;left:0;z-index:2}th{padding:0.05em 0.3em 0.15em 0.3em;text-align:left;background-color:#ddd;border:1px solid #777;font-size:.95em;font-family:Segoe UI Semibold,sans-serif;font-weight:bold}th.private{font-family:Segoe UI;font-weight:normal;font-style:italic}td.private{background:#f4f4ee}td.private table{background:white}td,th.member{padding:0.1em 0.3em 0.2em 0.3em;position:initial}tr.repeat>th{font-size:90%;font-family:Segoe UI Semibold,sans-serif;border:none;background-color:#eee;color:#999;padding:0.0em 0.2em 0.15em 0.3em}td.typeheader{font-size:.95em;background-color:#4C74B2;color:white;padding:0 0.3em 0.25em 0.2em}td.n{text-align:right}a.typeheader,a:link.typeheader,a:visited.typeheader,a:link.extenser,a:visited.extenser{font-family:Segoe UI Semibold,sans-serif;font-size:.95em;font-weight:bold;text-decoration:none;color:white;margin-bottom:-0.1em;float:left}a.difheader,a:link.difheader,a:visited.difheader{color:#ff8}a.extenser,a:link.extenser,a:visited.extenser{margin:0 0 0 0.3em;padding-left:0.5em;padding-right:0.3em}a:hover.extenser{text-decoration:none}span.extenser{font-size:1.1em;line-height:0.8}span.cyclic{padding:0 0.2em 0 0;margin:0;font-family:Arial,sans-serif;font-weight:bold;margin:2px;font-size:1.5em;line-height:0;vertical-align:middle}.arrow-up,.arrow-down{display:inline-block;margin:0 0.3em 0.15em 0.1em;width:0;height:0;cursor:pointer}.arrow-up{border-left:0.35em solid transparent;border-right:0.35em solid transparent;border-bottom:0.35em solid white}.arrow-down{border-left:0.35em solid transparent;border-right:0.35em solid transparent;border-top:0.35em solid white}table.group{border:none;margin:0}td.group{border:none;padding:0 0.1em}div.spacer{margin:0.6em 0}div.headingpresenter{border:none;border-left:0.17em dashed #1a5;margin:.8em 0 1em 0.1em;padding-left:.5em}div.headingcontinuation{border:none;border-left:0.2em dotted #1a5;margin:-0.4em 0 1em 0.1em;padding-left:.5em}h1.headingpresenter{border:none;padding:0 0 0.3em 0;margin:0;font-family:Segoe UI Semibold,Arial;font-weight:bold;background-color:white;color:#209020;font-size:1.1em;line-height:1}td.summary{background-color:#DAEAFA;color:black;font-size:.95em;padding:0.05em 0.3em 0.2em 0.3em}tr.columntotal>td{background-color:#eee;font-family:Segoe UI Semibold,Verdana,sans-serif;font-weight:bold;font-size:.95em;color:#4C74B2;text-align:right}.error > table{border-color:#B56172}.error > table > thead > tr > td.summary{background-color:#F4DEE3;color:black}.error > table > thead > tr > td.typeheader{background-color:#B56172}span.graphbar{background:#DAEAFA;color:#DAEAFA;padding-bottom:1px;margin-left:-0.2em;margin-right:0.2em}a.graphcolumn,a:link.graphcolumn,a:visited.graphcolumn{color:#4C74B2;text-decoration:none;font-weight:bold;font-family:Arial;font-size:1em;line-height:1;letter-spacing:-0.2em;margin-left:0.15em;margin-right:0.2em;cursor:pointer}a.collection,a:link.collection,a:visited.collection{color:#209020}a.reference,a:link.reference,a:visited.reference{color:#0080D1}span.meta,span.null{color:#209020}span.warning{color:red}span.false{color:#888}span.true{font-weight:bold}.highlight{background:#ff8}code.xml b{color:blue;font-weight:normal}code.xml i{color:brown;font-weight:normal;font-style:normal}code.xml em{color:red;font-weight:normal;font-style:normal}span.cc{background:#666;color:white;margin:0 1.5px;padding:0 1px;font-family:Consolas,monospace;border-radius:3px}ol,ul{margin:0.7em 0.3em;padding-left:2.5em}li{margin:0.3em 0}.difadd{background:#a3f3a3;border:1px solid #88d888}.difremove{background:#ffc8c8;border:1px solid #e8b3b3}.rendering{font-style:italic;color:brown}p.scriptLog{color:#a77;background:#f8f6f6;font-family:Consolas,monospace;font-size:9pt;padding:.1em .3em}::-ms-clear{display:none}input,textarea,button,select{font-family:Segoe UI;font-size:1em;padding:.2em}button{padding:.2em .4em}input,textarea,select{margin:.15em 0}input[type=""checkbox""],input[type=""radio""]{margin:0 0.4em 0 0;height:0.9em;width:0.9em}input[type=""radio""]:focus,input[type=""checkbox""]:focus{outline:thin dotted red}.checkbox-label{vertical-align:middle;position:relative;bottom:.07em;margin-right:.5em}fieldset{margin:0 .2em .4em .1em;border:1pt solid #aaa;padding:.1em .6em .4em .6em}legend{padding:.2em .1em}";

    private static string NullHtml { get; } = @"<div><span class=""null"">null</span></div>";

    // https://developer.mozilla.org/en-US/docs/Web/API/Document_Object_Model
    private static string HtmlPageTemplate { get; } = @"
<!DOCTYPE html>
<html lang=""%lang%"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    %title%

    <style type='text/css'>
        %css%
    </style>

    <script language='JavaScript' type='text/javascript'>
        function appendRawHTML(html) 
        {
            var newElement = document.createElement('div');
            newElement.innerHTML = html;
            document.body.appendChild(newElement);
        }
		function toggle(id) 
        {
			var table = document.getElementById(id);
			if (table == null) return false;
			var updown = document.getElementById(id + 'ud');
			if (updown == null) return false;
			var expand = updown.className == 'arrow-down';
			updown.className = expand ? 'arrow-up' : 'arrow-down';
			table.style.borderBottomStyle = expand ? 'solid' : 'dashed';
			if (table.rows.length < 2 || table.tBodies.length == 0) return false;
			table.tBodies[0].style.display = expand ? '' : 'none';
			if (table.tHead.rows.length == 2 && !table.tHead.rows[1].id.indexOf('sum') == 0)
				table.tHead.rows[1].style.display = expand ? '' : 'none';
			if (table.tFoot != null)
				table.tFoot.style.display = expand ? '' : 'none';
			if (expand)
				table.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
			return false;
		}
	</script>
</head>
<body>
</body>
</html>";

    // Delegato predefinito per metodi senza ritorno (void), e un parametro di tipo stringa
    public static Action<string>? AppendRawHTML { get; set; }

    private static uint _numElement = 0;

    public static T Dump<T>(this T obj, string? title = null, int maxDepth = 5)
    {
        //---------------------------
        // Convert the Objet to HTML
        //---------------------------
        string html = obj.ToHtmlString(maxDepth);

        // Manage the Title if passed
        if (!string.IsNullOrWhiteSpace(title))
        {
            var sb = new StringBuilder();
            sb.Append(@"<div class=""headingpresenter"">");
            sb.Append($@"<h1 class=""headingpresenter"">{MakeSafeHTMLString(title)}</h1>");
            sb.Append(html);
            sb.Append("</div>");
            html = sb.ToString();
        }
        
        if (AppendRawHTML is not null) { AppendRawHTML(html); }
        return obj;
    }

    public static string ToHtmlString(this object? obj, int maxDepth = 5, HashSet<object>? visitedObjects = null)
    {
        if (obj == null) { return NullHtml; }

        visitedObjects ??= new HashSet<object>(new ReferenceEqualityComparer());

        Type type = obj.GetType();

        if (IsSimpleType(type)) 
        { 
            return MakeSafeHTMLString(obj.ToString() ?? ""); 
        }
        else if (obj is DataTable dataTable)
        {
            return DataTableToHtmlTable(dataTable, maxDepth, visitedObjects);
        }
        else if (obj is DataRowCollection dataRowCollection)
        {
            var relatedDataTable = dataRowCollection.Count > 0 ? dataRowCollection[0].Table : new DataTable();
            return DataTableToHtmlTable(relatedDataTable, maxDepth, visitedObjects, nameof(DataRowCollection));
        }
        else if (obj is DataSet dataSet)
        {
            return DataSetToHtmlTable(dataSet, maxDepth, visitedObjects);
        }
        else if (obj is IEnumerable enumerable)
        {
            return EnumerableToHtmlTable(enumerable, maxDepth, visitedObjects);
        }
        else
        {
            return ObjectToHtmlTable(obj, maxDepth, visitedObjects);
        }
    }

    private static string DataSetToHtmlTable(DataSet dataSet, int maxDepth, HashSet<object> visitedObjects, string headerText = "Result Set")
    {
        string pluralSuffix = GetPluralSuffix(dataSet.Tables.Count);

        var tableId = $"t{Interlocked.Increment(ref _numElement)}"; // Thread-safe increment
        var sb = new StringBuilder();

        // Genera l'intestazione della tabella
        sb.AppendLine($@"<table id=""{tableId}"">");
        sb.AppendLine($@"<thead><tr>");
        sb.AppendLine($@"<td class=""typeheader"" colspan=""1"">");
        sb.AppendLine($@"<a class=""typeheader"" onclick=""return toggle('{tableId}');"">");
        sb.AppendLine($@"<span class=""arrow-up"" id=""{tableId}ud""></span>");
        sb.AppendLine($@"{MakeSafeHTMLString(headerText)} ({dataSet.Tables.Count} item{pluralSuffix})");
        sb.AppendLine($@"</a></td>");
        sb.AppendLine($@"</tr></thead>");
        sb.AppendLine($@"<tbody><tr><td>");

        // Genera le tabelle interne
        foreach (DataTable dataTable in dataSet.Tables)
        {
            sb.AppendLine(DataTableToHtmlTable(dataTable, maxDepth - 1, visitedObjects));
        }

        // Chiude la tabella
        sb.AppendLine("</td></tr></tbody>");
        sb.AppendLine("</table>");

        return sb.ToString();
    }

    private static string DataTableToHtmlTable(DataTable dataTable, int maxDepth, HashSet<object> visitedObjects, string headerText = "Result Set")
    {
        var tableId = $"t{Interlocked.Increment(ref _numElement)}";
        string pluralSuffix = GetPluralSuffix(dataTable.Rows.Count);

        var sb = new StringBuilder();

        sb.AppendLine($@"<table id=""{tableId}"">");

        bool headerPrinted = false;

        foreach (DataRow item in dataTable.Rows)
        {
            if (item == null) { continue; }

            // Per il primo elemento, genera l'header basato sulle proprietà
            if (!headerPrinted)
            {
                sb.AppendLine($@"<thead><tr>");
                sb.AppendLine($@"<td class=""typeheader"" colspan=""{dataTable.Columns.Count}"">");
                sb.AppendLine($@"<a class=""typeheader"" onclick=""return toggle('{tableId}');"">");
                sb.AppendLine($@"<span class=""arrow-up"" id=""{tableId}ud""></span>{MakeSafeHTMLString(headerText)} ({dataTable.Rows.Count} item{pluralSuffix})</a>");
                sb.AppendLine($@"</td></tr><tr>");

                // Genera le colonne dell'intestazione
                foreach (DataColumn column in dataTable.Columns)
                {
                    sb.AppendFormat("<th>{0}</th>", MakeSafeHTMLString(column.ColumnName));
                }
                sb.AppendLine("</tr></thead>");
                headerPrinted = true;

                sb.AppendLine("<tbody>");
            }

            sb.AppendLine("<tr>");
            foreach (DataColumn column in dataTable.Columns)
            {
                object? value = item[column.ColumnName];
                sb.AppendFormat("<td>{0}</td>", FormatValue(value ?? "", maxDepth -1, visitedObjects));
            }
            sb.AppendLine("</tr>");
        }

        // Chiude la tabella
        sb.AppendLine("</tbody>");
        sb.AppendLine("</table>");

        return sb.ToString();
    }

    private static string EnumerableToHtmlTable(IEnumerable enumerable, int maxDepth, HashSet<object> visitedObjects)
    {
        var tableId = $"t{Interlocked.Increment(ref _numElement)}";

        var sb = new StringBuilder();

        sb.AppendLine(@"<div class=""spacer"">");
        sb.AppendLine($@"<table id=""{tableId}"">");

        bool headerPrinted = false;
        List<MemberInfo> members = [];

        foreach (var item in enumerable)
        {
            if (item == null) { continue; }

            // Per il primo elemento, genera l'header basato sulle proprietà
            if (!headerPrinted)
            {
                // Ottieni i membri pubblici solo dal primo elemento
                Type itemType = item.GetType();
                bool isSilpeType = IsSimpleType(itemType);
                if (!isSilpeType) { members = GetPublicMembers(itemType); }
                int length = GetEnumerableLength(enumerable);

                if (members.Count > 0)
                {
                    var formattedTypeName = $"{enumerable.GetType().Name.Replace("[]", $"[{length}]")}";

                    sb.AppendLine("<thead><tr>");
                    sb.AppendLine($@"<td class=""typeheader"" colspan=""{members.Count}"">");
                    sb.AppendLine($@"<a class=""typeheader"" onclick=""return toggle('{tableId}');"">");
                    sb.AppendLine($@"<span class=""arrow-up"" id=""{tableId}ud""></span>{MakeSafeHTMLString(formattedTypeName)}</a>");
                    sb.AppendLine($@"</td></tr>");

                    sb.AppendLine($@"<tr>");
                    foreach (var member in members)
                    {
                        sb.AppendFormat("<th>{0}</th>", MakeSafeHTMLString(member.Name));
                    }
                    sb.AppendLine("</tr></thead>");
                }
                else 
                {
                    bool showItemRow = true;
                    string cleanTypeName = TypeHelper.GetCleanTypeName((enumerable));

                    var formattedTypeName = MakeSafeHTMLString($"{cleanTypeName}");
                    if (isSilpeType && !cleanTypeName.StartsWith("List<"))
                    {
                        showItemRow = false;
                        formattedTypeName = $"{formattedTypeName[..^1]}{length}]";
                    }
                    else
                    {
                        formattedTypeName += WebUtility.HtmlEncode($"<{itemType.Name}> ({length}) items)");
                    }

                    sb.AppendLine("<thead><tr>");
                    sb.AppendLine($@"<td class=""typeheader"" colspan=""{members.Count}"">");
                    sb.AppendLine($@"<a class=""typeheader"" onclick=""return toggle('{tableId}');"">");
                    sb.AppendLine($@"<span class=""arrow-up"" id=""{tableId}ud""></span>{formattedTypeName}</a>");
                    sb.AppendLine($@"</td></tr>");

                    if (showItemRow)
                    {
                        sb.AppendLine($@"<tr>");
                        sb.AppendFormat("<th>{0}</th>", "Item");
                        sb.AppendLine("</tr></thead>");
                    }
                }

                headerPrinted = true;

                sb.AppendLine("<tbody>");
            }

            if (members.Count > 0)
            { 
                // Genera le righe della tabella
                sb.AppendLine("<tr>");
                foreach (var member in members)
                {
                    object? value;
                    try
                    {
                        value = member is FieldInfo info ? info.GetValue(item) : ((PropertyInfo)member).GetValue(item, null);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        continue;
                    }
                    sb.AppendFormat("<td>{0}</td>", FormatValue(value ?? "", maxDepth - 1, visitedObjects));
                }
                sb.AppendLine("</tr>");
            }
            else
            {
                sb.AppendLine("<tr>");
                sb.AppendFormat("<td>{0}</td>", item);
                sb.AppendLine("</tr>");
            }
        }

        sb.AppendLine("</tbody>");
        sb.AppendLine("</table>");
        sb.AppendLine("</div>");

        return sb.ToString();
    }

    private static string ObjectToHtmlTable(object obj, int maxDepth, HashSet<object> visitedObjects)
    {
        var tableId = $"t{Interlocked.Increment(ref _numElement)}";

        Type type = obj.GetType();
        string typeToShowFirstLine;
        if (type.IsAnonymous()) { typeToShowFirstLine = "ø"; } else { typeToShowFirstLine = type.Name; }

        // Controlla se l'oggetto è già stato visitato
        if (visitedObjects.Contains(obj))
        {
            // Cyclic Reference
            var sbc = new StringBuilder();

            sbc.AppendLine($@"<table id=""{tableId}"" class=""limit"" title=""Cyclic reference"">");
            sbc.AppendLine($@"<thead>");
            sbc.AppendLine($@"<tr><td class=""typeheader"" colspan=""2""><span class=""cyclic"">∞</span>{MakeSafeHTMLString(typeToShowFirstLine)}</td></tr>");
            sbc.AppendLine($@"</thead>");
            sbc.AppendLine($@"<tbody></tbody>");
            sbc.AppendLine($@"</table>");

            return sbc.ToString();
        }

        // Aggiungi l'oggetto all'insieme dei visitati
        visitedObjects.Add(obj);

        const int numColumns = 2;

        var members = GetPublicMembers(type);

        var sb = new StringBuilder();

        sb.AppendLine($@"<div class=""spacer"">");
        sb.AppendLine($@"<table id=""{tableId}"">");
        sb.AppendLine("<thead>");

        // Prima riga della tabella
        sb.AppendLine("<tr>");
        sb.AppendLine($@"<td class=""typeheader"" colspan=""{numColumns}"">");
        sb.AppendLine($@"<a class=""typeheader"" onclick=""return toggle('{tableId}');"">");
        sb.AppendLine($@"<span class=""arrow-up"" id=""{tableId}ud""></span>{MakeSafeHTMLString(typeToShowFirstLine)}</a>");
        sb.AppendLine("</td></tr>");

        try
        {
            string secondLine = "";
            var checker = new CircularReferenceChecker();

            if (type.IsAnonymous())
            {
                secondLine = "";
            }
            else if (type.IsRecord())
            {
                secondLine = "";
            }
            else if (type.IsClass)
            {
                // NOTA: Se l'oggetto ha reference circolari, il metodo .ToString() genera errore, pertanto si evita di chiamarlo
                if (!checker.HasCircularReference(obj)) { secondLine = obj.ToString() ?? ""; }
                if (string.IsNullOrEmpty(secondLine))   { secondLine = type.FullName ?? ""; }
            }

            if (secondLine.Length > 0)
            {
                // Seconda riga della tabella
                sb.AppendLine($@"<tr id=""sum1"">");
                sb.AppendLine($@"<td class=""summary"" colspan=""2"">{MakeSafeHTMLString(secondLine)}</td>");
                sb.AppendLine($@"</tr>");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        sb.AppendLine("</thead>");
        sb.AppendLine("<tbody>");

        // Scorre tutti i membri dell'oggetto e per ognuno genera una riga della tabella con nome e valore
        foreach (var member in members)
        {
            Debug.WriteLine($"Tipo: {member.MemberType}, Nome: {member.Name}");

            object? value;
            try
            {
                value = member is FieldInfo info ? info.GetValue(obj) : ((PropertyInfo)member).GetValue(obj, null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                continue;
            }

            var memberType = value?.GetType();

            sb.AppendLine("<tr>");
            sb.AppendLine($@"<th =""member"" title=""{MakeSafeHTMLString(memberType?.FullName ?? "")}"">{MakeSafeHTMLString(member.Name)}</th>");
            sb.AppendFormat("<td>{0}</td>", FormatValue(value ?? "", maxDepth - 1, visitedObjects));
            sb.AppendLine("</tr>");
        }

        sb.AppendLine("</tbody>");
        sb.AppendLine("</table>");
        sb.AppendLine("</div>");

        visitedObjects.Remove(obj);

        return sb.ToString();
    }

    private static string? FormatValue(object value, int maxDepth, HashSet<object> visitedObjects)
    {
        if (value == null) { return NullHtml; }            

        Type type = value.GetType();
        
        if (IsSimpleType(type))
        {
            return MakeSafeHTMLString(value.ToString() ?? "");
        }
        else if (value is IEnumerable enumerable && value is not string)
        {
            if (maxDepth > 0) { return EnumerableToHtmlTable(enumerable, maxDepth, visitedObjects); }
                
            return MakeSafeHTMLString(value.ToString() ?? "");
        }
        else
        {
            // Oggetto complesso: usa la ricorsione se maxDepth non è esaurito
            if (maxDepth > 0) { return ObjectToHtmlTable(value, maxDepth, visitedObjects); }
                
            return MakeSafeHTMLString(value.ToString() ?? "");
        }
    }

    // The primitive types are: Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, and Single.
    // https://learn.microsoft.com/en-us/dotnet/api/system.type.isprimitive?view=net-9.0&devlangs=csharp&f1url=%3FappId%3DDev17IDEF1%26l%3DEN-US%26k%3Dk(System.Type.IsPrimitive)%3Bk(DevLang-csharp)%26rd%3Dtrue
    private static bool IsSimpleType(Type type) => type.IsPrimitive || 
                                                   type.IsEnum ||
                                                   type == typeof(string) ||
                                                   type == typeof(decimal) ||
                                                   type == typeof(DateTime) ||
                                                   type == typeof(TimeSpan) ||
                                                   type == typeof(DateTimeOffset) ||
                                                   type == typeof(Guid) ||
                                                   type == typeof(decimal);

    private static bool IsRecord(this Type type)
    {
        // I record generano un metodo non pubblico "PrintMembers"
        var printMembersMethod = type.GetMethod("PrintMembers", BindingFlags.Instance | BindingFlags.NonPublic);
        return printMembersMethod != null;

        // Controlla se il tipo ha l'attributo 'IsExternalInit', tipico dei record
        //return type.GetCustomAttributes(typeof(System.Runtime.CompilerServices.IsExternalInit), false).Any();
    }

    private static string GetPluralSuffix(int count) => count > 1 ? "s" : "";

    public static List<MemberInfo> GetPublicMembers(Type type)
    {
        // Ottieni tutti i Fields e le Properties pubbliche dell'oggetto, ordinati per nome
        return [.. type.GetMembers(BindingFlags.Public | BindingFlags.Instance)
                       .Where(member => member.MemberType == MemberTypes.Field || member.MemberType == MemberTypes.Property)
                       .OrderBy(member => member.Name)];
    }

    public static string GetInterfaceTypeName<T>(T obj)
    {
        if (obj is null) { return ""; }

        Type type = obj.GetType();

        // Cerchiamo l'interfaccia IEnumerable<T>
        Type? ienumerableInterface = type.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

        if (ienumerableInterface != null)
        {
            // Otteniamo il tipo generico dell'IEnumerable
            Type genericArg = ienumerableInterface.GetGenericArguments()[0];
            return $"IEnumerable<{genericArg.Name}>";
        }

        // Altrimenti, usiamo il metodo originale
        return GetCleanTypeName(obj);
    }

    public static string GetCleanTypeName<T>(T obj)
    {
        if (obj is null) { return ""; }

        Type type = obj.GetType();
        if (!type.IsGenericType) { return type.Name; }

        string baseName = type.Name[..type.Name.IndexOf('`')];
        string[] genericArgs = [.. type.GetGenericArguments().Select(t => t.Name)];

        return $"{baseName}<{string.Join(", ", genericArgs)}>";
    }

    private static int GetEnumerableLength(IEnumerable enumerable)
    {
        if (enumerable == null) { return 0; }

        // Gestione degli array
        if (enumerable is Array array) { return array.Length; }

        // Gestione delle collezioni non generiche
        if (enumerable is ICollection collection) {  return collection.Count; }

        // Gestione delle collezioni generiche
        // Usa reflection per verificare se implementa ICollection<T> o IReadOnlyCollection<T>
        var type = enumerable.GetType();
        foreach (var interfaceType in type.GetInterfaces())
        {
            if (interfaceType.IsGenericType)
            {
                var genericTypeDef = interfaceType.GetGenericTypeDefinition();

                if (genericTypeDef == typeof(ICollection<>) ||
                    genericTypeDef == typeof(IReadOnlyCollection<>))
                {
                    // Usa reflection per chiamare la proprietà Count
                    var countProperty = interfaceType.GetProperty("Count");
                    if (countProperty != null)
                    {
                        return (int?)countProperty.GetValue(enumerable) ?? 0;
                    }
                }
            }
        }

        // Se tutto il resto fallisce, conta manualmente
        int count = 0;
        var enumerator = enumerable.GetEnumerator();
        while (enumerator.MoveNext()) { count++; }

        // Rilascia le risorse se l'enumeratore implementa IDisposable
        if (enumerator is IDisposable disposable) {  disposable.Dispose(); }

        return count;
    }

    private class ReferenceEqualityComparer : IEqualityComparer<object>
    {
        public new bool Equals(object? x, object? y) => ReferenceEquals(x, y);

        public int GetHashCode(object obj) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
    }

    public static string GetHtmlPageTemplate(string title = "", string lang = "en", bool darkMode = false)
    {
        return HtmlPageTemplate.Replace("%lang%" ,lang)
                               .Replace("%title%", !string.IsNullOrEmpty(title) ? "" : $"<title>{title}</title>")
                               .Replace("%css%", darkMode ? CssDarkMode : CssLightMode);
    }

    public static string MakeSafeHTMLString(string data) => JsonSerializer.Serialize(data).Trim('"');

    public static bool IsAnonymous(this Type type)
    {
        if (!string.IsNullOrEmpty(type.Namespace))
        {
            return false;
        }
        if (type.Name == "<>f__AnonymousType0")
        {
            return true;
        }
        if (!type.IsGenericType)
        {
            return false;
        }
        return IsAnonymous(type.Name);
    }

    public static bool IsAnonymous(string typeName) => typeName.Length > 5 && 
                                                       ((typeName[0] == '<' && typeName[1] == '>' && 
                                                       ((typeName[5] == 'A' && typeName[6] == 'n') || typeName.IndexOf("anon", StringComparison.OrdinalIgnoreCase) > -1)) || 
                                                        (typeName[0] == 'V' && typeName[1] == 'B' && typeName[2] == '$' && typeName[3] == 'A' && typeName[4] == 'n'));
}
