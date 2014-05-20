using System;
using Framework.Core.Extensions;
using Framework.Data.Enumerations;

namespace Framework.Data
{
	/// <summary>Class to handle units of space on drives.</summary>
	public class UnitOfSpace
	{
		private const string SizeFormat = @"{0} {1}";

		internal UnitOfSpace() {
		}

		/// <summary>Constructor.</summary>
		/// <param name="sizeInByte">.</param>
		public UnitOfSpace(ulong sizeInByte) {
			Process(sizeInByte);
		}

		/// <summary>Enumeration for SizeUnit.</summary>
		public SizeUnit UnitOfSize { get; set; }

		/// <summary>Actual size value.</summary>
		public double Size { get; set; }

		private void Process(ulong sizeInByte) {
			UnitOfSize = sizeInByte < (ulong) SizeUnit.Eb
			             	? (sizeInByte < (ulong) SizeUnit.Tb
			             	   	? (sizeInByte < (ulong) SizeUnit.Gb
			             	   	   	? (sizeInByte < (ulong) SizeUnit.Mb ? SizeUnit.Kb : SizeUnit.Mb)
			             	   	   	: SizeUnit.Gb)
			             	   	: SizeUnit.Tb)
			             	: SizeUnit.Eb;
			switch (UnitOfSize) {
				case SizeUnit.Kb:
				case SizeUnit.Mb:
				case SizeUnit.Gb:
				case SizeUnit.Tb:
				case SizeUnit.Eb:
					Size = sizeInByte/(double) UnitOfSize;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() {
			return SizeFormat.FormatWith(Size.ToString("0.00"), UnitOfSize);
		}
	}
}