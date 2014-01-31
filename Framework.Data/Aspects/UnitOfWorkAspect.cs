using System;
using Framework.Data.Abstract;
using Framework.Data.Enumerations;
using Framework.Data.Interfaces;
using PostSharp.Aspects;

namespace Framework.Data.Aspects
{
	/// <summary>Unit of work aspect.</summary>
	[Serializable]
	public class UnitOfWorkAspect : OnMethodBoundaryAspect
	{
		/// <summary>The unit of work to be executed.</summary>
		[NonSerialized]
		private IUnitOfWork _work;
		/// <summary>Options for controlling the unit of work.</summary>
		[NonSerialized]
		private readonly UnitOfWorkOptions _options;

		/// <summary>Constructor.</summary>
		/// <param name="options">Options for controlling the operation.</param>
		public UnitOfWorkAspect(UnitOfWorkOptions options = UnitOfWorkOptions.DefaultOptions) {
			_options = options;
		}

		/// <summary>Method executed <b>before</b> the body of methods to which this aspect is applied.</summary>
		/// <param name="args">Event arguments specifying which method is being executed, which are its arguments, and how should the execution
		/// continue after the execution of
		/// <see cref="M:PostSharp.Aspects.IOnMethodBoundaryAspect.OnEntry(PostSharp.Aspects.MethodExecutionArgs)" />.</param>
		public override void OnEntry(MethodExecutionArgs args) {
			_work = UnitOfWork.OnEntryAdvice(_options);
			base.OnEntry(args);
		}

		/// <summary>
		/// Method executed <b>after</b> the body of methods to which this aspect is applied, but only when the method successfully returns (i.e.
		/// when no exception flies out the method.).
		/// </summary>
		/// <param name="args">Event arguments specifying which method is being executed and which are its arguments.</param>
		public override void OnSuccess(MethodExecutionArgs args) {
			UnitOfWork.OnSuccessAdvice(_work);
			base.OnSuccess(args);
		}

		/// <summary>
		/// Method executed <b>after</b> the body of methods to which this aspect is applied, even when the method exists with an exception (this
		/// method is invoked from the <c>finally</c> block).
		/// </summary>
		/// <param name="args">Event arguments specifying which method is being executed and which are its arguments.</param>
		public override void OnExit(MethodExecutionArgs args) {
			UnitOfWork.OnExitAdvice(_work);
			base.OnExit(args);
		}

		/// <summary>
		/// Method executed <b>after</b> the body of methods to which this aspect is applied, in case that the method resulted with an exception.
		/// </summary>
		/// <param name="args">Event arguments specifying which method is being executed and which are its arguments.</param>
		public override void OnException(MethodExecutionArgs args) {
			args.FlowBehavior = FlowBehavior.Continue;
			UnitOfWork.OnExceptionAdvice(_work);
			base.OnException(args);
		}
	}
}