using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;

namespace PDFiumSharp
{
    internal static class StringExtensions
    {
        public static string RemoveControlCharacters(this string text)
        {
            return text.AsSpan().RemoveControlCharacters();
        }

        public static string RemoveControlCharacters(this ReadOnlySpan<char> text)
        {
            if (text == null) return null;
            var newString = new StringBuilder(text.Length);
            char ch;
            for (int i = 0; i < text.Length; i++)
            {
                ch = text[i];
                if (ch == '\r' || ch == '\n' || !char.IsControl(ch)) //keep new lines
                {
                    if (ch != '\uF022' && ch != '￾')
                    {
                        newString.Append(ch);
                    }
                    else
                    {
                        newString.Append(' ');
                    }

                }
            }
            return newString.ToString();
        }


        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(text.Length);

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string RemoveLigatures(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) { return input; }

            if (!input.AsSpan().HasLigatures())
            {
                return input;
            }
            else
            {
                var sb = new StringBuilder(input.Length);
                var s = input.AsSpan();

                var p = 0;
                var n = s.IndexOfAny(CharacterClasses.UnicodeLatinLigatures);
                while (n >= 0)
                {
                    sb.Append(s.Slice(p, n).ToArray());
                    var i = Array.IndexOf(CharacterClasses.UnicodeLatinLigatures, s.Slice(p + n, 1)[0]);
                    sb.Append(CharacterClasses.UnicodeLatinLigatureReplacements[i]);
                    p = p + n + 1;
                    n = s.Slice(p).IndexOfAny(CharacterClasses.UnicodeLatinLigatures);
                }
                if (p < s.Length)
                {
                    sb.Append(s.Slice(p).ToArray());
                }

                return sb.ToString();
            }
        }

        public static string ToTitleCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) { return str; }

            var sb = new StringBuilder(str.Length);
            char prev = str[0];
            sb.Append(char.ToUpperInvariant(prev));
            for (int i = 1; i < str.Length; i++)
            {
                char cur = str[i];
                if (char.IsWhiteSpace(prev) || (char.IsPunctuation(prev) && prev != '\''))
                {
                    cur = char.ToUpperInvariant(cur);
                }
                else
                {
                    cur = char.ToLowerInvariant(cur);
                }
                sb.Append(cur);
                prev = cur;
            }
            return sb.ToString();
        }
    }

    internal static class CharSpanExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsNullOrWhiteSpace(this ReadOnlySpan<char> span) { if (span.Length == 0) { return true; } for (int i = 0; i < span.Length; i++) { if (!char.IsWhiteSpace(span[i])) { return false; } } return true; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsCapitalized(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } return char.IsUpper(span[0]) && !span.IsAllUpperCase(); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsAllUpperCase(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (char.IsLower(span[i]) && !char.IsNumber(span[i])) { return false; } } return true; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsAllLowerCase(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (char.IsUpper(span[i]) && !char.IsNumber(span[i])) { return false; } } return true; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsAllLetterOrDigit(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (!char.IsLetterOrDigit(span[i])) { return false; } } return true; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsLetter(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (!char.IsLetter(span[i])) { return false; } } return true; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsDigit(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (!char.IsDigit(span[i])) { return false; } } return true; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsNumeric(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (!CharacterClasses.NumericCharacters.Contains(span[i])) { return false; } } return CharacterClasses.RE_AllNumeric.IsMatch(new string(span.ToArray())); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool HasNumeric(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (CharacterClasses.NumericCharacters.Contains(span[i])) { return true; } } return false; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsSentencePunctuation(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (!CharacterClasses.SentencePunctuationCharacters.Contains(span[i])) { return false; } } return true; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsAnyPunctuation(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (char.IsPunctuation(span[i])) { return true; } } return false; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsPunctuation(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (!char.IsPunctuation(span[i])) { return false; } } return true; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsSymbol(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (!char.IsSymbol(span[i])) { return false; } } return true; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsCurrency(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (!CharacterClasses.CurrencyCharacters.Contains(span[i])) { return false; } } return true; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsHyphen(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (!CharacterClasses.HyphenCharacters.Contains(span[i])) { return false; } } return true; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsOpenQuote(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (!CharacterClasses.OpenQuotesCharacters.Contains(span[i])) { return false; } } return true; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsEmailLocalPart(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (!CharacterClasses.EmailLocalPartCharacters.Contains(span[i])) { return false; } } return true; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsEmailDomainPart(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (!CharacterClasses.EmailDomainPartCharacters.Contains(span[i])) { return false; } } return true; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool IsQuoteCharacters(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (!CharacterClasses.QuotesCharacters.Contains(span[i])) { return false; } } return true; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool HasLigatures(this ReadOnlySpan<char> span) { if (span.Length == 0) { return false; } for (int i = 0; i < span.Length; i++) { if (span[i] < CharacterClasses.MinimumLigatureValue || span[i] > CharacterClasses.MaximumLigatureValue) { continue; } else { if (CharacterClasses.UnicodeLatinLigatures.Contains(span[i])) { return true; } } } return false; }
    }

    internal static class CharacterClasses
    {
        public static string Alpha = @"\p{L}";
        public static string AlphaLower = @"\p{Ll}";
        public static string AlphaUpper = @"\p{Lu}";
        public static string Units = @"mb|m\/s|km\/h|kmh|mph|km³|km²|km|m³|m²|m|dm³|dm²|dm|cm³|cm²|cm|mm³|mm²|mm|ha|µm|nm|yd|in|ft|kg|mg|µg|g|t|lb|oz|hPa|Pa|mbar|MB|kb|KB|gb|GB|tb|TB|T|G|M"; // this should actually be sorted by token length, so that e.g. m/s matches before m
        public static string Currency = @"\p{Sc}";
        public static string SentencePunctuation = @"…|:|;|\!|\?|¿|¡|\.";
        public static string Punctuation = @"…|,|:|;|\!|\?|¿|¡|\(|\)|\[|\]|\{|\}|<|>|_|#|\*|&";
        public static string Quotes = @"'|''|""|”|“|``|`|‘|´|‚|,|„|»|«";
        public static string OpenQuotes = @"'|''|""|“|``|`|‘|«";
        public static string Hyphens = @"-|–|—";
        public static string Symbols = @"\p{So}";  //Symbols like dingbats, but also emoji, see: https://www.compart.com/en/unicode/category/So
        public static string Ellipses = @"\.\.+|…";

        public static string Prefixes = $"§|%|=|\\+|{Punctuation}|{Ellipses}|{Quotes}|{Currency}|{Symbols}";
        public static string Sufixes = $"{Punctuation}|{Ellipses}|{Quotes}|{Currency}|{Symbols}|{Hyphens}|['’][sS]|(?<=[0-9])\\+|(?<=°[FfCcKk])\\.]|(?<=[0-9])(?:{Currency})|(?<=[0-9])(?:{Units})|(?<=[0-9{AlphaLower}%²\\-\\)\\]\\+(?:{Quotes})])\\.|(?<=[{AlphaUpper}][{AlphaUpper}])\\.";
        public static string Infixes = $"{Ellipses}|{Symbols}|(?<=[0-9])[+\\-\\*^](?=[0-9-])|(?<=[{AlphaLower}])\\.(?=[{AlphaUpper}])|(?<=[{Alpha}]),(?=[{Alpha}])|(?<=[{Alpha}])[?\";:=,.]*(?:{Hyphens})(?=[{Alpha}])|(?<=[{Alpha}\"])[:<>=\\/](?=[{Alpha}])";

        public static Regex RE_Prefixes = new Regex(string.Join("|", Prefixes.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(e => "^" + e)), RegexOptions.Compiled);
        public static Regex RE_Sufixes = new Regex(string.Join("|", Sufixes.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e + "$")), RegexOptions.Compiled);
        public static Regex RE_Infixes = new Regex(Infixes, RegexOptions.Compiled);


        public static Regex RE_OpenQuotes = new Regex(OpenQuotes, RegexOptions.Compiled);
        public static Regex RE_Currency = new Regex(Currency, RegexOptions.Compiled);
        public static Regex RE_SentencePunctuation = new Regex($"^({SentencePunctuation})*$", RegexOptions.Compiled);
        public static Regex RE_AnyPunctuation = new Regex($"^({Punctuation})*$", RegexOptions.Compiled);

        public static Regex RE_IsSymbol = new Regex($"^({Symbols})+$", RegexOptions.Compiled);

        public static Regex RE_IsHyphen = new Regex($"^({Hyphens})+$", RegexOptions.Compiled);
        public static Regex RE_AllLetters = new Regex($"^({Alpha})+$", RegexOptions.Compiled);
        public static Regex RE_AllNumeric = new Regex(@"^[\-\+\p{Sc}]{0,3}[0-9.,]{1,}[\p{Sc}]{0,1}$", RegexOptions.Compiled);
        public static Regex RE_HasNumeric = new Regex(@"\d", RegexOptions.Compiled);

        private static HashSet<T> ToHashSet<T>(this IEnumerable<T> input) => new HashSet<T>(input);


        public static readonly char[] WhitespaceCharacters = new char[] { ' ', '\n', '\r', '\t', '\v', '\f' }.Union(Enumerable.Range(0, 0x10000).Select(i => ((char)i)).Where(c => char.IsWhiteSpace(c))).ToArray(); //Optimization to have the simplest whitespace first, as this reduces the searchspace for the unusual chars quite a lot
        public static readonly HashSet<char> NumericCharacters = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }.ToHashSet();
        public static readonly HashSet<char> CurrencyCharacters = Enumerable.Range(0, 0x10000).Select(i => ((char)i)).Where(c => RE_Currency.IsMatch(c.ToString())).ToHashSet();
        public static readonly HashSet<char> SymbolCharacters = Enumerable.Range(0, 0x10000).Select(i => ((char)i)).Where(c => RE_IsSymbol.IsMatch(c.ToString())).ToHashSet();
        public static readonly HashSet<char> ASCIISymbolCharacters = Enumerable.Range(0, 0x100).Select(i => ((char)i)).Where(c => RE_IsSymbol.IsMatch(c.ToString())).ToHashSet();
        public static readonly HashSet<char> ASCIICurrencyCharacters = Enumerable.Range(0, 0x100).Select(i => ((char)i)).Where(c => RE_Currency.IsMatch(c.ToString())).ToHashSet();
        public static readonly HashSet<char> HyphenCharacters = new char[] { '-', '–', '—', '~' }.ToHashSet();
        public static readonly char[] QuotesCharactersArray = new char[] { '\'', '"', '”', '“', '`', '‘', '´', '‘', '’', '‚', '„', '»', '«', '「', '」', '『', '』', '（', '）', '〔', '〕', '【', '】', '《', '》', '〈', '〉' };
        public static readonly HashSet<char> QuotesCharacters = new char[] { '\'', '"', '”', '“', '`', '‘', '´', '‘', '’', '‚', '„', '»', '«', '「', '」', '『', '』', '（', '）', '〔', '〕', '【', '】', '《', '》', '〈', '〉' }.ToHashSet();
        public static readonly HashSet<char> ASCIIQuotesCharacters = QuotesCharacters.Where(c => (int)c < 256).ToHashSet();
        public static readonly HashSet<char> OpenQuotesCharacters = new char[] { '\'', '"', '“', '`', '‘', '´', '‘', '«', '「', '『', '（', '〔', '【', '《', '〈' }.ToHashSet();
        public static readonly HashSet<char> CloseQuotesCharacters = new char[] { '\'', '"', '”', '´', '’', '‚', ',', '„', '»', '」', '』', '）', '〕', '】', '》', '〉' }.ToHashSet();
        //public static readonly char[] OpenQuotesCharacters  = new char[] { '\'', '"', '“', '`', '‘', '«' };
        public static readonly HashSet<char> SentencePunctuationCharacters = new char[] { '…', ':', ';', '!', '?', '.' }.ToHashSet();
        public static readonly HashSet<char> PunctuationCharacters = new char[] { '…', ',', ':', ';', '!', '?', '¿', '¡', '(', ')', '[', ']', '{', '}', '<', '>', '_', '#', '*', '&' }.ToHashSet();
        public static readonly HashSet<char> InvalidURLCharacters = new char[] { '…', ';', '¿', '¡', '[', ']', '{', '}', '<', '>' }.ToHashSet();
        public static readonly HashSet<char> ValidURLCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~:/?#[]@!$&'()*+,;=".ToCharArray().ToHashSet();
        public static readonly HashSet<char> ASCIIPrefixesCharacters = new char[] { '§', '%', '=', '+' }.Union(PunctuationCharacters).Union(ASCIISymbolCharacters).Union(ASCIIQuotesCharacters).Union(ASCIICurrencyCharacters).ToHashSet();
        public static readonly HashSet<char> ASCIIInfixCharacters = PunctuationCharacters.Union(ASCIISymbolCharacters).Union(ASCIIQuotesCharacters).Union(ASCIICurrencyCharacters).ToHashSet();
        public static readonly HashSet<char> OtherPrefixInfixCharacters = SymbolCharacters.Union(CurrencyCharacters).Union(QuotesCharacters).Except(ASCIICurrencyCharacters).Except(ASCIISymbolCharacters).ToHashSet();
        public static readonly char[][] UnitsCharacters = Units.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.ToCharArray()).ToArray();
        public static readonly HashSet<char> ExtraSufixesCharacters = new char[] { '%', '°', '²', '³', '-', '+', ')', ']', '>', '}' }.ToHashSet();

        public static readonly HashSet<char> EmailLocalPartCharacters = "abcdefghijklmnopqrstuwxyzABCDEFGHIJKLMNOPQRSTUWXYZ0123456789!#$%&'*+-/=?^_`{|}~;.()".ToCharArray().ToHashSet();
        public static readonly HashSet<char> EmailDomainPartCharacters = "abcdefghijklmnopqrstuwxyzABCDEFGHIJKLMNOPQRSTUWXYZ0123456789-.".ToCharArray().ToHashSet();

        public static readonly char[] UnicodeLatinLigatures = new char[] { 'Ꜳ', 'ꜳ', 'Æ', 'æ', 'Ꜵ', 'ꜵ', 'Ꜷ', 'ꜷ', 'Ꜹ', 'ꜹ', 'Ꜻ', 'ꜻ', 'Ꜽ', 'ꜽ', 'ﬀ', 'ﬃ', 'ﬄ', 'ﬁ', 'ﬂ', 'Œ', 'œ', 'Ꝏ', 'ꝏ', 'ﬆ', 'ﬅ', 'Ꜩ', 'ꜩ', 'ᵫ', 'Ꝡ', 'ꝡ' }; // removed from the ligatures normalization: ẞ,ß, 🙰 -> ſs, ſz, et
        public static readonly char MinimumLigatureValue = UnicodeLatinLigatures.Min();
        public static readonly char MaximumLigatureValue = UnicodeLatinLigatures.Max();
        public static readonly string[] UnicodeLatinLigatureReplacements = "AA,aa,AE,ae,AO,ao,AU,au,AV,av,AV,av,AY,ay,ff,ffi,ffl,fi,fl,OE,oe,OO,oo,st,ſt,TZ,tz,ue,VY,vy".Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    }
}
