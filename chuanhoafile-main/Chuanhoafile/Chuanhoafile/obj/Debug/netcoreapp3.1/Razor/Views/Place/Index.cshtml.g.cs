#pragma checksum "C:\Users\hieuh\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\Place\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "040742e576cf8614e6ff8f57fd365106eb177052"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Place_Index), @"mvc.1.0.view", @"/Views/Place/Index.cshtml")]
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
#line 1 "C:\Users\hieuh\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\_ViewImports.cshtml"
using Chuanhoafile;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\hieuh\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\_ViewImports.cshtml"
using Chuanhoafile.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"040742e576cf8614e6ff8f57fd365106eb177052", @"/Views/Place/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8b31cc4d49f1fa10d5b664e520453155b274aad8", @"/Views/_ViewImports.cshtml")]
    public class Views_Place_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Chuanhoafile.Models.places>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\hieuh\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\Place\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""container-fluid"">

    <div class=""card shadow mt-3"">
        <div class=""row"">
            <div class=""col-12"">
                <ul class=""breadcrumb bg-white font-weight-bold"" style=""margin: unset !important"">
                    <li class=""breadcrumb-item"">
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "040742e576cf8614e6ff8f57fd365106eb1770524693", async() => {
                WriteLiteral("Trang chủ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    </li>
                    <li class=""breadcrumb-item active"">
                        <a>Quản lý địa điểm khu vực</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>

    <hr />
    <div class=""container-fluid"">
        <div class=""row"">
            <button id=""btnAdd"" class="" mr-md-2 btn btn-primary btn-icon-split shadow w-auto ml-auto"" data-toggle=""ajax-modal"" data-target=""#order-model"" data-url=""");
#nullable restore
#line 26 "C:\Users\hieuh\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\Place\Index.cshtml"
                                                                                                                                                               Write(Url.Action("CreateByFile"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""">
                <span class=""icon text-white-50"">
                    <i class=""fas fa-plus-circle""></i>
                </span>
                <span class=""text"">Thêm nhiều</span>
            </button>
            <button id=""btnAdd"" class="" mr-md-0 btn btn-primary btn-icon-split shadow w-auto"" data-toggle=""ajax-modal"" data-target=""#order-model"" data-url=""");
#nullable restore
#line 32 "C:\Users\hieuh\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\Place\Index.cshtml"
                                                                                                                                                       Write(Url.Action("Create"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""">
                <span class=""icon text-white-50"">
                    <i class=""fas fa-plus-circle""></i>
                </span>
                <span class=""text"">Thêm mới</span>
            </button>

        </div>
    </div>
    <hr />
    <div id=""modal-placeholder""></div>
</div>
<div class=""card shadow m-4"">
    <div class=""card-body"">
        <div class=""table-responsive"">

            ");
#nullable restore
#line 48 "C:\Users\hieuh\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\Place\Index.cshtml"
       Write(await Html.PartialAsync("_DataTablePartial.cshtml", Model));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n        </div>\r\n    </div>\r\n</div>\r\n<hr />\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 56 "C:\Users\hieuh\Documents\GitHub\chuanhoadulieu\chuanhoafile-main\Chuanhoafile\Chuanhoafile\Views\Place\Index.cshtml"
      await Html.RenderPartialAsync("_ModalScriptInit", "places");

#line default
#line hidden
#nullable disable
            }
            );
            WriteLiteral("\r\n\r\n<script>\r\n\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Chuanhoafile.Models.places>> Html { get; private set; }
    }
}
#pragma warning restore 1591