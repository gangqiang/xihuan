using System;
using System.Collections;
using System.Data;

namespace PersistenceLayer
{
	/// <summary>
	/// OrGroup与Condition的用法基本上一致的
	/// OrGroup内部都是"AND"关系
	/// 为了实现(a or b ) and c这样的效果，a条件与b条件之间是or关系，再与c进行and组合
	/// 假设c为已经实例化的对象
	/// OrGroup og=c.GetOrGroup();
	/// og.AddEqualTo(...)  //条件a
	/// og.AddEqualTo(...)  //条件b
	/// c.AddEuqalTo(...)   //条件c
	/// </summary>
	public class OrGroup
	{

		private const string NO_NULLl="不能传入Null!";
		private const string NO_FIELD="指字的比较字段名不存在";

		private IList parameters = new ArrayList ();
		private ClassMap _classMap = null;
		private string booleanOperator = " OR ";

	
		internal OrGroup(Type entityObject)
		{

		}
		internal OrGroup (ClassMap clsMap)
		{
			this._classMap = clsMap;
		}


		/// <summary>
		///		“=”比较指定值与指定属性
		/// </summary>
		/// <param name="attributeName">比较的属性</param>
		/// <param name="attributeValue">指定值</param>
		public void AddEqualTo(string attributeName,object attributeValue)
		{
			SelectionCriteria criteria;
			criteria=new SelectionCriteria (SelectionTypes.EqualTo,
				this._classMap.GetAttributeMap (attributeName,true),attributeValue,this._classMap);
			this.parameters.Add(criteria);
		}

		/// <summary>
		/// "="比较两个字段是否相等　
		/// </summary>
		/// <param name="attributeName">第一个字段名</param>
		/// <param name="attributeName2">第二个字段名</param>
		public void AddEqualToField(string attributeName,string attributeName2)
		{
			if (attributeName2==null) 
				throw new PlException(NO_FIELD);
			SelectionCriteria criteria;
			criteria=new SelectionCriteria (SelectionTypes.EqualTo,
				this._classMap.GetAttributeMap (attributeName,true),this._classMap.GetAttributeMap (attributeName2,true),_classMap);
			this.parameters.Add(criteria);
		}
		
		/// <summary>
		///		“>”比较指定值与指定属性
		/// </summary>
		/// <param name="attributeName">比较的属性</param>
		/// <param name="attributeValue">指定值</param>
		public void AddGreaterThan(string attributeName,object attributeValue)
		{
			if (attributeValue==null) 
				throw new PlException(NO_NULLl);
			SelectionCriteria criteria;
			criteria=new SelectionCriteria (SelectionTypes.GreaterThan,
				this._classMap.GetAttributeMap (attributeName,true),attributeValue,_classMap);
			this.parameters.Add(criteria);
		}

		/// <summary>
		/// ">"比较第一个字段是否大于第二个字段
		/// </summary>
		/// <param name="attributeName">第一个字段名称</param>
		/// <param name="attributeName2">第二个字段名称</param>
		public void AddGreaterThanField(string attributeName,string attributeName2)
		{
			if (attributeName2==null) 
				throw new PlException(NO_FIELD);
			SelectionCriteria criteria;
			criteria=new SelectionCriteria (SelectionTypes.GreaterThan,
				this._classMap.GetAttributeMap (attributeName,true),this._classMap.GetAttributeMap (attributeName2,true),_classMap);
			this.parameters.Add(criteria);
		}
		
		/// <summary>
		///		“>=”比较指定值与指定属性
		/// </summary>
		/// <param name="attributeName">比较的属性</param>
		/// <param name="attributeValue">指定值</param>
		public void AddGreaterThanOrEqualTo(string attributeName,object attributeValue)
		{
			if (attributeValue==null) 
				throw new PlException(NO_NULLl);			
			SelectionCriteria criteria;
			criteria = new SelectionCriteria (SelectionTypes.GreaterThanAndEqualTo,
				this._classMap.GetAttributeMap (attributeName,true),attributeValue,this._classMap);
			this.parameters.Add(criteria);
		}

		/// <summary>
		/// ">="比较第一个字段是否大于等于第二个字段
		/// </summary>
		/// <param name="attributeName">第一个字段名称</param>
		/// <param name="attributeName2">第二个字段名称</param>
		public void AddGreaterThanOrEqualToField(string attributeName,string attributeName2)
		{
			if (attributeName2==null) 
				throw new PlException(NO_FIELD);			
			SelectionCriteria criteria;
			criteria = new SelectionCriteria (SelectionTypes.GreaterThanAndEqualTo,
				this._classMap.GetAttributeMap (attributeName,true),this._classMap.GetAttributeMap (attributeName2,true),_classMap);
			this.parameters.Add(criteria);
		}



		/// <summary>
		///		“不等于”"比较指定值与指定属性
		/// </summary>
		/// <param name="attributeName">比较的属性</param>
		/// <param name="attributeValue">指定值</param>
		public void AddNotEqualTo(string attributeName,object attributeValue)
		{
			if (attributeValue==null) 
				throw new PlException(NO_NULLl);		
			SelectionCriteria criteria;
			criteria=new SelectionCriteria (SelectionTypes.NotEqualTo,
				this._classMap.GetAttributeMap (attributeName,true),attributeValue,_classMap);
			this.parameters.Add(criteria);
		}

		/// <summary>
		/// "不等于"比较两个字段是否不相等
		/// </summary>
		/// <param name="attributeName">第一个字段名称</param>
		/// <param name="attributeName2">第二个字段名称</param>
		public void AddNotEqualToField(string attributeName,string attributeName2)
		{
			if (attributeName2==null) 
				throw new PlException(NO_FIELD);		
			SelectionCriteria criteria;
			criteria=new SelectionCriteria (SelectionTypes.NotEqualTo,
				this._classMap.GetAttributeMap (attributeName,true),this._classMap.GetAttributeMap (attributeName2,true),_classMap);
			this.parameters.Add(criteria);
		}

		/// <summary>
		///		“小于”比较指定值与指定属性
		/// </summary>
		/// <param name="attributeName">比较的属性</param>
		/// <param name="attributeValue">指定值</param>
		public void AddLessThan(string attributeName,object attributeValue)
		{
			if (attributeValue==null) 
				throw new PlException(NO_NULLl);
			SelectionCriteria criteria;
			criteria=new SelectionCriteria (SelectionTypes.LessThan,
				this._classMap.GetAttributeMap (attributeName,true),attributeValue,_classMap);
			this.parameters.Add(criteria);		
		}	   

		/// <summary>
		/// "小于"比较第一个字段是否小于第二个字段
		/// </summary>
		/// <param name="attributeName">第一个字段名称</param>
		/// <param name="attributeName2">第二个字段名称</param>
		public void AddLessThanField(string attributeName,string attributeName2)
		{
			if (attributeName2==null) 
				throw new PlException(NO_FIELD);	
			SelectionCriteria criteria;
			criteria=new SelectionCriteria (SelectionTypes.LessThan,
				this._classMap.GetAttributeMap (attributeName,true),this._classMap.GetAttributeMap (attributeName2,true),_classMap);
			this.parameters.Add(criteria);		
		}	   

		/// <summary>
		///		“ 小于等于 ”"比较指定值与指定属性
		/// </summary>
		/// <param name="attributeName">比较的属性</param>
		/// <param name="attributeValue">指定值</param>
		public void AddLessThanOrEqualTo(string attributeName,object attributeValue)
		{
			if (attributeValue==null) 
				throw new PlException(NO_NULLl);
			SelectionCriteria criteria;
			criteria=new SelectionCriteria (SelectionTypes.LessThanAndEqualTo,
				this._classMap.GetAttributeMap (attributeName,true),attributeValue,_classMap);
			this.parameters.Add(criteria);		
		}		

		/// <summary>
		/// "小于等于"比较第一个字段是否小于等于第二个字段
		/// </summary>
		/// <param name="attributeName">第一个字段</param>
		/// <param name="attributeName2">第二个字段</param>
		public void AddLessThanOrEqualToField(string attributeName,string attributeName2)
		{
			if (attributeName2==null) 
				throw new PlException(NO_FIELD);	
			SelectionCriteria criteria;
			criteria=new SelectionCriteria (SelectionTypes.LessThanAndEqualTo,
				this._classMap.GetAttributeMap (attributeName,true),this._classMap.GetAttributeMap (attributeName2,true),_classMap);
			this.parameters.Add(criteria);		
		}		

		/// <summary>
		///		指定子字符串与指定属匹配
		/// </summary>
		/// <param name="attributeName">属性</param>
		/// <param name="attributeValue">指定字符串</param>
		public void AddMatch(string attributeName,string attributeValue)
		{
			if (attributeValue==null) 
				throw new PlException(NO_NULLl);
			SelectionCriteria criteria;
			attributeValue="%"+attributeValue+"%";
			criteria=new SelectionCriteria(SelectionTypes.Match,
				this._classMap.GetAttributeMap (attributeName,true),attributeValue,_classMap);
			this.parameters.Add(criteria);		
		}		
		
		/// <summary>
		///		前缀匹配
		/// </summary>
		/// <param name="attributeName">属性</param>
		/// <param name="attributeValue">匹配值</param>
		public void AddMatchPrefix(string attributeName,string attributeValue)
		{
			if (attributeValue==null) 
				throw new PlException(NO_NULLl);

			SelectionCriteria criteria;
			attributeValue=attributeValue+"%";
			criteria=new SelectionCriteria(SelectionTypes.MatchPrefix,
				this._classMap.GetAttributeMap (attributeName,true),attributeValue,_classMap);
			this.parameters.Add(criteria);		
		}		

		/// <summary>
		///		前缀不匹配
		/// </summary>
		/// <param name="attributeName">属性</param>
		/// <param name="attributeValue">匹配值</param>
		public void AddNotMatchPrefix(string attributeName,string attributeValue)
		{
			if (attributeValue==null) 
				throw new PlException(NO_NULLl);

			SelectionCriteria criteria;
			attributeValue=attributeValue+"%";
			criteria=new SelectionCriteria(SelectionTypes.NotMatchPrefix,
				this._classMap.GetAttributeMap (attributeName,true),attributeValue,_classMap);
			this.parameters.Add(criteria);		
		}	

		/// <summary>
		///		后缀匹配
		/// </summary>
		/// <param name="attributeName">属性</param>
		/// <param name="attributeValue">匹配值</param>
		public void AddMatchSuffix(string attributeName,string attributeValue)
		{
			if (attributeValue==null) 
				throw new PlException(NO_NULLl);

			SelectionCriteria criteria;
			attributeValue="%"+attributeValue;
			criteria=new SelectionCriteria(SelectionTypes.MatchSuffix,
				this._classMap.GetAttributeMap (attributeName,true),attributeValue,_classMap);
			this.parameters.Add(criteria);		
		}		

		/// <summary>
		///		后缀不匹配
		/// </summary>
		/// <param name="attributeName">属性</param>
		/// <param name="attributeValue">匹配值</param>
		public void AddNotMatchSuffix(string attributeName,string attributeValue)
		{
			if (attributeValue==null) 
				throw new PlException(NO_NULLl);

			SelectionCriteria criteria;
			attributeValue="%"+attributeValue;
			criteria=new SelectionCriteria(SelectionTypes.NotMatchSuffix,
				this._classMap.GetAttributeMap (attributeName,true),attributeValue,_classMap);
			this.parameters.Add(criteria);		
		}	

		/// <summary>
		///		与给定的列表list里的值匹配
		/// </summary>
		/// <param name="attributeName">属性</param>
		/// <param name="list">值列表</param>
		public void AddIn (string attributeName , object[] list)
		{
			if (list == null || list.Length == 0) 
				throw new PlException("传入的list长度不能为0！");
			SelectionCriteria selection = new SelectionCriteria (SelectionTypes.In,
				this._classMap.GetAttributeMap (attributeName,true),list,_classMap);
			this.parameters.Add (selection);
		}

		/// <summary>
		///		与给定的列表list里的值不匹配
		/// </summary>
		/// <param name="attributeName">属性</param>
		/// <param name="list">值列表</param>
		public void AddNotIn (string attributeName , object[] list)
		{
			if (list == null || list.Length == 0) 
				throw new PlException("传入的list长度不能为0！");
			SelectionCriteria selection = new SelectionCriteria (SelectionTypes.NotIn,
				this._classMap.GetAttributeMap (attributeName,true),list,_classMap);
			this.parameters.Add (selection);
		}

		/// <summary>
		///		清楚所有条件
		/// </summary>
		public void Clear ()
		{
			this.parameters.Clear ();
		}

		/*属性*/
		//Where条件集合
		internal IList Parameters 
		{
			get{return this.parameters;}
		}

		internal string BooleanOperator
		{
			get {return this.booleanOperator;}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		internal string AsSqlClause()
		{
			string sql="";
			string sqlPre="(";
			string sqlfix=")";
			SelectionCriteria selection;
			int size = this.parameters.Count;
			if (size > 0)
			{
				sql=sqlPre;
				for(int i=0;i<size;i++)
				{
					if(i>0)
						sql += this.booleanOperator;
					selection = (SelectionCriteria)this.Parameters [i];
					sql += selection.AsSqlClause ();
				}
				sql +=sqlfix;
			}
			
			return sql;
		}

	}
	
}
