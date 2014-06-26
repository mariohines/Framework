Framework
=========

Primary location for Framework utility libraries.

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