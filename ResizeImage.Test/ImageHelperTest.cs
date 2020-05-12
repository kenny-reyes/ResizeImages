using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Xunit;

namespace ResizeImage.Test
{
	public class ImageFormatData : IEnumerable<object[]>
	{
		public IEnumerator<object[]> GetEnumerator()
		{
			yield return new object[] { ImageFormat.Bmp };
			yield return new object[] { ImageFormat.Png };
			yield return new object[] { ImageFormat.Gif };
			yield return new object[] { ImageFormat.Jpeg };
			yield return new object[] { ImageFormat.Emf };
			yield return new object[] { ImageFormat.Icon };
			yield return new object[] { ImageFormat.Tiff };
			yield return new object[] { ImageFormat.Wmf };
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}

	public class ImageHelperTest
	{
		private const string Postfix = "_Thumbnail";
		private const string Images = "Images";
		private const int Width = 100;
		private const int Height = 100;

		[Theory]
		[ClassData(typeof(ImageFormatData))]
		public void IfThereIsImagesToConvertInWidthAndHeight_ItShouldResizeInBmp(ImageFormat formatToConvert)
		{
			foreach (var filePath in Directory.GetFiles(Images))
			{
				var directoryResults = "Converted" + formatToConvert;

				Directory.CreateDirectory(directoryResults);

				using var image = new Bitmap(System.Drawing.Image.FromFile(filePath));
				using var bitmapImage = ImageHelper.ResizeImage(image, Width, Height);
				bitmapImage.Save($"{directoryResults}\\{Path.GetFileNameWithoutExtension(filePath)}_{Postfix}.{formatToConvert}", formatToConvert);
			}
			Assert.True(true);
		}

		[Theory]
		[ClassData(typeof(ImageFormatData))]
		public void IfThereIsImagesToConvertInWidth_ItShouldResizeInBmp(ImageFormat formatToConvert)
		{
			foreach (var filePath in Directory.GetFiles(Images))
			{
				var directoryResults = "ConvertedHorizontal" + formatToConvert;

				Directory.CreateDirectory(directoryResults);

				using var image = new Bitmap(System.Drawing.Image.FromFile(filePath));
				using var bitmapImage = ImageHelper.ResizeImage(image, Width, ResizeMode.Horizontal);
				bitmapImage.Save($"{directoryResults}\\{Path.GetFileNameWithoutExtension(filePath)}_{Postfix}.{formatToConvert}", formatToConvert);
			}
			Assert.True(true);
		}

		[Theory]
		[ClassData(typeof(ImageFormatData))]
		public void IfThereIsImagesToConvertInHeight_ItShouldResizeInBmp(ImageFormat formatToConvert)
		{
			foreach (var filePath in Directory.GetFiles(Images))
			{
				var directoryResults = "ConvertedVertical" + formatToConvert;

				Directory.CreateDirectory(directoryResults);

				using var image = new Bitmap(System.Drawing.Image.FromFile(filePath));
				using var bitmapImage = ImageHelper.ResizeImage(image, Width, ResizeMode.Vertical);
				bitmapImage.Save($"{directoryResults}\\{Path.GetFileNameWithoutExtension(filePath)}_{Postfix}.{formatToConvert}", formatToConvert);
			}
			Assert.True(true);
		}
	}
}
