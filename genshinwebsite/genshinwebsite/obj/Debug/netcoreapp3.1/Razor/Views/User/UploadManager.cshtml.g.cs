#pragma checksum "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "74995645ee94f9bfc0b4afa367c5c2c84d40796b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_UploadManager), @"mvc.1.0.view", @"/Views/User/UploadManager.cshtml")]
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
#nullable restore
#line 1 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"74995645ee94f9bfc0b4afa367c5c2c84d40796b", @"/Views/User/UploadManager.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"037654766ad0b54edbd979f81eefdc4d8ab1c03f", @"/Views/_ViewImports.cshtml")]
    public class Views_User_UploadManager : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<MusicViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/musicsheet-display.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/UploadManager.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-thumbnail"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/f5dd25c2527841ec883df8f3ceadc983.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-light"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("target", new global::Microsoft.AspNetCore.Html.HtmlString("_blank"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_PageBar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 5 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"
  
    if (SignInManager.IsSignedIn(User))
    {
        ViewData["Title"] = @User.Identity.Name + "???????????????";
    }
    int filter_idx = 1;
    if (ViewData["filter_idx"] != null)
    {
        filter_idx = int.Parse(ViewData["filter_idx"].ToString());
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 17 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"
Write(User.Identity.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ?????????</h1>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "74995645ee94f9bfc0b4afa367c5c2c84d40796b7359", async() => {
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
            WriteLiteral("<script src=\"https://cdn.staticfile.org/jquery/3.5.1/jquery.min.js\"></script>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "74995645ee94f9bfc0b4afa367c5c2c84d40796b8587", async() => {
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
            WriteLiteral("\r\n");
#nullable restore
#line 22 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"
 for (int i = 0; i < Model.Count;)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div style=\"min-width:600px\" class=\"row\">\r\n");
#nullable restore
#line 25 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"
         for (int j = 0; i < Model.Count && j < 3; j++, i++)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-md-4 col-sm-12\" style=\"margin-bottom:10px\">\r\n                <div class=\"img-thumbnail\">\r\n\r\n                    <div class=\"h3\">");
#nullable restore
#line 30 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"
                               Write(Model[i].MusicTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                    <div class=\"h5\">????????????");
#nullable restore
#line 31 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"
                                   Write(Model[i].Uploader);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                    <p>???????????????");
#nullable restore
#line 32 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"
                       Write(Model[i].Datetime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <p class=\"abstract_normal\" style=\"height:100px\">");
#nullable restore
#line 33 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"
                                                               Write(Model[i].Abstract_content);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "74995645ee94f9bfc0b4afa367c5c2c84d40796b11791", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    <p>?????????");
#nullable restore
#line 35 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"
                     Write(Model[i].Download_num);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <p>?????????");
#nullable restore
#line 36 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"
                     Write(Model[i].View_num);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "74995645ee94f9bfc0b4afa367c5c2c84d40796b13547", async() => {
                WriteLiteral("\r\n                        ??????\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1523, "~/User/UploadManager/", 1523, 21, true);
#nullable restore
#line 37 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"
AddHtmlAttributeValue("", 1544, Model[i].Id, 1544, 12, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    <button class=\"btn btn-dark\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1678, "\"", 1717, 3);
            WriteAttributeValue("", 1688, "delete_my_music(", 1688, 16, true);
#nullable restore
#line 40 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"
WriteAttributeValue("", 1704, Model[i].Id, 1704, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1716, ")", 1716, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        ??????\r\n                    </button>\r\n                </div>\r\n\r\n            </div>\r\n");
#nullable restore
#line 46 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"

        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n");
#nullable restore
#line 49 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\User\UploadManager.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "74995645ee94f9bfc0b4afa367c5c2c84d40796b16542", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<UserModel> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<UserModel> UserManager { get; private set; }
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
