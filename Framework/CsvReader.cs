using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Framework.Core.Extensions;
using LinqKit;

namespace Framework.Core
{
	/// <summary>A CSV reader.</summary>
	public sealed class CsvReader
	{
		private const string ColumnLineExpression = @"(?<ColumnName>\w+(\s|_)?)(?:(\s{4,5}|\t))?";
		private readonly Stream _stream;

		#region Public Members
		/// <summary>Gets or sets the filename of the file.</summary>
		/// <value>The filename.</value>
		public string Filename { get; private set; }

		/// <summary>Gets or sets the output.</summary>
		/// <value>The output.</value>
		public DataTable Output { get; private set; }

		/// <summary>Gets or sets a list of names of the columns.</summary>
		/// <value>A list of names of the columns.</value>
		public Dictionary<int, string> IndexAndColumnName { get; private set; } 
		#endregion

		/// <summary>Default constructor.</summary>
		public CsvReader() {
			Output = new DataTable(Filename.NullIfEmpty() ?? "CsvData");
		}

		/// <summary>Constructor.</summary>
		/// <param name="fileName">The filename.</param>
		public CsvReader(string fileName) : this() {
			if (!fileName.HasValue()) {
				throw new ArgumentNullException("fileName");
			}
			Filename = fileName;
		}

		/// <summary>Constructor.</summary>
		/// <param name="stream">The stream.</param>
		public CsvReader(Stream stream) : this() {
			if (stream == null) {
				throw new ArgumentNullException("stream");
			}
			_stream = stream;
		}

		/// <summary>Reads the stream.</summary>
		/// <exception cref="ArgumentException">Thrown when one or more arguments have unsupported or illegal values.</exception>
		public async void ReadStream() {
			if (_stream == null) {
				throw new ArgumentException("This class was not instantiated with a stream.");
			}

			var organizedData = new List<string>();
			using (var reader = new StreamReader(_stream)) {
				while (!reader.EndOfStream) {
					var line = await reader.ReadLineAsync();
					organizedData.Add(line);
				}
			}

			BreakDownData(organizedData);
		}

		/// <summary>Reads the file.</summary>
		/// <exception cref="ArgumentException">Thrown when one or more arguments have unsupported or illegal values.</exception>
		public async void ReadFile() {
			if (!Filename.HasValue()) {
				throw new ArgumentException("This class was not instantied with a file name.");
			}

			var organizedData = new List<string>();
			using (var stream = new FileStream(Filename, FileMode.Open)) {
				using (var reader = new StreamReader(stream)) {
					while (!reader.EndOfStream) {
						var line = await reader.ReadLineAsync();
						organizedData.Add(line);
					}
				}
			}

			BreakDownData(organizedData);
		}

		#region Private Methods

		private void BreakDownData(IReadOnlyList<string> data) {
			for (var i = 0; i < data.Count; i++) {
				if (i == 0) {
					BuildDataTableColumns(data[i]);
				}
				PopulateDataTable(data[i]);
			}
		}

		private void BuildDataTableColumns(string line) {
			var match = Regex.Match(line, ColumnLineExpression);
			IndexAndColumnName = match.Captures.Cast<Capture>().Select((c, i) => new {Val = c.Value, Index = i}).ToDictionary(k => k.Index, v => v.Val);
			IndexAndColumnName.ForEach(k => Output.Columns.Add(k.Value));
		}

		private void PopulateDataTable(string line) {
			var collection = line.Split(char.Parse("\t"));
			var row = Output.NewRow();
			for (var i = 0; i < collection.GetLength(0); i++) {
				row[IndexAndColumnName[i]] = collection[i];
			}
			Output.Rows.Add(row);
		}

		#endregion
	}
}