using System;
using System.Collections;
using System.Data;

namespace PersistenceLayer
{
	/// <summary>
	///		该类定义用以限定对象范围的选择条件。用户不能直接生成他的实例。
	///		如要得到Conditon，需要通过RetrieveCriteria、DeleteCriteria的GetNewConditon()创建。
	///		Condition内部是"AND"关系、Condition与Condition之间是"OR"的关系，如：
	///		RetrieveCriteria rc=new RetrieveCrititeria(typeof(aEntity));
	///		Condition c1=rc.GetNewCondition();
	///		c1.AddEqualTo(aEntity.Name,"听棠");
	///		c1.AddEqualTo(aEntity.Age,"27");
	///		Condtion c2=rc.GetNewCondition();
	///		c2.AddEqualTo(aEntity.City,"苏州");
	///		则最终生成的查询是：where (Name="听棠" and Age=27) or (City="苏州")
	///		若为了生成"(a or b) and c"这种形式，则请参考GetOrGroup()方法
	/// </summary>
	public class Condition
	{
		private const string NO_NULLl="不能传入Null!";
		private const string NO_FIELD="指字的比较字段名不存在";

		private IList parameters = new ArrayList ();
		private ClassMap _classMap = null;
		private string booleanOperator = " AND ";

		internal Condition(Type entityObject)
		{

		}
		internal Condition (ClassMap clsMap)
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
				this._classMap.GetAttributeMap (attributeName,true),attributeValue,_classMap);
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
				this._classMap.GetAttributeMap (attributeName,true),attributeValue,_classMap);
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
		/// "不相等"比较两个字段是否不相等
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
		///		指定子字符串与指定属不匹配
		/// </summary>
		/// <param name="attributeName">属性</param>
		/// <param name="attributeValue">指定字符串</param>
		public void AddNotMatch(string attributeName,string attributeValue)
		{
			if (attributeValue==null) 
				throw new PlException(NO_NULLl);
			SelectionCriteria criteria;
			attributeValue="%"+attributeValue+"%";
			criteria=new SelectionCriteria(SelectionTypes.NotMatch,
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
		/// 创建一个OrGroup对象
		/// </summary>
		/// <returns></returns>
		public OrGroup GetNewOrGroup()
		{
			OrGroup orgroup=new OrGroup(this._classMap);
			this.parameters.Add(orgroup);
			return orgroup;
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
	}
}
