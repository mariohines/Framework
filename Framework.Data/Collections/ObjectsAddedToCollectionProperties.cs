using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Framework.Data.Collections
{
	/// <summary>Objects added to collection properties.</summary>
	[CollectionDataContract(Name = "ObjectsAddedToCollectionProperties",
				ItemName = "AddedObjectsForProperty", KeyName = "CollectionPropertyName", ValueName = "AddedObjects")]
	public class ObjectsAddedToCollectionProperties : Dictionary<string, ObjectList> { }
}