#pragma checksum "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6cfe01954422f621055f94ac2d1710d823dddb91"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Profile_Index), @"mvc.1.0.view", @"/Views/Profile/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6cfe01954422f621055f94ac2d1710d823dddb91", @"/Views/Profile/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a1d9fce3dfd861de0f45d4b1a671e8fff8818d09", @"/Views/_ViewImports.cshtml")]
    public class Views_Profile_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Lab3.Models.User>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
  
    ViewData["Title"] = "Profile";

    String userType;
    if (Model is Administrator)
    {
        userType = "Administrator";
    }
    else
    {
        userType = "Student";
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>User Profile</h1>\r\n\r\n<div>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            <a>User Type</a>\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            <a>");
#nullable restore
#line 26 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
          Write(userType);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n        </dd><dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 28 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.username));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 31 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
       Write(Html.DisplayFor(model => model.username));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 34 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.emailaccount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 37 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
       Write(Html.DisplayFor(model => model.emailaccount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 40 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.phonenumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 43 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
       Write(Html.DisplayFor(model => model.phonenumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 46 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.dateofbirth));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 49 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
       Write(Html.DisplayFor(model => model.dateofbirth));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 52 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.address));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 55 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
       Write(Html.DisplayFor(model => model.address));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n");
#nullable restore
#line 57 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
         if (userType == "Administrator")
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 60 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
           Write(Html.DisplayNameFor(model => ((Administrator)model).departmentname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 63 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
           Write(Html.DisplayFor(model => ((Administrator)model).departmentname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n");
#nullable restore
#line 65 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <dt class=\"col-sm-2\">\r\n                ");
#nullable restore
#line 69 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
           Write(Html.DisplayNameFor(model => ((Student)model).programmename));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dt>\r\n            <dd class=\"col-sm-10\">\r\n                ");
#nullable restore
#line 72 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
           Write(Html.DisplayFor(model => ((Student)model).programmename));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </dd>\r\n");
#nullable restore
#line 74 "C:\Users\maxwi\OneDrive\Dokument\KTH\Kurser\DBas - DD1368\Lab\Lab 3\DbasLab3\Views\Profile\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </dl>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Lab3.Models.User> Html { get; private set; }
    }
}
#pragma warning restore 1591
