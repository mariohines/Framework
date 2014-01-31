using System;
using Framework.Enumerations;

namespace Framework.Extensions
{
	public static partial class Extensions
	{
		#region DateTime Extensions

		/// <summary>Extension method to give true/false on the date being a weekend.</summary>
		/// <param name="dateTime">The source DateTime.</param>
		/// <returns>A boolean value.</returns>
		public static bool IsWeekend (this DateTime dateTime) {
			var result = dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
			return result;
		}

		/// <summary>Extension method to return the first day of a passed in date.</summary>
		/// <param name="dateTime">The source DateTime.</param>
		/// <returns>A DateTime of the first day.</returns>
		public static DateTime FirstDay (this DateTime dateTime) {
			return new DateTime(dateTime.Year, dateTime.Month, 1);
		}

		/// <summary>Extension method to return the last day of a passed in date.</summary>
		/// <param name="dateTime">The source DateTime.</param>
		/// <returns>A DateTime of the last day.</returns>
		public static DateTime LastDay (this DateTime dateTime) {
			var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
			return new DateTime(dateTime.Year, dateTime.Month, daysInMonth);
		}

		/// <summary>Extension method to get an enumerated Month value for a datetime.</summary>
		/// <param name="dateTime">The source DateTime.</param>
		/// <returns>A Month enumeration value.</returns>
		public static Month GetMonth (this DateTime dateTime) {
			return (Month)dateTime.Month;
		}

		///<summary>Extension method to resolve zodiac.</summary>
		///<remarks>Mhines, 11/24/2012.</remarks>
		///<exception cref="ArgumentOutOfRangeException">
		///Thrown when one or more arguments are outside the required range.
		///</exception>
		///<param name="dateTime">The source DateTime.</param>
		///<returns>.</returns>
		public static ZodiacSign ResolveZodiac (this DateTime dateTime) {
			ZodiacSign sign;

			switch ((Month)dateTime.Month) {
				case Month.January:
					sign = dateTime.Day <= 19 ? ZodiacSign.Capricorn : ZodiacSign.Aquarius;
					break;
				case Month.February:
					sign = dateTime.Day <= 18 ? ZodiacSign.Aquarius : ZodiacSign.Pisces;
					break;
				case Month.March:
					sign = dateTime.Day <= 20 ? ZodiacSign.Pisces : ZodiacSign.Aries;
					break;
				case Month.April:
					sign = dateTime.Day <= 19 ? ZodiacSign.Aries : ZodiacSign.Taurus;
					break;
				case Month.May:
					sign = dateTime.Day <= 20 ? ZodiacSign.Taurus : ZodiacSign.Gemini;
					break;
				case Month.June:
					sign = dateTime.Day <= 20 ? ZodiacSign.Gemini : ZodiacSign.Cancer;
					break;
				case Month.July:
					sign = dateTime.Day <= 22 ? ZodiacSign.Cancer : ZodiacSign.Leo;
					break;
				case Month.August:
					sign = dateTime.Day <= 22 ? ZodiacSign.Leo : ZodiacSign.Virgo;
					break;
				case Month.September:
					sign = dateTime.Day <= 22 ? ZodiacSign.Virgo : ZodiacSign.Libra;
					break;
				case Month.October:
					sign = dateTime.Day <= 22 ? ZodiacSign.Libra : ZodiacSign.Scorpio;
					break;
				case Month.November:
					sign = dateTime.Day <= 21 ? ZodiacSign.Scorpio : ZodiacSign.Sagittarius;
					break;
				case Month.December:
					sign = dateTime.Day <= 21 ? ZodiacSign.Sagittarius : ZodiacSign.Capricorn;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			return sign;
		}

		#endregion End DateTime Extensions
	}
}