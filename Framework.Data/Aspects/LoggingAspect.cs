using System;
using System.Diagnostics;
using Framework.Extensions;
using Framework.Interfaces;
using Framework.IoC;
using Framework.IoC.Ninject;
using PostSharp.Aspects;

namespace Framework.Data.Aspects
{
	/// <summary>Logging aspect.</summary>
	[Serializable]
	public class LoggingAspect : OnMethodBoundaryAspect
	{
		[NonSerialized] private readonly ILoggable _logger;

		[NonSerialized] private readonly Stopwatch _stopwatch;

		/// <summary>Default constructor.</summary>
		/// <param name="logTime">(optional) time of the log.</param>
		public LoggingAspect(bool logTime = false) {
			_logger = GenericIocManager.IsInUse ? GenericIocManager.GetBindingOfType<ILoggable>() : NinjectManager.GetBindingOfType<ILoggable>();
			if (logTime) {
				_stopwatch = new Stopwatch();
			}
		}

		/// <summary>Method executed <b>before</b> the body of methods to which this aspect is applied.</summary>
		/// <param name="args">Event arguments specifying which method is being executed, which are its arguments, and how should the execution
		/// continue after the execution of
		/// <see cref="M:PostSharp.Aspects.IOnMethodBoundaryAspect.OnEntry(PostSharp.Aspects.MethodExecutionArgs)" />.</param>
		public override void OnEntry(MethodExecutionArgs args) {
			if (_stopwatch.IsNull()) {}

			base.OnEntry(args);
		}

		/// <summary>
		/// Method executed <b>after</b> the body of methods to which this aspect is applied, in case that the method resulted with an exception.
		/// </summary>
		/// <param name="args">Event arguments specifying which method is being executed and which are its arguments.</param>
		public override void OnException(MethodExecutionArgs args) {
			base.OnException(args);
		}

		/// <summary>
		/// Method executed <b>after</b> the body of methods to which this aspect is applied, even when the method exists with an exception (this
		/// method is invoked from the <c>finally</c> block).
		/// </summary>
		/// <param name="args">Event arguments specifying which method is being executed and which are its arguments.</param>
		public override void OnExit(MethodExecutionArgs args) {
			if (_stopwatch.IsNull()) {
				_stopwatch.Stop();
			}

			base.OnExit(args);
		}
	}
}