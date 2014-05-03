using System.ComponentModel;

namespace Framework.Core.Tests.TestObjects
{
	public enum TestEnumOne
	{
		[Description("Cat")]
		Cat = 1,
		[Description("Dog")]
		Dog,
		[Description("Bird")]
		Bird,
		[Description("Fish")]
		Fish
	}

	public enum TestEnumTwo : byte
	{
		[Description("Mario Hines")]
		Mario = 1 << 0,
		[Description("Tyana Barker")]
		Tyana,
		[Description("Kain Hines")]
		Kain,
		[Description("Meliyah Hines")]
		Meliyah
	}
}