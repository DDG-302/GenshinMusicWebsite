#pragma checksum "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ceed14108a00213c2297ca8d3ce55e6106f6cdf8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Pages_Detail), @"mvc.1.0.view", @"/Views/Pages/Detail.cshtml")]
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
#line 1 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ceed14108a00213c2297ca8d3ce55e6106f6cdf8", @"/Views/Pages/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"037654766ad0b54edbd979f81eefdc4d8ab1c03f", @"/Views/_ViewImports.cshtml")]
    public class Views_Pages_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MusicDetailViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/comment-style.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.SingleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.SingleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/Comment.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "download", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("user_icon"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/login-user.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:100%;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/User"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
  
    ViewData["Title"] = Model.MusicTitle;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ceed14108a00213c2297ca8d3ce55e6106f6cdf87491", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n<script src=\"https://cdn.staticfile.org/jquery/3.5.1/jquery.min.js\"></script>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ceed14108a00213c2297ca8d3ce55e6106f6cdf88771", async() => {
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
            WriteLiteral("\r\n");
#nullable restore
#line 13 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
 if (SignInManager.IsSignedIn(User))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>\r\n        is_login = true;\r\n    </script>\r\n");
#nullable restore
#line 18 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("<script>\r\n    muid = ");
#nullable restore
#line 20 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
      Write(Model.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n</script>\r\n<style>\r\n    .abs {\r\n        white-space: pre;\r\n    }\r\n</style>\r\n<div class=\"alert alert-info\">\r\n    <span class=\"h2\">曲名：");
#nullable restore
#line 28 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
                   Write(Model.MusicTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n    <span style=\"float:right;height:100%;padding:10px 0;\">编号: ");
#nullable restore
#line 29 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
                                                         Write(Model.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n</div>\r\n");
            WriteLiteral("\r\n<table class=\"table table-bordered table-striped table-hover\">\r\n\r\n\r\n    <tbody>\r\n        <tr>\r\n            <th>上传用户：</th>\r\n            <td colspan=2>\r\n                <a href=\"#\">\r\n                    ");
#nullable restore
#line 52 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
               Write(Model.Uploader);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>上传日期：</th>\r\n            <td colspan=2>  ");
#nullable restore
#line 58 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
                       Write(Model.Datetime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <th>下载量：</th>\r\n            <td colspan=2>");
#nullable restore
#line 62 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
                     Write(Model.Download_num);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <th>浏览量：</th>\r\n            <td colspan=2>");
#nullable restore
#line 66 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
                     Write(Model.View_num);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <th>简介：</th>\r\n            <td colspan=2><span class=\"abs\">");
#nullable restore
#line 70 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
                                       Write(Model.Abstract_content);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> </td>\r\n        </tr>\r\n        <tr>\r\n            <th>下载：</th>\r\n            <td>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ceed14108a00213c2297ca8d3ce55e6106f6cdf813614", async() => {
                WriteLiteral("\r\n                    工程文件下载(.genmujson)\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-muid", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 75 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
                                                                  WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["muid"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-muid", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["muid"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                <a class=\"btn btn-dark\" href=\"#\">\r\n                    键盘谱下载(.zip)[功能未实现]\r\n                </a>\r\n            </td>\r\n        </tr>\r\n    </tbody>\r\n</table>\r\n\r\n<hr />\r\n\r\n");
#nullable restore
#line 90 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
 if (SignInManager.IsSignedIn(User))
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""row"">
        <div class=""col-md-8 offset-md-2 offset-sm-3 col-sm-6"">
            <textarea style=""height:100px;width:100%"" id=""comment_input_box"" placeholder=""在这里可以编写评论（注意，每个用户在一个乐谱下只能发送一条评论，如果已经发过评论了，再次评论会覆盖原有评论哦）""></textarea>
        </div>

    </div>
");
#nullable restore
#line 99 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""row"" style=""margin-top:20px;margin-bottom:20px"">
        <div class=""col-md-6 offset-md-3 offset-sm-2 col-sm-8"">
            <button style=""width:100%;"" class=""btn btn-dark"" type=""button"" onclick=""comment_submit_click()"">提交</button>
        </div>
    </div>
    <h2>我的评论</h2>
    <div class=""row"">
        <div class=""block offset-md-2 col-md-8 offset-sm-2 col-sm-8 offset-xs-2 col-xs-8"" style=""background-color:#eeedab"">
            <div>
                <div class=""card_head"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ceed14108a00213c2297ca8d3ce55e6106f6cdf817571", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    <p id=\"my_id\">");
#nullable restore
#line 112 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
                             Write(User.Identity.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" </p>

                </div>
                <div>
                    <p id=""my_upload_date"" class=""date_time_p"">发布时间： 未知</p>
                    <p id=""my_update_date"" class=""date_time_p"">最后更新时间： 未知</p>
                    <hr />
                    <p id=""my_comment"" class=""comment_content"">正在查询我的评论...</p>
                </div>
                <div>

                </div>
                <button id=""delete_btn"" class=""btn btn-danger"" onclick=""delete_my_comment();"" disabled=""disabled"">删除评论</button>
            </div>
        </div>

    </div>
    <hr />
");
#nullable restore
#line 130 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""row"">
        <div class=""col-md-8 offset-md-2 offset-sm-3 col-sm-6"">
            <textarea style=""height:100px;width:100%"" id=""comment_input_box"" placeholder=""评论前请先登录"" disabled=""disabled""></textarea>
        </div>
    </div>
    <div class=""row"" style=""margin-top:20px;margin-bottom:20px"">
        <div class=""offset-md-3 col-md-6 offset-sm-3 col-sm-6"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ceed14108a00213c2297ca8d3ce55e6106f6cdf820249", async() => {
                WriteLiteral("去登录");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 143 "D:\visual studio save\genshinmusic_v2\genshinwebsite\genshinwebsite\genshinwebsite\Views\Pages\Detail.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h2>评论区</h2>
<div>
    <div id=""main_div"">

    </div>
    <div id=""guard""></div>
    <div class=""row"">
        <a class=""col-12"" style=""text-align: center;font-style:italic;""  href=""javascript:get_comment_list(muid);"">如果没有加载评论请尝试点击此处</a>
    </div>
   
</div>


");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<UserModel> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<UserModel> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MusicDetailViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
