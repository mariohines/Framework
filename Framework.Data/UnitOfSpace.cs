using System;
using Framework.Data.Enumerations;

namespace Framework.Data
{
	/// <summary>Class to handle units of space on drives.</summary>
	public class UnitOfSpace
	{
		/// <summary></summary>
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
					Size = sizeInByte/(double) UnitOfSize;
					break;
				case SizeUnit.Mb:
					Size = sizeInByte/(double) UnitOfSize;
					break;
				case SizeUnit.Gb:
					Size = sizeInByte/(double) UnitOfSize;
					break;
				case SizeUnit.Tb:
					Size = sizeInByte/(double) UnitOfSize;
					break;
				case SizeUnit.Eb:
					Size = sizeInByte/(double) UnitOfSize;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary></summary>
		/// <returns></returns>
		public override string ToString() {
			return string.Format("{0} {1}", Size.ToString("0.00"), UnitOfSize);
		}
	}
}