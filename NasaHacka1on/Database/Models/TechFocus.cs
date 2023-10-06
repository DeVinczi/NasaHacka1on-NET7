namespace NasaHacka1on.Database.Models;

public enum TechFocusEnum
{
    Angular,
    Bash,
    C,
    CPlusPlus,
    CSharp,
    CouchDb,
    Dart,
    Go,
    GraphQL,
    Haskell,
    HtmlxCss,
    Java,
    JQuery,
    JavaScript,
    Lua,
    Markdown,
    MongoDB,
    MySQL,
    NextJS,
    NodeJS,
    Php,
    Python,
    React,
    Ruby,
    Rust,
    Shell,
    SqlLite,
    TypeScript
}

public class TechFocusModel : IIdentifiable
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}