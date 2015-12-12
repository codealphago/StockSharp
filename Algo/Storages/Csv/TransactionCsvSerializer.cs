namespace StockSharp.Algo.Storages.Csv
{
	using System;
	using System.IO;
	using System.Text;

	using Ecng.Common;

	using StockSharp.Messages;

	/// <summary>
	/// The transaction serializer in the CSV format.
	/// </summary>
	public class TransactionCsvSerializer : CsvMarketDataSerializer<ExecutionMessage>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TransactionCsvSerializer"/>.
		/// </summary>
		/// <param name="securityId">Security ID.</param>
		/// <param name="encoding">Encoding.</param>
		public TransactionCsvSerializer(SecurityId securityId, Encoding encoding = null)
			: base(securityId, encoding)
		{
		}

		/// <summary>
		/// Write data to the specified writer.
		/// </summary>
		/// <param name="writer">CSV writer.</param>
		/// <param name="data">Data.</param>
		protected override void Write(TextWriter writer, ExecutionMessage data)
		{
			writer.Write($"{data.ServerTime.UtcDateTime.ToString(TimeFormat)};{data.ServerTime.ToString("zzz")};{data.TransactionId};{data.OrderId};{data.OrderPrice};{data.Volume};{data.Side};{data.OrderState};{data.TimeInForce};{data.TradeId};{data.TradePrice};{data.PortfolioName};{data.IsSystem}");
		}

		/// <summary>
		/// Load data from the specified reader.
		/// </summary>
		/// <param name="reader">CSV reader.</param>
		/// <param name="date">Date.</param>
		/// <returns>Data.</returns>
		protected override ExecutionMessage Read(FastCsvReader reader, DateTime date)
		{
			return new ExecutionMessage
			{
				SecurityId = SecurityId,
				ExecutionType = ExecutionTypes.Order,
				ServerTime = reader.ReadTime(date),
				TransactionId = reader.ReadLong(),
				OrderId = reader.ReadLong(),
				OrderPrice = reader.ReadDecimal(),
				Volume = reader.ReadDecimal(),
				Side = reader.ReadEnum<Sides>(),
				OrderState = reader.ReadEnum<OrderStates>(),
				TimeInForce = reader.ReadEnum<TimeInForce>(),
				TradeId = reader.ReadNullableLong(),
				TradePrice = reader.ReadNullableDecimal(),
				PortfolioName = reader.ReadString(),
				IsSystem = reader.ReadNullableBool(),
			};
		}
	}
}