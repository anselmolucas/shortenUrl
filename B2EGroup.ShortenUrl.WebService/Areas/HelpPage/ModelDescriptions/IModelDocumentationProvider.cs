using System;
using System.Reflection;

namespace B2EGroup.ShortenUrl.WebService.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}