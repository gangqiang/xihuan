using System;

namespace PersistenceLayer
{
	/// <summary>
	///		CardinalityTypes 的摘要说明。
	/// </summary>
	enum CardinalityTypes
	{
		None,
		OneToOne,
		OneToMany
	}

	/// <summary>
	///		ActionTypes 的摘要说明。
	///		tintown Add "SqlCommand" at 2004-09-06
	///		添加处理SQL事务的类型
	/// </summary>
	enum ActionTypes
	{
		InsertCommand,
		SelectCommand,
		UpdateCommand,
		DeleteCommand,
		SqlCommand,
		/// <summary>
		///		处理标准
		/// </summary>
		PesistentCriteria,
	
		RetrieveCriteria
	}

	/// <summary>
	///		ColumnKeyTypes 数据表Column的键类型
	/// </summary>
	enum ColumnKeyTypes
	{
		NoneKey,				//不是键
		PrimaryKey,				//主键
		ForeignKey				//外键
	}
	
	/// <summary>
	///		实体层错误类型
	/// </summary>
	public enum ErrorTypes 
	{
		
		/// <summary>格式错误</summary>
		FormatException,
		/// <summary>未发现</summary>
		NotFound,			
		/// <summary>Xml文件错误</summary>
		XmlError,					
		/// <summary>未知错误 </summary>
		Unknown,
		/// <summary>数据库错误</summary>
		DatabaseError,
		
		/// <summary>
		/// 数据库不存在错误
		/// </summary>
		DatabaseConnectionError,
		
		/// <summary>数据库未处理错误</summary>
		DatabaseUnknwnError,		
		/// <summary>数据不唯一，原因可能是标识该条记录的主键、索引重复</summary>
		NotUnique,					
		/// <summary>数据过长</summary>
		DataTooLong,				
		/// <summary>字符串不能为零长度</summary>
		NotAllowStringEmpty,		
		/// <summary>数据不能为空</summary>
		NotAllowDataNull,			
		/// <summary>数据类型不匹配</summary>
		DataTypeNotMatch,
		/// <summary>自动产生值，不能指定</summary>
		AutoValueOn,
		/// <summary>更新失败，原因可能是数据已被删除，或则数据被其他人修改</summary>
		UpdateFail,		
		/// <summary>由于约束机制，导致的错误</summary>
		RestrictError,
		/// <summary>缺少必要的属性(null)，如标识该对象的属性</summary>
		RequireAttribute,

		/// <summary>实体层一般错误 </summary>
		PesistentError,

		/// <summary>
		/// 更新中有脏数据，可能是并发引起的
		/// </summary>
		DirtyEntity
	
	}

	enum SelectionTypes
	{
		EqualTo,								//  = 
		GreaterThan,							//  > 
		GreaterThanAndEqualTo,					//  >=
		NotEqualTo,								//  <>
		LessThan,								//  < 
		LessThanAndEqualTo,						//  <=
		Match,									//  LIKE %..%
		MatchPrefix,							//  LIKE %...
		In,										//  IN	
		NotMatch,
		NotMatchPrefix,
		NotIn,
		MatchSuffix,
		NotMatchSuffix
	}

	/// <summary>
	/// 标准类型
	/// </summary>
	internal enum CriteriaTypes
	{

		Retrive,								//RetrieveCritera
		Delete,									//DeleteCriteria
		Update									//UpdateCriteria
	}

	enum SqlValueTypes
	{
		/// <summary>字符串型</summary>
		PrototypeString,
		/// <summary>'xxxx'型的字符串</summary>
		SimpleQuotesString,
		String,
		BoolToString,
		BoolToInterger,
		AccessDateString,
		NotSupport,
		OracleDate
	}

	/// <summary>
	/// 定义返回所有属性
	/// 在RetrieveCriteria中可以使用
	/// </summary>
	public enum AttributeType
	{
		/// <summary>
		/// 返回所有的字段
		/// 这相当于"*"功能
		/// </summary>
		All=0                //在Query中用于返回所有属性
	}
}
