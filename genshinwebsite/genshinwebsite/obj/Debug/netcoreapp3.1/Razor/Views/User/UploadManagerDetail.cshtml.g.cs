#pragma checksum "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManagerDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2f08f0eb2cce778f071a2511344e21078b88fb68"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_UploadManagerDetail), @"mvc.1.0.view", @"/Views/User/UploadManagerDetail.cshtml")]
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
#line 1 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\_ViewImports.cshtml"
using genshinwebsite;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\_ViewImports.cshtml"
using genshinwebsite.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\_ViewImports.cshtml"
using genshinwebsite.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2f08f0eb2cce778f071a2511344e21078b88fb68", @"/Views/User/UploadManagerDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"037654766ad0b54edbd979f81eefdc4d8ab1c03f", @"/Views/_ViewImports.cshtml")]
    public class Views_User_UploadManagerDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MusicViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/MusicUpload.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManagerDetail.cshtml"
  
    ViewData["Title"] = "??????????????????";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2f08f0eb2cce778f071a2511344e21078b88fb684044", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            WriteLiteral("<script src=\"https://cdn.staticfile.org/jquery/3.5.1/jquery.min.js\"></script>\r\n<script>\r\n\r\n</script>\r\n");
            WriteLiteral("<h1>??????????????????</h1>\r\n<hr />\r\n");
#nullable restore
#line 14 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManagerDetail.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"alert alert-info\">\r\n    <span class=\"h2\">?????????");
#nullable restore
#line 16 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManagerDetail.cshtml"
                   Write(Model.MusicTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n    <span style=\"float:right;height:100%;padding:10px 0;\">??????: ");
#nullable restore
#line 17 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManagerDetail.cshtml"
                                                         Write(Model.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n</div>\r\n\r\n<table class=\"table table-bordered table-striped table-hover\">\r\n\r\n\r\n    <tbody>\r\n        <tr>\r\n            <th>???????????????</th>\r\n            <td colspan=2>  ");
#nullable restore
#line 26 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManagerDetail.cshtml"
                       Write(Model.Datetime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <th>????????????</th>\r\n            <td colspan=2>");
#nullable restore
#line 30 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManagerDetail.cshtml"
                     Write(Model.Download_num);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <th>????????????</th>\r\n            <td colspan=2>");
#nullable restore
#line 34 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManagerDetail.cshtml"
                     Write(Model.View_num);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
        </tr>


    </tbody>
</table>
<div class=""alert alert-warning"">
    <p>????????????????????????<b>??????????????????????????????</b></p>
</div>
<div id=""info_div"">

</div>

<div class=""row"">
    <div class=""col-3 offset-2 col-md-3 offset-md-2"">
        <span class=""label"">
            ????????????????????????????????????????????????????????????
        </span>
    </div>
    <div class=""col-4 offset-1 col-md-5 offset-md-1"">
        <input class=""btn btn-info"" style=""width:100%;"" id=""File"" data-val-required=""?????????????????????"" type=""file"" name=""File"" accept="".genmujson"" />
    </div>
</div>

<div class=""row mt-4"">
    <div class=""col-3 offset-2 col-md-3 offset-md-2"">
        <span class=""label"">
            ?????????
        </span>
    </div>
    <div class=""col-4 offset-1 col-md-5 offset-md-1"">
        <textarea style=""height:200px;width:100%"" class=""text-body"" type=""text"" id=""abs"" placeholder=""????????????????????????500????????????"">");
#nullable restore
#line 65 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManagerDetail.cshtml"
                                                                                                                  Write(Model.Abstract_content);

#line default
#line hidden
#nullable disable
            WriteLiteral("</textarea>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n<div class=\"row\" style=\"top:20px;\">\r\n    <div class=\"col-3 offset-6\">\r\n        <button class=\"btn btn-info\" style=\"width:100%\" type=\"button\"");
            BeginWriteAttribute("onclick", " onclick=\"", 2017, "\"", 2055, 3);
            WriteAttributeValue("", 2027, "update_btn_click(", 2027, 17, true);
#nullable restore
#line 73 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManagerDetail.cshtml"
WriteAttributeValue("", 2044, Model.Id, 2044, 9, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2053, ");", 2053, 2, true);
            EndWriteAttribute();
            WriteLiteral(@">????????????</button>
    </div>
</div>

<script src=""https://cdn.jsdelivr.net/npm/popper.js@1.14.7/dist/umd/popper.min.js"" integrity=""sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1"" crossorigin=""anonymous""></script>
<script src=""https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.min.js"" integrity=""sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM"" crossorigin=""anonymous""></script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MusicViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
