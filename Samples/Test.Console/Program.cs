using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDFiumSharp;
using System.IO;

namespace TestConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			//using (var doc = new PdfDocument("TestDoc.pdf", "password"))
			using (var doc = new PdfDocument(@"C:\Users\rafae\Downloads\EN-US-CNTNT-eBook-Build a Competitive Edge With SaaS Apps.pdf"))
			{
				int pageNumber = 0;
				foreach (var page in doc.Pages)
				{
					using (page)
					{
						using (var bitmap = new PDFiumBitmap((int)page.Width, (int)page.Height, true))
						using (var stream = new FileStream($"{pageNumber}.bmp", FileMode.Create))
						{
							page.Render(bitmap);
							bitmap.Save(stream);
						}

						using (var text = PdfText.Load(page))
						{
							File.WriteAllText($"{pageNumber}.txt", text.GetText());

							var parts = text.GetSegmentedText();

							File.WriteAllLines($"{pageNumber}.bti.txt", parts.Select(ti => ti.Text));
						}
					}
					pageNumber++;
				}
			}
		}
	}
}
