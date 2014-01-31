using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Framework.Data.Collections
{
	/// <summary>Dictionary of original values.</summary>
	[CollectionDataContract(Name = "OriginalValuesDictionary",
				ItemName = "OriginalValues", KeyName = "Name", ValueName = "OriginalValue")]
	public class OriginalValuesDictionary : Dictionary<string, Object> { }
}