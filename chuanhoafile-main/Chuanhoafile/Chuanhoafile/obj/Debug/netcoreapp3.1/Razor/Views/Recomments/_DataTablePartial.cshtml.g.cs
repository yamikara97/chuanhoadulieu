#pragma checksum "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\Recomments\_DataTablePartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3ed028f04415db95a0a7e41fa30ce7ec3bb96102"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Recomments__DataTablePartial), @"mvc.1.0.view", @"/Views/Recomments/_DataTablePartial.cshtml")]
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
#line 1 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\_ViewImports.cshtml"
using Chuanhoafile;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\_ViewImports.cshtml"
using Chuanhoafile.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ed028f04415db95a0a7e41fa30ce7ec3bb96102", @"/Views/Recomments/_DataTablePartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"26a89a5395db20d467cb8c849f5ec4c91cad61b3", @"/Views/_ViewImports.cshtml")]
    public class Views_Recomments__DataTablePartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Chuanhoafile.Models.Recomment>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral(@"
<table id=""places"" class=""table nowrap table-bordered table-striped mt-5 table-data"">
    <thead>
        <tr>
            <th>Ngày</th>
            <th>Người gửi</th>
            <th>Nội dung</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 14 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\Recomments\_DataTablePartial.cshtml"
         foreach (var item in Model)
        {
            if (item != null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 19 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\Recomments\_DataTablePartial.cshtml"
                   Write(item.DateUpdate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 20 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\Recomments\_DataTablePartial.cshtml"
                   Write(item.Author);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 21 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\Recomments\_DataTablePartial.cshtml"
                   Write(item.Content);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"text-center\">\r\n                        <button class=\"btn btn-primary action-btn border-0\" data-toggle=\"ajax-modal\" data-target=\"#order-model\" data-url=\"");
#nullable restore
#line 23 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\Recomments\_DataTablePartial.cshtml"
                                                                                                                                      Write(Url.Action("Create") + "/" + item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                            <span>Xem</span>\r\n                        </button>\r\n                        <button class=\"btn btn-danger action-btn border-0\" data-toggle=\"ajax-modal\" data-target=\"#delete-model\" data-url=\"");
#nullable restore
#line 26 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\Recomments\_DataTablePartial.cshtml"
                                                                                                                                      Write(Url.Action("Delete") + "/" + item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                            <span>Xóa</span>\r\n                        </button>\r\n                    </td>\r\n\r\n                </tr>\r\n");
#nullable restore
#line 32 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\Recomments\_DataTablePartial.cshtml"
            }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </tbody>\r\n</table>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Chuanhoafile.Models.Recomment>> Html { get; private set; }
    }
}
#pragma warning restore 1591
