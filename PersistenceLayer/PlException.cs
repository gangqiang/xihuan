using System;
using System.Data.OleDb;

namespace PersistenceLayer
{
	/// <summary>
	///		PlException 实体控制层异常。在实体层发生的异常都会以PlExcetion异常抛出。
	/// </summary>
	public class PlException:Exception
	{
		private ErrorTypes m_ErrorType;
		private Exception  m_ErrorSource;

		/// <summary>
		///		生成一个PlException异常实例
		/// </summary>
		/// <param name="message">异常信息</param>
		public PlException(string message):base(message)
		{
			this.m_ErrorType = ErrorTypes.Unknown;
		}
		
		/// <summary>
		///		生成一个PlException异常实例
		/// </summary>
		/// <param name="message">异常信息</param>
		/// <param name="errorType">异常代码</param>
		public PlException(string message,ErrorTypes errorType):base(message)
		{
			this.m_ErrorType = errorType;
		}

		/// <summary>
		///		生成一个PlException异常实例
		/// </summary>
		/// <param name="e">异常实例</param>
		public PlException(Exception e):base("实体层未处理的错误！")
		{
			this.m_ErrorType=ErrorTypes.Unknown;
			this.m_ErrorSource=e;
		}

		/*属性*/
		
		/// <summary>
		///		返回当前PlExcetpion实例的错误代码(枚举类型)
		/// </summary>
		public ErrorTypes ErrorType
		{
			get{return this.m_ErrorType;}
		}
		
		/// <summary>
		///		返回引起抛出PlExcetpion的Exception。当从PlException无法得到足够的错误信息，
		///		可从这里获得引发异常的原始Exception。
		/// </summary>
		public Exception ErrorSource
		{
			get{return this.m_ErrorSource;}
		}
	}


}
