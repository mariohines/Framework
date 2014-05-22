Framework
=========

Primary location for Framework utility libraries.

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