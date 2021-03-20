using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDFiumSharp.Types;

namespace PDFiumSharp
{
    public sealed partial class PdfText
    {
        public class PdfTextBlock
        {
            public List<PdfTextInfo> Lines { get; private set; } = new List<PdfTextInfo>();
            public FS_RECTF BoundingRectangle { get; private set; }

            private string _text;

            public void AddLine(PdfTextInfo line)
            {
                if (Lines.Any())
                {
                    BoundingRectangle = BoundingRectangle.Union(line.BoundingRectangle);
                }
                else
                {
                    BoundingRectangle = line.BoundingRectangle;
                }

                Lines.Add(line);
            }

            public string Text 
            {
                get 
                {
                    if (_text is object) return _text;
                    var sb = new StringBuilder();
                    foreach(var l in Lines)
                    {
                        sb.Append(l.Text);
                        if(sb.Length > 0)
                        {
                            var last = sb[sb.Length-1];
                            if(last != '\n' && last != '\r')
                            {
                                sb.Append(' ');
                            }
                        }
                    }
                    _text = sb.ToString();
                    return _text;
                }
            }


        }
    }
}
