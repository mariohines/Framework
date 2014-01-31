using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Framework.Data.Collections
{
	/// <summary>Dictionary of extended properties.</summary>
	[CollectionDataContract(Name = "ExtendedPropertiesDictionary",
				ItemName = "ExtendedProperties", KeyName = "Name", ValueName = "ExtendedProperty")]
	public class ExtendedPropertiesDictionary : Dictionary<string, Object> { }
}