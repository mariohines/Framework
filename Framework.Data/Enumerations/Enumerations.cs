using System;

namespace Framework.Data.Enumerations
{
	#region EnumForObjectState
    /// <summary>Bitfield of flags for specifying ObjectState.</summary>
	[Flags]
	public enum ObjectState
	{
		/// <summary>.</summary>
		Unchanged = 0x1,
		/// <summary>.</summary>
		Added = 0x2,
		/// <summary>.</summary>
		Modified = 0x4,
		/// <summary>.</summary>
		Deleted = 0x8
	}
	#endregion

	#region ISpecification Enums

    /// <summary>Values that represent SortDirection.</summary>
	public enum SortDirection
	{
        /// <summary>Ascending SortDirection.</summary>
		Ascending,
        /// <summary>Descending SortDirection.</summary>
		Descending
	}

	#endregion

	#region Unit Of Work Enums

    /// <summary>Defines the Unit OF Work status.</summary>
	public enum UnitOfWorkStatus
	{
		/// <summary>
		/// Unit of Work is active.
		/// </summary>
		Active,

		/// <summary>
		/// Unit of Work was commited successfully.
		/// </summary>
		Committed,

		/// <summary>
		/// Unit of Work was Rolled Back.
		/// </summary>
		RolledBack,

		/// <summary>
		/// Unit of Work was Dispossed.
		/// </summary>
		Disposed,
	}

    /// <summary>Specify the UnitOfWork options such as AutoCommit.</summary>
	[Flags]
	public enum UnitOfWorkOptions
	{
		/// <summary>
		/// An explicit Commit is needed before the end of the unit of work scope or changes are rolled back, 
		/// Any exceptions during the Unit of Work scope will Rollback changes.
		/// </summary>
		DefaultOptions = 0,

		/// <summary>
		/// Implicit Commit at the end of the Unit of Work Scope. 
		/// </summary>
		AutoCommit = 1,
	}

	#endregion

    /// <summary>Defines the posible status for a Repository object.</summary>
	public enum RepositoryStatus
	{
        /// <summary>Active Status.</summary>
		Active,
        /// <summary>Disposed Status.</summary>
		Disposed
	}

    /// <summary>Enumeration for the units of space for harddisk.</summary>
    /// <remarks>Mario, 12/8/2012.</remarks>
	public enum SizeUnit : ulong
	{
		/// <summary>Kilobyte</summary>
		Kb = 0x400,

		/// <summary>Megabyte</summary>
		Mb = 0x100000,

		///<summary>Gigabyte</summary>
		Gb = 0x40000000,

		///<summary>Terabyte</summary>
		Tb = 0x10000000000,

		///<summary>Exabyte</summary>
		Eb = 0x4000000000000
	}

	/// <summary>Values that represent ConcurrencyStrategy.</summary>
	public enum ConcurrencyStrategy
	{
		/// <summary>Strategy to where the client values get priority on save.</summary>
		Client,
		/// <summary>Strategy to where the database values get priority on save.</summary>
		Database
	}
}