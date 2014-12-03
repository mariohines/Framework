#Framework Solution#

###Primary location for Framework utility libraries.###

- 12-03-2014
>- Created a NoWeb configuration to publish to Nuget without requiring web.
>- Added an IEnumerable call for getting bindings in NinjectManager, GenericIocManager, and IDependencyInjector.
>- Added Coalesce extension for string.
>- Added async calls for Translate.

- 07-09-2014
>- Put an obsolete attribute on the Ninject-based objects.
>- Removed use of Ninject-based objects in preparation of removing dependency of Ninject.
>- Added new method to IUnitOfWork and its implementation.
>- Added new property to expose the current IDataContext in the UnitOfWork object.
>- Removed the CsvReader class.
>- Incremented version from 4.2.2 to 4.3.0.

- 06-26-2014
>- Fixed issue with using the 'Update' extension.
>- Incrememted version from 4.2.1 to 4.2.2.

- 06-19-2014
>- Fixed an extension for IEnumerable&lt;TSource&gt;.
>- Incremented version from 4.2.0 to 4.2.1.

- 05-21-2014
>- Added 2 new method signatures in IDbRepository, 'Any' and 'Update' (Update is an extension from EntityFramework.Extended library)
>- Implemented new methods in BaseDbRepository.
>- Incremented the minor version.

- 05-20-2014
>- Put an obsolete attribute on NinjectKernel and NinjectManager (without forcing an error) in favor of using IDependencyInjector for when I remove the Ninject dependency from the entire solution.
>- Modified UnitOfWorkAspect. Made default UnitOfWorkOptions be 'AutoCommit'.
>- Modified UnitOfWork class. Made default UnitOfWorkOptions be 'AutoCommit'.
>- Completed the LoggingAspect.
>- Removed ITranslator and created 2 new interfaces, ISimplexTranslator (one-way translation) and IDuplexTranslator (bi-directional translation).
>- Modified UnitOfSpace to make the output a private constant string.