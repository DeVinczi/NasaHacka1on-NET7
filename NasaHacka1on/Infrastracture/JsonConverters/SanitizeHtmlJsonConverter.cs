using AngleSharp.Dom;
using AngleSharp.Text;
using Ganss.XSS;
using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NasaHacka1on.Models.JsonConverters;

public sealed class SanitizeHtmlJsonConverter : JsonConverter<string>
{
    private readonly HtmlSanitizer _htmlSanitizer;

    public SanitizeHtmlJsonConverter()
    {
        _htmlSanitizer =
            new HtmlSanitizer(
            new HtmlSanitizerOptions
            {
                AllowedSchemes = HtmlSanitizerDefaults.AllowedSchemes,
                AllowedTags = HtmlSanitizerDefaults.AllowedTags,
                AllowedAttributes = HtmlSanitizerDefaults.AllowedAttributes,
            })
            {
                OutputFormatter = new LogHtmlFormatter()
            };
    }

    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return _htmlSanitizer.Sanitize(reader.GetString() ?? string.Empty);
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        var sanitized = _htmlSanitizer.Sanitize(value);

        writer.WriteStringValue(sanitized);
    }
}

internal class LogHtmlFormatter : HtmlFormatter
{
    public static readonly LogHtmlFormatter logHtmlFormatter = new();

    public override string Text(ICharacterData text)
    {
        var stringBuilder = StringBuilderPool.Obtain();

        foreach (var symbol in text.Data)
        {
            _ = symbol switch
            {
                Symbols.Ampersand => stringBuilder.Append(""),
                Symbols.NoBreakSpace => stringBuilder.Append("&nbsp;"),
                Symbols.GreaterThan => stringBuilder.Append(""),
                Symbols.LessThan => stringBuilder.Append(""),
                _ => stringBuilder.Append(symbol)
            };
        }

        return stringBuilder.ToPool();
    }

    protected override string Attribute(IAttr attr)
    {
        var namespaceUri = attr.NamespaceUri;
        var localName = attr.LocalName;
        var value = attr.Value;

        var stringBuilder = StringBuilderPool.Obtain();

        if (string.IsNullOrEmpty(namespaceUri))
        {
            stringBuilder.Append(localName);
        }
        else if (namespaceUri == NamespaceNames.XmlUri)
        {
            stringBuilder.Append(NamespaceNames.XmlPrefix).Append(':').Append(localName);
        }
        else if (namespaceUri == NamespaceNames.XLinkUri)
        {
            stringBuilder.Append(NamespaceNames.XLinkPrefix).Append(':').Append(localName);
        }
        else if (namespaceUri == NamespaceNames.XmlNsUri)
        {
            stringBuilder.Append(XmlNamespaceLocalName(localName));
        }
        else
        {
            stringBuilder.Append(attr.Name);
        }

        stringBuilder.Append('=').Append('"');

        foreach (var symbol in value)
        {
            _ = symbol switch
            {
                '&' => stringBuilder.Append("&amp;"),
                '\u00a0' => stringBuilder.Append("&nbps;"),
                '"' => stringBuilder.Append("&quot;"),
                '<' => stringBuilder.Append("&lt;"),
                '>' => stringBuilder.Append("&gt;"),
                _ => stringBuilder.Append(symbol)
            };
        }

        return stringBuilder.Append('"').ToPool();

    }
}

internal static class HtmlSanitizerDefaults
{
    public static ISet<string> AllowedSchemes { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "data"
    }.ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);

    public static ISet<string> AllowedTags { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "a", "abbr", "acronym", "address", "area", "b",
        "big", "blockquote", "br", "button", "caption", "center", "cite",
        "code", "col", "colgroup", "dd", "del", "dfn", "dir", "div", "dl", "dt",
        "em", "fieldset", "font", "form", "h1", "h2", "h3", "h4", "h5", "h6",
        "hr", "i", "img", "input", "ins", "kbd", "label", "legend", "li", "map",
        "menu", "ol", "optgroup", "option", "p", "pre", "q", "s", "samp",
        "select", "small", "span", "strike", "strong", "sub", "sup", "table",
        "tbody", "td", "textarea", "tfoot", "th", "thead", "tr", "tt", "u",
        "ul", "var",
        // HTML5
        // Sections
        "section", "nav", "article", "aside", "header", "footer", "main",
        // Grouping content
        "figure", "figcaption",
        // Text-level semantics
        "data", "time", "mark", "ruby", "rt", "rp", "bdi", "wbr",
        // Forms
        "datalist", "keygen", "output", "progress", "meter",
        // Interactive elements
        "details", "summary", "menuitem",
        // document elements
        "html", "head", "body"
    }.ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);

    public static ISet<string> AllowedAttributes { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        // https://developer.mozilla.org/en-US/docs/Web/HTML/Attributes
        "abbr", "accept", "accept-charset", "accesskey",
        "action", "align", "alt", "axis", "bgcolor", "border", "cellpadding",
        "cellspacing", "char", "charoff", "charset", "checked", "cite", "class",
        "clear", "cols", "colspan", "color", "compact", "coords", "datetime",
        "dir", "disabled", "enctype", "for", "frame", "headers", "height",
        "href", "hreflang", "hspace", "id", "ismap", "label", "lang",
        "longdesc", "maxlength", "media", "method", "multiple", "name",
        "nohref", "noshade", "nowrap", "prompt", "readonly", "rel", "rev",
        "rows", "rowspan", "rules", "scope", "selected", "shape", "size",
        "span", "src", "start", "summary", "tabindex", "target", "title",
        "type", "usemap", "valign", "value", "vspace", "width",
        // HTML5
        "high", // <meter>
        "keytype", // <keygen>
        "list", // <input>
        "low", // <meter>
        "max", // <input>, <meter>, <progress>
        "min", // <input>, <meter>
        "novalidate", // <form>
        "open", // <details>
        "optimum", // <meter>
        "pattern", // <input>
        "placeholder", // <input>, <textarea>
        "pubdate", // <time>
        "radiogroup", // <menuitem>
        "required", // <input>, <select>, <textarea>
        "reversed", // <ol>
        "spellcheck", // Global attribute
        "step", // <input>
        "wrap", // <textarea>
        "challenge", // <keygen>
        "contenteditable", // Global attribute
        "draggable", // Global attribute
        "dropzone", // Global attribute
        "autocomplete", // <form>, <input>
        "autosave", // <input>
    }.ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);
}
