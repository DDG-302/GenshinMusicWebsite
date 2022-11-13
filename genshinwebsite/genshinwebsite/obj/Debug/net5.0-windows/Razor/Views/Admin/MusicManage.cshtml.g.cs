#pragma checksum "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3003f7491cfde5cd965203330daa04d1327675d0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_MusicManage), @"mvc.1.0.view", @"/Views/Admin/MusicManage.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3003f7491cfde5cd965203330daa04d1327675d0", @"/Views/Admin/MusicManage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"037654766ad0b54edbd979f81eefdc4d8ab1c03f", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_MusicManage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<MusicViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/musicsheet-display.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/UploadManager.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/MusicFilter.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-thumbnail"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/f5dd25c2527841ec883df8f3ceadc983.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_PageBar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 7 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
  
    ViewData["Title"] = "乐谱管理";
    int filter_idx = 1;
    if (ViewData["filter_idx"] != null)
    {
        filter_idx = int.Parse(ViewData["filter_idx"].ToString());
    }
    

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 17 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
 if (User.IsInRole("Admin") || User.IsInRole("God"))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "3003f7491cfde5cd965203330daa04d1327675d06751", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            WriteLiteral("    <script src=\"https://cdn.staticfile.org/jquery/3.5.1/jquery.min.js\"></script>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3003f7491cfde5cd965203330daa04d1327675d07987", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3003f7491cfde5cd965203330daa04d1327675d09030", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    <script>\r\n    selected_filter = ");
#nullable restore
#line 25 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
                 Write(filter_idx);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </script>\r\n");
            WriteLiteral("    <div class=\"row\" style=\"margin-bottom:10px\">\r\n        <div class=\"col-md-3\"></div>\r\n        <div class=\"col-md-4\">\r\n");
#nullable restore
#line 31 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
             if (ViewData["music_title"] != null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input id=\"search_input_box\" type=\"text\"");
            BeginWriteAttribute("value", " value=\"", 1031, "\"", 1074, 1);
#nullable restore
#line 33 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
WriteAttributeValue("", 1039, ViewData["music_title"].ToString(), 1039, 35, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"width:100%\" />\r\n");
#nullable restore
#line 34 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input id=\"search_input_box\" type=\"text\" style=\"width:100%\" />\r\n");
#nullable restore
#line 38 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n            <button style=\"width:100%\" onclick=\"search_click()\">搜索</button>\r\n        </div>\r\n        <div class=\"col-md-3\"></div>\r\n    </div>\r\n    <ul class=\"nav nav-pills\">\r\n");
#nullable restore
#line 47 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
         if (filter_idx == 1)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li><a id=\"filter_1\" class=\"nav-link active\" href=\"javascript:set_filter(1);\">最新发布顺序</a></li>\r\n");
#nullable restore
#line 50 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"

        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li><a id=\"filter_1\" class=\"nav-link\" href=\"javascript:set_filter(1);\">最新发布顺序</a></li>\r\n");
#nullable restore
#line 55 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 57 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
         if (filter_idx == 2)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li><a id=\"filter_2\" class=\"nav-link active\" href=\"javascript:set_filter(2);\">下载顺序</a></li>\r\n");
#nullable restore
#line 60 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"

        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li><a id=\"filter_2\" class=\"nav-link\" href=\"javascript:set_filter(2);\">下载顺序</a></li>\r\n");
#nullable restore
#line 65 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 67 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
         if (filter_idx == 3)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li><a id=\"filter_3\" class=\"nav-link active\" href=\"javascript:set_filter(3);\">浏览顺序</a></li>\r\n");
#nullable restore
#line 70 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"

        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li><a id=\"filter_3\" class=\"nav-link\" href=\"javascript:set_filter(3);\">浏览顺序</a></li>\r\n");
#nullable restore
#line 75 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 78 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
         if (filter_idx == 4)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li><a id=\"filter_4\" class=\"nav-link active\" href=\"javascript:set_filter(4);\">标题</a></li>\r\n");
#nullable restore
#line 81 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"

        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li><a id=\"filter_4\" class=\"nav-link\" href=\"javascript:set_filter(4);\">标题</a></li>\r\n");
#nullable restore
#line 86 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </ul>\r\n");
#nullable restore
#line 90 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
     for (int i = 0; i < Model.Count;)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div style=\"min-width:600px\" class=\"row\">\r\n");
#nullable restore
#line 93 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
             for (int j = 0; i < Model.Count && j < 3; j++, i++)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col-md-4 col-sm-12\" style=\"margin-bottom:10px\">\r\n                    <div class=\"img-thumbnail\">\r\n\r\n                        <div class=\"h3\">");
#nullable restore
#line 98 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
                                   Write(Model[i].MusicTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                        <div class=\"h5\">上传者：");
#nullable restore
#line 99 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
                                       Write(Model[i].Uploader);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                        <p>发布日期：");
#nullable restore
#line 100 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
                           Write(Model[i].Datetime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        <p style=\"height:100px\">");
#nullable restore
#line 101 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
                                           Write(Model[i].Abstract_content);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "3003f7491cfde5cd965203330daa04d1327675d018368", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        <p>下载：");
#nullable restore
#line 103 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
                         Write(Model[i].Download_num);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        <p>浏览：");
#nullable restore
#line 104 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
                         Write(Model[i].View_num);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        <button class=\"btn btn-light\" onclick=\"alert(\'功能未实现\')\">\r\n                            修改\r\n                        </button>\r\n                        <button class=\"btn btn-dark\"");
            BeginWriteAttribute("onclick", " onclick=\"", 3659, "\"", 3695, 3);
            WriteAttributeValue("", 3669, "delete_music(", 3669, 13, true);
#nullable restore
#line 108 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
WriteAttributeValue("", 3682, Model[i].Id, 3682, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3694, ")", 3694, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                            删除\r\n                        </button>\r\n                    </div>\r\n\r\n                </div>\r\n");
#nullable restore
#line 114 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"

            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n");
#nullable restore
#line 117 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "3003f7491cfde5cd965203330daa04d1327675d021515", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 120 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Admin\MusicManage.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<MusicViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
