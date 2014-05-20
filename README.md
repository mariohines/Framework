Framework
=========

Primary location for Framework utility libraries.

- 05-20-2014
>- Put an obsolete attribute on NinjectKernel and NinjectManager (without forcing an error) in favor of using IDependencyInjector for when I remove the Ninject dependency from the entire solution.
>- Modified UnitOfWorkAspect. Made default UnitOfWorkOptions be 'AutoCommit'.
>- Modified UnitOfWork class. Made default UnitOfWorkOptions be 'AutoCommit'.
>- Completed the LoggingAspect.
>- Removed ITranslator and created 2 new interfaces, ISimplexTranslator (one-way translation) and IDuplexTranslator (bi-directional translation).
>- Modified UnitOfSpace to make the output a private constant string.

- 05-14-2014
>- Grabbing a copy to put on my work laptop.
