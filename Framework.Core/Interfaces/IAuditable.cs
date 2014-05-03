using System;

namespace Framework.Core.Interfaces
{
	/// <summary>Interface for auditable.</summary>
	/// <typeparam name="TKey">Type of the Id property.</typeparam>
	public interface IAuditable<TKey>
	{
		/// <summary>Gets or sets the identifier.</summary>
		/// <value>The identifier.</value>
		TKey Id { get; set; }

		/// <summary>Gets or sets the date of the create.</summary>
		/// <value>The date of the create.</value>
		DateTime CreateDate { get; set; }

		/// <summary>Gets or sets the amount to created by.</summary>
		/// <value>The amount to created by.</value>
		string CreatedBy { get; set; }

		/// <summary>Gets or sets the date of the edit.</summary>
		/// <value>The date of the edit.</value>
		DateTime? EditDate { get; set; }

		/// <summary>Gets or sets the amount to edited by.</summary>
		/// <value>The amount to edited by.</value>
		string EditedBy { get; set; }
	}
}