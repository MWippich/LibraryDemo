#pragma checksum "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Books\_AuthorForm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dbba17750bd4af9de58d40d9adbf8da994a0c3be"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Books__AuthorForm), @"mvc.1.0.view", @"/Views/Books/_AuthorForm.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\_ViewImports.cshtml"
using Lab3;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\_ViewImports.cshtml"
using Lab3.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dbba17750bd4af9de58d40d9adbf8da994a0c3be", @"/Views/Books/_AuthorForm.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a1d9fce3dfd861de0f45d4b1a671e8fff8818d09", @"/Views/_ViewImports.cshtml")]
    public class Views_Books__AuthorForm : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Lab3.Models.Author>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div id=\"AuthorRows\">\r\n");
#nullable restore
#line 4 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Books\_AuthorForm.cshtml"
     for (int i = 0; i < Model.Count; i++)
    {
        ViewData["ID"] = i;
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Books\_AuthorForm.cshtml"
   Write(await Html.PartialAsync("_AuthorRow", Model[i]));

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Books\_AuthorForm.cshtml"
                                                        
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n<input type=\"button\" value=\"Add Author\" onclick=\"addRow();\" class=\"btn btn-outline-success\">\r\n\r\n<script>\r\n\r\n    var numAuthors = ");
#nullable restore
#line 14 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Books\_AuthorForm.cshtml"
                Write(Model.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n\r\n    function addRow() {\r\n        $.ajax({\r\n            url: \'");
#nullable restore
#line 18 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Books\_AuthorForm.cshtml"
             Write(Url.Action("_AddAuthorRow", "Books"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/'+ numAuthors,
            type: ""GET"",
            success: function (html) {
                $(""#AuthorRows"").append(html)
            }
        });
        numAuthors++;

    };

    function removeRow(index) {
        $('#' + index).remove();
    }
</script>
");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Lab3.Models.Author>> Html { get; private set; }
    }
}
#pragma warning restore 1591
