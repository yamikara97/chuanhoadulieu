#pragma checksum "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\placeCases\_DataTablePartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "64f91ac3333b062f5ab0b087bc593fde8a92d646"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_placeCases__DataTablePartial), @"mvc.1.0.view", @"/Views/placeCases/_DataTablePartial.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"64f91ac3333b062f5ab0b087bc593fde8a92d646", @"/Views/placeCases/_DataTablePartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"26a89a5395db20d467cb8c849f5ec4c91cad61b3", @"/Views/_ViewImports.cshtml")]
    public class Views_placeCases__DataTablePartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Chuanhoafile.Models.placeCase>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral(@"
<table id=""placescase"" class=""table nowrap table-bordered table-striped mt-5 table-data"">
    <thead>
        <tr>
            <th>Giá trị sai</th>
            <th>Mã địa điểm đúng</th>
            <th>Mã địa điểm cấp trên</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 14 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\placeCases\_DataTablePartial.cshtml"
         foreach (var item in Model)
        {
            if (item != null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 19 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\placeCases\_DataTablePartial.cshtml"
           Write(item.nameCase);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 20 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\placeCases\_DataTablePartial.cshtml"
           Write(item.placeCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 21 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\placeCases\_DataTablePartial.cshtml"
           Write(item.placeFatherCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td class=\"text-center\">\r\n                <button class=\"btn btn-primary action-btn border-0\" data-toggle=\"ajax-modal\" data-target=\"#order-model\" data-url=\"");
#nullable restore
#line 23 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\placeCases\_DataTablePartial.cshtml"
                                                                                                                              Write(Url.Action("Create") + "/" + item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                    <span>Sửa</span>\r\n                </button>\r\n                <button class=\"btn btn-danger action-btn border-0\" data-toggle=\"ajax-modal\" data-target=\"#delete-model\" data-url=\"");
#nullable restore
#line 26 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\placeCases\_DataTablePartial.cshtml"
                                                                                                                              Write(Url.Action("Delete") + "/" + item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                    <span>Xóa</span>\r\n                </button>\r\n            </td>\r\n\r\n        </tr>\r\n");
#nullable restore
#line 32 "C:\Users\Cheem\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\placeCases\_DataTablePartial.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Chuanhoafile.Models.placeCase>> Html { get; private set; }
    }
}
#pragma warning restore 1591
