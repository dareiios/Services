#pragma checksum "C:\Users\user\source\repos\site1\site1\Views\Shared\MetatagsPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "14de9d3ce94945bfa9b6c271dfc602a5d02597b3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_MetatagsPartial), @"mvc.1.0.view", @"/Views/Shared/MetatagsPartial.cshtml")]
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
#line 4 "C:\Users\user\source\repos\site1\site1\Views\_ViewImports.cshtml"
using site1.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\user\source\repos\site1\site1\Views\_ViewImports.cshtml"
using site1.Domain.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\user\source\repos\site1\site1\Views\_ViewImports.cshtml"
using site1.Models.ViewComponents;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\user\source\repos\site1\site1\Views\Shared\MetatagsPartial.cshtml"
using site1.Service;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"14de9d3ce94945bfa9b6c271dfc602a5d02597b3", @"/Views/Shared/MetatagsPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c3eb3601c99721e6d68e81511835c9bc13407795", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_MetatagsPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<meta charset=\"utf-8\" />\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1, user-scalable=no\" />\r\n\r\n<!--viewBag-временное динамческое хранилище -->\r\n");
#nullable restore
#line 6 "C:\Users\user\source\repos\site1\site1\Views\Shared\MetatagsPartial.cshtml"
 if (ViewBag.Title != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <title>");
#nullable restore
#line 8 "C:\Users\user\source\repos\site1\site1\Views\Shared\MetatagsPartial.cshtml"
      Write(ViewBag.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</title>\r\n");
#nullable restore
#line 9 "C:\Users\user\source\repos\site1\site1\Views\Shared\MetatagsPartial.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <title>");
#nullable restore
#line 12 "C:\Users\user\source\repos\site1\site1\Views\Shared\MetatagsPartial.cshtml"
      Write(Config.CompanyName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</title>\r\n");
#nullable restore
#line 13 "C:\Users\user\source\repos\site1\site1\Views\Shared\MetatagsPartial.cshtml"

}

#line default
#line hidden
#nullable disable
            WriteLiteral("<!--для описания и ключевых слов тоже самое -->\r\n");
#nullable restore
#line 16 "C:\Users\user\source\repos\site1\site1\Views\Shared\MetatagsPartial.cshtml"
 if (ViewBag.Description != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <meta name=\"description\"");
            BeginWriteAttribute("content", " content=\"", 428, "\"", 458, 1);
#nullable restore
#line 18 "C:\Users\user\source\repos\site1\site1\Views\Shared\MetatagsPartial.cshtml"
WriteAttributeValue("", 438, ViewBag.Discription, 438, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n");
#nullable restore
#line 19 "C:\Users\user\source\repos\site1\site1\Views\Shared\MetatagsPartial.cshtml"
}

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "C:\Users\user\source\repos\site1\site1\Views\Shared\MetatagsPartial.cshtml"
 if (ViewBag.Keywords != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <meta name=\"keywords\"");
            BeginWriteAttribute("content", " content=\"", 527, "\"", 554, 1);
#nullable restore
#line 22 "C:\Users\user\source\repos\site1\site1\Views\Shared\MetatagsPartial.cshtml"
WriteAttributeValue("", 537, ViewBag.Keywords, 537, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n");
#nullable restore
#line 23 "C:\Users\user\source\repos\site1\site1\Views\Shared\MetatagsPartial.cshtml"

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
