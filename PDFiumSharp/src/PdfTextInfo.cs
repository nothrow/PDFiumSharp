using System.Collections.Generic;
using System.Collections.ObjectModel;
using PDFiumSharp.Types;

namespace PDFiumSharp
{
    public class PdfTextInfo
    {
        internal PdfTextInfo(string text, int startIndex, int length, FS_RECTF boundingRect, List<FS_RECTF> charBoxes)
        {
            Text = text.RemoveControlCharacters().RemoveLigatures();

            StartIndex = startIndex;
            Length = length;
            BoundingRectangle = boundingRect;
            charBoxList = charBoxes;
        }
        internal List<FS_RECTF> charBoxList;

        public string Text { get; internal set; }
        public int StartIndex { get; internal set; }
        public int Length { get; internal set; }
        public FS_RECTF BoundingRectangle { get; internal set; }
        public ReadOnlyCollection<FS_RECTF> CharBoxes { get { return new ReadOnlyCollection<FS_RECTF>(charBoxList); } }

        public override string ToString() => Text;
    }
}
